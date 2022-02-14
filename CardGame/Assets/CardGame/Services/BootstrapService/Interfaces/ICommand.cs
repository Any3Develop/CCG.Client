using System;

namespace CardGame.Services.BootstrapService
{
    public interface ICommand
    {
        event EventHandler Done;
        void Do();
    }
}