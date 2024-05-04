using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Cards;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data.Enums;

namespace Shared.Game.Collections
{
    public class ObjectsCollection : RuntimeCollectionBase<IRuntimeObject>, IObjectsCollection
    {
        protected override int GetId(IRuntimeObject value) =>
            value?.RuntimeData?.Id ?? int.MinValue;
        
        public int GetOccupiedTableSpace(string ownerId) // TODO: move to conditions
        {
            var checkOwner = !string.IsNullOrWhiteSpace(ownerId); 
            return GetAll<IRuntimeCard>().Count(x => (!checkOwner || x.RuntimeData.OwnerId == ownerId) 
                                                     && x.RuntimeData.State == ObjectState.InTable);
        }
    }
}