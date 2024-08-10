using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.UIService.Abstractions;

namespace Services.UIService
{
    public sealed class UINoAnimationSource : IUIAnimationSource
    {
        public bool Enabled { get; private set; }

        public IEnumerable<IUIAnimation> Animations => Array.Empty<IUIAnimation>();

        public void Dispose() => Enable(false);

        public void Enable(bool value) => Enabled = value;

        public Task ExecuteAsync(object id, CancellationToken token) => Task.CompletedTask;
    }
}