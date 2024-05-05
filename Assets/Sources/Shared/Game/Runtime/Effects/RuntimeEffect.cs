using Shared.Abstractions.Game.Context.Logic;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Effects;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;
using Shared.Game.Events.Context.Effects;
using Shared.Game.Utils;

namespace Shared.Game.Runtime.Effects
{
    public abstract class RuntimeEffect : IRuntimeEffect
    {
        public EffectData Data { get; private set; }
        public IRuntimeEffectData RuntimeData { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public IRuntimeEffect Init(
            EffectData data,
            IRuntimeEffectData runtimeData,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            EventsSource = eventsSource;
            Initialized = true;
            return this;
        }

        public void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            Data = null;
            RuntimeData = null;
            EventsSource = null;
        }

        public IRuntimeEffect Sync(IRuntimeEffectData runtimeData)
        {
            if (!Initialized)
                return this;
            
            RuntimeData = runtimeData;
            return this;
        }

        public bool IsExecuteAllowed() => true;

        public void Execute()
        {
            if (!Initialized)
                return;
            
            OnBeforeExecute();
            OnAfterExecute();
        }

        public void Expire()
        {
            if (!Initialized)
                return;
            
            OnBeforeExpire();
            OnAfterExpire();
        }

        #region Callbacks

        protected virtual void OnBeforeChanged(bool notify = true) =>
            EventsSource.Publish<EffectBeforeChangedEvent>(Initialized && notify, this);

        protected virtual void OnAfterChanged(bool notify = true) =>
            EventsSource.Publish<EffectAfterChangedEvent>(Initialized && notify, this);

        protected virtual void OnBeforeExecute() =>
            EventsSource.Publish<EffectBeforeExecuteEvent>(Initialized, this);

        protected virtual void OnAfterExecute() =>
            EventsSource.Publish<EffectAfterExecuteEvent>(Initialized, this);

        protected virtual void OnBeforeExpire() =>
            EventsSource.Publish<EffectBeforeExpireEvent>(Initialized, this);

        protected virtual void OnAfterExpire() =>
            EventsSource.Publish<EffectAfterExpireEvent>(Initialized, this);

        #endregion

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}