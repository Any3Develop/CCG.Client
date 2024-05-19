using Server.Domain.Contracts;

namespace Server.Domain.Entities
{
    public abstract class EntityBase : IEntity<string>
    {
        public string Id { get; set; }
    }
}