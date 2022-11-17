using ApplicationAPI.Resolvers;
using ApplicationAPI.Utils.Extensions;
using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.ViewModels;
public class ResetPasswordViewModel : ViewModel
{
	private string? _username;
	private string? _newPassword;
	private string? _secretAnswer;

	private readonly IAccountResolver _accountResolver;

	public ResetPasswordViewModel(
		NavigateToLoginViewCommand navigateToLoginViewCommand,
		ResetPasswordCommand resetPasswordCommand,
		IAccountResolver accountResolver
	) 
	{
		(NavigateLoginCommand = navigateToLoginViewCommand).CanExecuteCondition = () => true;
		(ResetPasswordCommand = resetPasswordCommand).CanExecuteCondition = CredentialsProvided;
		_accountResolver = accountResolver;
	}

	public string? Username 
	{
		get => _username;
		set 
		{
			if(string.Equals(value, _username)) return;

			_username = value;
			InvokePropertyChanged();
			ResetPasswordCommand.InvokeCanExecuteChanged();
		}
	}

	public string? NewPassword 
	{
		get => _newPassword;
		set 
		{
			if(string.Equals(value, _newPassword)) return;

			_newPassword = value;
			InvokePropertyChanged();
			ResetPasswordCommand.InvokeCanExecuteChanged();
		}
	}

	public string? SecretAnswer
	{
		get => _secretAnswer;
		set
		{
			if(string.Equals(value, _secretAnswer)) return;

			_secretAnswer = value;
			if(Username is null)
			{
				IsRightSecretAnswer = false;
				InvokePropertyChanged(nameof(IsRightSecretAnswer));
				InvokePropertyChanged();
				return;
			}

			IsRightSecretAnswer = _accountResolver.ValidateSecretAnswer(Username, _secretAnswer).Result;
			InvokePropertyChanged(nameof(IsRightSecretAnswer));
			InvokePropertyChanged();
		}
	}

	public AsyncCommand NavigateLoginCommand { get; }
	public AsyncCommand ResetPasswordCommand { get; }

	public bool IsRightSecretAnswer { get; set; }

	private bool CredentialsProvided()
		=>  Username?.IsEmptyOrWhitespace() is false &&
			NewPassword?.IsEmptyOrWhitespace() is false;
}