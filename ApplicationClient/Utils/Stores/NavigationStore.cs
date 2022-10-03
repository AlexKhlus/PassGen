using System;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.Utils.Stores;
public sealed class NavigationStore 
{
	private ViewModel _currentViewModel;

	public ViewModel CurrentViewModel 
	{
		get => _currentViewModel;
		set
		{
			var prevViewModel = _currentViewModel;

			_currentViewModel?.Dispose();
			_currentViewModel = value;

			CurrentViewModelChanged?.Invoke(sender: this, e: new () {
				PrevViewModel = prevViewModel,
				CurrViewModel = _currentViewModel
			});
		}
	}

	public event EventHandler<CurrentViewModelChangedEventArgs>? CurrentViewModelChanged;
}

public class CurrentViewModelChangedEventArgs : EventArgs 
{
	internal ViewModel? PrevViewModel { get; init; }
	internal ViewModel? CurrViewModel { get; init; }
}