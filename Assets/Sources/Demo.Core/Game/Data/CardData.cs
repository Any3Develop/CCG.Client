using System.Collections.Generic;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Game.Enums;

namespace Demo.Core.Game.Data
{
    public class CardData : ICardData
    {
        public string Id { get; set; }
        public ObjectType Type { get; set; }
        public string Title { get; set; }
        public List<StatData> Stats { get; set; } = new();
    }
}