using System.Globalization;
using System.Windows;
using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Extensions;
using ApplicationClient.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ApplicationClient;
public partial class App : Application 
{
	private readonly IHost _host;

	public App() 
	{
		_host = Host.CreateDefaultBuilder().ConfigureServices((builder, services) => 
		{
			services
				.AddDataConnection(builder.Configuration.GetConnectionString("Default"))
				.AddStores()
				.AddResolvers()
				.AddViewModels();
		}).Build();
	}

	protected override async void OnStartup(StartupEventArgs args) 
	{
		base.OnStartup(args);

		SingleAppInstance.Register(application: this);
		SingleAppInstance.ShutdownIfAnotherRegistered();
		CultureInfo.CurrentUICulture = new CultureInfo("uk-ua", false);

		await _host.StartAsync();
		await _host.Services.GetRequiredService<NavigateToLoginViewCommand>().ExecuteAsync();

		MainWindow = _host.Services.GetRequiredService<MainWindow>();
		MainWindow.Show();
	}

	protected override async void OnExit(ExitEventArgs e) 
	{
		await _host.StopAsync();
		SingleAppInstance.Unregister();

		base.OnExit(e);
	}
}