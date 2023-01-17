using ApplicationAPI.Data.Repository;
using FluentValidation;

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ClassNeverInstantiated.Global

namespace ApplicationAPI.Contracts;
public record ResetPasswordRequest
{
	public required string Username { get; init; }
	public required string SecretAnswer { get; init; }
	public required string NewPassword { get; init; }
}
public record ResetPasswordResponse
{
	public required bool Success { get; init; }
	public required string? ErrorMessage { get; init; }
}
public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
	public ResetPasswordRequestValidator(IUserRepository userRepository)
	{
		RuleFor(request => request.Username)
			.NotNull()
			.NotEmpty().WithMessage($"Username cannot be empty!")
			.Must(username => userRepository.GetUserBy(username!).Result is not null).WithMessage($"Cannot found existing user!");

		RuleFor(request => request.SecretAnswer)
			.NotNull()
			.NotEmpty().WithMessage($"Secret answer cannot be empty");

		RuleFor(request => request)
			.Must(request => {
				var user = userRepository.GetUserBy(request.Username!).Result!;
				return string.Equals(request.SecretAnswer, user.SecretAnswer);
			}).WithMessage($"Incorrect secret answer!");

		RuleFor(request => request.NewPassword)
			.NotNull()
			.NotEmpty().WithMessage($"Username cannot be empty!");
	}
}
