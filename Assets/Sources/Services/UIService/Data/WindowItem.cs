using Services.UIService.Abstractions;

namespace Services.UIService.Data
{
	public class WindowItem
	{
		public IUIWindow Window { get; }
		public IDisplayOption Previous { get; set; }

		public WindowItem(IUIWindow window)
		{
			Window = window;
		}
	}
}