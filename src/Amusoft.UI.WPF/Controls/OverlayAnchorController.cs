using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors.Layout;

namespace Amusoft.UI.WPF.Controls
{
	public static class OverlayAnchorController
	{
		private static readonly Dictionary<Screen, Window> WindowByScreen = new Dictionary<Screen, Window>();

		private static void EnsureOverlaysExist()
		{
			if (WindowByScreen.Count > 0)
				return;

			foreach (var screen in Screen.AllScreens)
			{
				WindowByScreen.Add(screen, CreateOverlayWindow(screen));
			}
		}

		private static Window CreateOverlayWindow(Screen screen)
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
			var window = new Window();
			window.Content = new TextBlock() { Text = "Hi" }; ;
			window.LocationChanged += (sender, args) => UpdateSizes(window, true);
			window.SizeChanged += (sender, args) => UpdateSizes(window, true);
			window.WindowStyle = WindowStyle.None;
			window.AllowsTransparency = true;
			window.IsHitTestVisible = false;
			window.Topmost = true;
			window.Opacity = 0;
			window.Background = new SolidColorBrush(Color.FromArgb(180, 255, 100, 100));

			UpdateSizes(window, false);

			window.Show();
			var adorner = AdornerLayer.GetAdornerLayer(window.Content as Visual);
			var container = new AdornerContainer(window);
			adorner.Add(container);
			return window;
		}

		public static void Display()
		{
			EnsureOverlaysExist();
		}
	}
}
