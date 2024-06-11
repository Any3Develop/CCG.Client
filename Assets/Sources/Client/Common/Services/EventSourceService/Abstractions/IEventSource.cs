using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Client.Common.Services.EventSourceService
{
    public interface IEventSource
    {
        IDisposable Subscribe(Action<object> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<UniTask> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<T, UniTask> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null);
		
        void Publish<T>(T value);
        UniTask PublishParallelAsync<T>(T value);
        UniTask PublishAsync<T>(T value);
        void Clear();
    }
}