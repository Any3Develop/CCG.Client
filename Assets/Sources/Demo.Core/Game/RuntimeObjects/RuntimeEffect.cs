using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Events.Effects;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.RuntimeObjects
{
    public abstract class RuntimeEffect : IRuntimeEffect
    {
        public EffectData Data { get; private set; }
        public IRuntimeEffectData RuntimeData { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public void Init(
            EffectData data,
            IRuntimeEffectData runtimeData,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            EventsSource = eventsSource;
            Initialized = true;
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

        public void Sync(IRuntimeEffectData runtimeData, bool notify = true)
        {
            OnBeforeChanged(notify);
            RuntimeData = runtimeData;
            OnAfterChanged(notify);
        }

        public bool IsExecuteAllowed() => true;

        public void Execute()
        {
            OnBeforeExecute();
            OnAfterExecute();
        }

        public void Expire()
        {
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

        IRuntimeData IRuntimeObjectBase.RuntimeData => RuntimeData;
        IData IRuntimeObjectBase.Data => Data;

        #endregion
    }
}