using System;
using UnityEngine;

#if ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif

namespace Services.UIService.Data
{
    [Serializable]
    public struct UIAudioClipData
    {
        [SerializeField, Tooltip("Direct reference of an audio clip from project.")] public AudioClip clip;
        [SerializeField, Tooltip("Use it when wants to control an asset external.")] public string clipId;
#if ADDRESSABLES
        [SerializeField, Tooltip("Use it when wants to control an asset external.")] public AssetReferenceT<AudioClip> clipReference;
#endif

        public bool IsEmpty() => !clip
                                 && string.IsNullOrEmpty(clipId)
#if ADDRESSABLES
                                 && string.IsNullOrEmpty(clipReference.AssetGUID);
#else
                                ;
#endif
    }
}