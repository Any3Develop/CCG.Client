using System.Collections.Generic;
using Shared.Game.Data;
using Shared.Game.Data.Enums;

namespace Client.Game.Abstractions.Runtime.Models
{
    public interface IRuntimeObjectModel : IRuntimeModel
    {
        ObjectData Data { get; }
        List<IRuntimeStatModel> Stats { get; }
        ObjectState PreviousState { get; set; }
        ObjectState State { get; set; }
    }
}