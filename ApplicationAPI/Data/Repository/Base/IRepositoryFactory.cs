namespace ApplicationAPI.Data.Repository.Base;
public interface IRepositoryFactory 
{
	IRepository GetRepository();
}