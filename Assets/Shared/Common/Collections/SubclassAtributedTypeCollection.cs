using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shared.Abstractions.Common.Collections;
using Shared.Common.Logger;

namespace Shared.Common.Collections
{
    /// <summary>
    /// Use it only as project singleton to prevent iteration whole assembly every scope creation.
    /// </summary>
    public class SubclassAtributedTypeCollection<TKey, TAttribute> : ITypeCollection<TKey> where TAttribute : Attribute
    {
        private readonly Dictionary<TKey, Type> collection;

        public SubclassAtributedTypeCollection(Func<Attribute, TKey> selector, Type baseType = null)
        {
            collection = RegisterTypes(selector, baseType);
        }

        private static Dictionary<TKey, Type> RegisterTypes(Func<TAttribute, TKey> selector, Type baseType)
        {
            var subclassTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        SharedLogger.Error(e);
                        return e.Types.Where(type => type != null);
                    }
                })
                .Where(x => x.IsClass
                            && !x.IsAbstract
                            && (baseType == null || x.IsSubclassOf(baseType))
                            && x.CustomAttributes.Any());

            var registred = new Dictionary<TKey, Type>();
            foreach (var type in subclassTypes)
            {
                foreach (var attribute in type.GetCustomAttributes<TAttribute>(false))
                {
                    var key = selector.Invoke(attribute);
                    if (registred.ContainsKey(key))
                    {
                        SharedLogger.Error($"Key : {key} already exist in collection, type : {type}");
                        continue;
                    }

                    registred.Add(selector.Invoke(attribute), type);
                }
            }

            return registred;
        }

        public virtual Type Get(TKey key)
        {
            return collection[key];
        }

        public virtual bool TryGet(TKey key, out Type result)
        {
            return collection.TryGetValue(key, out result);
        }
    }
}