using System;

namespace Client.Common.Utils
{
    public static class DisposableExtensions
    {
        public static IDisposableBlock CreateBlock()
        {
            return new DisposableBlock();
        }
        
        public static T AddTo<T>(this T disposable, ref IDisposableBlock disposableBlock) where T : IDisposable
        {
            disposableBlock ??= CreateBlock();
            disposableBlock.Add(disposable);
            return disposable;
        }

        public static T AddTo<T>(this T disposable, IDisposableBlock disposableBlock) where T : IDisposable
        {
            disposableBlock?.Add(disposable);
            return disposable;
        }
    }
}