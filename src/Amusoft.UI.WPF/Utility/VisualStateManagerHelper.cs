using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;

namespace Amusoft.UI.WPF.Utility
{
	public static class VisualStateManagerHelper
	{
		private static readonly Dictionary<VisualStateGroup, IEnumerable<VisualState>> Empty = new();

		public static Dictionary<VisualStateGroup, IEnumerable<VisualState>> GetVisualStateMap(FrameworkElement element)
		{
			if (element == null)
				throw new ArgumentNullException(nameof(element));

			if (VisualStateManager.GetVisualStateGroups(element) is { } groups)
			{
				return groups.Cast<VisualStateGroup>().ToDictionary(
					d => d, 
					group => group.States.Count > 0 
						? group.States.Cast<VisualState>() 
						: Enumerable.Empty<VisualState>());
			}
			else
			{
				return Empty;
			}
		}

		public static async Task GoToStateAsync(FrameworkElement control, string stateName, bool useTransition)
		{
			if (GetRoot(control) is { } root)
			{
				var tcs = new TaskCompletionSource<object?>();
				var group = GetVisualStateMap(root).FirstOrDefault(d => d.Value.Any(state => string.Equals(state.Name, stateName, StringComparison.OrdinalIgnoreCase)));
				EventHandler<VisualStateChangedEventArgs>? handler = null;
				handler = (sender, args) =>
				{
					group.Key.CurrentStateChanged -= handler;
					tcs.TrySetResult(null);
				};
				if (group.Key != null)
				{
					group.Key.CurrentStateChanged += handler;
					VisualStateManager.GoToState(control, stateName, useTransition);
					await tcs.Task;
				}
				else
				{
					VisualStateManager.GoToState(control, stateName, useTransition);
				}
			}
		}

		private static FrameworkElement? GetRoot(FrameworkElement control)
		{
			if (typeof(FrameworkElement).GetProperty("StateGroupsRoot", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) is {} property)
			{
				var root = property.GetGetMethod(true)?.Invoke(control, null);
				return root as FrameworkElement;
			}

			return null;
		}
	}
}