using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Amusoft.UI.WPF.Controls;
using Amusoft.UI.WPF.Notifications;
using Microsoft.Xaml.Behaviors.Core;
using MessageBox = System.Windows.MessageBox;

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
			var random = new Random();
			for (int i = 0; i < 5; i++)
			{
				var notification = new SimpleNotification(DateTime.Now.ToString());
				notification.AutoClose = true;
				if (random.Next(0, 2) == 1)
					notification.AutoCloseDelay = TimeSpan.FromSeconds(6);
				host.DisplayAsync(notification, AnchorPosition);
				await Task.Delay(1000);
			}
		}

		private async void DisplayGlobalNotifactionsExecute(object obj)
		{
			var host = NotificationHostManager.GetHostByScreen(Screen.PrimaryScreen);
			var random = new Random();
			for (int i = 0; i < 5; i++)
			{
				var notification = new SimpleNotification(DateTime.Now.ToString(), SelectedCallback, ClosedCallback);
                notification.CloseOnSelect = true;
				if (random.Next(0, 2) == 1)
					notification.AutoCloseDelay = TimeSpan.FromSeconds(6);
				host.DisplayAsync(notification, AnchorPosition);
				await Task.Delay(1000);
			}
		}

		private void ClosedCallback(SimpleNotification obj)
		{
            Debug.WriteLine($"Closed {obj.Text}.");
		}

		private void SelectedCallback(SimpleNotification obj)
        {
            Debug.WriteLine($"Selected {obj.Text}.");
		}
	}
}