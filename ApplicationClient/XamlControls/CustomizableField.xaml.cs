using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ApplicationClient.XamlControls;
public partial class CustomizableField : UserControl 
{
	private bool _isTextChanged;

	public CustomizableField() => InitializeComponent();

	#region Public Properties

	public string Placeholder 
	{
		get => (string)GetValue(PlaceholderProperty);
		set => SetValue(PlaceholderProperty, value);
	}
	public string Text 
	{
		get => (string)GetValue(TextProperty); 
		set => SetValue(TextProperty, value);
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
	public bool HasClearButton 
	{
		get => (bool)GetValue(HasClearButtonProperty);
		set => SetValue(HasClearButtonProperty, value);
	}

	#endregion
	#region Dependency Properties

	public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
		name: nameof(Placeholder), 
		propertyType: typeof(string), 
		ownerType: typeof(CustomizableField)
	);
	public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
		name: nameof(Text), 
		propertyType: typeof(string), 
		ownerType: typeof(CustomizableField),
		typeMetadata: new PropertyMetadata(string.Empty, OnTextPropertyChanged)
	);
	public static readonly DependencyProperty HasLeadingIconProperty = DependencyProperty.Register(
		name: nameof(HasLeadingIcon),
		propertyType: typeof(bool),
		ownerType: typeof(CustomizableField)
	);
	public static readonly DependencyProperty LeadingIconProperty = DependencyProperty.Register(
		name: nameof(LeadingIcon),
		propertyType: typeof(Geometry),
		ownerType: typeof(CustomizableField)
	);
	public static readonly DependencyProperty HasClearButtonProperty = DependencyProperty.Register(
		name: nameof(HasClearButton),
		propertyType: typeof(bool),
		ownerType: typeof(CustomizableField)
	);

	#endregion
	#region Private Methods

	private void OnClearButtonClick(
		object sender, 
		RoutedEventArgs e
	) 
	{
		TextField.Focus();
		TextField.Text = string.Empty;
	}
	private void OnLeadingIconClick(
		object sender, 
		RoutedEventArgs e
	) 
	{
		TextField.Focus();
	}
	private void OnFieldClick(
		object sender, 
		MouseButtonEventArgs e
	) 
	{
		TextField.Focus();
	}

	private static void OnTextPropertyChanged(
		DependencyObject dependencyObject, 
		DependencyPropertyChangedEventArgs e
	) 
	{
		if(dependencyObject is CustomizableField textBox) 
			textBox.UpdateText();
	}
	private void OnTextChanged(
		object sender, 
		TextChangedEventArgs e
	) 
	{
		_isTextChanged = true;
		Text = TextField.Text;
		_isTextChanged = false;
	}
	private void UpdateText() 
	{
		if(!_isTextChanged)
			TextField.Text = Text;
	}

	#endregion
}