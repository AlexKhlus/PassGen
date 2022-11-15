using System.Data;
using ApplicationAPI.Data.Database.Base;
using Dapper;
using Microsoft.Data.Sqlite;


namespace ApplicationAPI.Data.Database;
public sealed class DbConnectionFactory : IDbConnectionFactory
{
	private readonly string _connectionString;

	public DbConnectionFactory(string connectionString) => _connectionString = connectionString;

	public IDbConnection CreateConnection()
	{
		var connection = new SqliteConnection(_connectionString);
		connection.Execute(@"
create table if not exists User
(
    Id integer not null
        constraint UserId_pk
            primary key autoincrement,
    Username TEXT not null
        constraint UserUsername_pk
            unique,
    Password TEXT not null
);");

		return connection;
	}
}