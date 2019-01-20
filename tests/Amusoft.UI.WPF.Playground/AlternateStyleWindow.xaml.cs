using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Playground
{
	/// <summary>
	/// Interaktionslogik für AlternateStyleWindow.xaml
	/// </summary>
	public partial class AlternateStyleWindow : Window
	{
		public AlternateStyleWindow()
		{
			InitializeComponent();
			Items = new ObservableCollection<SimpleNotification>();
			for (int i = 0; i < 20; i++)
			{
				Items.Add(new SimpleNotification("Some text"));
			}
		}


		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
			nameof(Items), typeof(ObservableCollection<SimpleNotification>), typeof(AlternateStyleWindow), new PropertyMetadata(default(ObservableCollection<SimpleNotification>)));

		public ObservableCollection<SimpleNotification> Items
		{
			get { return (ObservableCollection<SimpleNotification>) GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}
	}
}
