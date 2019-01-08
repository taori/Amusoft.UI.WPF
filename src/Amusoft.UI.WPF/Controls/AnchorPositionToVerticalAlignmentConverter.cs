using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Amusoft.UI.WPF.Controls
{
	public class AnchorPositionToVerticalAlignmentConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is AnchorPosition position)
			{
				switch (position)
				{
					case AnchorPosition.BottomRight:
						return VerticalAlignment.Bottom;
					case AnchorPosition.Bottom:
						return VerticalAlignment.Bottom;
					case AnchorPosition.BottomLeft:
						return VerticalAlignment.Bottom;
					case AnchorPosition.Left:
						return VerticalAlignment.Center;
					case AnchorPosition.TopLeft:
						return VerticalAlignment.Top;
					case AnchorPosition.Top:
						return VerticalAlignment.Top;
					case AnchorPosition.TopRight:
						return VerticalAlignment.Top;
					case AnchorPosition.Right:
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