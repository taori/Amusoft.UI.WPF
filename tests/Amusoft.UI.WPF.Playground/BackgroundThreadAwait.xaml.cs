using System;
using System.Collections.Generic;
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
using Amusoft.UI.WPF.Utility;

namespace Amusoft.UI.WPF.Playground
{
	/// <summary>
	/// Interaction logic for BackgroundThreadAwait.xaml
	/// </summary>
	public partial class BackgroundThreadAwait : Window
	{
		public BackgroundThreadAwait()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			Task.Run(async () =>
			{
				await ThreadHelper.UI;
				return ctrlTextBlock.Text = "Update without exception successful.";
			});
		}
	}
}
