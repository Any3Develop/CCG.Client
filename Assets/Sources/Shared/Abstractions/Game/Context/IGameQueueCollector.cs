using System;
using Shared.Abstractions.Game.Events;

namespace Shared.Abstractions.Game.Context
{
    public interface IGameQueueCollector : IDisposable
    {
        void Register(IGameEvent value);
        void Release();
    }
}