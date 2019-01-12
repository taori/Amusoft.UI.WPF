using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Converters
{
	public class SimpleNotificationToColorConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is SimpleNotification simpleNotification)
			{
				switch (simpleNotification.Type)
				{
					case SimpleNotificationType.None:
						return new SolidColorBrush(Colors.Transparent);
					case SimpleNotificationType.Info:
						return new SolidColorBrush(Colors.White);
					case SimpleNotificationType.Warning:
						return new SolidColorBrush(Colors.Yellow);
					case SimpleNotificationType.Error:
						return new SolidColorBrush(Colors.Red);
					case SimpleNotificationType.Done:
						return new SolidColorBrush(Colors.LawnGreen);
					default:
						return new SolidColorBrush(Colors.White); 
				}
			}

			return new SolidColorBrush(Colors.White);
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}