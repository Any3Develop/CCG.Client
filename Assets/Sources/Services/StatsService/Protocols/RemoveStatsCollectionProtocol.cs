using CardGame.Services.CommandService;

namespace CardGame.Services.StatsService
{
    public struct RemoveStatsCollectionProtocol : IProtocol
    {
        public string Id;
    }
}