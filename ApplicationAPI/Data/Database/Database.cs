using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ApplicationAPI.Data.Database.Base;
using Dapper;


namespace ApplicationAPI.Data.Database;
public sealed class Database : IDatabase, IDisposable 
{
	private readonly IDbConnectionFactory _factory;
	private IDbConnection? _connection;
	private IDbTransaction? _transaction;
	private const int DEFAULT_COMMAND_TIMEOUT = 30;

	private IDbConnection Connection 
	{
		get => _connection ??= _factory.CreateConnection();
	}

	public Database(IDbConnectionFactory factory) 
	{
		_factory = factory;
		_transaction = null;
	}

	public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
		=> _transaction ??= Connection.BeginTransaction(isolation);

	public void CommitTransaction() 
	{
		_transaction?.Commit();
		_transaction = null;
	}

	public void RollbackTransaction() 
	{
		_transaction?.Rollback();
		_transaction = null;
	}

	public void Dispose() 
	{
		if(_connection is not null)
		{
			if(_connection.State is not ConnectionState.Closed)
			{
				RollbackTransaction();
				_connection.Close();
			}

			_connection = null;
		}

		GC.SuppressFinalize(this);
	}

	public int Execute(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.Execute(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public Task<int> ExecuteAsync(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.ExecuteAsync(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public T ExecuteScalar<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.ExecuteScalar<T>(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public Task<T> ExecuteScalarAsync<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.ExecuteScalarAsync<T>(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public IEnumerable<T> Query<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		bool buffered = true, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.Query<T>(sqlQuery, param, transaction ?? _transaction, buffered, commandTimeout, commandType);

	public Task<IEnumerable<T>> QueryAsync<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.QueryAsync<T>(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public SqlMapper.GridReader QueryMultiple(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.QueryMultiple(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public Task<SqlMapper.GridReader> QueryMultipleAsync(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.QueryMultipleAsync(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public T SingleOrDefault<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.QuerySingleOrDefault<T>(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);

	public Task<T> SingleOrDefaultAsync<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = DEFAULT_COMMAND_TIMEOUT, 
		CommandType? commandType = null
	) => Connection.QuerySingleOrDefaultAsync<T>(sqlQuery, param, transaction ?? _transaction, commandTimeout, commandType);
}