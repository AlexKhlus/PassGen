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
create table if not exists SecretQuestion
(
    Id integer not null
        constraint SecretQuestionId_pk
            primary key autoincrement,
    Question TEXT not null
        constraint SecretQuestionQuestion_pk
            unique
);
insert or replace into SecretQuestion (Id, Question)
values
    (1, 'Name of the pet'),
    (2, 'Favorite writer'),
    (3, 'Favorite book'),
    (4, 'Favorite movie'),
    (5, 'Mother''s Maiden Name');
create table if not exists Category
(
    Id integer not null
        constraint CategoryId_pk
            primary key autoincrement,
    Category text not null
        constraint CategoryCategory_pk
            unique,
    Description text
);
create table if not exists User
(
    Id integer not null
        constraint UserId_pk
            primary key autoincrement,
    Username text not null
        constraint UserUsername_pk
            unique,
    Password text not null,
    SecretQuestionId integer not null
        constraint User_SecretQuestion__fk
            references SecretQuestion (Id)
                on update cascade,
    SecretAnswer text not null
);
create table if not exists Password
(
    Id integer not null
        constraint PasswordId_pk
            primary key autoincrement,
    UserId integer not null
        constraint Password_User_fk
            references User (Id)
                on update cascade,
    CategoryId integer not null
        constraint Password_Category_fk
            references Category (Id)
                on update cascade,
    Password text not null
);
");

		return connection;
	}
}