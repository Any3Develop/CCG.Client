// using CardGame.App;
using CardGame.Services.BootstrapService;
using CardGame.Services.TypeRegistryService;
using Zenject;

namespace CardGame.Cards.Bootstraps
{
    public class InitCardsCommad : Command
    {
        private readonly TypeRegistry _typeStorage;

        public InitCardsCommad(IBootstrap bootstrap,
                               IInstantiator instantiator,
                               TypeRegistry typeStorage)
        {
            _typeStorage = typeStorage;
            // bootstrap.AddCommand(instantiator.Instantiate<InitModelsCommand<CardModel>>());
            // bootstrap.AddCommand(instantiator.Instantiate<InitModelsCommand<CardStatModel>>());
        }
    }
}