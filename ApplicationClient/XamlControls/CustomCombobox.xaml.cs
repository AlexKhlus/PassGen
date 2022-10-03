using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ApplicationClient.XamlControls;
public partial class CustomCombobox : UserControl 
{
	public CustomCombobox() => InitializeComponent();

	#region Public Properties

	public IEnumerable ItemsSource 
	{
		get => (IEnumerable)GetValue(ItemsSourceProperty);
		set => SetValue(ItemsSourceProperty, value);
	}
	public object SelectedItem 
	{
		get => (object)GetValue(SelectedItemProperty);
		set => SetValue(SelectedItemProperty, value);
	}
	public bool Enable 
	{
		get => (bool)GetValue(EnableProperty);
		set => SetValue(EnableProperty, value);
	}

	#endregion
	#region Dependency Properties

	public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
		name: nameof(ItemsSource),
		propertyType: typeof(IEnumerable),
		ownerType: typeof(CustomCombobox),
		typeMetadata: new PropertyMetadata(OnItemsSourcePropertyChanged)
	);
	public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
		name: nameof(SelectedItem),
		propertyType: typeof(object),
		ownerType: typeof(CustomCombobox),
		typeMetadata: new PropertyMetadata(OnSelectedItemPropertyChanged)
	);
	public static readonly DependencyProperty EnableProperty = DependencyProperty.Register(
		name: nameof(Enable),
		propertyType: typeof(bool),
		ownerType: typeof(CustomCombobox)
	);

	#endregion
	#region Private Methods

	private static void OnSelectedItemPropertyChanged(
		DependencyObject dependencyObject, 
		DependencyPropertyChangedEventArgs e
	) 
	{
		if(dependencyObject is CustomCombobox customCombobox) 
			customCombobox.UpdateSelectedItem();
	}
	private static void OnItemsSourcePropertyChanged(
		DependencyObject dependencyObject, 
		DependencyPropertyChangedEventArgs e
	) 
	{
		if(dependencyObject is CustomCombobox customCombobox) 
			customCombobox.UpdateItemsSource();
	}
	private void UpdateSelectedItem() 
	{
		Dropdown.SelectedItem = SelectedItem;
	}
	private void UpdateItemsSource() 
	{
		Dropdown.ItemsSource = ItemsSource;
	}
		
	private void OnComboBoxClick(
		object sender, 
		MouseButtonEventArgs e
	)  
	{
		if(Dropdown.Focusable) 
		{
			Dropdown.IsDropDownOpen = true;
		}
	}

	#endregion
}