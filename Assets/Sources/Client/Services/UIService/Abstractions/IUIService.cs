using System.Collections.Generic;
using Client.Services.UIService.Options;
using UnityEngine;

namespace Client.Services.UIService
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
        IEnumerable<IUIWindow> CreateAll(object groupId);

        /// <summary>
        /// Get the instance window by a specific type and a group.
        /// IMPORTANT: If there are several windows of the same type, it will select the first or with matching windowId.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="windowId">Unique identifier of the window. By default skipped.</param>
        /// <param name="groupId">Which group to look for your windows in. By default all the groups.</param>
        /// <returns>An existing instance of the window. Can be null.</returns>
        T Get<T>(string windowId = null, object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Get instances of a window by a specific type and a group.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="groupId">Which group to look for your windows in. By default all the groups.</param>
        /// <returns>Existing window instances.</returns>
        IEnumerable<T> GetAll<T>(object groupId = null) where T : IUIWindow;

        /// <summary>
        /// Try to get a window instance by a specific type and group.
        /// IMPORTANT: If there are several windows of the same type, it will select the first or with matching windowId.
        /// </summary>
        /// <typeparam name="T">You can use an interface, a base class, or a final implementation.</typeparam>
        /// <param name="result">An existing instance of the window.</param>
        /// <param name="windowId">Unique identifier of the window. By default skipped.</param>
        /// <param name="groupId">Which group to look for your windows in. By default all the groups.</param>
        /// <returns>Was the operation successful?</returns>
        bool TryGet<T>(out T result, string windowId = null, object groupId = null) where T : IUIWindow;
        
        /// <summary>
        /// Move a window instance between containers to control the drawing order.
        /// </summary>
        /// <param name="window">Target window instance to move.</param>
        /// <param name="parent">By default, this is the <see cref="IUIRoot.MiddleContainer"/>.</param>
        /// <param name="order">Display order or sibling index. It has the highest priority by default.</param>
        /// <returns>An existing instance of the window.</returns>
        T Move<T>(T window, Transform parent = null, int? order = null) where T : IUIWindow;

        /// <summary>
        /// Start actions open or hide with options.
        /// </summary>
        IUIOptions<IUIWindow> Begin();
        
        /// <summary>
        /// Run open or hide actions with type-specific options.
        /// </summary>
        IUIOptions<T> Begin<T>() where T : IUIWindow;
    }
}