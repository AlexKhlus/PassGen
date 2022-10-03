using ApplicationClient.Utils.Stores;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.ViewModels;
public sealed class MainWindowViewModel : ViewModel 
{
	private readonly NavigationStore _navigationStore;

	public MainWindowViewModel(
		NavigationStore navigationStore,
		NavigationSideBarViewModel navigationSideBarViewModel
	) 
	{
		_navigationStore = navigationStore;
		NavigationSideBarViewModel = navigationSideBarViewModel;

		_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
	}

	public override void Dispose() => _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

	public NavigationSideBarViewModel NavigationSideBarViewModel { get; }
	public ViewModel CurrentViewModel 
	{
		get
		{
			IsLoggedIn = GetCurrentViewModel() is GeneratorViewModel or StorageViewModel;
			return GetCurrentViewModel();
		}
	}

	private bool _isLoggedIn;
	public bool IsLoggedIn 
	{
		get => _isLoggedIn;
		set
		{
			_isLoggedIn = value;
			InvokePropertyChanged();
		}
	}

	private void OnCurrentViewModelChanged(object? sender, CurrentViewModelChangedEventArgs args) => SetCurrentViewModel();
	private void SetCurrentViewModel() => InvokePropertyChanged(nameof(CurrentViewModel));
	private ViewModel GetCurrentViewModel() => _navigationStore.CurrentViewModel;
}