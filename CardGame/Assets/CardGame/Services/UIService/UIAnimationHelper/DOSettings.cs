using System;
using DG.Tweening;

namespace CardGame.Services.UIService
{
    [Serializable]
    public struct DOSettings
    {
        public float durationIn;
        public float durationOut;
        public Ease easeIn;
        public Ease easeOut;
        public float delay;
    }
}