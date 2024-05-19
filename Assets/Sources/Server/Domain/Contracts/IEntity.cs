namespace Server.Domain.Contracts
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}