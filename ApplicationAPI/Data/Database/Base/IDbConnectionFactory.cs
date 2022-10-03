using System.Data;


namespace ApplicationAPI.Data.Database.Base;
public interface IDbConnectionFactory
{
	IDbConnection CreateConnection();
}