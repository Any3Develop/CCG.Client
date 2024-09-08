using System;
using UnityEngine;

namespace Client.Services.UIService
{
    public static class UIAnimationExtensions
    {
	    public const string SHOW_EVENT = "Show";
	    public const string SHOWED_EVENT = "Showed";
	    public const string HIDE_EVENT = "Hide";
	    public const string HIDDEN_EVENT = "Hidden";
	    
	    public static AnimationCurve MirrorHorizontally(this AnimationCurve curve, bool center = false)
        {
            if (curve == null || curve.length == 0)
                return curve;

            var keys = curve.keys;
            var maxTime = keys[^1].time;
        
            for (var i = 0; i < keys.Length; i++)
            {
                keys[i].time = maxTime - keys[i].time;
                keys[i].inTangent = -keys[i].inTangent;
                keys[i].outTangent = -keys[i].outTangent;
            }

            Array.Sort(keys, (a, b) => a.time.CompareTo(b.time));

            var mirroredCurve = new AnimationCurve(keys);
            return mirroredCurve;
        }

        public static AnimationCurve MirrorVertically(this AnimationCurve curve, bool center = false)
        {
            if (curve == null || curve.length == 0)
                return curve;

            var keys = curve.keys;
        
            for (var i = 0; i < keys.Length; i++)
            {
                keys[i].value = -keys[i].value + (center ? 1 : 0);
                keys[i].inTangent = -keys[i].inTangent;
                keys[i].outTangent = -keys[i].outTangent;
            }

            var mirroredCurve = new AnimationCurve(keys);
            return mirroredCurve;
        }
    }
}