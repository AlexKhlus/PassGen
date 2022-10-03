using ApplicationAPI;
using ApplicationAPI.Data.Database;
using ApplicationAPI.Data.Database.Base;
using ApplicationAPI.Data.Repository;
using ApplicationAPI.Data.Repository.Base;
using ApplicationClient.Utils.Commands;
using ApplicationClient.Utils.Stores;
using ApplicationClient.ViewModels;
using ApplicationClient.Views;
using Microsoft.Extensions.DependencyInjection;


namespace ApplicationClient.Utils.Extensions;
public static class ServiceCollectionExtensions 
{
	public static IServiceCollection AddDataConnection(
		this IServiceCollection services,
		string connectionString
	) 
	{
		var connectionFactory = new DbConnectionFactory(connectionString);
		services
			.AddSingleton<IDbConnectionFactory>(connectionFactory)

			.AddScoped<IDatabase, Database>()
			.AddScoped<IRepositoryFactory, RepositoryFactory>()

			.AddTransient<IRepository, Repository>();

		return services;
	}

	public static IServiceCollection AddStores(this IServiceCollection services) 
	{
		services.AddSingleton<NavigationStore>();

		return services;
	}

	public static IServiceCollection AddResolvers(this IServiceCollection services)
	{
		services.AddTransient<IAuthorizationResolver, AuthorizationResolver>();

		return services;
	}

	public static IServiceCollection AddViewModels(this IServiceCollection services)
	{
		services
			.AddSingleton<MainWindowViewModel>()
			.AddSingleton<MainWindow>(provider => new () { DataContext = provider.GetRequiredService<MainWindowViewModel>() })

			.AddTransient<LoginViewModel>()
			.AddTransient<NavigateToLoginViewCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<LoginViewModel>
			)))

			.AddTransient<RegistrationViewModel>()
			.AddTransient<NavigateToRegistrationViewModelCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<RegistrationViewModel>
			)))

			.AddTransient<GeneratorViewModel>()
			.AddTransient<NavigateToGeneratorViewModelCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<GeneratorViewModel>
			)))

			.AddTransient<StorageViewModel>()
			.AddTransient<NavigateToStorageViewModelCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<StorageViewModel>
			)))

			.AddTransient<LoginCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<GeneratorViewModel>
			), authorizationResolver: provider.GetRequiredService<IAuthorizationResolver>()))

			.AddTransient<LogoutCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<LoginViewModel>
			)))

			.AddTransient<ExtendBarCommand>()

			.AddTransient<NavigationSideBarViewModel>();

		return services;
	}
}