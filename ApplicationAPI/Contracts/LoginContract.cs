using ApplicationAPI.Data.Repository;
using ApplicationAPI.Utils.Extensions;
using FluentValidation;

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ClassNeverInstantiated.Global

namespace ApplicationAPI.Contracts;
public record LoginRequest
{
	public required string Username { get; init; }
	public required string Password { get; init; }
}
public record LoginResponse
{
	public required bool Success { get; init; }
	public required string? ErrorMessage { get; init; }
}
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	public LoginRequestValidator(IUserRepository userRepository)
	{
		RuleFor(request => request.Username)
			.NotNull()
			.NotEmpty().WithMessage($"Username cannot be empty!")
			.Must(username => username?.IsWhiteSpace() is false)
			.Must(username => userRepository.GetUserBy(username!).Result is not null).WithMessage($"Cannot found existing user!");

		RuleFor(request => request.Password)
			.NotNull()
			.NotEmpty().WithMessage($"Password cannot be empty!");
	}
}
