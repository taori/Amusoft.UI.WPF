﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace Amusoft.UI.WPF.Controls
{
	public class NotificationManager
	{
		private NotificationManager()
		{
		}

		public static NotificationHost GetHostByVisual(Visual target)
		{
			var manager = new AnchorAdornerManager(target);
			var host = new NotificationHost(manager);
			return host;
		}

		public static NotificationHost GetHostByScreen(Screen screen)
		{
			var manager = ScreenAnchorAdornerManager.Instance[screen];
			var host = new NotificationHost(manager);
			return host;
		}
	}

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
			if(Manager.TryGetPresenter(position, out var presenter))
			{
				if (presenter.Content == null)
				{
					var listView = new NotificationListView();
					listView.AnchorPosition = position;
					if (!ItemsByPosition.TryGetValue(position, out var c))
					{
						c = new ObservableCollection<INotification>();
						if (!ItemsByPosition.TryAdd(position, c))
							c = ItemsByPosition[position];

						listView.ItemsSource = c;
					}
					presenter.Content = listView;
				}

				if (!ItemsByPosition.TryGetValue(position, out var collection))
				{
					throw new Exception("At this point the ObservableCollection should be present.");
				}
				
				collection.Add(notification);
				Manager.Update();

				if (notification.AutoClose)
				{
					await Task.Delay(notification.AutoCloseDelay);
					collection.Remove(notification);
					Manager.Update();
				}
			}
		}
	}
}