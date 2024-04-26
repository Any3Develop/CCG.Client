using System;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.Runtime.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Common
{
    public interface IRuntimeObjectBase : IDisposable
    {
        IRuntimeDataBase RuntimeData { get; }
        IData Data { get; }
    }
}