using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;

namespace Shared.Abstractions.Game.Runtime.Stats
{
    public interface IRuntimeStat : IRuntimeObjectBase
    {
        StatData Data { get; }
        new IRuntimeStatData RuntimeData { get; }
        
        IRuntimeStat Sync(IRuntimeStatData runtimeData, bool notify = true);
        void SetValue(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void Reset(bool notify = true);
    }
}