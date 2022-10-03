using System;
using System.ComponentModel.DataAnnotations;


namespace ApplicationAPI.Attributes.Base;
[AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)] 
public abstract class ExtendedValidationAttribute : ValidationAttribute 
{
	private readonly ValidationAttributeAddon _addon;
	protected ValidationAttributeAddon Addon 
	{
		get => _addon;
	}

	internal ExtendedValidationAttribute() 
		=> _addon = new ValidationAttributeAddon(validationAttribute: this);
}
