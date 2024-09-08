using System.Collections.Generic;
using Client.Services.UIService.Data;

namespace Client.Services.UIService
{
	public interface IUIServiceRepository
	{
		WindowItem Get<T>(string windowId, string groupId);
		
		IEnumerable<WindowItem> GetAll<T>(string groupId);
		
		void Add(WindowItem value);
		
		bool Remove(WindowItem value);
	}
}