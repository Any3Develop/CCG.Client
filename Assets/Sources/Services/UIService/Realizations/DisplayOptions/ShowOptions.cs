using System;
using System.Threading;
using System.Threading.Tasks;
using Services.UIService.Abstractions;
using Services.UIService.Data;
using UnityEngine;

namespace Services.UIService
{
    public class ShowOptions : IDisplayOption
    {
        private readonly WindowItem windowItem;
        private readonly IUIService uiService;
        private readonly IUIFullFadePresenter fullFade;

        private Transform parent;
        private CancellationToken externalToken;
        private CancellationTokenSource asyncAction;
        private bool noInterrupts;
        private bool noAnimations;
        private int? order;

        public ShowOptions(WindowItem windowItem, IUIService uiService, IUIFullFadePresenter fullFade)
        {
            this.windowItem = windowItem;
            this.uiService = uiService;
            this.fullFade = fullFade;
        }
        
        public IDisplayOption WithParent(Transform value)
        {
            parent = value;
            return this;
        }

        public IDisplayOption WithNoInterrupt()
        {
            noInterrupts = true;
            return this;
        }

        public IDisplayOption WithNoAnimation(bool value = true)
        {
            noAnimations = value;
            return this;
        }

        public IDisplayOption WithOrder(int value)
        {
            order = value;
            return this;
        }

        public IDisplayOption WithToken(CancellationToken value)
        {
            externalToken = value;
            return this;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                if (windowItem?.Window == null)
                    return;

                asyncAction = externalToken.CanBeCanceled
                    ? CancellationTokenSource.CreateLinkedTokenSource(externalToken)
                    : new CancellationTokenSource();

                if (!noInterrupts)
                    windowItem.Previous?.Dispose();

                windowItem.Previous = this;
                windowItem.Window.AnimationSource.Enable(!noAnimations);

                var token = asyncAction.Token;
                var window = windowItem.Window;
                uiService.Move(window, parent, order);

                // always resize to screen size
                window.Parent.offsetMin = Vector2.zero;
                window.Parent.offsetMax = Vector2.zero;

                window.Show();
                fullFade.OnShow(window);
                await window.AnimationSource.ExecuteAsync(nameof(window.Show), token);

                window.Showed();
                await window.AnimationSource.ExecuteAsync(nameof(window.Showed), token);
            }
            catch (OperationCanceledException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                Dispose();
            }
        }

        public void Execute(Action onComplete = null)
        {
            ExecuteAsync().ContinueWith(_ =>
            {
                onComplete?.Invoke();
                Dispose();
            }).GetAwaiter();
        }

        public void Dispose()
        {
            if (windowItem != null && windowItem.Previous == this)
                windowItem.Previous = null;
            
            asyncAction?.Cancel();
            asyncAction?.Dispose();
            asyncAction = null;
            parent = null;
            order = null;
        }
    }
}