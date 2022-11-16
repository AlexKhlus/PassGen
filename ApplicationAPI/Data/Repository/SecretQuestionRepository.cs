using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationAPI.Data.Database.Base;


namespace ApplicationAPI.Data.Repository;
public interface ISecretQuestionRepository
{
	Task<List<string>> GetQuestions();
}

public class SecretQuestionRepository : ISecretQuestionRepository
{
	private readonly IDatabase _database;

	public SecretQuestionRepository(IDatabase database) => _database = database;

	public async Task<List<string>> GetQuestions()
		=> (await _database.QueryAsync<string>(@"select Question from SecretQuestion")).ToList();
}