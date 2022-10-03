using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationAPI.Data.Database.Base;
using ApplicationAPI.Data.Models;
using ApplicationAPI.Data.Repository.Base;


namespace ApplicationAPI.Data.Repository;
public sealed class Repository : IRepository 
{
	private readonly IDatabase _database;

	public Repository(IDatabase database) => _database = database;

	public async Task<List<User>> GetUsers() 
		=> (await _database.QueryAsync<User>($@"select * from User")).ToList();

	public async Task<User?> GetUser(int id) 
		=> await _database.SingleOrDefaultAsync<User>(
			@"select * from User where Id = @Id", 
			new { Id = id }
		);

	public async Task<User?> GetUser(string username) 
		=> await _database.SingleOrDefaultAsync<User>(
			@"select * from User where Username = @Login", 
			new { Login = username}
		);

	public async Task<bool> AddUser(User user) 
	{
		var rowsCount = await _database.ExecuteAsync(@"
insert into User(Username, Password, Email)
values (@Login, @Password, @Email)", user);

		return rowsCount >= 1;
	}
}