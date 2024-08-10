using System;
using System.Collections.Generic;
using System.Linq;
using Services.UIService.Abstractions;
using Services.UIService.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.UIService
{
    public class UIService : IUIService
    {
        private readonly IUIRoot uiRoot;
        private readonly IUIWindowFactory windowFactory;
        private readonly IDisplayOptionFactory optionFactory;
        private readonly IUIFullFadePresenter fullFade;
        private readonly Dictionary<string, List<WindowItem>> instanceStorage;
        
        public object DefaultGroupId { get; }

        public UIService(
            IUIRoot uiRoot,
            IUIWindowFactory windowFactory,
            IDisplayOptionFactory optionFactory,
            IUIFullFadePresenter fullFade,
            string defaultGroupId)
        {
            if (string.IsNullOrWhiteSpace(defaultGroupId))
                throw new ArgumentNullException($"{nameof(DefaultGroupId)} cannot be null, empty or white spaces.");
            
            this.uiRoot = uiRoot;
            this.windowFactory = windowFactory;
            this.optionFactory = optionFactory;
            this.fullFade = fullFade;
            fullFade.Init(this);
            optionFactory.Init(this);
            instanceStorage = new Dictionary<string, List<WindowItem>>();
            DefaultGroupId = defaultGroupId;
        }
        
        public void Destroy(IUIWindow window)
        {
	        if (window == null)
		        return;

	        foreach (var (groupId, list) in instanceStorage)
	        {
		        var windowItem = list.FirstOrDefault(x => x.Window == window);
		        if (windowItem == null)
			        continue;

		        list.Remove(windowItem);
		        DestoryInternal(windowItem);

		        if (list.Count == 0)
			        instanceStorage.Remove(groupId);

		        break; // to prevent InvalidOperationException the collection changed while enumeration.
	        }
        }

        public void DestroyAll(object groupId = null)
        {
	        if (groupId == null)
	        {
		        foreach (var list in instanceStorage.Values)
			        DestoryInternal(list);

		        instanceStorage.Clear();
	        }
	        else
	        {
		        var id = DefaultIfEmpty(groupId);
		        if (!instanceStorage.TryGetValue(id, out var list)) 
			        return;
		        
		        DestoryInternal(list);
		        instanceStorage.Remove(id);
	        }
        }

        public T Create<T>(object groupId) where T : IUIWindow
        {
	        var id = DefaultIfEmpty(groupId);
	        var window = windowFactory.Create<T>(id, uiRoot.DeactivatedContainer);
        
	        if (!instanceStorage.TryGetValue(id, out var list))
		        instanceStorage[id] = list = new List<WindowItem>();
        
	        list.Add(new WindowItem(window));
	        return window;
        }

        public IEnumerable<IUIWindow> Create(object groupId)
        {
	        var id = DefaultIfEmpty(groupId);
	        var windows = windowFactory.Create(id, uiRoot.DeactivatedContainer).ToArray();

	        if (!instanceStorage.TryGetValue(id, out var list))
		        instanceStorage[id] = list = new List<WindowItem>();

	        list.AddRange(windows.Select(window => new WindowItem(window)));
	        return windows;
        }

        public T Get<T>(object groupId = null) where T : IUIWindow 
            => TryGet(out T result, groupId) ? result : default;

        public IEnumerable<T> GetAll<T>(object groupId = null) where T : IUIWindow
        {
	        if (groupId == null)
		        return instanceStorage.Values
			        .SelectMany(windowItems => windowItems)
			        .Select(windowItem => windowItem.Window)
			        .OfType<T>()
			        .ToArray();
	        
	        return instanceStorage.TryGetValue(DefaultIfEmpty(groupId), out var list) 
		        ? list.Select(x => x.Window).OfType<T>().ToArray() 
		        : Enumerable.Empty<T>();
        }

        public bool TryGet<T>(out T result, object groupId = null) where T : IUIWindow
        {
	        result = default;
	        if (!instanceStorage.TryGetValue(DefaultIfEmpty(groupId), out var list)) 
		        return false;
	        
	        result = (T)list.FirstOrDefault(x => x.Window is T)?.Window;
	        return result != null;
        }

        public T Move<T>(Transform parent, int? order = null, object groupId = null) where T : IUIWindow
            => TryGet(out T window, groupId) ? Move(window, OriginIfEmpty(parent, window.Parent.parent), order) : default;

        public T Move<T>(T window, Transform parent = null, int? order = null) where T : IUIWindow
        {
            if (!window?.Parent)
                return default;

            window.Parent.SetParent(DefaultIfEmpty(parent), false);
            if (order.HasValue)
                window.Parent.SetSiblingIndex(order.Value);
            else
                window.Parent.SetAsLastSibling();
            
            return window;
        }

        public IDisplayOption Show<T>(object groupId = null) where T : IUIWindow
        {
	        if (!instanceStorage.TryGetValue(DefaultIfEmpty(groupId), out var list))
		        throw new KeyNotFoundException($"[{GetType().Name}] There's no group with id : '{groupId}'");
	        
            var options = list.Where(x => x.Window is T)
	            .Select(windowItem => optionFactory.CreateShow(windowItem))
	            .ToArray();
            
            return optionFactory.CreateWrapper(options);
        }

        public IDisplayOption Show(object groupId)
        {
	        if (!instanceStorage.TryGetValue(DefaultIfEmpty(groupId), out var list))
		        throw new KeyNotFoundException($"[{GetType().Name}] There's no group with id : '{groupId}'");
            
	        var options = list
		        .Select(windowItem => optionFactory.CreateShow(windowItem))
		        .ToArray();
	        
            return optionFactory.CreateWrapper(options);
        }

        public IDisplayOption Hide<T>(object groupId = null) where T : IUIWindow
        {
	        if (!instanceStorage.TryGetValue(DefaultIfEmpty(groupId), out var list))
		        throw new KeyNotFoundException($"[{GetType().Name}] There's no group with id : '{groupId}'");
            
	        var options = list.Where(x => x.Window is T)
		        .Select(windowItem => optionFactory.CreateHide(windowItem))
		        .ToArray();
	        
	        return optionFactory.CreateWrapper(options);
        }

        public IDisplayOption Hide(object groupId)
        {
	        if (!instanceStorage.TryGetValue(DefaultIfEmpty(groupId), out var list))
		        throw new KeyNotFoundException($"[{GetType().Name}] There's no group with id : '{groupId}'");
            
	        var options = list
		        .Select(windowItem => optionFactory.CreateHide(windowItem))
		        .ToArray();
	        
	        return optionFactory.CreateWrapper(options);
        }
        
        private void DestoryInternal(WindowItem windowItem)
        {
	        if (windowItem?.Window?.Parent == null)
		        return;
	        
	        fullFade.OnDeleted(windowItem.Window);
	        Object.Destroy(windowItem.Window.Parent.gameObject);
	        windowItem.Previous?.Dispose();
	        windowItem.Previous = null;
        }

        private void DestoryInternal(List<WindowItem> list)
        {
	        if (list == null)
		        return;
	        
	        foreach (var windowItem in list)
		        DestoryInternal(windowItem);
	        
	        list.Clear();
        }
        
        private Transform OriginIfEmpty(Transform parent, Transform origin)
            => parent ? parent : origin;
        
        private Transform DefaultIfEmpty(Transform parent = null)
            => parent ? parent : uiRoot.MiddleContainer;
        
        private string DefaultIfEmpty(object groupId)
            => groupId?.ToString() ?? DefaultGroupId.ToString();
    }
}