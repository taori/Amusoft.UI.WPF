using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Amusoft.UI.WPF.Controls
{
	public class NotificationHost
	{
		public AnchorAdornerManager Manager { get; }

		public ConcurrentDictionary<AnchorPosition, ObservableCollection<INotification>> ItemsByPosition { get; }

		public NotificationHost(AnchorAdornerManager manager)
		{
			Manager = manager;
			ItemsByPosition = new ConcurrentDictionary<AnchorPosition, ObservableCollection<INotification>>();
		}

		public async void Display(INotification notification, AnchorPosition position)
		{
			if(!Manager.TryGetPresenter(position, out var presenter))
				return;

			EnsureBuildNotificationDisplayExists(position, presenter);

			if (!ItemsByPosition.TryGetValue(position, out var collection))
			{
				throw new Exception("At this point the ObservableCollection should be present.");
			}

			collection.Add(notification);
			Manager.Update();
			notification.IsVisible = true;

			if (notification.AutoClose)
			{
				await Task.Delay(notification.AutoCloseDelay);
				notification.IsVisible = false;
				//					collection.Remove(notification);
				//					Manager.Update();
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

				display.ItemsSource = c;
			}

			presenter.Content = display;
		}
	}
}