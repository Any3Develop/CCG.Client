using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeStat
    {
        IRuntimeStatData RuntimeData { get; }
        void Set(int value, bool notify = true);
        void SetBase(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void Reset(bool notify = true);
    }
}