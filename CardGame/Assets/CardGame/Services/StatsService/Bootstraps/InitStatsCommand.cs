using CardGame.Services.BootstrapService;
using CardGame.Services.StatsService.Modifiers;
using CardGame.Services.TypeRegistryService;

namespace CardGame.Services.StatsService
{
    public class InitStatsCommand : Command
    {
        private readonly TypeRegistry _typeRegistry;

        public InitStatsCommand(TypeRegistry typeRegistry)
        {
            _typeRegistry = typeRegistry;
        }
        public override void Do()
        {
            _typeRegistry.Registry(nameof(Stat), 
                                  nameof(StatModifiable), 
                                  nameof(StatAttribute), 
                                  nameof(StatVital),
                                  nameof(StatDto),
                                  nameof(StatModifiableDto),
                                  nameof(StatAttributeDto),
                                  nameof(StatVitalDto),
                                  nameof(StatModBaseAdd),
                                  nameof(StatModBasePercent), 
                                  nameof(StatModTotalAdd), 
                                  nameof(StatModTotalPercent),
                                  nameof(StatLinker));
            OnDone();
        }
    }
}