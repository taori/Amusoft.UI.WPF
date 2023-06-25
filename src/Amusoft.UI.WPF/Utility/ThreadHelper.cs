namespace Amusoft.UI.WPF.Utility
{
	// ReSharper disable once InconsistentNaming
	public class ThreadHelper
	{
		public static DispatcherAwaiter UI => new DispatcherAwaiter();
	}
}