namespace Server.Domain.Entities
{
    public class UserDataEntity : EntityBase
    {
        public string AccessToken { get; set; }
        public string DeckId { get; set; }
    }
}