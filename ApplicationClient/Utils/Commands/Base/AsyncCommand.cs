using System;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ApplicationClient.Utils.Commands.Base;
public abstract class AsyncCommand : ICommand, IDisposable 
{
	public virtual void Dispose() 
	{
		CanExecuteChanged = null;
		Started = null;
		Completed = null;
		Failed = null;
		Executed = null;

		CanExecuteCondition = null;
	}

	private bool _isExecuting;
	private bool IsExecuting 
	{
		get => _isExecuting;
		set 
		{
			_isExecuting = value;
			InvokeCanExecuteChanged();
		}
	}

	#region Events

	public event EventHandler? CanExecuteChanged;
	public event EventHandler<AsyncCommandStartedEventArgs>? Started;
	public event EventHandler<AsyncCommandCompletedEventArgs>? Completed;
	public event EventHandler<AsyncCommandFailedEventArgs>?    Failed;
	public event EventHandler<AsyncCommandExecutedEventArgs>?  Executed;

	#endregion
	#region CanExecute

	public Func<bool>? CanExecuteCondition { get; set; }

	public virtual bool CanExecute(object? parameter) => IsExecuting is false && (CanExecuteCondition?.Invoke() ?? false);

	public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(sender: this, EventArgs.Empty);

	#endregion
	#region Execute

	public abstract Task ExecuteAsync(object? parameter = null);
    public async void Execute(object? paramater = null) 
    {
    	IsExecuting = true;

		try
		{
			Started?.Invoke(sender: this, e: new AsyncCommandStartedEventArgs());
			await ExecuteAsync(paramater);
			Completed?.Invoke(sender: this, e: new AsyncCommandCompletedEventArgs());
		}
		catch(Exception exception) 
		{
			Failed?.Invoke(sender: this, e: new AsyncCommandFailedEventArgs() {
				Message = "Executing of async command has been failed.",
				Exception = exception
			});
		}
		finally 
		{
			Executed?.Invoke(sender: this, e: new AsyncCommandExecutedEventArgs());
		}

    	IsExecuting = false;
    }

	#endregion
}

public sealed class AsyncCommandStartedEventArgs : EventArgs { }
public sealed class AsyncCommandCompletedEventArgs : EventArgs { }
public sealed class AsyncCommandExecutedEventArgs : EventArgs { }
public sealed class AsyncCommandFailedEventArgs : EventArgs 
{
	public string? Message { get; init; }
	public Exception? Exception { get; init; }
}

