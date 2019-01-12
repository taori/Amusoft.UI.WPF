using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace Amusoft.UI.WPF.TriggerActions
{
	public class GoToVisualStateAction : TriggerAction<FrameworkElement>
	{
		public static readonly DependencyProperty StateNameProperty = DependencyProperty.Register(
			nameof(StateName), typeof(string), typeof(GoToVisualStateAction), new PropertyMetadata(default(string)));

		public string StateName
		{
			get { return (string) GetValue(StateNameProperty); }
			set { SetValue(StateNameProperty, value); }
		}

		public static readonly DependencyProperty UseTransitionsProperty = DependencyProperty.Register(
			nameof(UseTransitions), typeof(bool), typeof(GoToVisualStateAction), new PropertyMetadata(default(bool)));

		public bool UseTransitions
		{
			get { return (bool) GetValue(UseTransitionsProperty); }
			set { SetValue(UseTransitionsProperty, value); }
		}


		public static readonly DependencyProperty ControlProperty = DependencyProperty.Register(
			nameof(Control), typeof(FrameworkElement), typeof(GoToVisualStateAction), new PropertyMetadata(default(FrameworkElement)));

		public FrameworkElement Control
		{
			get { return (FrameworkElement) GetValue(ControlProperty); }
			set { SetValue(ControlProperty, value); }
		}

		/// <inheritdoc />
		protected override void Invoke(object parameter)
		{
			VisualStateManager.GoToState(Control, StateName, UseTransitions);
		}
	}
}