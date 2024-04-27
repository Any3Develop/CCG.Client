using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Runtime.Objects;

namespace Demo.Core.Game.Collections
{
    public class ObjectsCollection : RuntimeCollection<IRuntimeObject>, IObjectsCollection {}
}