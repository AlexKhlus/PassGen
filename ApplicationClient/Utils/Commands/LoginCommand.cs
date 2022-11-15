using System;
using System.Threading.Tasks;
using System.Windows;
using ApplicationAPI.Resolvers;
using ApplicationClient.Utils.Commands.Base;


namespace ApplicationClient.Utils.Commands;
public sealed class LoginCommand : AsyncCommand 
{
	private readonly IAuthorizationResolver _authorizationResolver;
	private readonly NavigateToGeneratorViewCommand _navigateToGeneratorViewCommand;

	public LoginCommand(
		IAuthorizationResolver authorizationResolver,
		NavigateToGeneratorViewCommand navigateToGeneratorViewCommand
	) 
	{
		_authorizationResolver = authorizationResolver;
		_navigateToGeneratorViewCommand = navigateToGeneratorViewCommand;
	}

	public override async Task ExecuteAsync(object? parameter = null) 
	{
		var parameters = (object[])parameter!;
		var (username, password) = (parameters[0].ToString()!, parameters[1].ToString()!);

		var response = await _authorizationResolver.Login(new () {
			Username = username, Password = password
		});
		if(response.Success)
		{
			await Task.Delay(1000);
			await _navigateToGeneratorViewCommand.ExecuteAsync();
		}
		else MessageBox.Show($"Wrong credentials!{Environment.NewLine}{response.ErrorMessage}");
	}
}