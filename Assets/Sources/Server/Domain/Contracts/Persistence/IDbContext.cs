using System.Collections.Generic;
using Server.Domain.Entities;

namespace Server.Domain.Contracts.Persistence
{
    public interface IDbContext
    {
        HashSet<UserDataEntity> Users { get; }
        HashSet<DeckEntity> Decks { get; }
    }
}