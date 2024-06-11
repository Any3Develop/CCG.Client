using System;

namespace Client.Common.Services.UIService.Animations
{
    [Flags, Serializable]
    public enum UIAnimationTrigger
    {
        Show = 2,
        Showed = 4,
        Hide = 8,
        Hided = 16,
    }
}