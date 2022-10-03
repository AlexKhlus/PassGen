using ApplicationAPI.Attributes.Base;


namespace ApplicationAPI.Attributes;
public sealed class NoWhitespace : ExtendedRegexValidationAttribute 
{
	public NoWhitespace() : base(@"^\S+$") 
	{
		Addon.InvalidFormatErrorMessage = $"Only solid word (without whitespace characters) is allowed!";
	}
}