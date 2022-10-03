using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;


namespace ApplicationAPI.Attributes.Base;

[AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public abstract class ExtendedRegexValidationAttribute : RegularExpressionAttribute 
{
	private readonly ValidationAttributeAddon _addon;
	protected ValidationAttributeAddon Addon
	{
		get => _addon;
	}

	protected ExtendedRegexValidationAttribute([RegexPattern] string pattern) : base(pattern) 
	{
		_addon = new ValidationAttributeAddon(validationAttribute: this) {
			InvalidUsageErrorMessage = $"Attribute can only be used with {nameof(String)} and {nameof(IEnumerable<string>)}."
		};
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) 
	{
		return value switch {
			IEnumerable<string> sequence => sequence
				.Select(element => base.IsValid(element, validationContext))
				.Any(result => result != ValidationResult.Success) ? 
				new ValidationResult(
					errorMessage: Addon.SetInvalidFormatErrorMessage(),
					memberNames: new [] { validationContext.DisplayName }
				) : 
				ValidationResult.Success,

			string => base.IsValid(value, validationContext),
			null   => ValidationResult.Success,
			_      => new ValidationResult(Addon.SetInvalidUsageErrorMessage())
		};
	}

	public override bool IsValid(object? value)
	{
		ArgumentNullException.ThrowIfNull(value);

		Addon.SetInvalidFormatErrorMessage();
		return base.IsValid(value);
	}
}