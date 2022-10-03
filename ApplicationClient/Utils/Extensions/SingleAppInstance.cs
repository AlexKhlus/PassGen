using System.Threading;
using System.Windows;


namespace ApplicationClient.Utils.Extensions;
public static class SingleAppInstance
{
	private static Application? _application;
	private static Mutex? _currentAppMutex;
	private static bool _instanceUnique;

	public static void Register(Application application) 
	{
		_application = application;
		_currentAppMutex = new Mutex(
			initiallyOwned: true, name: typeof(App).FullName, 
			createdNew: out _instanceUnique
		);
	}

	public static void ShutdownIfAnotherRegistered() 
	{
		if(_instanceUnique is false)
			_application?.Shutdown();
	}

	public static void Unregister() 
	{
		_currentAppMutex?.ReleaseMutex();
		_currentAppMutex?.Close();
	}
}