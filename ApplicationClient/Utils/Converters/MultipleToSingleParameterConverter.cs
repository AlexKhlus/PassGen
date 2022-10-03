using System;
using System.Globalization;
using System.Windows.Data;


namespace ApplicationClient.Utils.Converters;

[ValueConversion(typeof(object), typeof(object))]
public class MultipleToSingleParameterConverter : IMultiValueConverter
{
	public object Convert(
		object[] values, 
		Type targetType, 
		object parameter, 
		CultureInfo culture
	) 
	{
		if(targetType != typeof(object)) throw new InvalidOperationException("The target must be a object");
		return values.Clone();
	}

	public object[] ConvertBack(
		object value,
		Type[] targetTypes, 
		object parameter, 
		CultureInfo culture
	) => throw new NotSupportedException();
}