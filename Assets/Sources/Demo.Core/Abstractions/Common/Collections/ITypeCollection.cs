using System;

namespace Demo.Core.Abstractions.Common.Collections
{
    public interface ITypeCollection<in TKey>
    {
        Type Get(TKey key);
        bool TryGet(TKey key, out Type result);
    }
}