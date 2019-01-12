using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Amusoft.UI.WPF.Controls
{
	public class TintedImage : ContentControl
	{
		static TintedImage()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TintedImage), new FrameworkPropertyMetadata(typeof(TintedImage)));
		}

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
			nameof(Brush), typeof(Brush), typeof(TintedImage), new PropertyMetadata(default(Brush)));

		public Brush Brush
		{
			get { return (Brush) GetValue(BrushProperty); }
			set { SetValue(BrushProperty, value); }
		}

		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
			nameof(Source), typeof(ImageSource), typeof(TintedImage), new PropertyMetadata(default(ImageSource)));

		public ImageSource Source
		{
			get { return (ImageSource) GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}
	}
}