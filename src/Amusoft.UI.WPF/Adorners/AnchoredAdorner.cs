using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Adorners
{
	public class AnchoredAdorner : Adorner
	{
		public UIElement DisplayedElement { get; }

		public AnchorPosition Position { get; }

		/// <inheritdoc />
		public AnchoredAdorner(UIElement adornedElement, FrameworkElement displayedElement, AnchorPosition position) : base(adornedElement)
		{
			DisplayedElement = ComposeGrid(displayedElement, position);
			Position = position;
			AddVisualChild(DisplayedElement);
			AddLogicalChild(DisplayedElement);
		}

		private UIElement ComposeGrid(FrameworkElement displayedElement, AnchorPosition position)
		{
			var grid = new Grid();
			grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
			grid.Children.Add(displayedElement);
			switch (position)
			{
				case AnchorPosition.BottomRight:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
					displayedElement.VerticalAlignment = VerticalAlignment.Bottom;
					break;
				case AnchorPosition.Bottom:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
					displayedElement.VerticalAlignment = VerticalAlignment.Bottom;
					break;
				case AnchorPosition.BottomLeft:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
					displayedElement.VerticalAlignment = VerticalAlignment.Bottom;
					break;
				case AnchorPosition.Left:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
					displayedElement.VerticalAlignment = VerticalAlignment.Stretch;
					break;
				case AnchorPosition.TopLeft:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
					displayedElement.VerticalAlignment = VerticalAlignment.Top;
					break;
				case AnchorPosition.Top:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
					displayedElement.VerticalAlignment = VerticalAlignment.Top;
					break;
				case AnchorPosition.TopRight:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
					displayedElement.VerticalAlignment = VerticalAlignment.Top;
					break;
				case AnchorPosition.Right:
					displayedElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
					displayedElement.VerticalAlignment = VerticalAlignment.Stretch;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(position), position, null);
			}

			return grid;
		}

		/// <inheritdoc />
		protected override Visual GetVisualChild(int index)
		{
			return DisplayedElement;
		}

		/// <inheritdoc />
		protected override Size MeasureOverride(Size constraint)
		{
			DisplayedElement.Measure(constraint);
			var desired = DisplayedElement.DesiredSize;
			Debug.WriteLine($"{"Measure".PadLeft(10, ' ')}: width:{desired.Width} height:{desired.Height}");
			return desired;
		}

		/// <inheritdoc />
		protected override Size ArrangeOverride(Size measured)
		{
			var calculateSize = CalculateSize(measured);
			var calculatePosition = CalculatePosition(measured);
			Debug.WriteLine($"{"Arrange".PadLeft(10, ' ')} ({Position.ToString().PadLeft(8, ' ')}): position:{calculatePosition.X:0}x{calculatePosition.Y:0} size:{calculateSize.Width:0}x{calculateSize.Height:0}");
			DisplayedElement.Arrange(new Rect(calculatePosition, calculateSize));
			return measured;
		}

		private Size CalculateSize(Size measured)
		{
			switch (Position)
			{
				case AnchorPosition.TopLeft:
				case AnchorPosition.TopRight:
				case AnchorPosition.BottomRight:
				case AnchorPosition.BottomLeft:
					return new Size(measured.Width, measured.Height);

				case AnchorPosition.Bottom:
				case AnchorPosition.Top:
					return new Size(AdornedElement.RenderSize.Width, measured.Height);

				case AnchorPosition.Left:
				case AnchorPosition.Right:
					return new Size(measured.Width, AdornedElement.RenderSize.Height);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private Point CalculatePosition(Size measured)
		{
			switch (Position)
			{
				case AnchorPosition.BottomRight:
					return new Point(AdornedElement.RenderSize.Width - measured.Width, AdornedElement.RenderSize.Height - measured.Height);
				case AnchorPosition.Bottom:
					return new Point(0, AdornedElement.RenderSize.Height - measured.Height);
				case AnchorPosition.BottomLeft:
					return new Point(0, AdornedElement.RenderSize.Height - measured.Height);
				case AnchorPosition.Left:
					return new Point(0, 0);
				case AnchorPosition.TopLeft:
					return new Point(0, 0);
				case AnchorPosition.Top:
					return new Point(0, 0);
				case AnchorPosition.TopRight:
					return new Point(AdornedElement.RenderSize.Width - measured.Width, 0);
				case AnchorPosition.Right:
					return new Point(AdornedElement.RenderSize.Width - measured.Width, 0);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <inheritdoc />
		protected override int VisualChildrenCount => 1;
	}
}