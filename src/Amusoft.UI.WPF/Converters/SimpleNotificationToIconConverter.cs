using System;
using System.Globalization;
using System.Windows.Data;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Converters
{
	public class SimpleNotificationToIconConverter : IValueConverter
	{
		/// <inheritdoc />
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is SimpleNotification simpleNotification)
			{
				switch (simpleNotification.Type)
				{
					case SimpleNotificationType.None:
						return null;
					case SimpleNotificationType.Info:
						return "/Amusoft.UI.WPF;component/Resources/info.png";
					case SimpleNotificationType.Warning:
						return "/Amusoft.UI.WPF;component/Resources/warning.png";
					case SimpleNotificationType.Error:
						return "/Amusoft.UI.WPF;component/Resources/error.png";
					case SimpleNotificationType.Done:
						return "/Amusoft.UI.WPF;component/Resources/done.png";
					default:
						return "/Amusoft.UI.WPF;component/Resources/info.png";
				}
			}

			return "/Amusoft.UI.WPF;component/Resources/info.png";
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}