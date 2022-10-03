using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ApplicationClient.Utils.Converters;
public sealed class BoolToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		=> targetType == typeof(Visibility) && (bool)value ? Visibility.Collapsed : Visibility.Visible;

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		=> targetType == typeof(bool) && value is Visibility.Visible;
}