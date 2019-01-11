using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Amusoft.UI.WPF.Controls
{
	public class AnchorAdornerManager
	{
		public Visual Target { get; }

		private readonly Dictionary<AnchorPosition, ContentPresenter> _presenters = new Dictionary<AnchorPosition, ContentPresenter>();
		private readonly Dictionary<AnchorPosition, AnchoredAdorner> _adorners = new Dictionary<AnchorPosition, AnchoredAdorner>();

		public ContentPresenter this[AnchorPosition position] => TryGetPresenter(position, out var p) ? p : null;

		public bool TryGetPresenter(AnchorPosition position, out ContentPresenter presenter)
		{
			if (_presenters.TryGetValue(position, out presenter))
				return true;

			var layer = AdornerLayer.GetAdornerLayer(Target);

			_presenters.Add(position, new ContentPresenter());
			var adorner = new AnchoredAdorner(Target as UIElement, _presenters[position], position);
			layer.Add(adorner);
			_adorners.Add(position, adorner);
			presenter = _presenters[position];

			return true;
		}

		public AnchorAdornerManager(Visual target)
		{
			Target = target;
		}

		public void Update()
		{
			Debug.WriteLine($"Update called.");
			foreach (var adorner in _adorners)
			{
				adorner.Value.InvalidateMeasure();
				adorner.Value.InvalidateArrange();
				adorner.Value.InvalidateVisual();
//				adorner.Value.InvalidateProperty(FrameworkElement.WidthProperty);
//				adorner.Value.UpdateLayout();
			}
		}
	}
}