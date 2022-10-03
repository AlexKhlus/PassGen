using System.Threading.Tasks;
using ApplicationAPI.Contracts;
using ApplicationAPI.Data.Repository.Base;


namespace ApplicationAPI;
public interface IAuthorizationResolver
{
	Task<LoginResponse> Login(LoginRequest request);
}

public sealed class AuthorizationResolver : IAuthorizationResolver
{
	private readonly IRepository _repository;

	public AuthorizationResolver(IRepository repository) => _repository = repository;

	public async Task<LoginResponse> Login(LoginRequest request)
	{
		if(request.Username is null)
			return new () {
				Success = false,
				ErrorMessage = "Username cannot be null"
			};

		var user = await _repository.GetUser(request.Username);
		if(user is null)
			return new () {
				Success = false,
				ErrorMessage = "Cannot found existing user"
			};

		if(user.Password!.Equals(request.Password))
			return new () {
				Success = true
			};

		return new () {
			Success = false,
			ErrorMessage = "Unexpected issue during authorization"
		};
	}
}

