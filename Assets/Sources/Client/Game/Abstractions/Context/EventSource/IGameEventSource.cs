using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.EventSource
{
    public interface IGameEventSource
    {
        IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
        IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
        IDisposable Subscribe<T>(Func<UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
        IDisposable Subscribe<T>(Func<T, UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
    }
}