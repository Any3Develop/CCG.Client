using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Abstractions.Game.Context.EventSource
{
    public interface IEventsSource : IDisposable
    {
        IDisposable Subscribe(Action<object> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null);
		
        void Publish<T>(T value);
        Task PublishAsync<T>(T value);
        Task PublishParallelAsync<T>(T value);
        void Clear();
    }
}