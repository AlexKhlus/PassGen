using System;
using System.Threading.Tasks;
using System.Windows;
using ApplicationAPI.Resolvers;
using ApplicationClient.Utils.Commands.Base;


namespace ApplicationClient.Utils.Commands;
public class ResetPasswordCommand : AsyncCommand
{
	private readonly IAccountResolver _accountResolver;
	private readonly NavigateToLoginViewCommand _navigateToLoginViewCommand;

	public ResetPasswordCommand(
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
			newPassword
		) = (
			parameters[0].ToString()!,
			parameters[1].ToString()!
		);

		var response = await _accountResolver.ResetPassword(new () {
			Username = username,
			NewPassword = newPassword
		});
		if(response.Success)
		{
			await Task.Delay(1000);
			await _navigateToLoginViewCommand.ExecuteAsync();
		}
		else MessageBox.Show($"Could not change password!{Environment.NewLine}{response.ErrorMessage}");
	}
}