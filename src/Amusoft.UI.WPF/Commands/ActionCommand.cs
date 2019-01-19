using System;
using System.Windows.Input;
using JetBrains.Annotations;

namespace Amusoft.UI.WPF.Commands
{
	internal class ActionCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		public ActionCommand(Action execute, Predicate<object> canExecute = null) : this(p => execute(), canExecute)
		{
		}

		public ActionCommand([NotNull] Action<object> execute, Predicate<object> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		/// <inheritdoc />
		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		/// <inheritdoc />
		public void Execute(object parameter)
		{
			_execute.Invoke(parameter);
		}

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}