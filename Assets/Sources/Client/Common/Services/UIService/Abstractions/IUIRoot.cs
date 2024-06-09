using UnityEngine;

namespace Client.Common.Services.UIService
{
    public interface IUIRoot
    {
        Camera UICamera { get; }
        Canvas UICanvas { get; }

        RectTransform SafeArea { get; }
        RectTransform PoolContainer { get; }
        RectTransform DeactivatedContainer { get; }
        
        RectTransform TopContainer { get; }
        RectTransform MiddleContainer { get; }
        RectTransform ButtomContainer { get; }
    }
}