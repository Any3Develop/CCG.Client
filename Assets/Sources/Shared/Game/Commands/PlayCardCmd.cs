using System;
using Shared.Abstractions.Game.Runtime.Cards;
using Shared.Game.Commands.Base;
using Shared.Game.Commands.Models;
using Shared.Game.Data.Enums;
using Shared.Game.Exceptions;

namespace Shared.Game.Commands
{
    public class PlayCardCmd : Command<PlayCardModel>
    {
        protected override void OnExecute()
        {
            if (!Context.PlayersCollection.Contains(ExecutorId))
                throw new NullReferenceException("Player who executed the command was found.");
            
            if (!Context.ObjectsCollection.TryGet<IRuntimeCard>(Model.Id, out var runtimeCard))
                throw new NullReferenceException($"Requested card with id {Model.Id} not found.");

            if (Context.ObjectsCollection.GetOccupiedTableSpace(ExecutorId) >= Context.Config.MaxInTableCount)
                throw new NotEnoughTableSpaceException();
            
            runtimeCard.SetPosition(Model.Position);
            runtimeCard.SetState(ObjectState.InTable);
        }
    }
}