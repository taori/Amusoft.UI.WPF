using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Amusoft.UI.WPF.Controls
{
	public class NotificationHost
	{
		public AnchorAdornerManager Manager { get; }

		public ConcurrentDictionary<AnchorPosition, ObservableCollection<INotification>> ItemsByPosition { get; }

		public NotificationHost(AnchorAdornerManager manager, NotificationSettings settings)
		{
			Manager = manager;
			ItemsByPosition = new ConcurrentDictionary<AnchorPosition, ObservableCollection<INotification>>();
		}

		public async void DisplayAsync(INotification notification, AnchorPosition position)
		{
			if(!Manager.TryGetPresenter(position, out var presenter))
				return;

			EnsureBuildNotificationDisplayExists(position, presenter);

			if (!ItemsByPosition.TryGetValue(position, out var collection))
			{
				throw new Exception("At this point the ObservableCollection should be present.");
			}

			notification.CloseRequested += (sender, args) => notification.CloseCommand?.Execute(notification);
//			notification.CloseRequested += (sender, args) => collection.Remove(notification);

			collection.Add(notification);

			notification.NotifyDisplayed();

			if (notification.AutoClose && notification.AutoCloseDelay > TimeSpan.Zero)
			{
				await Task.Delay(notification.AutoCloseDelay);

				if (!notification.Closed)
					notification.CloseCommand?.Execute(notification);
			}
		}

		private void EnsureBuildNotificationDisplayExists(AnchorPosition position, ContentPresenter presenter)
		{
			if (presenter.Content != null)
				return;

			var display = new NotificationDisplay();
			display.AnchorPosition = position;
			if (!ItemsByPosition.TryGetValue(position, out var c))
			{
				c = new ObservableCollection<INotification>();
				if (!ItemsByPosition.TryAdd(position, c))
					c = ItemsByPosition[position];

				c.CollectionChanged += (sender, args) => display.Visibility = c.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
				display.ItemsSource = c;
			}

			presenter.Content = display;
		}
	}
}