using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Enums;
using Demo.Core.Game.RuntimeData;
using Demo.Core.Game.RuntimeObjects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeObjectFactory : IRuntimeFactory<IRuntimeObject>
    {
        private readonly IDatabaseCollection databaseCollection;
        private readonly IRuntimeIdGenerator runtimeIdGenerator;

        public RuntimeObjectFactory(
            IDatabaseCollection databaseCollection,
            IRuntimeIdGenerator runtimeIdGenerator)
        {
            this.databaseCollection = databaseCollection;
            this.runtimeIdGenerator = runtimeIdGenerator;
        }

        public IRuntimeObject Create(string ownerId, IDatabase data)
        {
            return Create(CreateRuntimeData(ownerId, data));
        }

        public IRuntimeObject Create(IRuntimeData runtimeData)
        {
            return Create(runtimeData, databaseCollection.GetFirst(runtimeData.DataId));
        }

        public IRuntimeObject Create(IRuntimeObjectData runtimeData, IDatabase data)
        {
            return runtimeData switch
            {
                IRuntimeCardData => CreateCardObject(runtimeData, data),
                _ => throw new NotImplementedException()
            };
        }

        private IRuntimeObject CreateCardObject(IRuntimeObjectData runtimeData, IDatabase data)
        {
            if (data is not ICardData cardData)
                throw new InvalidOperationException($"Can't create card if data not for cards : {data}");

            return cardData.Type switch
            {
                // TODO: later will separated controllers
                ObjectType.Creature or ObjectType.Spell => new RuntimeCard(data, runtimeData, null, null, null),
                _ => throw new NotImplementedException()
            };
        }

        private IRuntimeObjectData CreateRuntimeData(string ownerId, IDatabase data)
        {
            var runtimeId = runtimeIdGenerator.Next();
            return data switch
            {
                ICardData cardData => new RuntimeCardData
                {
                    DataId = data.Id,
                    Id = runtimeId,
                    OwnerId = ownerId,
                    Stats = cardData.Stats.Select(o => Create(o, runtimeId)).ToList(),
                },
                _ => throw new NotImplementedException()
            };
        }

        private IRuntimeStatData Create(StatData statData, int runtimeId)
        {
            return new RuntimeStatData
            {
                Id = runtimeId,
                DataId = statData.Id,
                Base = statData.Base,
                Max = statData.Max,
                Value = statData.Value,
                Name = statData.Name
            };
        }
    }
}