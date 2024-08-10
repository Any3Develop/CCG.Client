using System;
using UnityEngine;

namespace Services.UIService.Data
{
    [Serializable]
    public struct Vector2WithFlags
    {
        public static string XFlagName => nameof(useX);
        public static string YFlagName => nameof(useY);
        public static string VectorName => nameof(vector);

        public bool useX;
        public bool useY;
        public Vector2 vector;
        
        public Vector3 GetAllowed(Vector3 defaultVector)
        {
            return new Vector3
            {
                x = useX ? vector.x : defaultVector.x,
                y = useY ? vector.y : defaultVector.y,
            };
        }
    }
}