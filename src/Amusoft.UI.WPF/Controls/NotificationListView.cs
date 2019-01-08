using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Amusoft.UI.WPF.Controls
{
	public class NotificationListView : ItemsControl
	{
		static NotificationListView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationListView), new FrameworkPropertyMetadata(typeof(NotificationListView)));
		}

		public static readonly DependencyProperty AnchorPositionProperty = DependencyProperty.Register(
			nameof(AnchorPosition), typeof(AnchorPosition), typeof(NotificationListView), new PropertyMetadata(default(AnchorPosition)));

		public AnchorPosition AnchorPosition
		{
			get { return (AnchorPosition) GetValue(AnchorPositionProperty); }
			set { SetValue(AnchorPositionProperty, value); }
		}
	}
}
