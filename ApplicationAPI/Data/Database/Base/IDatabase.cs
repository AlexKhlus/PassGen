using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;


namespace ApplicationAPI.Data.Database.Base;
public interface IDatabase
{
	void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);
	void CommitTransaction();
	void RollbackTransaction();

	IEnumerable<T> Query<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		bool buffered = true, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	Task<IEnumerable<T>> QueryAsync<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	T SingleOrDefault<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	Task<T> SingleOrDefaultAsync<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	SqlMapper.GridReader QueryMultiple(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	Task<SqlMapper.GridReader> QueryMultipleAsync(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	int Execute(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	Task<int> ExecuteAsync(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	T ExecuteScalar<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);

	Task<T> ExecuteScalarAsync<T>(
		string sqlQuery, 
		object? param = null, 
		IDbTransaction? transaction = null, 
		int? commandTimeout = null, 
		CommandType? commandType = null
	);
}