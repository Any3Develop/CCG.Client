using System.Collections.Generic;

namespace CardGame.Services.StatsService
{
    public interface IStatLinkable
    {
        float LinkerValue { get; }

        void AddLinker(IStatLinker linker);

        void RemoveLinker(IStatLinker linker);
        
        void ClearLinkers();
        
        IEnumerable<IStatLinker> GetLinkers();
        
        IEnumerable<IStatLinker> GetLinkers(string linkerId);
        void UpdateLinkers();
    }
}