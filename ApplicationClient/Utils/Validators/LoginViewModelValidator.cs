using ApplicationAPI.Utils.Extensions;
using ApplicationClient.ViewModels;
using FluentValidation;

namespace ApplicationClient.Utils.Validators;
public sealed class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
	public LoginViewModelValidator()
	{
		RuleFor(viewModel => viewModel.Username)
			.NotNull()
			.NotEmpty().WithMessage($"Username cannot be empty or whitespace!")
			.Must(username => username?.IsWhiteSpace() is false).WithMessage($"Username cannot be empty or whitespace!");

		RuleFor(viewModel => viewModel.Password)
			.NotNull()
			.NotEmpty().WithMessage($"Password cannot be empty or whitespace!")
			.Must(password => password?.IsWhiteSpace() is false).WithMessage($"Password cannot be empty or whitespace!");
	}
}
