using System;
using ApplicationAPI.Data.Repository.Base;
using Microsoft.Extensions.DependencyInjection;


namespace ApplicationAPI.Data.Repository;
public sealed class RepositoryFactory : IRepositoryFactory
{
	private readonly IServiceProvider _provider;

	public RepositoryFactory(IServiceProvider provider) => _provider = provider;

	public IRepository GetRepository() => _provider.GetRequiredService<IRepository>();
}