using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationAPI.Data.Database.Base;
using ApplicationAPI.Data.Models;


namespace ApplicationAPI.Data.Repository;
public interface IUserRepository 
{
	//TODO: Create more methods for work with db
	Task<List<User>> GetUsers();
	Task<User?> GetUserBy(int id);
	Task<User?> GetUserBy(string username);
	Task<bool> RegisterUser(User user);
	Task<bool> ChangePassword(User user);
}

public sealed class UserRepository : IUserRepository 
{
	private readonly IDatabase _database;

	public UserRepository(IDatabase database) => _database = database;

	public async Task<List<User>> GetUsers() 
		=> (await _database.QueryAsync<User>(@"select * from User")).ToList();

	public async Task<User?> GetUserBy(int id) 
		=> await _database.SingleOrDefaultAsync<User>(
			@"
select Username, Password, Question, SecretAnswer from User
inner join SecretQuestion on User.SecretQuestionId = SecretQuestion.Id
where User.Id = @Id", 
			new { Id = id }
		);

	public async Task<User?> GetUserBy(string username)
	{
		var user = await _database.SingleOrDefaultAsync<User>(
			@"
select Username, Password, Question, SecretAnswer from User
inner join SecretQuestion on User.SecretQuestionId = SecretQuestion.Id
where User.Username = @Username",
			new { Username = username }
		);

		return user;
	}

	public async Task<bool> RegisterUser(User user)
		=> await _database.ExecuteAsync(
			@"
insert into User(Username, Password, SecretQuestionId, SecretAnswer)
values 
    (
        @Username, 
        @Password, 
        (
        	select Id from SecretQuestion 
        	where Question = @SecretQuestion
        ), 
        @SecretAnswer
    )
", 
			user) >= 1;

	public async Task<bool> ChangePassword(User user)
		=> await _database.ExecuteAsync(@"
update User
set Password = @Password
where Username = @Username
", user) >= 1;
}