using System;

namespace Client.Services.UIService.Data
{
	public class WindowItem
	{
		public IUIWindow Window { get; }
		public IDisposable ActiveTask { get; set; }
		public string GroupId { get;}
		public string Id { get; }

		public WindowItem(IUIWindow window, string groupId)
		{
			Window = window;
			Id = window.Id;
			GroupId = groupId;
		}
	}
}