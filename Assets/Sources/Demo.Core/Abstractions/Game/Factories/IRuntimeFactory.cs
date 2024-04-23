using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime> where TRuntime : IRuntimeObjectBase
    {
        TRuntime Create(int? runtimeId, string ownerId, IData data, bool notify = true);
        TRuntime Create(IRuntimeData runtimeData, bool notify = true);
    }
}