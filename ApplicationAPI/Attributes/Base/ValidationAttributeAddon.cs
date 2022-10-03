using System.ComponentModel.DataAnnotations;
using ApplicationAPI.Exceptions;


namespace ApplicationAPI.Attributes.Base;
public sealed class ValidationAttributeAddon 
{
	private readonly ValidationAttribute _attribute;
	private readonly RegexAddonException _nullErrorMessageException;

	public ValidationAttributeAddon(ValidationAttribute validationAttribute) 
	{
		_attribute = validationAttribute;
		_nullErrorMessageException = new RegexAddonException($"Can't set error message. Error message is NULL. ");
	}

	public string? InvalidUsageErrorMessage  { private get; set; }
	public string? InvalidFormatErrorMessage { private get; set; }

	public string SetInvalidUsageErrorMessage() 
		=> SetErrorMessage(InvalidUsageErrorMessage ?? throw _nullErrorMessageException);

	public string SetInvalidFormatErrorMessage() 
		=> SetErrorMessage(InvalidFormatErrorMessage ?? throw _nullErrorMessageException);

	private string SetErrorMessage(string value)
		=> _attribute.ErrorMessage = value;
}
