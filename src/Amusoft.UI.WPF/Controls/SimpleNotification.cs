using System;
using System.Windows.Input;

namespace Amusoft.UI.WPF.Controls
{
	public class SimpleNotification : INotification
	{
		/// <inheritdoc />
		public SimpleNotification(string text)
		{
			Text = text;
		}

		public string Text { get; set; }

		/// <inheritdoc />
		public ICommand CloseCommand { get; }

		/// <inheritdoc />
		public event EventHandler CloseRequested;

		/// <inheritdoc />
		public event EventHandler Displayed;

		/// <inheritdoc />
		public bool AutoClose { get; set; }

		/// <inheritdoc />
		public TimeSpan AutoCloseDelay { get; set; }
	}
}