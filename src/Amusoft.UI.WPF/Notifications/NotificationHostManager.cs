using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Amusoft.UI.WPF.Adorners;

namespace Amusoft.UI.WPF.Notifications
{
	public class NotificationSettings
	{
		public Style? Style { get; private set; }

		public NotificationSettings WithStyle(Style value)
		{
			Style = value;
			return this;
		}
	}

	public class NotificationHostManager
	{
		internal static readonly DependencyProperty NotificationHostProperty = DependencyProperty.RegisterAttached(
			"NotificationHost", typeof(NotificationHost), typeof(NotificationHostManager), new PropertyMetadata(default(NotificationHost)));

		internal static void SetNotificationHost(Visual element, NotificationHost value)
		{
			element.SetValue(NotificationHostProperty, value);
		}

		internal static NotificationHost? GetNotificationHost(Visual element)
		{
			return (NotificationHost) element.GetValue(NotificationHostProperty);
		}

		private NotificationHostManager()
		{
		}

		private static readonly object HostByVisualLock = new();
		public static NotificationHost GetHostByVisual(Visual target, NotificationSettings? settings = null)
		{
			lock (HostByVisualLock)
			{
				var oldHost = GetNotificationHost(target);
				if (oldHost != null)
					return oldHost;

				var newHost = CreateHostByVisual(target, settings);
				SetNotificationHost(target, newHost);
				return newHost;
			}
		}

		private static NotificationHost CreateHostByVisual(Visual target, NotificationSettings? settings = null)
		{
			var manager = new AnchorAdornerManager(target);
			var host = new NotificationHost(manager, settings);
			return host;
		}

		private static readonly ConcurrentDictionary<Screen, NotificationHost> HostsByScreen = new();

		public static NotificationHost GetHostByScreen(Screen screen, NotificationSettings? settings = null)
		{
			return HostsByScreen.GetOrAdd(screen, s =>
			{
				var manager = ScreenAnchorAdornerManager.Instance[s];
				var host = new NotificationHost(manager, settings);
				return host;
			});
		}
	}
}