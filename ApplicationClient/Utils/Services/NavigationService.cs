using System;
using ApplicationClient.Utils.Stores;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.Utils.Services;
public sealed class NavigationService : INavigationService 
{
	private readonly NavigationStore _navigationStore;
	private readonly Func<ViewModel> _destinationViewModel;

	public NavigationService(
		NavigationStore navigationStore, 
		Func<ViewModel> destinationViewModel
	) 
	{
		_navigationStore = navigationStore;
		_destinationViewModel = destinationViewModel;
	}

	public void Navigate() => _navigationStore.CurrentViewModel = _destinationViewModel();
}