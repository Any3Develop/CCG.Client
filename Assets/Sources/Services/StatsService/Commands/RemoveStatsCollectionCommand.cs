using System.Threading.Tasks;
using CardGame.Services.CommandService;

namespace CardGame.Services.StatsService
{
    public class RemoveStatsCollectionCommand : ICommand<RemoveStatsCollectionProtocol>
    {
        private readonly StatsCollectionStorage _statCollectionStorage;
        private readonly StatsCollectionDtoStorage _statsCollectionDtoStorage;

        public RemoveStatsCollectionCommand(StatsCollectionStorage statCollectionStorage,
                                            StatsCollectionDtoStorage statsCollectionDtoStorage)
        {
            _statCollectionStorage = statCollectionStorage;
            _statsCollectionDtoStorage = statsCollectionDtoStorage;
        }

        public Task Execute(RemoveStatsCollectionProtocol protocol)
        {
            if (_statCollectionStorage.HasEntity(protocol.Id))
            {
                _statCollectionStorage.Remove(protocol.Id);
            }
            
            if (_statsCollectionDtoStorage.HasEntity(protocol.Id))
            {
                _statsCollectionDtoStorage.Remove(protocol.Id);
            }
            
            return Task.CompletedTask;
        }
    }
}