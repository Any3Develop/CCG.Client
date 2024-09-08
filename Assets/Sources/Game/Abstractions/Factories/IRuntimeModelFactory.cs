namespace Client.Game.Abstractions.Factories
{
    public interface IRuntimeModelFactory<out TModel, in TRuntimeData>
    {
        TModel Create(TRuntimeData runtimeData);
    }
}