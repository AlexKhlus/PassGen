using ApplicationAPI.Attributes.Base;


namespace ApplicationAPI.Attributes;
public sealed class EmailAddress : ExtendedRegexValidationAttribute 
{
	public EmailAddress() : base(@"^(?:[a-z0-9]+\.?)+@(?:[a-z0-9]+)+\.(?:[a-z0-9])+$") 
	{
		Addon.InvalidFormatErrorMessage = $"Only emails of pattern X+@X+.X+ are allowed!";
	}
}