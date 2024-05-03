using System;

namespace CardGame.Services.BootstrapService
{
    public interface IBootstrap
    {
        /// <summary>
        /// 0...1
        /// Where 1 is completed 
        /// </summary>
        event EventHandler<float> ProgressUpdate;
        event EventHandler AllCommandsDone;
        int Count { get; }
        void StartExecute();
        void AddCommand(ICommand cmd);
    }
}