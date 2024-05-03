using System;

namespace Shared.Abstractions.Common.Collections
{
    public interface ITypeCollection<in TKey>
    {
        Type Get(TKey key);
        bool TryGet(TKey key, out Type result);
    }
}