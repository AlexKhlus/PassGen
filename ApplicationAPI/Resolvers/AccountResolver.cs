using System.Threading.Tasks;
using ApplicationAPI.Contracts;
using ApplicationAPI.Data.Repository;
using ApplicationAPI.Utils.Extensions;


namespace ApplicationAPI.Resolvers;
public interface IAccountResolver
{
	Task<RegistrationResponse> RegisterAccount(RegistrationRequest request);
	Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
	Task<bool> ValidateSecretAnswer(string? username, string? secretAnswer);
	Task<bool> IsUserExist(string? username);
}

public sealed class AccountResolver : IAccountResolver
{
	private readonly IUserRepository _userRepository;

	public AccountResolver(IUserRepository userRepository) => _userRepository = userRepository;

	public async Task<RegistrationResponse> RegisterAccount(RegistrationRequest request) 
	{
		if(request.Username?.IsEmptyOrWhitespace() is true)
			return new () {
				Success = false,
				ErrorMessage = "Username cannot be empty"
			};
		if(await _userRepository.GetUserBy(request.Username!) is not null)
			return new () {
				Success = false,
				ErrorMessage = "User already register"
			};
		if(request.SecretQuestion?.IsEmptyOrWhitespace() is true)
			return new () {
				Success = false,
				ErrorMessage = "Secret question cannot be empty"
			};
		if(request.SecretAnswer?.IsEmptyOrWhitespace() is true)
			return new () {
				Success = false,
				ErrorMessage = "Secret answer cannot be empty"
			};
		if(string.Equals(request.Password, request.RepeatPassword) is false)
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

	public async Task<bool> IsUserExist(string? username) 
	{
		return username?.IsEmptyOrWhitespace() is false && await _userRepository.GetUserBy(username) is not null;
	}
}