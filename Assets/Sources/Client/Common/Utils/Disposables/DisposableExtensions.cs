using System;

namespace Client.Common.Utils.Disposables
{
    public static class DisposableExtensions
    {
        public static IDisposables CreateDisposables()
        {
            return new Disposables();
        }

        public static T AddTo<T>(this T disposable, ref IDisposables disposables) where T : IDisposable
        {
            disposables ??= CreateDisposables();
            disposables.Add(disposable);
            return disposable;
        }

        public static T AddTo<T>(this T disposable, IDisposables disposables) where T : IDisposable
        {
            if (disposable != null && disposables != null)
                disposables.Add(disposable);

            return disposable;
        }

        public static IDisposables AsDisposables<T>(this T disposable) where T : IDisposable
        {
            if (disposable is IDisposables disposables)
                return disposables;

            return CreateDisposables().Add(disposable);
        }
    }
}