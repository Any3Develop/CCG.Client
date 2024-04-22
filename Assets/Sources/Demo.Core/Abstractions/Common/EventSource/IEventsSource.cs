using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Core.Abstractions.Common.EventSource
{
    public interface IEventsSource : IDisposable
    {
        IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null);
		
        void Publish<T>(T value);
    }
}