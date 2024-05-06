using Shared.Abstractions.Game.Context.EventSource;
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
            
            EventsSource.Publish<BeforeEffectExecutedEvent>(Initialized, this);
            OnBeforeExecute();
            OnAfterExecute();
            EventsSource.Publish<AfterEffectExecutedEvent>(Initialized, this);
        }

        public void Expire()
        {
            if (!Initialized)
                return;
            
            EventsSource.Publish<BeforeEffectExpireEvent>(Initialized, this);
            OnBeforeExpire();
            OnAfterExpire();
            EventsSource.Publish<AfterEffectExpiredEvent>(Initialized, this);
        }

        #region Callbacks

        protected virtual void OnBeforeChanged(bool notify = true) =>
            EventsSource.Publish<BeforeEffectChangedEvent>(Initialized && notify, this);

        protected virtual void OnAfterChanged(bool notify = true) =>
            EventsSource.Publish<AfterEffectChangedEvent>(Initialized && notify, this);

        protected virtual void OnBeforeExecute() {}
        protected virtual void OnAfterExecute() {}
        protected virtual void OnBeforeExpire() {}
        protected virtual void OnAfterExpire() {}

        #endregion

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}