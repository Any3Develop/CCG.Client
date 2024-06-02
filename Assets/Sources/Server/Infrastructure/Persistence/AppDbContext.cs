using System.Collections.Generic;
using Server.Domain.Contracts.Persistence;
using Server.Domain.Entities;

namespace Server.Infrastructure.Persistence
{
    public class AppDbContext : IDbContext
    {
        public HashSet<UserDataEntity> Users { get; } = new();
        public HashSet<DeckEntity> Decks { get; } = new();
    }
}