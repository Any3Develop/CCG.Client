using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Game.Data;

namespace Shared.Game.Context
{
    public class Database : IDatabase
    {
        public IDataCollection<ObjectData> Objects { get; set; }
        public IDataCollection<EffectData> Effects { get; set; }
        public IDataCollection<StatData> Stats{ get; set; }
    }
}