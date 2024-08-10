using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.UIService.Abstractions;

namespace Services.UIService
{
    public class UIAnimationSource : IUIAnimationSource
    {
        protected IUIWindow Window { get; private set; }
        protected bool Initialized { get; private set; }
        
        public bool Enabled { get; private set; }
        public IEnumerable<IUIAnimation> Animations { get; private set; }
        
        public IUIAnimationSource Init(IUIWindow window, IEnumerable<IUIAnimation> animations)
        {
            Window = window;
            Animations = animations;
            Enabled = Animations.Any(x => x.Enabled);
            Initialized = true;
            return this;
        }
        
        public void Dispose()
        {
            if (!Initialized)
                return;
            
            OnDisposed();
            Initialized = false;
        }

        public void Enable(bool value)
        {
            if (value == Enabled || !Initialized)
                return;

            Enabled = value;
            foreach (var animation in Animations)
            {
                if (Enabled)
                {
                    animation.Enable();
                    continue;
                }
                
                animation.Disable();
            }

            if (Enabled)
            {
                OnEnabled();
                return;
            }
            
            OnDisabled();
        }

        public virtual async Task ExecuteAsync(object id, CancellationToken token)
        { 
            token.ThrowIfCancellationRequested();
            await Task.WhenAll(Animations.SelectMany(x => GetAnimations(x, id, token)));
            token.ThrowIfCancellationRequested();
        }
        
        protected virtual IEnumerable<Task> GetAnimations(IUIAnimation animation, object id, CancellationToken token)
        {
            if (!animation.Enabled)
                yield break;
            
            if (animation.Configuration.HasStopEvent(id))
                yield return animation.StopAsync(token);

            if (animation.Configuration.HasResetEvent(id))
                yield return animation.ResetAsync(token);

            if (animation.Configuration.HasPlayEvent(id))
                yield return animation.PlayAsync(token);
        }

        protected virtual void OnEnabled() {}
        protected virtual void OnDisabled() {}
        protected virtual void OnDisposed() {}
    }
}