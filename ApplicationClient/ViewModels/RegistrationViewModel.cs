using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ApplicationAPI.Data.Repository;
using ApplicationAPI.Resolvers;
using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.Utils.Resources;
using ApplicationClient.ViewModels.Base;
using FluentValidation;
using ApplicationAPI.Utils.Extensions;

namespace ApplicationClient.ViewModels;
public sealed class RegistrationViewModel : ViewModel, IDataErrorInfo
{
	private readonly IAccountResolver _accountResolver;
	private readonly IValidator<RegistrationViewModel> _validator;

	private string? _username;
	private string? _password;
	private string? _repeatPassword;
	private string? _selectedSecretQuestion;
	private string? _secretAnswer;

	public RegistrationViewModel(
		ISecretQuestionRepository secretQuestionRepository,
		IAccountResolver accountResolver,
		IValidator<RegistrationViewModel> validator,
		NavigateToLoginViewCommand navigateToLoginViewCommand,
		RegisterAccountCommand registerAccountCommand
	)
	{
		_accountResolver = accountResolver;
		_validator = validator;

		(NavigateLoginCommand = navigateToLoginViewCommand).CanExecuteCondition = () => true;
		(RegisterAccountCommand = registerAccountCommand).CanExecuteCondition = CredentialsProvided;

		(SecretQuestions = new () { Strings.SecretQuestionDefault }).AddRange(secretQuestionRepository.GetQuestions().Result);
		SelectedSecretQuestion = SecretQuestions.First();

	}

	public string? Username
	{
		get => _username;
		set
		{
			if(string.Equals(value, _username)) return;

			_username = value;
			InvokePropertyChanged();
			RegisterAccountCommand.InvokeCanExecuteChanged();
		}
	}

	public string? Password
	{
		get => _password;
		set
		{
			if(string.Equals(value, _password)) return;

			_password = value;
			InvokePropertyChanged();
			RegisterAccountCommand.InvokeCanExecuteChanged();
		}
	}

	public string? RepeatPassword
	{
		get => _repeatPassword;
		set
		{
			if(string.Equals(value, _repeatPassword)) return;

			_repeatPassword = value;
			InvokePropertyChanged();
			RegisterAccountCommand.InvokeCanExecuteChanged();
		}
	}

	public List<string?> SecretQuestions { get; }

	public string? SelectedSecretQuestion
	{
		get => _selectedSecretQuestion;
		set
		{
			if(string.Equals(value, _selectedSecretQuestion)) return;

			_selectedSecretQuestion = value;
			InvokePropertyChanged();
			RegisterAccountCommand.InvokeCanExecuteChanged();
		}
	}

	public string? SecretAnswer
	{
		get => _secretAnswer;
		set
		{
			if(string.Equals(value, _secretAnswer)) return;

			_secretAnswer = value;
			InvokePropertyChanged();
			RegisterAccountCommand.InvokeCanExecuteChanged();
		}
	}

	public string Error { get; }

	public string this[string columnName]
	{
		get
		{
			var error = string.Empty;
			switch(columnName)
			{
				case nameof(RepeatPassword):
					if(string.Equals(RepeatPassword, Password) is false)
						error = Strings.ApprovePasswordError;
					break;
				case nameof(Username):
					if(_accountResolver.IsUserExist(Username).Result)
						error = Strings.UserExistError;
					break;
			}

			return error;
		}
	}

	public AsyncCommand NavigateLoginCommand { get; }
	public AsyncCommand RegisterAccountCommand { get; }

	private bool CredentialsProvided() => _validator.Validate(this).IsValid;
}

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
