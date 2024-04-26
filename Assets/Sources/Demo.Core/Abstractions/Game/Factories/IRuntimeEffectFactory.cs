using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Effects;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect, IRuntimeEffectData>{}
}