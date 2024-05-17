using System;
using Shared.Abstractions.Game.Data;

namespace Shared.Game.Data
{
    public class PlayerData : IData
    {
        public string Id { get; set; }
        public string[] StatIds { get; set; } = Array.Empty<string>();
    }
}