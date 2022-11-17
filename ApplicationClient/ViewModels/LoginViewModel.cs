using ApplicationAPI.Utils.Extensions;
using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.ViewModels;
public sealed class LoginViewModel : ViewModel 
{
	private string? _username;
	private string? _password;
	private bool _isLoggingInProgress;

	public LoginViewModel(
		LoginCommand loginCommand,
		NavigateToRegistrationViewCommand navigateToRegistrationViewCommand,
		NavigateToResetAccountViewCommand navigateToResetAccountViewCommand
	) 
	{
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

	private bool CredentialsProvided() => Username?.IsEmptyOrWhitespace() is false && Password?.IsEmptyOrWhitespace() is false;
	private void OnLoginCommandStarted(object? sender, AsyncCommandStartedEventArgs e) => IsLoggingInInProgress = true;
	private void OnLoginCommandExecuted(object? sender, AsyncCommandExecutedEventArgs e) => IsLoggingInInProgress = false;
}