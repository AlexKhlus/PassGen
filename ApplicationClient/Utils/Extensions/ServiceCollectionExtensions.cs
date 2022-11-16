using System.IO;
using ApplicationAPI.Data.Database;
using ApplicationAPI.Data.Database.Base;
using ApplicationAPI.Data.Repository;
using ApplicationAPI.Resolvers;
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
		var dbFilePath = connectionString.Replace("DataSource=", string.Empty);
		var dbDirectory = dbFilePath.Replace(@"\PassGenDB.db", string.Empty);
		var isDbFileCreated = Directory.Exists(dbDirectory) && File.Exists(dbFilePath);
		if(isDbFileCreated is false)
		{
			Directory.CreateDirectory(dbDirectory);
			File.Create(dbFilePath);
		}
		var connectionFactory = new DbConnectionFactory(connectionString);
		services
			.AddSingleton<IDbConnectionFactory>(connectionFactory)

			.AddScoped<IDatabase, Database>()
			.AddScoped<IRepositoryFactory, RepositoryFactory>()

			.AddTransient<IUserRepository, UserRepository>()
			.AddTransient<ISecretQuestionRepository, SecretQuestionRepository>();

		return services;
	}

	public static IServiceCollection AddStores(this IServiceCollection services) 
	{
		services.AddSingleton<NavigationStore>();

		return services;
	}

	public static IServiceCollection AddResolvers(this IServiceCollection services)
	{
		services
			.AddTransient<IAuthorizationResolver, AuthorizationResolver>()
			.AddTransient<IAccountResolver, AccountResolver>();

		return services;
	}

	public static IServiceCollection AddViewModels(this IServiceCollection services) 
	{
		services
			.AddSingleton<MainWindowViewModel>()
			.AddSingleton<MainWindow>(provider => new () { DataContext = provider.GetRequiredService<MainWindowViewModel>() })

			.AddTransient<LoginViewModel>()
			.AddTransient<RegistrationViewModel>()
			.AddTransient<ResetPasswordViewModel>()

			.AddTransient<GeneratorViewModel>()
			.AddTransient<StorageViewModel>();

		return services;
	}

	public static IServiceCollection AddCommands(this IServiceCollection services)
	{
		services
			.AddTransient<NavigateToLoginViewCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<LoginViewModel>
			)))
			.AddTransient<NavigateToRegistrationViewCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<RegistrationViewModel>
			)))
			.AddTransient<NavigateToResetAccountViewCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<ResetPasswordViewModel>
			)))

			.AddTransient<NavigateToGeneratorViewCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<GeneratorViewModel>
			)))
			.AddTransient<NavigateToStorageViewCommand>(provider => new (navigationService: new (
				navigationStore: provider.GetRequiredService<NavigationStore>(),
				destinationViewModel: provider.GetRequiredService<StorageViewModel>
			)))

			.AddTransient<LoginCommand>()
			.AddTransient<LogoutCommand>()
			.AddTransient<RegisterAccountCommand>()
			.AddTransient<ResetPasswordCommand>()

			.AddTransient<ExtendBarCommand>()
			.AddTransient<NavigationSideBarViewModel>();

		return services;
	}
}