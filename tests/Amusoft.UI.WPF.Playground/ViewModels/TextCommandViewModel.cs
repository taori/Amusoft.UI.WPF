using System.Windows.Input;

namespace Amusoft.UI.WPF.Playground.ViewModels
{
	public class TextCommandViewModel : ViewModelBase
	{
		/// <inheritdoc />
		public TextCommandViewModel(ICommand command, string text)
		{
			_command = command;
			_text = text;
		}

		private ICommand _command;

		public ICommand Command
		{
			get => _command;
			set => SetValue(ref _command, value, nameof(Command));
		}

		private string _text;

		public string Text
		{
			get => _text;
			set => SetValue(ref _text, value, nameof(Text));
		}
	}
}