using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Controls;

namespace Amusoft.UI.WPF.Notifications
{
	public class NotificationHost
	{
		private AnchorAdornerManager Manager { get; }

		private NotificationSettings Settings { get; }

		private ConcurrentDictionary<AnchorPosition, ObservableCollection<object>> ItemsByPosition { get; }

		public NotificationHost(AnchorAdornerManager manager, NotificationSettings settings)
		{
			Manager = manager;
			Settings = settings;
			ItemsByPosition = new ConcurrentDictionary<AnchorPosition, ObservableCollection<object>>();
		}

		public async void DisplayAsync(INotification notification, AnchorPosition position)
		{
			if(!Manager.TryGetPresenter(position, out var presenter))
				throw new Exception($"There should be a {nameof(ContentPresenter)} present for {position}.");

			EnsureBuildNotificationDisplayExists(position, presenter);

			if (!ItemsByPosition.TryGetValue(position, out var collection))
			{
				throw new Exception("At this point the ObservableCollection should be present.");
			}
			
			var displayItem = new NotificationDisplayItem()
			{
				DataContext = notification,
				IsCloseButtonVisible = !notification.CloseOnSelect || (notification.AutoClose && notification.AutoCloseDelay > TimeSpan.Zero)
			};
			displayItem.Closed += (sender, args) => 
				collection.Remove(displayItem);

			collection.Add(displayItem);

			notification.NotifyDisplayed();

			if (notification.AutoClose && notification.AutoCloseDelay > TimeSpan.Zero)
			{
				await Task.Delay(notification.AutoCloseDelay);
				if (!notification.Closed)
				{
					notification.Closed = true;
					notification.RequestClose();
				}
			}
		}

		private void EnsureBuildNotificationDisplayExists(AnchorPosition position, ContentPresenter presenter)
		{
			if (presenter.Content != null)
				return;
			
			var display = new NotificationDisplay();
			if (Settings?.Style is Style displayStyle)
				display.Style = displayStyle;

			display.AnchorPosition = position;
			if (!ItemsByPosition.TryGetValue(position, out var c))
			{
				c = new ObservableCollection<object>();
				if (!ItemsByPosition.TryAdd(position, c))
					c = ItemsByPosition[position];

				c.CollectionChanged += (sender, args) => display.Visibility = c.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
				display.ItemsSource = c;
			}

			presenter.Content = display;
		}
	}
}