using System.Collections.Generic;
using System.Linq;
using Client.Services.UIService.Data;

namespace Client.Services.UIService
{
	public class UIServiceRepository : IUIServiceRepository
	{
		private readonly List<WindowItem> storage = new();
		
		public WindowItem Get<T>(string windowId, string groupId)
		{
			var checkId = !string.IsNullOrEmpty(windowId);
			var checkGroup = !string.IsNullOrEmpty(groupId);
			return storage.FirstOrDefault(x => x.Window is T && (!checkId || x.Id == windowId) && (!checkGroup || x.GroupId == groupId));
		}

		public IEnumerable<WindowItem> GetAll<T>(string groupId) 
			=> storage.Where(x => x.Window is T && (groupId == null || groupId == x.GroupId));

		public void Add(WindowItem value) 
			=> storage.Add(value);

		public bool Remove(WindowItem value) 
			=> storage.Remove(value);
	}
}