using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;

namespace Client.Game.Runtime.Views
{
    public class RuntimeCardView : RuntimeObjectView, IRuntimeCardView
    {
        public new IRuntimeCardModel Model => (IRuntimeCardModel) base.Model;
    }
}