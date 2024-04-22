using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime> where TRuntime : IRuntimeBase
    {
        TRuntime Create(string ownerId, IData data);
        TRuntime Create(IRuntimeData runtimeData);
    }
}