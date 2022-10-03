using System;
using System.Threading.Tasks;
using System.Windows;
using ApplicationAPI;
using ApplicationClient.Utils.Services;


namespace ApplicationClient.Utils.Commands;
public sealed class LoginCommand : NavigationCommand 
{
	private readonly IAuthorizationResolver _authorizationResolver;

	public LoginCommand(
		NavigationService navigationService, 
		IAuthorizationResolver authorizationResolver
	) : base(navigationService)
	{
		_authorizationResolver = authorizationResolver;
	}

	public override async Task ExecuteAsync(object? parameter = null) 
	{
		var parameters = (object [])parameter!;
		var (username, password) = (parameters[0].ToString()!, parameters[1].ToString()!);

		var response = await _authorizationResolver.Login(new () {
			Username = username,
			Password = password
		});
		if(response.Success)
		{
			await Task.Delay(1000);
			await base.ExecuteAsync(parameter);
		}
		else MessageBox.Show($"Wrong credentials!{Environment.NewLine}{response.ErrorMessage}");
	}
}