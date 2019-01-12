using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace Amusoft.UI.WPF.Adorners
{
	public class ScreenAnchorAdornerManager
	{
		private ScreenAnchorAdornerManager()
		{
		}

		public static readonly ScreenAnchorAdornerManager Instance = new ScreenAnchorAdornerManager();

		private static readonly Dictionary<Screen, Window> WindowByScreen = new Dictionary<Screen, Window>();

		private static readonly Dictionary<Screen, AnchorAdornerManager> ManagerByScreen = new Dictionary<Screen, AnchorAdornerManager>();

		public AnchorAdornerManager this[Screen screen]
		{
			get
			{
				EnsureOverlaysExist();
				return ManagerByScreen.TryGetValue(screen, out var val) ? val : null;
			}
		}

		private static void EnsureOverlaysExist()
		{
			if (WindowByScreen.Count > 0)
				return;

			Debug.WriteLine(nameof(EnsureOverlaysExist));
			foreach (var screen in Screen.AllScreens)
			{
				CreateAdornerWindow(screen);
			}
		}

		private static void CreateAdornerWindow(Screen screen)
		{
			async void UpdateSizes(Window w, bool delay)
			{
				if (delay)
					await Task.Delay(20);
				w.Width = screen.WorkingArea.Width;
				w.Height = screen.WorkingArea.Height;
				w.Left = screen.WorkingArea.Left;
				w.Top = screen.WorkingArea.Top;
			}

			var window = new Window() { Title = "OverlayAnchorWindow" };
			window.Background = new SolidColorBrush { Color = Colors.White, Opacity = 0 };
			window.Content = new Border() { Background = Brushes.Transparent }; ;
			window.LocationChanged += (sender, args) => UpdateSizes(window, true);
			window.SizeChanged += (sender, args) => UpdateSizes(window, true);
			window.WindowStyle = WindowStyle.None;
			window.AllowsTransparency = true;
			window.Topmost = true;
			window.ShowInTaskbar = false;

			UpdateSizes(window, false);
			window.Show();

			var manager = new AnchorAdornerManager(window.Content as Visual);

			WindowByScreen.Add(screen, window);
			ManagerByScreen.Add(screen, manager);
		}
	}
}
