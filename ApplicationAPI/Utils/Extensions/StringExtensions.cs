using System;
using System.Linq;


namespace ApplicationAPI.Utils.Extensions;
public static class StringExtensions 
{
	public static bool IsEmptyOrWhitespace(this string value) 
	{
		ArgumentNullException.ThrowIfNull(value);
		return value.IsEmpty() || value.IsWhiteSpace();
	}

	public static bool IsWhiteSpace(this string value) 
	{
		ArgumentNullException.ThrowIfNull(value);
		return value.All(char.IsWhiteSpace);
	}

	public static bool IsEmpty(this string value) 
	{
		ArgumentNullException.ThrowIfNull(value);
		return 0u >= (uint)value.Length;
	}
}