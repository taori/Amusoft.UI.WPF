using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Amusoft.UI.WPF.Controls;
using Microsoft.Xaml.Behaviors.Core;

namespace Amusoft.UI.WPF.Playground.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public MainViewModel()
		{
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayGlobalNotifactionsExecute), "Display global notifications"));
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayWindowNotifactionsExecute), "Display window notifications"));
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayAlignmentTestWindowExecute), "Display aligntment test window"));
		}

		private Window _window;

		public Window Window
		{
			get => _window;
			set => SetValue(ref _window, value, nameof(Window));
		}

		private ObservableCollection<TextCommandViewModel> _commands = new ObservableCollection<TextCommandViewModel>();

		public ObservableCollection<TextCommandViewModel> Commands
		{
			get => _commands;
			set => SetValue(ref _commands, value, nameof(Commands));
		}

		private AnchorPosition _anchorPosition;

		public AnchorPosition AnchorPosition
		{
			get => _anchorPosition;
			set => SetValue(ref _anchorPosition, value, nameof(AnchorPosition));
		}

		private void DisplayAlignmentTestWindowExecute()
		{
			var testWindow = new AlignmentTestWindow();
			testWindow.Show();
		}

		private async void DisplayWindowNotifactionsExecute()
		{
			var host = NotificationHostManager.GetHostByVisual(Window.Content as Visual);
			for (int i = 0; i < 5; i++)
			{
				var notification = new SimpleNotification(DateTime.Now.ToString());
				notification.AutoClose = true;
				notification.AutoCloseDelay = TimeSpan.FromSeconds(6);
				host.Display(notification, AnchorPosition);
				await Task.Delay(1000);
			}
		}

		private async void DisplayGlobalNotifactionsExecute(object obj)
		{
			var host = NotificationHostManager.GetHostByScreen(Screen.PrimaryScreen);
			for (int i = 0; i < 5; i++)
			{
				var notification = new SimpleNotification(DateTime.Now.ToString());
				notification.AutoClose = true;
				notification.AutoCloseDelay = TimeSpan.FromSeconds(6);
				host.Display(notification, AnchorPosition);
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