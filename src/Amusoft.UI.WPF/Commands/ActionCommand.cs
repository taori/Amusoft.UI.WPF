using System;
using System.Windows.Input;

namespace Amusoft.UI.WPF.Commands
{
	internal class ActionCommand : ICommand
	{
		private readonly Action<object?> _execute;
		private readonly Predicate<object?>? _canExecute;

		public ActionCommand(Action execute, Predicate<object?>? canExecute = null) : this(p => execute(), canExecute)
		{
		}

		public ActionCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		/// <inheritdoc />
		public bool CanExecute(object? parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		/// <inheritdoc />
		public void Execute(object? parameter)
		{
			_execute.Invoke(parameter);
		}

		/// <inheritdoc />
		public event EventHandler? CanExecuteChanged
		{
			add
			{
				if (value == null)
					return;
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (value == null) 
					return;
				CommandManager.RequerySuggested -= value;
			}
		}
	}
}