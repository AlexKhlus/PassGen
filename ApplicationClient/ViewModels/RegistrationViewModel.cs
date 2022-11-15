using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.Utils.Extensions;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.ViewModels;
public sealed class RegistrationViewModel : ViewModel
{
	private string? _username;
	private string? _password;
	private string? _repeatPassword;

	public RegistrationViewModel(
		NavigateToLoginViewCommand navigateToLoginViewCommand,
		RegisterAccountCommand registerAccountCommand
	) 
	{
		(NavigateLoginCommand = navigateToLoginViewCommand).CanExecuteCondition = () => true;
		(RegisterAccountCommand = registerAccountCommand).CanExecuteCondition = CredentialsProvided;
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

	public AsyncCommand NavigateLoginCommand { get; }
	public AsyncCommand RegisterAccountCommand { get; }

	private bool CredentialsProvided()
		=>  Username?.IsEmptyOrWhitespace() is false &&
			Password?.IsEmptyOrWhitespace() is false &&
			RepeatPassword?.IsEmptyOrWhitespace() is false;
}