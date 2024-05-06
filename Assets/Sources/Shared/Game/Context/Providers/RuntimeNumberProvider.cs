using Shared.Abstractions.Game.Context.Providers;

namespace Shared.Game.Context.Providers
{
    public abstract class RuntimeNumberProvider : IRuntimeNumberProvider
    {
        public int Current
        {
            get
            {
                lock (concurentCurrent)
                    return current;
            }
            set // public set for support reflection
            {
                lock (concurentCurrent)
                    current = value;
            }
        }

        private int current = -1;
        private readonly object concurentCurrent = new();

        public virtual int Next()
        {
            Current++;
            return Current;
        }

        public virtual void Sync(int value)
        {
            Current = value;
        }
    }
}