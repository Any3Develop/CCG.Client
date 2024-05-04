using System;
using CardGame.App;
using CardGame.Cards.Bootstraps;
using CardGame.Services.BootstrapService;
using CardGame.Services.StatsService;
using CardGame.UI;
using Zenject;

namespace Startup
{
    public class StartupApplication : IInitializable
    {
        private readonly IBootstrap bootstrap;
        private readonly IInstantiator instantiator;

        public StartupApplication(
            IBootstrap bootstrap,
            IInstantiator instantiator)
        {
            this.bootstrap = bootstrap;
            this.instantiator = instantiator;
        }

        public void Initialize()
        {
            bootstrap.AddCommand(instantiator.Instantiate<InitStatsCommand>());
            // bootstrap.AddCommand(instantiator.Instantiate<UISetupCommand>());
            bootstrap.StartExecute();
            bootstrap.AllCommandsDone += OnPreLoadInitsCompleteHandler;
        }

        private void OnPreLoadInitsCompleteHandler(object sender, EventArgs e)
        {
            bootstrap.AllCommandsDone -= OnPreLoadInitsCompleteHandler;
            bootstrap.AddCommand(instantiator.Instantiate<InitCardsCommad>(new[] {bootstrap}));
            bootstrap.AddCommand(instantiator.Instantiate<StartGameCommand>());
            bootstrap.StartExecute();
        }
    }
}