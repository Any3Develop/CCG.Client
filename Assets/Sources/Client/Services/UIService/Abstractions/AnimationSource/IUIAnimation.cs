using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.UIService
{
    public interface IUIAnimation : IDisposable
    {
        IUIAnimationConfig Configuration { get; }
        bool Enabled { get; }
        
        void Enable();
        void Disable();
        
        Task PlayAsync(CancellationToken token);
        Task StopAsync(CancellationToken token);
        Task ResetAsync(CancellationToken token);
    }
}