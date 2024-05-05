using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;
using Client.Game.Runtime.Models;

namespace Client.Game.Runtime.Views
{
    public class RuntimeObjectView : RuntimeView, IRuntimeObjectView
    {
        public TransformModel Previous { get; private set; }
        public TransformModel Transform { get; private set; } = TransformModel.Default();
        public new IRuntimeObjectModel Model => (IRuntimeObjectModel)base.Model;
        
        public void SetTransform(TransformModel value)
        {
            Previous = Transform;
            Transform = value;
            OnTransfromChanged();
        }

        protected virtual void OnTransfromChanged(){}
    }
}