using System.ComponentModel.DataAnnotations;


namespace ApplicationAPI.Contracts;
public sealed class RegistrationRequest
{
	[Required] public string? Username { get; init; }
	[Required] public string? Password { get; init; }
	[Required] public string? RepeatPassword { get; init; }
	[Required] public string? SecretQuestion { get; init; }
	[Required] public string? SecretAnswer { get; init; }
}

public sealed class RegistrationResponse 
{
	public bool Success { get; init; }
	public string? ErrorMessage { get; init; }
}