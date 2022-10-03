using System;
using System.Threading.Tasks;
using ApplicationClient.Utils.Services;


namespace ApplicationClient.Utils.Commands;
public class LogoutCommand : NavigationCommand
{
	public LogoutCommand(NavigationService navigationService) : base(navigationService) { }

	public override async Task ExecuteAsync(object? parameter = null) 
	{
		Logouted?.Invoke(this, new () { IsExtended = false });
		await base.ExecuteAsync(parameter);
	}

	public event EventHandler<LogoutCommandArgs>? Logouted;
}

public sealed class LogoutCommandArgs : EventArgs
{
	public bool IsExtended { get; init; }
}