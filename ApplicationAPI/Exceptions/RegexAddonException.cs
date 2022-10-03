using System;


namespace ApplicationAPI.Exceptions;

/// <summary>
/// Exception related to regex addon.
/// </summary>
public sealed class RegexAddonException : ApplicationException 
{
	/// <inheritdoc />
	public RegexAddonException(string message) : base(message) 
	{
		// empty
	}
}