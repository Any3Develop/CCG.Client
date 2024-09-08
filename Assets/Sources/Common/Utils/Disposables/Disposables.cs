using System;
using System.Collections.Generic;

namespace Client.Common.Utils.Disposables
{
    public interface IDisposables : IDisposable
    {
        IDisposables Add(IDisposable disposable);
    }

    public class Disposables : IDisposables
    {
        private readonly List<IDisposable> disposables = new();

        public IDisposables Add(IDisposable disposable)
        {
            disposables.Add(disposable);
            return this;
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