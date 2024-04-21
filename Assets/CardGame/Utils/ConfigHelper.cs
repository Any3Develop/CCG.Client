using System;
using System.IO;
using UnityEngine;

namespace CardGame.Utils
{
    public static class ConfigHelper
    {
        public static T[] Load<T>(string name)
        {
            var textAsset = Resources.Load<TextAsset>(name);
            if (!textAsset)
            {
                throw new NullReferenceException($"Json not loaded : {name}");
            }
            return JsonHelper.FromJson<T>(textAsset.text);
        }
    }
}