﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.Adorners
{
	public class AnchorAdornerManager
	{
		private readonly Visual _target;

		private readonly Dictionary<AnchorPosition, ContentPresenter> _presenters = new Dictionary<AnchorPosition, ContentPresenter>();

		private readonly Dictionary<AnchorPosition, AnchoredAdorner> _adorners = new Dictionary<AnchorPosition, AnchoredAdorner>();

		public ContentPresenter this[AnchorPosition position] => TryGetPresenter(position, out var p) ? p : null;

		public bool TryGetPresenter(AnchorPosition position, out ContentPresenter presenter)
		{
			if (_presenters.TryGetValue(position, out presenter))
				return true;

			var adornerTarget = GetAdornerTarget();
			var adornerLayer = AdornerLayer.GetAdornerLayer(adornerTarget);
			if(adornerLayer == null)
				throw new Exception($"Unable to get an AdornerLayer from {_target.GetType().FullName}.");

			_presenters.Add(position, new ContentPresenter());

			var adorner = new AnchoredAdorner(adornerTarget as UIElement, _presenters[position], position);
			adornerLayer.Add(adorner);
			_adorners.Add(position, adorner);
			presenter = _presenters[position];

			return true;
		}

		private Visual GetAdornerTarget()
		{
			if(_target is Window w)
				return w.Content as Visual;

			return _target;
		}

		public AnchorAdornerManager(Visual target)
		{
			_target = target;
		}
	}
}