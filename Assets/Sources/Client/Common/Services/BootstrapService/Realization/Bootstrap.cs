using System;
using UniRx;

namespace CardGame.Services.BootstrapService
{
    public class Bootstrap : ProcessingCommands, IBootstrap
    {
        public event EventHandler<float> ProgressUpdate;

        private ICommand _currentCommand;
        private int _commandsCount;
        private bool _canExecute = true;

        public override void StartExecute()
        {
            UpdateProgress(this,0);
            
            // run command execution
            Execute();
        }

        protected override void Execute()
        {
            if (!_canExecute)
            {
                return;
            }
            
            IsExecuting = true;
            _canExecute = false;
            
            _currentCommand = Dequeue();
            if (_currentCommand == null)
            {
                IsExecuting = false;
                _canExecute = true;
                _commandsCount = 0;
                OnComplete();
            }
            else
            {
                _currentCommand.Done += CurrentCommandOnDone;
                _currentCommand.Do();
            }
        }

        private void CurrentCommandOnDone(object sender, EventArgs e)
        {
            _currentCommand.Done -= CurrentCommandOnDone;
            UpdateProgress(sender,_commandsCount == 0 ? 1 : (1 - (float)Count / _commandsCount));
            _canExecute = true;
            
            // start next command on next frame
            Observable.NextFrame().Subscribe(_ =>
            {
                Execute();
            });
        }

        private void UpdateProgress(object sender, float value)
        {
            ProgressUpdate?.Invoke(sender, value);
        }

        protected override void OnAdded(ICommand cmd)
        {
            _commandsCount++;
        }
    }
}