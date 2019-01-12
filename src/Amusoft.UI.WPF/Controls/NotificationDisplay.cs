using System.Windows;
using System.Windows.Controls;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Controls
{
	public class NotificationDisplay : ItemsControl
	{
		static NotificationDisplay()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationDisplay), new FrameworkPropertyMetadata(typeof(NotificationDisplay)));
		}

		public static readonly DependencyProperty AnchorPositionProperty = DependencyProperty.Register(
			nameof(AnchorPosition), typeof(AnchorPosition), typeof(NotificationDisplay), new PropertyMetadata(default(AnchorPosition)));

		public AnchorPosition AnchorPosition
		{
			get { return (AnchorPosition) GetValue(AnchorPositionProperty); }
			set { SetValue(AnchorPositionProperty, value); }
		}

		public static readonly DependencyProperty IconTemplateProperty = DependencyProperty.Register(
			nameof(IconTemplate), typeof(DataTemplate), typeof(NotificationDisplay), new PropertyMetadata(default(DataTemplate)));

		public DataTemplate IconTemplate
		{
			get { return (DataTemplate) GetValue(IconTemplateProperty); }
			set { SetValue(IconTemplateProperty, value); }
		}

		public static readonly DependencyProperty CloseTemplateProperty = DependencyProperty.Register(
			nameof(CloseTemplate), typeof(DataTemplate), typeof(NotificationDisplay), new PropertyMetadata(default(DataTemplate)));

		public DataTemplate CloseTemplate
		{
			get { return (DataTemplate) GetValue(CloseTemplateProperty); }
			set { SetValue(CloseTemplateProperty, value); }
		}
	}
}
