using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Amusoft.UI.WPF.Controls;
using Application = System.Windows.Application;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace Amusoft.UI.WPF.Playground
{

	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.Loaded += OnLoaded;
		}

		private async void OnLoaded(object sender, RoutedEventArgs e)
		{
			var host = NotificationHostManager.GetHostByScreen(Screen.PrimaryScreen);
			for (int i = 0; i < 5; i++)
			{
				var notification = new SimpleNotification(DateTime.Now.ToString());
				notification.AutoClose = true;
				notification.AutoCloseDelay = TimeSpan.FromSeconds(6);
				host.Display(notification, AnchorPosition.Left);
				await Task.Delay(1000);
			}

//			var manager = new AnchorAdornerManager(Content as Visual);
//			manager[AnchorPosition.Right].Content = CreateSampleControl("Right");
//			manager[AnchorPosition.Left].Content = CreateSampleControl("Left", d=> d.VerticalAlignment = VerticalAlignment.Center);
//			manager[AnchorPosition.Top].Content = CreateSampleControl("Top");
//			manager[AnchorPosition.Bottom].Content = CreateSampleControl("Bottom");
//			manager[AnchorPosition.TopLeft].Content = CreateSampleControl("TopLeft");
//			manager[AnchorPosition.TopRight].Content = CreateSampleControl("TopRight");
//			manager[AnchorPosition.BottomLeft].Content = CreateSampleControl("BottomLeft");
//			manager[AnchorPosition.BottomRight].Content = CreateSampleControl("BottomRight");
//
//
//			var screenAnchorManager = Controls.ScreenAnchorAdornerManager.Instance[Screen.PrimaryScreen];
//			var displayedElement = new ItemsControl();
//			var dp = new DockPanel()
//			{
//				Background = Brushes.DarkOliveGreen,
//				Children =
//				{
//					displayedElement
//				}
//			};
////			displayedElement.HorizontalAlignment = HorizontalAlignment.Center;
//			displayedElement.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
//			displayedElement.Items.Add(new TextBlock() { Text = "hi adsdasd ", Background = Brushes.Yellow });
//			displayedElement.Items.Add(new TextBlock() { Text = "hi adsdasd2 ", Background = Brushes.Orange });
//			Task.Run(async () =>
//			{
//				for (int i = 0; i < 5; i++)
//				{
//					await Task.Delay(1000);
//
//					Application.Current.Dispatcher.Invoke(() =>
//					{
//						displayedElement.Items.Add(new TextBlock() { Text = DateTime.Now.ToString(), Background = Brushes.Yellow });
//						Debug.WriteLine($"items: {displayedElement.Items.Count}");
//						screenAnchorManager.Update();
//					});
//				}
//			});
//			displayedElement.Items.Add(new TextBlock() { Text = DateTime.Now.ToString(), Background = Brushes.Yellow });
//			displayedElement.Items.Add(new TextBlock() { Text = DateTime.Now.ToString(), Background = Brushes.Yellow });
//			displayedElement.Items.Add(new TextBlock() { Text = DateTime.Now.ToString(), Background = Brushes.Yellow });
//
//			screenAnchorManager[AnchorPosition.TopRight].Content = dp;
		}

//		private static DockPanel CreateSampleControl(string text, Action<DockPanel> mod = null)
//		{
//			var control = new DockPanel()
//			{
//				Background = Brushes.Purple,
//				Children =
//				{
//					new Border()
//					{
//						Width = 150, Background = Brushes.Orange, BorderThickness = new Thickness(2), BorderBrush = Brushes.Red,
//						Child = new TextBlock() {Text = $"This is the area for {text}.", Background = Brushes.Blue}
//					}
//				}
//			};
//			mod?.Invoke(control);
//			return control;
//		}
	}
}
