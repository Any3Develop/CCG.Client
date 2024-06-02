using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Common.Network;
using Shared.Common.Network.Data;
using Shared.Game.Collections;
using Shared.Game.Data;

namespace Shared.Game.Context
{
    public class Database : IDatabase
    {
        public IDataCollection<ObjectData> Objects { get; } = new DataCollection<ObjectData>();
        public IDataCollection<EffectData> Effects { get; } = new DataCollection<EffectData>();
        public IDataCollection<StatData> Stats{ get; } = new DataCollection<StatData>();
        public IDataCollection<PlayerData> Players{ get; } = new DataCollection<PlayerData>();
        
        public DatabaseModel GetModel()
        {
            return new DatabaseModel
            {
                Objects = Objects.ToArray(),
                Effects = Effects.ToArray(),
                Stats = Stats.ToArray(),
                Players = Players.ToArray(),
            };
        }
    }
}