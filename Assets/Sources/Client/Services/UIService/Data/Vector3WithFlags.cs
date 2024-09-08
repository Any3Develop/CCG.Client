using System;
using UnityEngine;

namespace Client.Services.UIService.Data
{
    [Serializable]
    public struct Vector3WithFlags
    {
        public static string XFlagName => nameof(useX);
        public static string YFlagName => nameof(useY);
        public static string ZFlagName => nameof(useZ);
        public static string VectorName => nameof(vector);

        public bool useX;
        public bool useY;
        public bool useZ;
        public Vector3 vector;

        public Vector3 GetAllowed(Vector3 defaultVector)
        {
            return new Vector3
            {
                x = useX ? vector.x : defaultVector.x,
                y = useY ? vector.y : defaultVector.y,
                z = useZ ? vector.z : defaultVector.z,
            };
        }
    }
}