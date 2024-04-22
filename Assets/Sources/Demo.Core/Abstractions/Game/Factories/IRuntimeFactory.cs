using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime>
    {
        TRuntime Create(string ownerId, IDatabase data);
        TRuntime Create(IRuntimeData runtimeData);
    }
}