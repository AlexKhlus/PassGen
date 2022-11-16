using System.Collections.Generic;
using System.Linq;
using ApplicationAPI.Data.Repository;
using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.Utils.Extensions;
using ApplicationClient.Utils.Resources;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.ViewModels;
public sealed class RegistrationViewModel : ViewModel
{
	private string? _username;
	private string? _password;
	private string? _repeatPassword;

	private string? _selectedSecretQuestion;
	private string? _secretAnswer;

	public RegistrationViewModel(
		NavigateToLoginViewCommand navigateToLoginViewCommand,
		RegisterAccountCommand registerAccountCommand,
		ISecretQuestionRepository secretQuestionRepository
	) 
	{
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

	public AsyncCommand NavigateLoginCommand { get; }
	public AsyncCommand RegisterAccountCommand { get; }

	private bool CredentialsProvided()
		=>  Username?.IsEmptyOrWhitespace() is false &&
			Password?.IsEmptyOrWhitespace() is false &&
			RepeatPassword?.IsEmptyOrWhitespace() is false &&
			string.Equals(Password, RepeatPassword) &&
			string.Equals(SelectedSecretQuestion, SecretQuestions.First()) is false &&
			SecretAnswer?.IsEmptyOrWhitespace() is false;
}