using System.Collections;
using System.Windows;
using System.Windows.Controls;


namespace ApplicationClient.XamlControls;
internal partial class CustomComboboxWithLabel : UserControl
{
	public CustomComboboxWithLabel()
		=> InitializeComponent();

	public string Label 
	{
		get => (string)GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	public bool IsBusy 
	{
		get => (bool)GetValue(IsBusyProperty);
		set => SetValue(IsBusyProperty, value);
	}

	public object SelectedItem 
	{
		get => (object)GetValue(SelectedItemProperty);
		set => SetValue(SelectedItemProperty, value);
	}

	public IEnumerable ItemSource 
	{
		get => (IEnumerable)GetValue(ItemSourceProperty);
		set => SetValue(ItemSourceProperty, value);
	}

	public static readonly DependencyProperty LabelProperty = DependencyProperty.Register
	(
		name: nameof(Label),
		propertyType: typeof(string),
		ownerType: typeof(CustomComboboxWithLabel)
	);

	public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register
	(
		name: nameof(IsBusy),
		propertyType: typeof(bool),
		ownerType: typeof(CustomComboboxWithLabel),
		typeMetadata: new PropertyMetadata(false)
	);

	public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register
	(
		name: nameof(SelectedItem),
		propertyType: typeof(object),
		ownerType: typeof(CustomComboboxWithLabel)
	);

	public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register
	(
		name: nameof(ItemSource),
		propertyType: typeof(IEnumerable),
		ownerType: typeof(CustomComboboxWithLabel)
	);
}