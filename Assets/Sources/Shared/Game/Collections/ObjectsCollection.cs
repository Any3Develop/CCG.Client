using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Collections
{
    public class ObjectsCollection : RuntimeCollection<IRuntimeObject>, IObjectsCollection {}
}