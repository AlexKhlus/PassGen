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

public sealed class NavigateToRegistrationViewCommand : NavigationCommand
{
	public NavigateToRegistrationViewCommand(NavigationService navigationService) : base(navigationService) { }
}

public sealed class NavigateToResetAccountViewCommand : NavigationCommand
{
	public NavigateToResetAccountViewCommand(NavigationService navigationService) : base(navigationService) { }
}

public sealed class NavigateToStorageViewCommand : NavigationCommand 
{
	public NavigateToStorageViewCommand(NavigationService navigationService) : base(navigationService) { }
}

public sealed class NavigateToGeneratorViewCommand : NavigationCommand 
{
	public NavigateToGeneratorViewCommand(NavigationService navigationService) : base(navigationService) { }
}