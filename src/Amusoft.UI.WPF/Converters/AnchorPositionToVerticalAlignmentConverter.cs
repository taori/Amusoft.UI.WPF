using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Controls;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Converters
{
	public class AnchorPositionToVerticalAlignmentConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Position position)
			{
				switch (position)
				{
					case Position.BottomRight:
						return VerticalAlignment.Bottom;
					case Position.Bottom:
						return VerticalAlignment.Bottom;
					case Position.BottomLeft:
						return VerticalAlignment.Bottom;
					case Position.Left:
						return VerticalAlignment.Center;
					case Position.TopLeft:
						return VerticalAlignment.Top;
					case Position.Top:
						return VerticalAlignment.Top;
					case Position.TopRight:
						return VerticalAlignment.Top;
					case Position.Right:
						return VerticalAlignment.Center;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}