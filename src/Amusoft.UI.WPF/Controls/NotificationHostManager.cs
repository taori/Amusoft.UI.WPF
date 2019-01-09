using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace Amusoft.UI.WPF.Controls
{
	public class NotificationHostManager
	{
		private NotificationHostManager()
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
}