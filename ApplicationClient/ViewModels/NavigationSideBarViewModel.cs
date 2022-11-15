using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.ViewModels.Base;


namespace ApplicationClient.ViewModels;
public sealed class NavigationSideBarViewModel : ViewModel
{
	private bool _isExtended;

	public NavigationSideBarViewModel(
		NavigateToGeneratorViewCommand navigateToGeneratorViewCommand,
		NavigateToStorageViewCommand navigateToStorageViewCommand,
		LogoutCommand logoutCommand,

		ExtendBarCommand extendBarCommand
	) 
	{
		(NavigateToGeneratorViewCommand = navigateToGeneratorViewCommand).CanExecuteCondition = () => true;
		(NavigateToStorageViewCommand = navigateToStorageViewCommand).CanExecuteCondition = () => true;

		(LogoutCommand = logoutCommand).CanExecuteCondition = () => true;
		LogoutCommand.Logouted += OnLogouted;

		(ExtendBarCommand = extendBarCommand).CanExecuteCondition = () => true;
		ExtendBarCommand.StateChanged += OnStateChanged;

		IsExtended = false;
	}

	public override void Dispose() 
	{
		ExtendBarCommand.StateChanged -= OnStateChanged;
	}

	public bool IsExtended 
	{
		get => _isExtended;
		set 
		{
			if(value.Equals(_isExtended)) return;

			_isExtended = value;
			InvokePropertyChanged();
		}
	}

	public AsyncCommand NavigateToGeneratorViewCommand { get; }
	public AsyncCommand NavigateToStorageViewCommand { get; }
	public LogoutCommand LogoutCommand { get; }

	public ExtendBarCommand ExtendBarCommand { get; }

	private void OnStateChanged(object? sender, IsExtendBarCommandArgs e) => IsExtended = e.IsMinimized;
	private void OnLogouted(object? sender, LogoutCommandArgs e) => IsExtended = e.IsExtended;
}