using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ApplicationClient.Views;
public partial class NavigationSideBarView : UserControl
{
	public NavigationSideBarView() => InitializeComponent();

	private void OnMouseUp(object sender, MouseButtonEventArgs e)
	{
		foreach(var item in ListView.Items) (item as ListViewItem)!.IsSelected = false;

		if     (sender.Equals(Generator)) (ListView.Items[0] as ListViewItem)!.IsSelected = true;
		else if(sender.Equals(Storage))   (ListView.Items[1] as ListViewItem)!.IsSelected = true;
	}

	private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		foreach(var item in ListView.Items) ((ListViewItem)item).IsSelected = false;

		if(Visibility is Visibility.Visible) ((ListViewItem)ListView.Items[0]).IsSelected = true;
	}
}