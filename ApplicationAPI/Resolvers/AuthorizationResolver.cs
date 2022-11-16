using System.Threading.Tasks;
using ApplicationAPI.Contracts;
using ApplicationAPI.Data.Repository;


namespace ApplicationAPI.Resolvers;
public interface IAuthorizationResolver
{
	Task<LoginResponse> Login(LoginRequest request);
}

public sealed class AuthorizationResolver : IAuthorizationResolver
{
	private readonly IUserRepository _userRepository;

	public AuthorizationResolver(IUserRepository userRepository) => _userRepository = userRepository;

	public async Task<LoginResponse> Login(LoginRequest request)
	{
		if(request.Username is null)
			return new () {
				Success = false,
				ErrorMessage = "Username cannot be null"
			};

		var user = await _userRepository.GetUserBy(request.Username);
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

