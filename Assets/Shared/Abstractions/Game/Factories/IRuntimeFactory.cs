using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime, TRuntimeData>
        where TRuntime : IRuntimeObjectBase
        where TRuntimeData : IRuntimeDataBase
    {
        TRuntimeData Create(int? runtimeId, string ownerId, string dataId, bool notify = true);
        TRuntime Create(TRuntimeData runtimeData, bool notify = true);
    }
}