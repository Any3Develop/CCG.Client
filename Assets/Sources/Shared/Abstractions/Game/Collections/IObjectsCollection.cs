using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Abstractions.Game.Collections
{
    public interface IObjectsCollection : IRuntimeCollection<IRuntimeObject>
    {
        int GetOccupiedTableSpace(string ownerId);
    }
}