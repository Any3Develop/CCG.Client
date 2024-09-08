using Client.Game.Abstractions.Runtime.Models;
using Shared.Abstractions.Game.Runtime.Data;

namespace Client.Game.Abstractions.Factories
{
    public interface IRuntimeObjectModelFactory : IRuntimeModelFactory<IRuntimeObjectModel, IRuntimeObjectData>{}
}