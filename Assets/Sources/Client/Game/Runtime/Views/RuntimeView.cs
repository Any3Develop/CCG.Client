using System.Threading;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;
using Cysharp.Threading.Tasks;

namespace Client.Game.Runtime.Views
{
    public abstract class RuntimeView : IRuntimeView
    {
        public IRuntimeModel Model { get; private set; }
        
        protected bool Initialized { get; private set; }

        private CancellationTokenSource setupSource;

        public void Setup(IRuntimeModel model)
        {
            Model = model;
            setupSource?.Cancel();
            setupSource?.Dispose();
            setupSource = new CancellationTokenSource();
            Initialized = true;
            OnSetupAsync(setupSource.Token)
                .SuppressCancellationThrow()
                .ContinueWith(cancelled =>
                {
                    if (!cancelled)
                        setupSource = null;
                }).Forget();
        }

        public void Dispose()
        {
            if (!Initialized)
                return;
            
            OnDisposed();
            Model = null;
            setupSource?.Cancel();
            setupSource?.Dispose();
            setupSource = null;
            Initialized = false;
        }

        protected virtual UniTask OnSetupAsync(CancellationToken token) => UniTask.CompletedTask;
        protected virtual void OnDisposed() {}
    }
}