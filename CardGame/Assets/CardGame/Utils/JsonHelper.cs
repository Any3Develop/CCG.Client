using System;
using UnityEngine;

namespace CardGame.Utils
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            return JsonUtility.FromJson<Wrapper<T>>(json).Items;
        }

        public static string ToJson<T>(T[] array)
        {
            return JsonUtility.ToJson(new Wrapper<T> {Items = array});
        }      
        
        public static string ToJson<T>(T item, bool prettyPrint = false)
        {
            return JsonUtility.ToJson(new Wrapper<T> {Items = new []{item}}, prettyPrint);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            return JsonUtility.ToJson(new Wrapper<T> {Items = array}, prettyPrint);
        }

    }
    
    [Serializable]
    public class Wrapper<T>
    {
        public T[] Items;
    }
}