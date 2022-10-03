using System;
using System.Threading.Tasks;
using ApplicationClient.Utils.Commands.Base;


namespace ApplicationClient.Utils.Commands;
public class ExtendBarCommand : AsyncCommand
{
	private bool _isMinimized;

	public override Task ExecuteAsync(object? parameter = null)
	{
		var isMinimizedPrevState = (bool)parameter!;
		_isMinimized = !isMinimizedPrevState;

		StateChanged?.Invoke(this, new () { IsMinimized = _isMinimized });
		return Task.CompletedTask;
	}

	public event EventHandler<IsExtendBarCommandArgs>? StateChanged;
}

public sealed class IsExtendBarCommandArgs : EventArgs 
{
	public bool IsMinimized { get; init; }
}