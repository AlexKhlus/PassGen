using System.ComponentModel.DataAnnotations;


namespace ApplicationAPI.Contracts;
public sealed class LoginRequest
{
	[Required] public string? Username { get; init; }

	[Required] public string? Password { get; init; }
}

public sealed class LoginResponse 
{
	public bool Success { get; init; }
	public string? ErrorMessage { get; init; }
}