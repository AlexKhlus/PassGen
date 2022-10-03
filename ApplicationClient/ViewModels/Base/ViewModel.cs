using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;


namespace ApplicationClient.ViewModels.Base;
public abstract class ViewModel : INotifyPropertyChanged, IDisposable 
{
	[NotifyPropertyChangedInvocator] 
	protected void InvokePropertyChanged([CallerMemberName] string propertyName = "")
		=> PropertyChanged?.Invoke(sender: this, e: new (propertyName));

	public virtual void Dispose() 
	{
		// override
	}

	public event PropertyChangedEventHandler? PropertyChanged;
}