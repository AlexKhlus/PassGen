using System;
using System.Threading.Tasks;
using System.Windows;
using ApplicationAPI.Resolvers;
using ApplicationClient.Utils.Commands.Base;


namespace ApplicationClient.Utils.Commands;
public sealed class RegisterAccountCommand : AsyncCommand
{
	private readonly IAccountResolver _accountResolver;
	private readonly NavigateToLoginViewCommand _navigateToLoginViewCommand;

	public RegisterAccountCommand(
		IAccountResolver accountResolver, 
		NavigateToLoginViewCommand navigateToLoginViewCommand
	) 
	{
		_accountResolver = accountResolver;
		_navigateToLoginViewCommand = navigateToLoginViewCommand;
	}

	public override async Task ExecuteAsync(object? parameter = null) 
	{
		var parameters = (object[])parameter!;
		var (
			username,
			password, 
			repeatPassword
		) = (
			parameters[0].ToString()!,
			parameters[1].ToString()!,
			parameters[2].ToString()!
		);

		var response = await _accountResolver.RegisterAccount(new () {
			Username = username,
			Password = password,
			RepeatPassword = repeatPassword
		});
		if(response.Success)
		{
			await Task.Delay(1000);
			await _navigateToLoginViewCommand.ExecuteAsync();
		}
		else MessageBox.Show($"Wrong credentials!{Environment.NewLine}{response.ErrorMessage}");
	}
}