using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Game.Data;

namespace Demo.Core.Game.Collections
{
    public class Database : IDatabase
    {
        public IDataCollection<ObjectData> Objects { get; set; }
        public IDataCollection<EffectData> Effects { get; set; }
    }
}