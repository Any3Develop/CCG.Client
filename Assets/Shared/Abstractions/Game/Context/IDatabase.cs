using Shared.Abstractions.Game.Collections;
using Shared.Game.Data;

namespace Shared.Abstractions.Game.Context
{
    public interface IDatabase
    {
        IDataCollection<ObjectData> Objects { get; }
        IDataCollection<EffectData> Effects { get; }
        IDataCollection<StatData> Stats { get; }
    }
}