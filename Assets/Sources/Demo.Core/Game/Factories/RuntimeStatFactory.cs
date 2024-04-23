using System;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.RuntimeData;
using Demo.Core.Game.RuntimeObjects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeStatFactory : IRuntimeFactory<IRuntimeStat>
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

        public IRuntimeStat Create(int? runtimeId, string ownerId, IData data, bool notify = true)
        {
            if (data is not StatData statData)
                throw new InvalidCastException($"To create {nameof(IRuntimeStat)} you should inject {nameof(StatData)} instead {data?.GetType().Name ?? "Type.Undefined"}");
            
            runtimeId ??= runtimeIdProvider.Next();
            var runtimeData = new RuntimeStatData
            {
                Id = runtimeId.Value,
                DataId = statData.Id,
                OwnerId = ownerId,
                Base = statData.Base,
                Max = statData.Max,
                Value = statData.Value,
                Name = statData.Name
            };
            
            return Create(runtimeData, notify);
        }

        public IRuntimeStat Create(IRuntimeData runtimeData, bool notify = true)
        {
            if (runtimeData is not IRuntimeStatData runtimeStatData)
                throw new InvalidCastException($"To create {nameof(IRuntimeStat)} you should inject {nameof(IRuntimeStatData)} instead {runtimeData?.GetType().Name ?? "Type.Undefined"}");

            if (!runtimePool.TryGet(runtimeData.Id, out IRuntimeObject runtimeObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeData.Id}, not found in {nameof(IRuntimePool)}");

            if (!database.Objects.TryGet(runtimeData.DataId, out var objectData))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<ObjectData>)}");

            var statData = objectData.Stats.FirstOrDefault(x => x.Id == runtimeStatData.DataId);
            var runtimeStat = new RuntimeStat().Init(statData, runtimeStatData, runtimeObject.EventsSource);
            runtimeObject.StatsCollection.Add(runtimeStat, notify);
            
            return runtimeStat;
        }
    }
}