using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Amusoft.UI.WPF.Controls;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Converters
{
	public class AnchorPositionToHorizontalAlignmentConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is AnchorPosition position)
			{
				switch (position)
				{
					case AnchorPosition.BottomRight:
						return HorizontalAlignment.Right;
					case AnchorPosition.Bottom:
						return HorizontalAlignment.Center;
					case AnchorPosition.BottomLeft:
						return HorizontalAlignment.Left;
					case AnchorPosition.Left:
						return HorizontalAlignment.Left;
					case AnchorPosition.TopLeft:
						return HorizontalAlignment.Left;
					case AnchorPosition.Top:
						return HorizontalAlignment.Center;
					case AnchorPosition.TopRight:
						return HorizontalAlignment.Right;
					case AnchorPosition.Right:
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