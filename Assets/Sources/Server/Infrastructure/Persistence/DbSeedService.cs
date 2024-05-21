using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Domain.Contracts.Persistence;
using Server.Domain.Contracts.Sessions;
using Server.Domain.Entities;
using Shared.Abstractions.Game.Context;
using Shared.Game.Data;
using Shared.Game.Data.Enums;

namespace Server.Infrastructure.Persistence
{
    public class DbSeedService : IDbSeedService
    {
        private readonly IDbContext dbContext;
        private readonly ISharedConfig sharedConfig;
        private readonly ISessionFactory sessionFactory;
        private readonly IRuntimeSessionRepository sessionRepository;
        private readonly IDatabase database;

        public DbSeedService(
            IDbContext dbContext,
            ISharedConfig sharedConfig,
            ISessionFactory sessionFactory,
            IRuntimeSessionRepository sessionRepository,
            IDatabase database)
        {
            this.dbContext = dbContext;
            this.sharedConfig = sharedConfig;
            this.sessionFactory = sessionFactory;
            this.sessionRepository = sessionRepository;
            this.database = database;
        }
        
        public Task SeedAsync()
        {
            SeedRuntimeDatabase();
            dbContext.Decks.Add(new DeckEntity
            {
                Id = "Default-Deck-1",
                CardIds = database.Objects.Select(x=> x.Id).ToArray()
            });
            
            dbContext.Decks.Add(new DeckEntity
            {
                Id = "Default-Deck-2",
                CardIds = database.Objects.Select(x=> x.Id).Reverse().ToArray()
            });
            
            dbContext.Users.Add(new UserDataEntity
            {
                AccessToken = "Player-1-token",
                Id = "Player-1",
                DeckId = dbContext.Decks.First().Id,
            });
            
            dbContext.Users.Add(new UserDataEntity
            {
                AccessToken = "Player-2-token",
                Id = "Player-2",
                DeckId = dbContext.Decks.Last().Id,
            });


            var session = sessionFactory.Create(Guid.NewGuid().ToString());
            sessionRepository.Add(session);
            session.Build(dbContext.Users.Select(x=> new SessionPlayer
            {
                Id = x.Id,
                DeckId = x.DeckId,
                DeckCards = dbContext.Decks.First(d => d.Id == x.DeckId).CardIds.ToArray()
            }));
            return Task.CompletedTask;
        }

        private void SeedRuntimeDatabase()
        {
            var random = new Random();
            database.Objects.AddRange(Enumerable.Range(0, sharedConfig.MaxInDeckCount).Select(id =>
            {
                return new CardData
                {
                    Id = id.ToString(),
                    Title = $"Card-Title-{id}",
                    ArtId = $"Card-Art-{id}",
                    Type = ObjectType.Creature,
                    StatIds = new[] {$"card-stat-cost-{id}", $"card-stat-hp-{id}", $"card-stat-attack-{id}"},
                    EffectIds = Array.Empty<string>()
                };
            }));
            
            database.Stats.AddRange(Enumerable.Range(0, sharedConfig.MaxInDeckCount).SelectMany(id =>
            {
                var costMax = random.Next(0, 10);
                var hpMax = random.Next(1, 10);
                var attackMax = random.Next(1, 10);
                return new[]
                {
                    new StatData
                    {
                        Id = $"card-stat-cost-{id}",
                        Max = costMax,
                        Value = costMax
                    },
                    new StatData
                    {
                        Id = $"card-stat-hp-{id}",
                        Max = hpMax,
                        Value = hpMax
                    },
                    new StatData
                    {
                        Id = $"card-stat-attack-{id}",
                        Max = attackMax,
                        Value = attackMax
                    }
                };
            }));
            
            database.Stats.Add(new StatData
            {
                Id = $"player-default-cost",
                Max = 99,
                Value = 1
            });
            
            database.Players.Add(new PlayerData
            {
                Id = $"player-default",
                StatIds = new []{"player-default-cost"}
            });
        }
    }
}