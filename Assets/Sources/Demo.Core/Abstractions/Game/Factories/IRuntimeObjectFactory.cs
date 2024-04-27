using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeObjectFactory : IRuntimeFactory<IRuntimeObject, IRuntimeObjectData>{}
}