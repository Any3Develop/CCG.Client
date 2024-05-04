using UnityEngine;

namespace Client.Game.Runtime.Models
{
    public struct RuntimeTransform
    {
        public Vector3? Position { get; set; }
        public Vector3? Scale { get; set; }
        public Quaternion? Rotation { get; set; }

        public static RuntimeTransform Default()
        {
            return new RuntimeTransform
            {
                Position = Vector3.zero,
                Scale = Vector3.one,
                Rotation = Quaternion.identity,
            };
        }
    }
}