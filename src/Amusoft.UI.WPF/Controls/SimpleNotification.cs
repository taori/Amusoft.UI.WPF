using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Amusoft.UI.WPF.Annotations;
using Microsoft.Xaml.Behaviors.Core;

namespace Amusoft.UI.WPF.Controls
{
	public class SimpleNotification : INotification
	{
		/// <inheritdoc />
		public SimpleNotification(string text)
		{
			Text = text;
			CloseCommand = new ActionCommand(CloseExecute);
		}

		private void CloseExecute()
		{
			if (Closed)
				return;

			Closed = true;
			CloseRequested?.Invoke(this, EventArgs.Empty);
		}

		private string _text;

		public string Text
		{
			get => _text;
			set
			{
				if (value == _text)
					return;

				_text = value;
				OnPropertyChanged();
			}
		}

		private ICommand _closeCommand;

		/// <inheritdoc />
		public ICommand CloseCommand
		{
			get => _closeCommand;
			set
			{
				if (Equals(value, _closeCommand))
					return;

				_closeCommand = value;
				OnPropertyChanged();
			}
		}

		/// <inheritdoc />
		public event EventHandler CloseRequested;

		/// <inheritdoc />
		public event EventHandler Displayed;

		private bool _autoClose;

		/// <inheritdoc />
		public bool AutoClose
		{
			get => _autoClose;
			set
			{
				if (value == _autoClose)
					return;

				_autoClose = value;
				OnPropertyChanged();
			}
		}

		private bool _closed;

		/// <inheritdoc />
		public bool Closed
		{
			get => _closed;
			set
			{
				if (value == _closed)
					return;

				_closed = value;
				OnPropertyChanged();
			}
		}

		private TimeSpan _autoCloseDelay;

		/// <inheritdoc />
		public TimeSpan AutoCloseDelay
		{
			get => _autoCloseDelay;
			set
			{
				if (value.Equals(_autoCloseDelay))
					return;

				_autoCloseDelay = value;
				OnPropertyChanged();
			}
		}

		/// <inheritdoc />
		public void NotifyDisplayed()
		{
			this.Displayed?.Invoke(this, EventArgs.Empty);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}