using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Context
{
    public interface IDatabase
    {
        IDataCollection<ObjectData> Objects { get; }
        IDataCollection<EffectData> Effects { get; }
        IDataCollection<StatData> Stats { get; }
    }
}