using System.ComponentModel;
using System.Runtime.CompilerServices;
using ApplicationClient.Annotations;


namespace ApplicationClient.ViewModels.Base;
public class ViewModelBase : INotifyPropertyChanged 
{
	[NotifyPropertyChangedInvocator] 
	protected virtual void OnPropertyChanged(
		[CallerMemberName] string propertyName = ""
	) 
	{
		PropertyChanged?.Invoke(
			sender: this, 
			e: new PropertyChangedEventArgs(propertyName)
		);
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
}