using System;
using System.Collections.Generic;

namespace Client.Common.Utils
{
    public interface IDisposableBlock : IDisposable
    {
        void Add(IDisposable disposable);
    }

    public class DisposableBlock : IDisposableBlock
    {
        private readonly List<IDisposable> disposables = new();
        
        public void Add(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void Dispose()
        {
            var mem = disposables.ToArray();
            disposables.Clear();
            foreach (var disposable in mem)
                disposable?.Dispose();
        }
    }
}