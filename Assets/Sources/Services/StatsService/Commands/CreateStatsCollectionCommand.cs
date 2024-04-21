using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGame.Services.CommandService;
using CardGame.Services.TypeRegistryService;
using Zenject;

namespace CardGame.Services.StatsService
{
    public class CreateStatsCollectionCommand<T> : ICommand<CreateStatsCollectionProtocol> where T : StatsCollection
    {
        private readonly IInstantiator _instantiator;
        private readonly StatsCollectionStorage _statCollectionStorage;
        private readonly StatsCollectionDtoStorage _statsCollectionDtoStorage;
        private readonly TypeRegistry _typeStorage;

        public CreateStatsCollectionCommand(IInstantiator instantiator,
                                            StatsCollectionStorage statCollectionStorage,
                                            StatsCollectionDtoStorage statsCollectionDtoStorage,
                                            TypeRegistry typeStorage)
        {
            _instantiator = instantiator;
            _statCollectionStorage = statCollectionStorage;
            _statsCollectionDtoStorage = statsCollectionDtoStorage;
            _typeStorage = typeStorage;
        }

        public Task Execute(CreateStatsCollectionProtocol protocol)
        {
            if (_statCollectionStorage.HasEntity(protocol.Guid))
            {
               return Task.CompletedTask;
            }

            StatsCollectionDto statCollectionDto;
            if (!_statsCollectionDtoStorage.HasEntity(protocol.Guid))
            {
                statCollectionDto = new StatsCollectionDto
                {
                    Guid = protocol.Guid,
                    Stats = new List<StatDto>(protocol.StatModels.Select(CreateStatDto))
                };
                _statsCollectionDtoStorage.Add(statCollectionDto);
            }
            else
            {
                // if the server has released updates,
                // need to check for new stats, or static stats load.
                statCollectionDto = _statsCollectionDtoStorage.Get(protocol.Guid);
                var stats = statCollectionDto.Stats.ToDictionary(x => x.StatId);
                foreach (var statModel in protocol.StatModels)
                {
                    if (!stats.ContainsKey(statModel.StatID))
                    {
                        statCollectionDto.Stats.Add(CreateStatDto(statModel));
                    }
                }
            }
            
            var statsCollection = _instantiator.Instantiate<T>(new object[]{statCollectionDto, protocol.StatModels});
            _statCollectionStorage.Add(statsCollection);
            return Task.CompletedTask;
        }

        private StatDto CreateStatDto(StatModel statModel)
        {
            var typeItem = _typeStorage.Get(statModel.StatType + "Dto");
            return (StatDto)_instantiator.Instantiate(typeItem.Type, new object[]{statModel});
        }
    }
}