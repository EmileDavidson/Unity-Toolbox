using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Toolbox.Attributes
{
    [CustomPropertyDrawer(typeof (DropdownAttribute))]
    internal sealed class DropdownPropertyDrawer : PropertyDrawerBase
    {
        protected override float GetPropertyHeightSafe(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeightSafe(property, label) + 20;
        }

        protected override void OnGUISafe(Rect position, SerializedProperty property, GUIContent label)
        {
         //TODO: MAKE IT SO WHEN THIS ATTRIBUTE IS ADDED THERE IS A BUTTON ON TOP OF PROPERTY ' DISPLAY / HIDE '
         // IF THE ATTRIBUTE.DROPPED VALUE IS TRUE WE WANT TO DISPLAY PROPERTY IF NOT WE WANT TO HIDE IT AND ONLY SHOW THE BUTTON 
        }
        
        public override bool IsPropertyValid(SerializedProperty property)
        {
            return true;
        }

        private DropdownAttribute Attribute => attribute as DropdownAttribute;


        private static class Style
        {
            public static readonly float Offset = 6.0f;
            public static readonly float Height = EditorGUIUtility.singleLineHeight;
            public static readonly float Spacing = 2.0f;

            public static readonly GUIStyle TextureStyle;
            public static readonly GUIStyle PreviewStyle;

            static Style()
            {
                TextureStyle = new GUIStyle();
                PreviewStyle = new GUIStyle("helpBox");
            }
        }
    }
}