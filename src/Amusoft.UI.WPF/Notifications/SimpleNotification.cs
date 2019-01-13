using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;

namespace Amusoft.UI.WPF.Notifications
{
	public enum SimpleNotificationType
	{
		None,
		Info,
		Warning,
		Error,
		Done,
	}

	public class SimpleNotification : INotification
	{
		/// <inheritdoc />
		public SimpleNotification(string text, 
			SimpleNotificationType type = SimpleNotificationType.Done, 
			Action<SimpleNotification> selectedCallback = null, 
			Action<SimpleNotification> closedCallback = null)
		{
			Type = type;
			Text = text;
			SelectedCallback = selectedCallback;
			ClosedCallback = closedCallback;
			CloseCommand = new ActionCommand(CloseExecute);
			SelectCommand = new ActionCommand(SelectExecute);
		}

		private void SelectExecute(object obj)
		{
			SelectedCallback?.Invoke(this);

			if (CloseOnSelect)
				CloseExecute();
		}

		private void CloseExecute()
		{
			if (Closed)
				return;

			Closed = true;
			ClosedCallback?.Invoke(this);
			CloseRequested?.Invoke(this, EventArgs.Empty);
		}

		public string Text { get; set; }

		public SimpleNotificationType Type { get; set; }

		public Action<SimpleNotification> SelectedCallback { get; }

		public Action<SimpleNotification> ClosedCallback { get; }

		/// <inheritdoc />
		public ICommand CloseCommand { get; set; }

		/// <inheritdoc />
		public ICommand SelectCommand { get; set; }

		/// <inheritdoc />
		public event EventHandler CloseRequested;

		/// <inheritdoc />
		public event EventHandler Displayed;

		/// <inheritdoc />
		public bool AutoClose { get; set; }

		/// <inheritdoc />
		public bool Closed { get; set; }

		/// <inheritdoc />
		public bool CloseOnSelect { get; set; }

		/// <inheritdoc />
		public TimeSpan AutoCloseDelay { get; set; }

		/// <inheritdoc />
		public void NotifyDisplayed()
		{
			this.Displayed?.Invoke(this, EventArgs.Empty);
		}

		/// <inheritdoc />
		public void RequestClose()
		{
			this.CloseRequested?.Invoke(this, EventArgs.Empty);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}