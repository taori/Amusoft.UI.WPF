using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
	}
}
