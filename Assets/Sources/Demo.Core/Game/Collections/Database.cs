using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Game.Data;

namespace Demo.Core.Game.Collections
{
    public class Database : IDatabase
    {
        public IDataCollection<ObjectData> Objects { get; set; }
        public IDataCollection<EffectData> Effects { get; set; }
        public IDataCollection<StatData> Stats{ get; set; }
    }
}