﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Notifications;
using Microsoft.Xaml.Behaviors.Core;

namespace Amusoft.UI.WPF.Playground.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public MainViewModel()
		{
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayAwaitUiThread), "await UI.Thread"));
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayGlobalNotifactionsExecute), "Display global notifications"));
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayWindowNotifactionsExecute), "Display window notifications"));
			Commands.Add(new TextCommandViewModel(new ActionCommand(DisplayAlignmentTestWindowExecute), "Display alignment test window"));
			Commands.Add(new TextCommandViewModel(new ActionCommand(VerifyAlternatingStyleExecute), "Verify alternating style."));
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

		private Position _anchorPosition;

		public Position AnchorPosition
		{
			get => _anchorPosition;
			set => SetValue(ref _anchorPosition, value, nameof(AnchorPosition));
		}

		private void DisplayAwaitUiThread()
		{
			var testWindow = new BackgroundThreadAwait();
			testWindow.Show();
		}

		private void DisplayAlignmentTestWindowExecute()
		{
			var testWindow = new AlignmentTestWindow();
			testWindow.Show();
		}

		private void VerifyAlternatingStyleExecute()
		{
			var window = new AlternateStyleWindow();
			window.Show();
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
				var notification = new SimpleNotification(DateTime.Now.ToString()+"\r\ntest", selectedCallback : SelectedCallback, closedCallback : ClosedCallback);
                notification.CloseOnSelect = random.Next(0, 2) == 1;
                notification.Type = (SimpleNotificationType) random.Next(0, 6);
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