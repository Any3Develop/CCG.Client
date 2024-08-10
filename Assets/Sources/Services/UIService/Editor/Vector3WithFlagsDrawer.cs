using System.Collections.Generic;
using Services.UIService.Data;
using UnityEditor;

namespace Services.UIService.Editor
{
    [CustomPropertyDrawer(typeof(Vector3WithFlags))]
    public class Vector3WithFlagsDrawer : Vector2WithFlagsDrawer
    {
        protected override IEnumerable<(SerializedProperty flag, SerializedProperty comp)> GetProps(SerializedProperty target)
        {
            var vectorProp = target.FindPropertyRelative(Vector3WithFlags.VectorName);
            yield return (target.FindPropertyRelative(Vector3WithFlags.XFlagName), vectorProp.FindPropertyRelative("x"));
            yield return (target.FindPropertyRelative(Vector3WithFlags.YFlagName), vectorProp.FindPropertyRelative("y"));
            yield return (target.FindPropertyRelative(Vector3WithFlags.ZFlagName), vectorProp.FindPropertyRelative("z"));
        }
    }
}