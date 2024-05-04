using System;
using Client.Game.Abstractions.Collections;
using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;
using Client.Game.Runtime.Views;
using Shared.Game.Data.Enums;

namespace Client.Game.Factories
{
    public class RuntimeObjectViewFactory : IRuntimeObjectViewFactory
    {
        private readonly IObjectViewCollection objectViewCollection;

        public RuntimeObjectViewFactory(IObjectViewCollection objectViewCollection)
        {
            this.objectViewCollection = objectViewCollection;
        }

        public IRuntimeObjectView Create(IRuntimeObjectModel runtimeModel)
        {
            if (objectViewCollection.TryGet(runtimeModel.Id, out var view))
            {
                view.Setup(runtimeModel);
                return view;
            }

            view = runtimeModel.Data.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardView(),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {runtimeModel.Data.Type}")
            };

            view.Setup(runtimeModel);
            objectViewCollection.Add(view);
            return view;
        }
    }
}