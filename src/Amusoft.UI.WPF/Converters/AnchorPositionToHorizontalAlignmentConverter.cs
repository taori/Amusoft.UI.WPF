using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Controls;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Converters
{
	public class AnchorPositionToHorizontalAlignmentConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Position position)
			{
				switch (position)
				{
					case Position.BottomRight:
						return HorizontalAlignment.Right;
					case Position.Bottom:
						return HorizontalAlignment.Center;
					case Position.BottomLeft:
						return HorizontalAlignment.Left;
					case Position.Left:
						return HorizontalAlignment.Left;
					case Position.TopLeft:
						return HorizontalAlignment.Left;
					case Position.Top:
						return HorizontalAlignment.Center;
					case Position.TopRight:
						return HorizontalAlignment.Right;
					case Position.Right:
						return HorizontalAlignment.Right;
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