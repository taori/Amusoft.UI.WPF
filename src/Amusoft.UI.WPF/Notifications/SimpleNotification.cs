using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;

namespace Amusoft.UI.WPF.Notifications
{
	public class SimpleNotification : INotification
	{
		/// <inheritdoc />
		public SimpleNotification(string text, Action<SimpleNotification> selectedCallback = null, Action<SimpleNotification> closedCallback = null)
		{
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

		public Action<SimpleNotification> SelectedCallback { get; }
		public Action<SimpleNotification> ClosedCallback { get; }

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

		private ICommand _selectCommand;

		/// <inheritdoc />
		public ICommand SelectCommand
		{
			get => _selectCommand;
			set
			{
				if (Equals(value, _selectCommand))
					return;

				_selectCommand = value;
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

        private bool _closeOnSelect;

        /// <inheritdoc />
        public bool CloseOnSelect
        {
            get => _closeOnSelect;
            set
            {
                if (value == _closeOnSelect)
                    return;

                _closeOnSelect = value;
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