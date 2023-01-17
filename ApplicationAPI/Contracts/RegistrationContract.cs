using ApplicationAPI.Data.Repository;
using ApplicationAPI.Utils.Extensions;
using FluentValidation;

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ClassNeverInstantiated.Global

namespace ApplicationAPI.Contracts;
public record RegistrationRequest
{
	public required string Username { get; init; }
	public required string Password { get; init; }
	public required string RepeatPassword { get; init; }
	public required string SecretQuestion { get; init; }
	public required string SecretAnswer { get; init; }
}
public record RegistrationResponse
{
	public required bool Success { get; init; }
	public required string? ErrorMessage { get; init; }
}
public sealed class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
	public RegistrationRequestValidator(IUserRepository userRepository)
	{
		RuleFor(request => request.Username)
			.NotNull()
			.NotEmpty().WithMessage($"Username cannot be empty!")
			.Must(username => username?.IsWhiteSpace() is false)
			.Must(username => userRepository.GetUserBy(username!).Result is null).WithMessage($"User already register");

		RuleFor(request => request.SecretQuestion)
			.NotNull()
			.NotEmpty().WithMessage($"Secret question cannot be empty or default!")
			.Must(question => question?.IsWhiteSpace() is false);

		RuleFor(request => request.SecretAnswer)
			.NotNull()
			.NotEmpty().WithMessage($"Answer cannot be empty!")
			.Must(answer => answer?.IsWhiteSpace() is false);

		RuleFor(request => request.Password)
			.NotNull()
			.NotEmpty().WithMessage($"Password cannot be empty!")
			.Must(password => password?.IsWhiteSpace() is false);
		RuleFor(request => request.RepeatPassword)
			.NotNull()
			.NotEmpty().WithMessage($"Repeated password cannot be empty!")
			.Must(password => password?.IsWhiteSpace() is false);

		RuleFor(request => request)
			.Must(request => string.Equals(request.Password, request.RepeatPassword)).WithMessage($"Passwords must be same!");
	}
}
