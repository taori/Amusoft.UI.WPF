using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Amusoft.UI.WPF.Notifications;
using Amusoft.UI.WPF.Utility;

namespace Amusoft.UI.WPF.Controls
{

	[TemplatePart(Name = "PART_SelectControl", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "PART_Close", Type = typeof(UIElement))]
	[TemplatePart(Name = "PART_Content", Type = typeof(UIElement))]
	[TemplatePart(Name = "PART_Icon", Type = typeof(UIElement))]
	[TemplateVisualState(GroupName = "CommonStates", Name = "Pressed")]
	[TemplateVisualState(GroupName = "CommonStates", Name = "Normal")]
	[TemplateVisualState(GroupName = "CommonStates", Name = "MouseOver")]
	[TemplateVisualState(GroupName = "TerminalStates", Name = "Closing")]
	public class NotificationDisplayItem : ContentControl
	{
		static NotificationDisplayItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationDisplayItem), new FrameworkPropertyMetadata(typeof(NotificationDisplayItem)));
		}

		/// <inheritdoc />
		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			if (e.Property.Name == nameof(DataContext))
			{
				OnDataContextChanged(e.OldValue, e.NewValue);
			}
		}

		private void OnDataContextChanged(object oldValue, object newValue)
		{
			if (oldValue is INotification oldNotification)
			{
				oldNotification.CloseRequested -= NewNotificationOnCloseRequested;
            }
			if (newValue is INotification newNotification)
			{
				newNotification.CloseRequested += NewNotificationOnCloseRequested;
			}
		}

		private async void NewNotificationOnCloseRequested(object sender, EventArgs e)
		{
			await CloseExecute(true);
		}

		public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
			nameof(ClosedEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationDisplayItem));

		public event RoutedEventHandler Closed
		{
			add { AddHandler(ClosedEvent, (Delegate) value, false); }
			remove { RemoveHandler(ClosedEvent, (Delegate) value); }
		}

		public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(
			nameof(ClosingEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationDisplayItem));

		public event RoutedEventHandler Closing
		{
			add { AddHandler(ClosingEvent, (Delegate) value, false); }
			remove { RemoveHandler(ClosingEvent, (Delegate) value); }
		}

		public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent(
			nameof(SelectedEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationDisplayItem));

		public event RoutedEventHandler Selected
		{
			add { AddHandler(SelectedEvent, (Delegate) value, false); }
			remove { RemoveHandler(SelectedEvent, (Delegate) value); }
		}

		public static readonly DependencyProperty CloseTemplateProperty = DependencyProperty.Register(
			nameof(CloseTemplate), typeof(DataTemplate), typeof(NotificationDisplayItem), new PropertyMetadata(default(DataTemplate)));

		public DataTemplate CloseTemplate
		{
			get { return (DataTemplate) GetValue(CloseTemplateProperty); }
			set { SetValue(CloseTemplateProperty, value); }
		}

		public static readonly DependencyProperty NotificationIconTemplateProperty = DependencyProperty.Register(
			nameof(NotificationIconTemplate), typeof(DataTemplate), typeof(NotificationDisplayItem), new PropertyMetadata(default(DataTemplate)));

		public DataTemplate NotificationIconTemplate
		{
			get { return (DataTemplate) GetValue(NotificationIconTemplateProperty); }
			set { SetValue(NotificationIconTemplateProperty, value); }
		}

		public static readonly DependencyProperty IsCloseButtonVisibleProperty = DependencyProperty.Register(
			nameof(IsCloseButtonVisible), typeof(bool), typeof(NotificationDisplayItem), new PropertyMetadata(default(bool)));

		public bool IsCloseButtonVisible
		{
			get { return (bool) GetValue(IsCloseButtonVisibleProperty); }
			set { SetValue(IsCloseButtonVisibleProperty, value); }
		}

		public FrameworkElement SelectControl { get; set; }

		/// <inheritdoc />
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (GetTemplateChild("PART_SelectControl") is FrameworkElement selectControl)
			{
				SelectControl = selectControl;
				selectControl.MouseLeftButtonUp += SelectControlMouseUp;
				selectControl.MouseLeftButtonDown += SelectControlMouseDown;
				selectControl.MouseEnter += SelectControlOnMouseEnter;
				selectControl.MouseLeave += SelectControlOnMouseLeave;
			}
			else
			{
				throw new Exception($"Template part \"PART_SelectControl\" missing.");
			}

			if (GetTemplateChild("PART_Close") is FrameworkElement closeControl)
			{
				closeControl.MouseLeftButtonUp += CloseExecute;
			}
			else
			{
				throw new Exception($"Template part \"PART_Close\" missing.");
			}
		}

		private void SelectControlOnMouseLeave(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "Normal", true);
        }

		private void SelectControlOnMouseEnter(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "MouseOver", true);
        }

		private async void CloseExecute(object sender, MouseButtonEventArgs e)
		{
			await CloseExecute(false);
		}

		private async Task CloseExecute(bool raiseEvent)
		{
			await HandleCloseInternal(raiseEvent);
		}

		private async Task HandleCloseInternal(bool raiseEvent)
		{
			if (raiseEvent)
				RaiseEvent(new RoutedEventArgs(ClosingEvent));

			if (DataContext is INotification notification)
			{
				notification.CloseCommand?.Execute(notification);
			}

			await VisualStateManagerHelper.GoToStateAsync(this, "Closing", true);

			RaiseEvent(new RoutedEventArgs(ClosedEvent));
		}

		private void SelectControlMouseDown(object sender, MouseButtonEventArgs e)
		{
			VisualStateManager.GoToState(this, "Pressed", true);
		}

		private async void SelectControlMouseUp(object sender, MouseButtonEventArgs e)
		{
			await HandleSelectInternal();
		}

		private async Task HandleSelectInternal()
		{
			RaiseEvent(new RoutedEventArgs(SelectedEvent));

			if (DataContext is INotification notification)
			{
				notification.SelectCommand?.Execute(notification);
				if (notification.CloseOnSelect)
				{
					await HandleCloseInternal(true);
				}
			}

			VisualStateManager.GoToState(this, "MouseOver", true);
		}
	}
}