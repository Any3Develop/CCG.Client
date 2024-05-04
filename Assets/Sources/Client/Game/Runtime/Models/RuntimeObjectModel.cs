using System.Collections.Generic;
using Client.Game.Abstractions.Runtime.Models;
using Shared.Game.Data;
using Shared.Game.Data.Enums;

namespace Client.Game.Runtime.Models
{
    public class RuntimeObjectModel : RuntimeModel, IRuntimeObjectModel
    {
        public ObjectData Data { get; set; }
        public List<IRuntimeStatModel> Stats { get; set; } = new();
        public ObjectState PreviousState { get; set; }
        public ObjectState State { get; set; }
    }
}