using UnityEngine;

namespace Client.Services.UIService
{
    public interface IUIRoot
    {
        /// <summary>
        /// Camera which render UI layers.
        /// </summary>
        Camera UICamera { get; }
        /// <summary>
        /// Main canvas
        /// </summary>
        Canvas UICanvas { get; }

        /// <summary>
        /// All containers are children safe area, use this container to correct screen area.
        /// </summary>
        RectTransform SafeArea { get; }
        /// <summary>
        /// Temporary pool windows or UI elements, usually it isn't visible.
        /// </summary>
        RectTransform PoolContainer { get; }
        /// <summary>
        /// Usually when window hided it just moved here and it isn't visible.
        /// </summary>
        RectTransform DeactivatedContainer { get; }
        
        /// <summary>
        /// The highest layer of UI, usually when window showed it just moved here and it is visible.
        /// </summary>
        RectTransform TopContainer { get; }
        /// <summary>
        /// The middle layer of UI, usually when window showed it just moved here and it is visible.
        /// </summary>
        RectTransform MiddleContainer { get; }
        /// <summary>
        /// The lowest layer of UI, usually when window showed it just moved here and it is visible.
        /// </summary>
        RectTransform ButtomContainer { get; }
    }
}