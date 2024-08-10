using System.Collections.Generic;
using UnityEngine;

namespace Services.UIService.Abstractions
{
    public interface IUIService
    {
        /// <summary>
        /// Default window group identifier.
        /// </summary>
        object DefaultGroupId { get; }
        
        /// <summary>
        /// Deleting a window instance from the service.
        /// </summary>
        void Destroy(IUIWindow window);

        /// <summary>
        /// Deleting all or a group of windows by group ID.
        /// </summary>
        /// <param name="groupId">Which group to look for your windows in. By default all the groups.</param>
        void DestroyAll(object groupId = null);

        /// <summary>
        /// Creating a window on a specific type of implementation with default or a specific group.
        /// </summary>
        /// <param name="groupId">Specify in which group to find the window prototype.</param>
        /// <typeparam name="T">Use the implementation type for correct operation, unpredictable behaviour is possible with interfaces.</typeparam>
        /// <returns>An instance of the window.</returns>
        T Create<T>(object groupId) where T : IUIWindow;

        /// <summary>
        /// Create a group of windows by group ID.
        /// </summary>
        /// <param name="groupId">In which group to create windows. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        /// <returns>Created window group.</returns>
        IEnumerable<IUIWindow> Create(object groupId);

        /// <summary>
        /// Get the instance window by a specific type and a group.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="groupId">Which group to look for your window in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        /// <returns>An existing instance of the window.</returns>
        T Get<T>(object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Get instances of a window by a specific type and a group.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="groupId">Which group to look for your windows in. By default all the groups.</param>
        /// <returns>Existing window instances.</returns>
        IEnumerable<T> GetAll<T>(object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Try to get a window instance by a specific type and group.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="result">An existing instance of the window.</param>
        /// <param name="groupId">Which group to look for your window in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        /// <returns>Was the operation successful?</returns>
        bool TryGet<T>(out T result, object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Move a window instance by a specific type and group between containers to control the drawing order.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="parent">The method moves to the specified parent at runtime. By default, this is the middle container.</param>
        /// <param name="order">Custom order. It has the highest priority by default.</param>
        /// <param name="groupId">Which group to look for your window in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        /// <returns>An existing instance of the window.</returns>
        T Move<T>(Transform parent, int? order = null, object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Move a window instance with a specific type and group between containers to control the drawing order.
        /// </summary>
        /// <param name="window">Target window instance to move.</param>
        /// <param name="parent">The method moves to the specified parent at runtime. By default, this is the middle container.</param>
        /// <param name="order">Custom order. It has the highest priority by default.</param>
        /// <returns>An existing instance of the window.</returns>
        T Move<T>(T window, Transform parent = null, int? order = null) where T : IUIWindow;

        /// <summary>
        /// Show a window instance of a specific type and group into the specified container, the window becomes visible.
        /// </summary>
        /// <typeparam name="T">The specific type of <see cref="IUIWindow"/> implementation.</typeparam>
        /// <param name="groupId">Which group to look for your window in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        IDisplayOption Show<T>(object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Show windows with group id, the windows becomes visible.
        /// </summary>
        /// <param name="groupId">Which group to look for your windows in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        IDisplayOption Show(object groupId);

        /// <summary>
        /// Hides the window instance by a specific type and group, the window becomes invisible.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="groupId">Which group to look for your window in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        IDisplayOption Hide<T>(object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Hides the windows by group id, the windows becomes invisible.
        /// </summary>
        /// <param name="groupId">Which group to look for your windows in. If it's empty <see cref="IUIService"/>.<see cref="IUIService.DefaultGroupId"/></param>
        IDisplayOption Hide(object groupId);
    }
}