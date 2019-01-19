using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Amusoft.UI.WPF.Controls;

namespace Amusoft.UI.WPF.Utility
{
	public static class VisualStateManagerHelper
	{
		public static Dictionary<VisualStateGroup, IEnumerable<VisualState>> GetVisualStateMap(FrameworkElement element)
		{
			if (element == null)
				throw new ArgumentNullException(nameof(element));

			var groups = VisualStateManager.GetVisualStateGroups(element);
			return groups.Cast<VisualStateGroup>().ToDictionary(d => d, group => group.States.Count > 0 ? group.States.Cast<VisualState>() : Enumerable.Empty<VisualState>());
		}

		public static async Task GoToStateAsync(FrameworkElement control, string stateName, bool useTransition)
		{
			var tcs = new TaskCompletionSource<object>();
			var root = GetRoot(control);
			var group = GetVisualStateMap(root).FirstOrDefault(d => d.Value.Any(state => string.Equals(state.Name, stateName, StringComparison.OrdinalIgnoreCase)));
			EventHandler<VisualStateChangedEventArgs> handler = null;
			handler = (sender, args) =>
			{
				group.Key.CurrentStateChanged -= handler;
				tcs.TrySetResult(null);
			};
			if (group.Key != null)
			{
				group.Key.CurrentStateChanged += handler;
				VisualStateManager.GoToState(control, stateName, true);
				await tcs.Task;
			}
			else
			{
				VisualStateManager.GoToState(control, stateName, true);
			}
		}

		private static FrameworkElement GetRoot(FrameworkElement control)
		{
			var property = typeof(FrameworkElement).GetProperty("StateGroupsRoot", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			var root = property.GetGetMethod(true).Invoke(control, null);
			return root as FrameworkElement;
		}
	}
}