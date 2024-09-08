#if DOTWEEN
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Sources.Client.Services.UIService.Editor
{
    /// <summary>
    /// Prevent multiply selection changes by automatically
    /// </summary>
    [CustomPropertyDrawer(typeof(Ease), true)]
    public class EaseSelectorDrawer : PropertyDrawer
    {
        private List<(string Name, string FullName)> options;
        private Type baseType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            var changes = EditorGUI.EnumPopup(position, label, (Ease) property.enumValueIndex);
            property.enumValueIndex = changes is Ease ease ? (int) ease : 0;
            EditorGUI.EndProperty();
        }
    }
}
#endif