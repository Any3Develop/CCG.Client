using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models;

namespace Client.Game.Abstractions.Runtime.Views
{
    public interface IRuntimeObjectView : IRuntimeView
    {
        RuntimeTransform Transform { get; }
        new IRuntimeObjectModel Model { get; }
        void SetTransform(RuntimeTransform value);
    }
}