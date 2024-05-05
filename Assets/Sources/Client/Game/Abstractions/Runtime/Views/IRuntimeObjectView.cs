using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models;

namespace Client.Game.Abstractions.Runtime.Views
{
    public interface IRuntimeObjectView : IRuntimeView
    {
        TransformModel Previous { get; }
        TransformModel Transform { get; }
        new IRuntimeObjectModel Model { get; }
        void SetTransform(TransformModel value);
    }
}