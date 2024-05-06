using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IObjectEventProcessor
    {
        void Subscribe(IRuntimeObjectBase runtimeObject);
    }
}