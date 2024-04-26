using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Common
{
    public interface IRuntimeStat : IRuntimeObjectBase
    {
        new StatData Data { get; }
        new IRuntimeStatData RuntimeData { get; }
        IRuntimeStat Sync(IRuntimeStatData runtimeData);
        void SetValue(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void Reset(bool notify = true);
    }
}