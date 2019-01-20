using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Controls
{
	[StyleTypedProperty(Property = nameof(NotificationDisplay.ItemContainerStyle), StyleTargetType = typeof(NotificationDisplayItem))]
	public class NotificationDisplay : ItemsControl
	{
		static NotificationDisplay()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationDisplay), new FrameworkPropertyMetadata(typeof(NotificationDisplay)));
		}

		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
			nameof(Position), typeof(Position), typeof(NotificationDisplay), new PropertyMetadata(default(Position)));

		public Position Position
		{
			get { return (Position) GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
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

		/// <inheritdoc />
		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);
			if (element is NotificationDisplayItem listItem)
				listItem.Style = ItemContainerStyle;
		}

		/// <inheritdoc />
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is NotificationDisplayItem;
		}

		/// <inheritdoc />
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NotificationDisplayItem();
		}
	}
}
