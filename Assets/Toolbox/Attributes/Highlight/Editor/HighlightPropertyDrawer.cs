using UnityEditor;
using UnityEngine;

namespace Toolbox.Attributes
{
    [CustomPropertyDrawer(typeof(HighlightAttribute))]
    public class HighlightPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Color oldColor = GUI.color;

            if (HighlightAttribute != null) GUI.color = HighlightAttribute.Color;
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, position.height), property, label);
            GUI.color = oldColor;
        }
        
        private HighlightAttribute HighlightAttribute => attribute as HighlightAttribute;
    }
}