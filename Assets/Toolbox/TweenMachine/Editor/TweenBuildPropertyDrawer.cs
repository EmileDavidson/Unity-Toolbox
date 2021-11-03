using Toolbox.TweenMachine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolbox.TweenMachine.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuild))]
    public class TestingEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(property.propertyPath);
            if (field == null)
            {
                EditorGUILayout.HelpBox("Could not find the field", MessageType.Error);
                return;
            }

            TweenBuild tweenBuild = field.GetValue(targetObject) as TweenBuild;
            
        }
    }
}