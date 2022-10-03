using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ApplicationClient.XamlControls;
public partial class TextArea : UserControl 
{
	private bool _isTextChanged;

	public TextArea() => InitializeComponent();

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
	public bool HasClearButton 
	{
		get => (bool)GetValue(HasClearButtonProperty);
		set => SetValue(HasClearButtonProperty, value);
	}
	public double ElementHeight 
	{
		get => (double)GetValue(ElementHeightProperty);
		set => SetValue(ElementHeightProperty, value);
	}
	public int MaxLength 
	{
		get => (int)GetValue(MaxLengthProperty);
		set => SetValue(MaxLengthProperty, value);
	}

	#endregion
	#region Dependency Properties

	public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
		name: nameof(Placeholder), 
		propertyType: typeof(string), 
		ownerType: typeof(TextArea)
	);
	public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
		name: nameof(Text), 
		propertyType: typeof(string), 
		ownerType: typeof(TextArea),
		typeMetadata: new PropertyMetadata(string.Empty, OnTextPropertyChanged)
	);
	public static readonly DependencyProperty HasClearButtonProperty = DependencyProperty.Register(
		name: nameof(HasClearButton),
		propertyType: typeof(bool),
		ownerType: typeof(TextArea)
	);
	public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
		name: nameof(MaxLength),
		propertyType: typeof(int),
		ownerType: typeof(TextArea)
	);
	public static readonly DependencyProperty ElementHeightProperty = DependencyProperty.Register(
		name: nameof(ElementHeight),
		propertyType: typeof(double),
		ownerType: typeof(TextArea),
		typeMetadata: new PropertyMetadata(200.0)
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
		if(dependencyObject is TextArea textBox) 
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
	private void OnPreviewTextInput(object sender, TextCompositionEventArgs e) 
	{
		if(new Regex("\\\\").IsMatch(e.Text)) e.Handled = true;
		base.OnPreviewTextInput(e);
	}
}