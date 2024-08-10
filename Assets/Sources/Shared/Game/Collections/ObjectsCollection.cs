using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Cards;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data.Enums;
using Shared.Game.Events.Context.Objects;
using Shared.Game.Utils;

namespace Shared.Game.Collections
{
    public class ObjectsCollection : RuntimeCollectionBase<IRuntimeObject>, IObjectsCollection
    {
        private readonly IEventPublisher eventPublisher;

        public ObjectsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        
        protected override int GetId(IRuntimeObject value) =>
            value?.RuntimeData?.Id ?? int.MinValue;
        
        public int GetOccupiedTableSpace(string ownerId) // TODO: move to conditions
        {
            var checkOwner = !string.IsNullOrWhiteSpace(ownerId); 
            return GetAll<IRuntimeCard>().Count(x => (!checkOwner || x.RuntimeData.OwnerId == ownerId) 
                                                     && x.RuntimeData.State == ObjectState.InTable);
        }
        
        public override bool Add(IRuntimeObject value, bool notify = true)
        {
            if (Contains(value))
                return false;
            
            var result = base.Add(value, notify);
            eventPublisher.Publish<AfterObjectAddedEvent>(notify && result, value);
            return result;
        }

        public override bool Remove(int id, bool notify = true)
        {
            if (!TryGet(id, out var value))
                return false;

            var result = base.Remove(value, notify);
            eventPublisher.Publish<AfterObjectDeletedEvent>(notify && result, value);
            return result;
        }
    }
}