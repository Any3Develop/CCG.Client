using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeStat : IRuntimeObjectBase
    {
        new StatData Data { get; }
        new IRuntimeStatData RuntimeData { get; }
        void Set(int value, bool notify = true);
        void SetBase(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void SetName(string value, bool notify = true);
        void Reset(bool notify = true);
    }
}