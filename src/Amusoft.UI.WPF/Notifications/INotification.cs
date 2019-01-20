

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Amusoft.UI.WPF.Notifications
{
	public interface INotification : INotifyPropertyChanged
	{
		ICommand CloseCommand { get; }

		ICommand SelectCommand { get; }

		event EventHandler CloseRequested;

		event EventHandler Displayed;

		bool AutoClose { get; }

		bool Closed { get; set; }

		bool CloseOnSelect { get; }

		TimeSpan AutoCloseDelay { get; }

		void NotifyDisplayed();

		void RequestClose();
	}
}