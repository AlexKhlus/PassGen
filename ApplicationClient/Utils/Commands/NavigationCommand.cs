using System.Threading.Tasks;
using ApplicationClient.Utils.Commands.Base;
using ApplicationClient.Utils.Services;


namespace ApplicationClient.Utils.Commands;
public class NavigationCommand : AsyncCommand 
{
	private readonly NavigationService _navigationService;

	public NavigationCommand(NavigationService navigationService) => _navigationService = navigationService;

	public override async Task ExecuteAsync(object? parameter = null) => await Task.Run(() => _navigationService.Navigate());
}

public sealed class NavigateToLoginViewCommand : NavigationCommand 
{
	public NavigateToLoginViewCommand(NavigationService navigationService) : base(navigationService) { }
}

public sealed class NavigateToRegistrationViewModelCommand : NavigationCommand
{
	public NavigateToRegistrationViewModelCommand(NavigationService navigationService) : base(navigationService) { }
}

public sealed class NavigateToStorageViewModelCommand : NavigationCommand 
{
	public NavigateToStorageViewModelCommand(NavigationService navigationService) : base(navigationService) { }
}

public sealed class NavigateToGeneratorViewModelCommand : NavigationCommand 
{
	public NavigateToGeneratorViewModelCommand(NavigationService navigationService) : base(navigationService) { }
}