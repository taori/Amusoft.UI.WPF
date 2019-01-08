using System;
using System.Windows.Input;

namespace Amusoft.UI.WPF.Controls
{
	public interface INotification
	{
		ICommand CloseCommand { get; }

		event EventHandler CloseRequested;

		event EventHandler Displayed;

		bool AutoClose { get; }

		TimeSpan AutoCloseDelay { get; }
	}
}