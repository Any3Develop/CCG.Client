using System;
using UnityEngine;

namespace Services.UIService.Abstractions
{
    public interface IUIWindow
    {
        /// <summary>
        /// Use to handle when the window changed.
        /// </summary>
        event Action OnChanged;
        
        /// <summary>
        /// Use to control animations in the window.
        /// </summary>
        IUIAnimationSource AnimationSource { get; }

        /// <summary>
        /// Use to control audios in the window.
        /// </summary>
        IUIAudioSource AudioSource { get; }

        /// <summary>
        /// The main window container where the component is placed.
        /// </summary>
        RectTransform Parent { get; }

        /// <summary>
        /// Contents / Graphics container.
        /// </summary>
        RectTransform Content { get; }

        /// <summary>
        /// As the window began to show.
        /// Used only through the <see cref="IUIService"/>
        /// <para><see cref="IUIService.ShowAsync()"/></para>
        /// <para><see cref="IUIService.Show()"/></para>
        /// </summary>
        void Show();

        /// <summary>
        /// When the window was completely showed.
        /// Used only through the <see cref="IUIService"/>
        /// <para><see cref="IUIService.ShowAsync()"/></para>
        /// <para><see cref="IUIService.Show()"/></para>
        /// </summary>
        void Showed();
        
        /// <summary>
        /// As the window began to hide.
        /// Used only through the <see cref="IUIService"/>
        /// <para><see cref="IUIService.HideAsync()"/></para>
        /// <para><see cref="IUIService.Hide()"/></para>
        /// </summary>
        void Hide();
        
        /// <summary>
        /// When the window was completely hidden.
        /// Used only through the <see cref="IUIService"/>
        /// <para><see cref="IUIService.HideAsync()"/></para>
        /// <para><see cref="IUIService.Hide()"/></para>
        /// </summary>
        void Hidden();
    }
}