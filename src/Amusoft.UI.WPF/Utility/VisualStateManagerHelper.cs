using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Amusoft.UI.WPF.Utility
{
	public static class VisualStateManagerHelper
	{
		public static Dictionary<VisualStateGroup, IEnumerable<VisualState>> GetVisualStateMap([JetBrains.Annotations.NotNull] FrameworkElement element)
		{
			if (element == null)
				throw new ArgumentNullException(nameof(element));

			var groups = VisualStateManager.GetVisualStateGroups(element);
			return groups.Cast<VisualStateGroup>().ToDictionary(d => d, group => group.States.Count > 0 ? group.States.Cast<VisualState>() : Enumerable.Empty<VisualState>());
		}

		public static async Task GoToStateAsync(FrameworkElement control, string stateName, bool useTransition)
		{
			var tcs = new TaskCompletionSource<object>();
			var group = GetVisualStateMap(control).FirstOrDefault(d => d.Value.Any(state => string.Equals(state.Name, stateName, StringComparison.OrdinalIgnoreCase)));
			EventHandler<VisualStateChangedEventArgs> handler = null;
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
}