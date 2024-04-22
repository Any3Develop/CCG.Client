using Demo.Core.Abstractions.Game.Context;

namespace Demo.Core.Game.Context
{
    public class RuntimeIdProvider : IRuntimeIdProvider
    {
        public int Current
        {
            get
            {
                lock (concurentCurrent)
                    return current;
            }
            private set
            {
                lock (concurentCurrent)
                    current = value;
            }
        }

        private int current = -1;
        private readonly object concurentCurrent = new();
        public int Next()
        {
            Current++;
            return Current;
        }

        public void Sync(int value)
        {
            Current = value;
        }
    }
}