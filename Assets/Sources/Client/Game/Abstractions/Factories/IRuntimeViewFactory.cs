namespace Client.Game.Abstractions.Factories
{
    public interface IRuntimeViewFactory<out TView, in TRuntimeData>
    {
        TView Create(TRuntimeData runtimeModel);
    }
}