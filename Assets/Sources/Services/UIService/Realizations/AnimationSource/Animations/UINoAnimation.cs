using System.Threading;
using System.Threading.Tasks;
using Services.UIService.Abstractions;

namespace Services.UIService
{
    public sealed class UINoAnimation : IUIAnimation
    {
        public IUIAnimationConfig Configuration { get; } = new UINoAnimationConfig();
        
        public bool Enabled { get; private set; }

        public void Dispose() => Disable();
        
        public void Enable() => Enabled = true;

        public void Disable() => Enabled = false;

        public Task PlayAsync(CancellationToken token) => Task.CompletedTask;

        public Task StopAsync(CancellationToken token) => Task.CompletedTask;

        public Task ResetAsync(CancellationToken token) => Task.CompletedTask;
    }
}