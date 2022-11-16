using System.ComponentModel.DataAnnotations;


namespace ApplicationAPI.Contracts;
public sealed class ResetPasswordRequest 
{
	[Required] public string? Username { get; init; }
	[Required] public string? NewPassword { get; init; }
}

public sealed class ResetPasswordResponse 
{
	public bool Success { get; init; }
	public string? ErrorMessage { get; init; }
}