using System.Collections.Generic;
using Demo.Core.Game.Data;
using Demo.Core.Game.Enums;

namespace Demo.Core.Abstractions.Game.Data
{
    public interface ICardData : IDatabase
    {
        ObjectType Type { get; }
        string Title { get; }
        List<StatData> Stats { get; }
    }
}