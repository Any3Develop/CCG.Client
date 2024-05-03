using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect, IRuntimeEffectData>{}
}