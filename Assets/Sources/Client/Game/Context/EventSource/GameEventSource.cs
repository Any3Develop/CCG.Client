using System;
using System.Threading;
using Client.Common.Services.EventSourceService.Realizations;
using Client.Game.Abstractions.Context.EventSource;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Context.EventSource
{
    public class GameEventSource : UnitaskBasedEventSource, IGameEventSource, IGameEventPublisher
    {
        public new IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null) 
            where T : IGameEvent => base.Subscribe(callback, token, order);

        public new IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null) 
            where T : IGameEvent => base.Subscribe<T>(callback, token, order);

        public new IDisposable Subscribe<T>(Func<UniTask> callback, CancellationToken? token = null, int? order = null) 
            where T : IGameEvent => base.Subscribe<T>(callback, token, order);

        public new IDisposable Subscribe<T>(Func<T, UniTask> callback, CancellationToken? token = null, int? order = null) 
            where T : IGameEvent => base.Subscribe(callback, token, order);

        public new UniTask PublishAsync<T>(T value) 
            where T : IGameEvent => base.PublishAsync(value);
    }
}