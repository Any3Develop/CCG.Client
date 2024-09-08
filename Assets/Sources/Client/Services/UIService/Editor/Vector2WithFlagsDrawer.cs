using System.Collections.Generic;
using Client.Services.UIService.Data;
using UnityEditor;
using UnityEngine;

namespace Sources.Client.Services.UIService.Editor
{
    [CustomPropertyDrawer(typeof(Vector2WithFlags))]
    public class Vector2WithFlagsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            
            EditorGUI.BeginProperty(position, label, property);
            EditorGUILayout.BeginVertical();
            EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));
            EditorGUI.indentLevel++;
            
            foreach (var (flag, vector) in GetProps(property))
                Draw(flag, vector);
            
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            EditorGUI.EndProperty();
            
            return;
            void Draw(SerializedProperty flag, SerializedProperty vector)
            {
                EditorGUILayout.BeginHorizontal();
                flag.boolValue = EditorGUILayout.Toggle(flag.boolValue, GUILayout.Width(28), GUILayout.ExpandWidth(false));
                vector.floatValue = EditorGUILayout.FloatField(flag.displayName, vector.floatValue);
                EditorGUILayout.EndHorizontal();
            }
        }

        protected virtual IEnumerable<(SerializedProperty flag, SerializedProperty comp)> GetProps(SerializedProperty target)
        {
            var vectorProp = target.FindPropertyRelative(Vector2WithFlags.VectorName);
            yield return (target.FindPropertyRelative(Vector2WithFlags.XFlagName), vectorProp.FindPropertyRelative("x"));
            yield return (target.FindPropertyRelative(Vector2WithFlags.YFlagName), vectorProp.FindPropertyRelative("y"));
        }
    }
}