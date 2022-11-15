using System.Threading.Tasks;
using ApplicationAPI.Contracts;
using ApplicationAPI.Data.Repository.Base;


namespace ApplicationAPI.Resolvers;
public interface IAccountResolver
{
	Task<RegistrationResponse> RegisterAccount(RegistrationRequest request);
	Task<ResetAccountResponse> ResetAccount(ResetAccountRequest request);
}

public sealed class AccountResolver : IAccountResolver
{
	private readonly IRepository _repository;

	public AccountResolver(IRepository repository) => _repository = repository;

	public async Task<RegistrationResponse> RegisterAccount(RegistrationRequest request)
	{
		if(request.Username is null)
			return new () {
				Success = false,
				ErrorMessage = "Username cannot be null"
			};
		else if(string.Equals(request.Password, request.RepeatPassword) is false)
			return new () {
				Success = false,
				ErrorMessage = "Passwords must be same"
			};

		var isRegisterSuccess = await _repository.AddUser(new () {
			Username = request.Username,
			Password = request.Password
		});

		return isRegisterSuccess ? 
			new () {
				Success = true
			} : 
			new () {
				Success = false,
				ErrorMessage = "Registration error. Try again later"
			};
	}
	public Task<ResetAccountResponse> ResetAccount(ResetAccountRequest request)
	{
		throw new System.NotImplementedException();
	}
}