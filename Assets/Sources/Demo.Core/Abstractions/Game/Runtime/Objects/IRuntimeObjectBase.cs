using System;
using Demo.Core.Abstractions.Game.Runtime.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Objects
{
    public interface IRuntimeObjectBase : IDisposable
    {
        IRuntimeDataBase RuntimeData { get; }
    }
}