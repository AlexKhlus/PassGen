using System.Threading.Tasks;
using ApplicationAPI.Contracts;
using ApplicationAPI.Data.Repository;


namespace ApplicationAPI.Resolvers;
public interface IAccountResolver
{
	Task<RegistrationResponse> RegisterAccount(RegistrationRequest request);
	Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
	Task<bool> ValidateSecretAnswer(string? username, string? secretAnswer);
}

public sealed class AccountResolver : IAccountResolver
{
	private readonly IUserRepository _userRepository;

	public AccountResolver(IUserRepository userRepository) => _userRepository = userRepository;

	public async Task<RegistrationResponse> RegisterAccount(RegistrationRequest request) 
	{
		if(request.Username is null)
			return new () {
				Success = false,
				ErrorMessage = "Username cannot be null"
			};
		else if(request.SecretQuestion is null)
			return new () {
				Success = false,
				ErrorMessage = "Secret question cannot be null"
			};
		else if(request.SecretAnswer is null)
			return new () {
				Success = false,
				ErrorMessage = "Secret answer cannot be null"
			};
		else if(string.Equals(request.Password, request.RepeatPassword) is false)
			return new () {
				Success = false,
				ErrorMessage = "Passwords must be same"
			};

		var isRegisterSuccess = await _userRepository.RegisterUser(new () {
			Username = request.Username,
			Password = request.Password,
			SecretQuestion = request.SecretQuestion,
			SecretAnswer = request.SecretAnswer
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

	public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request) 
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

		var isResetPasswordSuccess = await _userRepository.ChangePassword(new () {
			Username = request.Username,
			Password = request.NewPassword
		});

		return isResetPasswordSuccess ? 
			new () {
				Success = true
			} : 
			new () {
				Success = false,
				ErrorMessage = "Reset password error. Try again later"
			};
	}

	public async Task<bool> ValidateSecretAnswer(string? username, string? secretAnswer)
	{
		if(username is null || secretAnswer is null)
			return false;

		var user = await _userRepository.GetUserBy(username);
		return user is not null && string.Equals(secretAnswer, user.SecretAnswer);
	}
}