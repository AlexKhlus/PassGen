using System;
using System.Threading.Tasks;
using ApplicationClient.Utils.Commands.Base;


namespace ApplicationClient.Utils.Commands;
public class LogoutCommand : AsyncCommand
{
	private readonly NavigateToLoginViewCommand _navigateToLoginViewCommand;

	public LogoutCommand(NavigateToLoginViewCommand navigateToLoginViewCommand)
	{
		_navigateToLoginViewCommand = navigateToLoginViewCommand;
	}

	public override async Task ExecuteAsync(object? parameter = null) 
	{
		Logouted?.Invoke(this, new () { IsExtended = false });
		await _navigateToLoginViewCommand.ExecuteAsync();
	}

	public event EventHandler<LogoutCommandArgs>? Logouted;
}

public sealed class LogoutCommandArgs : EventArgs
{
	public bool IsExtended { get; init; }
}