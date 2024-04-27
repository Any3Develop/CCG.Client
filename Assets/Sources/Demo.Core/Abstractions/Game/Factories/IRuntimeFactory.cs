using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime, TRuntimeData>
        where TRuntime : IRuntimeObjectBase
        where TRuntimeData : IRuntimeDataBase
    {
        TRuntimeData Create(int? runtimeId, string ownerId, string dataId, bool notify = true);
        TRuntime Create(TRuntimeData runtimeData, bool notify = true);
    }
}