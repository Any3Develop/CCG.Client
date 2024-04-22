using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IDatabase
    {
        IDataCollection<ObjectData> Objects { get; }
        IDataCollection<EffectData> Effects { get; }
    }
}