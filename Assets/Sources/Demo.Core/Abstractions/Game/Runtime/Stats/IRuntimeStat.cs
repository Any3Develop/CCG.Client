using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Stats
{
    public interface IRuntimeStat : IRuntimeObjectBase
    {
        StatData Data { get; }
        new IRuntimeStatData RuntimeData { get; }
        IRuntimeStat Sync(IRuntimeStatData runtimeData);
        void SetValue(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void Reset(bool notify = true);
    }
}