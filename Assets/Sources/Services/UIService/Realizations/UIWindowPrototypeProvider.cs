using System;
using System.Collections.Generic;
using System.IO;
using Services.UIService.Abstractions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.UIService
{
    public class UIWindowPrototypeProvider : IUIPrototypeProvider
    {
        private readonly string path;

        public UIWindowPrototypeProvider(string path)
        {
            this.path = path;
        }

        public IEnumerable<Object> GetAll()
            => Resources.LoadAll<UIWindowBase>(path);

        public IEnumerable<Object> GetAll(string groupId)
            => Resources.LoadAll<UIWindowBase>(Path.Combine(path, groupId));

        public Object Get(string groupId, Type type)
        {
	        var directory = Path.Combine(path, groupId);
	        var prototype = Resources.Load(directory, type);
	        if (!prototype)
		        throw new FileNotFoundException($"[{GetType().Name}] Tried to load pototype at path : [{directory}] not found.");
	        
	        return prototype;
        }
    }
}