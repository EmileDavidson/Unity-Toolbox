using UnityEditor;
using UnityEngine;

namespace Toolbox.Attributes
{
    [CustomPropertyDrawer(typeof (ColoredHeaderAttribute))]
    internal sealed class HeaderDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            if (ColoredAttribute == null) return;
            
            position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;

            GUIStyle style = new GUIStyle();
            style.normal.textColor = ColoredAttribute.Color;
            style.fontStyle = FontStyle.Bold;
            
            GUI.Label(position, ColoredAttribute.Label , style);
        }

        public override float GetHeight() => EditorGUIUtility.singleLineHeight * 1.5f;
        private ColoredHeaderAttribute ColoredAttribute => attribute as ColoredHeaderAttribute;
    }
}