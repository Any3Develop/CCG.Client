using System;
using CardGame.Cards;
using CardGame.Cards.Bootstraps;
using CardGame.ImageRepository;
using CardGame.Services.BootstrapService;
using CardGame.Services.StatsService;
using CardGame.Services.UIService;
using CardGame.UI;
using Zenject;

namespace CardGame.App.Loader
{
    public class CardGameLoader : IInitializable
    {
        private readonly IBootstrap _bootstrap;
        private readonly IImageRepository _imageRepository;
        private readonly IInstantiator _instantiator;
        private readonly IUIService _uiService;

        public CardGameLoader(IBootstrap bootstrap, 
                              IImageRepository imageRepository,
                              IInstantiator instantiator,
                              IUIService uiService)
        {
            _bootstrap = bootstrap;
            _imageRepository = imageRepository;
            _instantiator = instantiator;
            _uiService = uiService;
        }

        public void Initialize()
        {
            _bootstrap.AddCommand(_instantiator.Instantiate<InitStatsCommand>());
            _bootstrap.AddCommand(_instantiator.Instantiate<UISetupCommand>());
            _bootstrap.StartExecute();
            _bootstrap.AllCommandsDone += OnPreLoadInitsCompleteHandler;
        }

        private void Connection()
        {
            var connectionTask = _imageRepository.Connection();
            connectionTask.GetAwaiter().OnCompleted(() =>
            {
                if (!connectionTask.Result)
                {
                    var window = _uiService.Show<UIMessageBox>();
                    window.SetHeaderText("Нет подключения!");
                    window.SetMessageText("Нет подключение к удаленнуму сервису," +
                                          " повторите попытку или зайдите позже.");
                    window.SetAcceptButtonText("Повторить");
                    window.AcceptEvent += (sender, args) =>
                    {
                        _uiService.Hide<UIMessageBox>();
                    };
                    window.HidedEvent += Connection;
                    return;
                }
                
                BootstrapLoad();
            });
        }

        private void BootstrapLoad()
        {
            _bootstrap.AddCommand(_instantiator.Instantiate<InitCardsCommad>(new []{_bootstrap}));
            _bootstrap.AddCommand(_instantiator.Instantiate<StartGameCommand>());
            _bootstrap.StartExecute();
        }
        
        private void OnPreLoadInitsCompleteHandler(object sender, EventArgs e)
        {
            _bootstrap.AllCommandsDone -= OnPreLoadInitsCompleteHandler;
            Connection();
        }
    }
}