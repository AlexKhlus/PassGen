using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.ViewModels.Base;
using FluentValidation;
using ApplicationAPI.Utils.Extensions;

namespace ApplicationClient.ViewModels;
public sealed class LoginViewModel : ViewModel
{
	private readonly IValidator<LoginViewModel> _validator;

	private string? _username;
	private string? _password;
	private bool _isLoggingInProgress;

	public LoginViewModel(
		IValidator<LoginViewModel> validator,
		LoginCommand loginCommand,
		NavigateToRegistrationViewCommand navigateToRegistrationViewCommand,
		NavigateToResetAccountViewCommand navigateToResetAccountViewCommand
	)
	{
		_validator = validator;

		_username = string.Empty;
		_password = string.Empty;
		_isLoggingInProgress = false;

		(LoginCommand = loginCommand).CanExecuteCondition = CredentialsProvided;
		LoginCommand.Started += OnLoginCommandStarted;
		LoginCommand.Executed += OnLoginCommandExecuted;

		(NavigateToRegistrationViewCommand = navigateToRegistrationViewCommand).CanExecuteCondition = () => true;
		(NavigateResetAccountViewCommand = navigateToResetAccountViewCommand).CanExecuteCondition = () => true;
	}

	public override void Dispose() => LoginCommand.Dispose();

	public string? Username
	{
		get => _username;
		set
		{
			if(string.Equals(value, _username)) return;

			_username = value;
			InvokePropertyChanged();
			LoginCommand.InvokeCanExecuteChanged();
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
			LoginCommand.InvokeCanExecuteChanged();
		}
	}
	public bool IsLoggingInInProgress
	{
		get => _isLoggingInProgress;
		set
		{
			if(value.Equals(_isLoggingInProgress)) return;

			_isLoggingInProgress = value;
			InvokePropertyChanged();
		}
	}

	public AsyncCommand LoginCommand { get; }
	public AsyncCommand NavigateToRegistrationViewCommand { get; }
	public AsyncCommand NavigateResetAccountViewCommand { get; }

	private bool CredentialsProvided() => _validator.Validate(this).IsValid;
	private void OnLoginCommandStarted(object? sender, AsyncCommandStartedEventArgs e) => IsLoggingInInProgress = true;
	private void OnLoginCommandExecuted(object? sender, AsyncCommandExecutedEventArgs e) => IsLoggingInInProgress = false;
}
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
