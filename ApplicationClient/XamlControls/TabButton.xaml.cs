using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ApplicationClient.XamlControls;
public partial class TabButton : UserControl
{
	public TabButton() => InitializeComponent();

	public ICommand Command 
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public object CommandParameter
	{
		get => GetValue(CommandParameterProperty);
		set => SetValue(CommandParameterProperty, value);
	}

	public Geometry Icon 
	{
		get => (Geometry)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}

	public bool HasLabel 
	{
		get => (bool)GetValue(HasLabelProperty);
		set => SetValue(HasLabelProperty, value);
	}

	public string Label 
	{
		get => (string)GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	public static readonly DependencyProperty CommandProperty = DependencyProperty.Register
	(
		name: nameof(Command),
		propertyType: typeof(ICommand),
		ownerType: typeof(TabButton)
	);

	public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
		name: nameof(CommandParameter),
		propertyType: typeof(object),
		ownerType: typeof(TabButton)
	);

	public static readonly DependencyProperty IconProperty = DependencyProperty.Register
	(
		name: nameof(Icon),
		propertyType: typeof(Geometry),
		ownerType: typeof(TabButton)
	);

	public static readonly DependencyProperty HasLabelProperty = DependencyProperty.Register(
		name: nameof(HasLabel),
		propertyType: typeof(bool),
		ownerType: typeof(TabButton),
		typeMetadata: new PropertyMetadata(true)
	);

	public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
		name: nameof(Label),
		propertyType: typeof(string),
		ownerType: typeof(TabButton),
		typeMetadata: new PropertyMetadata(string.Empty)
	);
}