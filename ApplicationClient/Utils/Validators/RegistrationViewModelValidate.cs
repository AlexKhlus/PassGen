using ApplicationAPI.Utils.Extensions;
using ApplicationClient.Utils.Resources;
using ApplicationClient.ViewModels;
using FluentValidation;

namespace ApplicationClient.Utils.Validators;
public sealed class RegistrationViewModelValidate : AbstractValidator<RegistrationViewModel>
{
	public RegistrationViewModelValidate()
	{
		RuleFor(viewModel => viewModel.Username)
			.NotNull()
			.NotEmpty().WithMessage($"Username cannot be empty or whitespace!")
			.Must(username => username?.IsWhiteSpace() is false).WithMessage($"Username cannot be empty or whitespace!");

		RuleFor(viewModel => viewModel.Password)
			.NotNull()
			.NotEmpty().WithMessage($"Password cannot be empty or whitespace!")
			.Must(password => password?.IsWhiteSpace() is false).WithMessage($"Password cannot be empty or whitespace!");

		RuleFor(viewModel => viewModel.RepeatPassword)
			.NotNull()
			.NotEmpty().WithMessage($"Repeat password cannot be empty or whitespace!")
			.Must(repeatPassword => repeatPassword?.IsWhiteSpace() is false).WithMessage($"Repeat password cannot be empty or whitespace!");

		RuleFor(viewModel => viewModel.SelectedSecretQuestion)
			.NotNull()
			.NotEmpty().WithMessage($"Secret question cannot be empty or default!")
			.Must(secretQuestion => string.Equals(secretQuestion, Strings.SecretQuestionDefault) is false).WithMessage($"Secret question cannot be empty or default!");

		RuleFor(viewModel => viewModel.SecretAnswer)
			.NotNull()
			.NotEmpty().WithMessage($"Secret answer cannot be empty or whitespace!")
			.Must(secretAnswer => secretAnswer?.IsWhiteSpace() is false).WithMessage($"Secret answer cannot be empty or whitespace!");

		RuleFor(viewModel => viewModel)
			.Must(viewModel => string.Equals(viewModel.Password, viewModel.RepeatPassword)).WithMessage($"Passwords must be same!");
	}
}
