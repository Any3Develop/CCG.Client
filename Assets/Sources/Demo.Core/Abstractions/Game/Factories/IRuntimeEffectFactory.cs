using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect, IRuntimeEffectData>{}
}