using System;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Runtime.Common;
using Demo.Core.Game.Runtime.Data;

namespace Demo.Core.Game.Factories
{
    public class RuntimeStatFactory : IRuntimeStatFactory
    {
        private readonly IDatabase database;
        private readonly IRuntimePool runtimePool;
        private readonly IRuntimeIdProvider runtimeIdProvider;

        public RuntimeStatFactory(
            IDatabase database,
            IRuntimePool runtimePool,
            IRuntimeIdProvider runtimeIdProvider)
        {
            this.database = database;
            this.runtimePool = runtimePool;
            this.runtimeIdProvider = runtimeIdProvider;
        }

        public IRuntimeStatData Create(int? runtimeOwnerId, string ownerId, string dataId, bool notify = true)
        {
            if (!runtimeOwnerId.HasValue)
                throw new NullReferenceException($"To create {nameof(IRuntimeStat)} you should inject {nameof(runtimeOwnerId)}");
            
            if (!runtimePool.TryGet(runtimeOwnerId.Value, out IRuntimeObject runtimeObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeOwnerId.Value}, not found in {nameof(IRuntimePool)}");

            if (runtimeObject.StatsCollection.TryGet(x => x.RuntimeData.DataId == dataId, out var runtimeStat))
                return runtimeStat.RuntimeData;
            
            if (!database.Stats.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(StatData)} with id {dataId}, not found in {nameof(IDataCollection<StatData>)}");
            
            return new RuntimeStatData
            {
                Id = runtimeIdProvider.Next(),
                DataId = dataId,
                OwnerId = ownerId,
                RuntimeOwnerId = runtimeOwnerId.Value,
                Max = data.Max,
                Value = data.Value,
            };
        }

        public IRuntimeStat Create(IRuntimeStatData runtimeData, bool notify = true)
        {
            if (!runtimePool.TryGet(runtimeData.RuntimeOwnerId, out IRuntimeObject runtimeObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeData.Id}, not found in {nameof(IRuntimePool)}");

            if (!database.Stats.TryGet(runtimeData.DataId, out var statData))
                throw new NullReferenceException($"{nameof(StatData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<StatData>)}");

            if (runtimeObject.StatsCollection.TryGet(runtimeData.Id, out var runtimeStat))
                return runtimeStat.Sync(runtimeData);
            
            runtimeStat = new RuntimeStat().Init(statData, runtimeData, runtimeObject.EventsSource);
            runtimeObject.StatsCollection.Add(runtimeStat, notify);
            
            return runtimeStat;
        }
    }
}