using System;
using Microsoft.Extensions.DependencyInjection;


namespace ApplicationAPI.Data.Repository;
public interface IRepositoryFactory 
{
	IUserRepository GetRepository();
}

public sealed class RepositoryFactory : IRepositoryFactory
{
	private readonly IServiceProvider _provider;

	public RepositoryFactory(IServiceProvider provider) => _provider = provider;

	public IUserRepository GetRepository() => _provider.GetRequiredService<IUserRepository>();
}