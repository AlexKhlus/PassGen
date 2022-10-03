using System.Data;
using ApplicationAPI.Data.Database.Base;
using Microsoft.Data.Sqlite;


namespace ApplicationAPI.Data.Database;
public sealed class DbConnectionFactory : IDbConnectionFactory
{
	private readonly string _connectionString;

	public DbConnectionFactory(string connectionString) => _connectionString = connectionString;

	public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}