using System.ComponentModel.DataAnnotations;
using ApplicationAPI.Attributes;


namespace ApplicationAPI.Data.Models;
public sealed class User 
{
	[Required, NoWhitespace] public string? Username { get; set; }
	[Required] public string? Password { get; set; }
	[Required] public string? SecretQuestion { get; set; }
	[Required] public string? SecretAnswer { get; set; }
}