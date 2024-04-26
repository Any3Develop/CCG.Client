using Demo.Core.Abstractions.Game.Runtime.Common;
using Demo.Core.Abstractions.Game.Runtime.Data;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeObjectFactory : IRuntimeFactory<IRuntimeObject, IRuntimeObjectData>{}
}