using System;

namespace Core.ArtRepository
{
    public class ArtNotFoundException : Exception
    {
        public string ArtId { get; }

        public ArtNotFoundException(string message, string artId) : base(message)
        {
            ArtId = artId;
        }
    }
}