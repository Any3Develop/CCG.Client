using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;
using Client.Game.Runtime.Models;

namespace Client.Game.Runtime.Views
{
    public class RuntimeObjectView : RuntimeView, IRuntimeObjectView
    {
        public RuntimeTransform Transform { get; private set; } = RuntimeTransform.Default();
        public new IRuntimeObjectModel Model => (IRuntimeObjectModel)base.Model;
        
        public void SetTransform(RuntimeTransform value)
        {
            Transform = value;
            OnTransfromChanged();
        }

        protected virtual void OnTransfromChanged(){}
    }
}