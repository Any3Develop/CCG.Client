using System;
using Client.Common.Abstractions.DependencyInjection;
using Client.Game.Abstractions.Collections;
using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;
using Shared.Abstractions.Game.Collections;
using Shared.Game.Data.Enums;

namespace Client.Game.Factories
{
    public class RuntimeEffectViewFactory : IRuntimeEffectViewFactory
    {
        private readonly IAbstractFactory abstractFactory;
        private readonly IEffectViewCollection effectViewCollection;
        private readonly ITypeCollection<VisualId> visualTypeCollection;

        public RuntimeEffectViewFactory(
            IAbstractFactory abstractFactory,
            IEffectViewCollection effectViewCollection,
            ITypeCollection<VisualId> visualTypeCollection)
        {
            this.abstractFactory = abstractFactory;
            this.effectViewCollection = effectViewCollection;
            this.visualTypeCollection = visualTypeCollection;
        }

        public IRuntimeEffectView Create(IRuntimeEffectModel runtimeModel)
        {
            if (effectViewCollection.TryGet(runtimeModel.Id, out var view))
            {
                view.Setup(runtimeModel);
                return view;
            }

            if (!visualTypeCollection.TryGet(runtimeModel.Data.VisualId, out var type))
                throw new NullReferenceException($"{nameof(Type)} with {nameof(VisualId)} {runtimeModel.Data.VisualId}, not found in {nameof(ITypeCollection<VisualId>)}");

            view = (IRuntimeEffectView)abstractFactory.Instantiate(type);
            view.Setup(runtimeModel);
            effectViewCollection.Add(view);
            return view;
        }
    }
}