using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ApplicationClient.XamlControls;
public partial class BindablePasswordBox : UserControl 
{
	private bool _isPasswordChanged;

	public BindablePasswordBox() => InitializeComponent();
	
	#region Public Properties

	public string Placeholder 
    {
    	get => (string)GetValue(PasswordProperty);
    	set => SetValue(PasswordProperty, value);
    }
    public string Password 
    {
    	get => (string)GetValue(PasswordProperty);
    	set => SetValue(PasswordProperty, value);
    }
    public bool HasLeadingIcon 
    {
    	get => (bool)GetValue(HasLeadingIconProperty);
    	set => SetValue(HasLeadingIconProperty, value);
    }
    public Geometry LeadingIcon 
    {
    	get => (Geometry)GetValue(LeadingIconProperty);
    	set => SetValue(LeadingIconProperty, value);
    }

	#endregion
	#region Dependency Properies

	public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
		name: nameof(Placeholder),
		propertyType: typeof(string),
		ownerType: typeof(BindablePasswordBox)
	);
	public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
		name: nameof(Password),
		propertyType: typeof(string),
		ownerType: typeof(BindablePasswordBox),
		typeMetadata: new PropertyMetadata(string.Empty, OnPasswordPropertyChanged)
	);
	public static readonly DependencyProperty HasLeadingIconProperty = DependencyProperty.Register(
		name: nameof(HasLeadingIcon),
		propertyType: typeof(bool),
		ownerType: typeof(BindablePasswordBox)
	);
	public static readonly DependencyProperty LeadingIconProperty = DependencyProperty.Register(
		name: nameof(LeadingIcon),
		propertyType: typeof(Geometry),
		ownerType: typeof(BindablePasswordBox)
	);

	#endregion
	#region Private Methods

	private static void OnPasswordPropertyChanged(
		DependencyObject dependencyObject, 
		DependencyPropertyChangedEventArgs e
	) 
	{
		if(dependencyObject is BindablePasswordBox passwordBox)
			passwordBox.UpdatePassword();
	}
	private void OnPasswordChanged(
		object sender, 
		RoutedEventArgs e
	) 
	{
		PasswordField.FontSize = 20;
		PasswordField.Margin = new Thickness(0, 20, 0, 0);
		if(string.IsNullOrEmpty(PasswordField.Password)) 
		{
			PasswordField.FontSize = 15;
			PasswordField.Margin = new Thickness(0);
		}

		TextBox.Text = PasswordField.Password;

		_isPasswordChanged = true;
		Password = PasswordField.Password;
		_isPasswordChanged = false;
	}

	private void OnFieldClick(
		object sender, 
		MouseButtonEventArgs e
	) 
	{
		FieldFocus();
	}
	private void OnLeadingIconClick(
		object sender, 
		RoutedEventArgs e
	) 
	{
		FieldFocus();
	}
	private void OnMouseUp(
		object sender, 
		RoutedEventArgs e
	) 
	{
		PasswordField.Focus();
	}
	
	private void FieldFocus() 
	{
		PasswordField.Focus();
	}
	private void UpdatePassword() 
	{
		if(!_isPasswordChanged)
			PasswordField.Password = Password;
	}

	#endregion
}