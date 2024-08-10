using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.UIService.Abstractions;
using UnityEngine;

namespace Services.UIService
{
    public class WrapperOptions : IDisplayOption
    {
        private readonly IEnumerable<IDisplayOption> options;
        public WrapperOptions(IEnumerable<IDisplayOption> options)
        {
            this.options = options;
        }

        public void Dispose()
        {
            foreach (var option in options)
                option?.Dispose();
        }

        public IDisplayOption WithParent(Transform value)
        {
            foreach (var option in options)
                option?.WithParent(value);

            return this;
        }

        public IDisplayOption WithNoAnimation(bool value = true)
        {
            foreach (var option in options)
                option?.WithNoAnimation(value);

            return this;
        }

        public IDisplayOption WithOrder(int value)
        {
            foreach (var option in options)
                option?.WithOrder(value);

            return this;
        }

        public IDisplayOption WithToken(CancellationToken value)
        {
            foreach (var option in options)
                option?.WithToken(value);

            return this;
        }

        public IDisplayOption WithNoInterrupt()
        {
            foreach (var option in options)
                option?.WithNoInterrupt();

            return this;
        }

        public async Task ExecuteAsync()
        {
            var task = Task.WhenAll(options.Select(x => x?.ExecuteAsync() ?? Task.CompletedTask));
            await task;
            if (task.Exception != null)
                throw task.Exception.GetBaseException();
        }

        public void Execute(Action onComplete = null)
        {
            foreach (var option in options)
                option?.Execute(onComplete);
        }
    }
}