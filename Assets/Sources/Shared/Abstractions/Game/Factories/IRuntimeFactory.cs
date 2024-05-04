namespace Shared.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime, TRuntimeData>
    {
        TRuntimeData Create(int? runtimeId, string ownerId, string dataId, bool notify = true);
        TRuntime Create(TRuntimeData runtimeData, bool notify = true);
    }
}