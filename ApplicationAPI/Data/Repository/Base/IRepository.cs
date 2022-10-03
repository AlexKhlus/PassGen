using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationAPI.Data.Models;


namespace ApplicationAPI.Data.Repository.Base;
public interface IRepository 
{
	//TODO: Create more methods for work with db
	Task<List<User>> GetUsers();
	Task<User?> GetUser(int id);
	Task<User?> GetUser(string username);
	Task<bool> AddUser(User user);
}