using System.ComponentModel.DataAnnotations;


namespace ApplicationAPI.Contracts;
public sealed class ResetAccountRequest 
{
	[Required] public string? Username { get; init; }
	[Required] public string? NewPassword { get; init; }
}

public sealed class ResetAccountResponse 
{
	public bool Success { get; init; }
	public string? ErrorMessage { get; init; }
}