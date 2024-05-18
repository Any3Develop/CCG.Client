using System.Collections.Generic;
using Server.Domain.Contracts;
using Server.Domain.Entities;

namespace Server.Infrastructure.Persistence
{
    public class AppDbContext : IDbContext
    {
        public HashSet<UserDataEntity> Users { get; set; } = new();
        public HashSet<DeckEntity> Decks { get; set; } = new();
    }
}