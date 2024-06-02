using Shared.Abstractions.Game.Collections;
using Shared.Common.Network.Data;
using Shared.Game.Data;

namespace Shared.Abstractions.Game.Context
{
    public interface IDatabase
    {
        IDataCollection<ObjectData> Objects { get; }
        IDataCollection<EffectData> Effects { get; }
        IDataCollection<StatData> Stats { get; }
        IDataCollection<PlayerData> Players { get; }

        DatabaseModel GetModel();
    }
}