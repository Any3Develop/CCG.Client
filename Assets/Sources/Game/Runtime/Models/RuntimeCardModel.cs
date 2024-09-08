using Client.Game.Abstractions.Runtime.Models;
using Shared.Game.Data;

namespace Client.Game.Runtime.Models
{
    public class RuntimeCardModel : RuntimeObjectModel, IRuntimeCardModel
    {
        public new CardData Data
        {
            get => (CardData) base.Data;
            set => base.Data = value;
        }
        
        public int? Position { get; set; }
    }
}