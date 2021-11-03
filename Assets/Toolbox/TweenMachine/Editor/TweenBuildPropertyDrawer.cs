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
            EditorGUI.BeginProperty(position, label, property);

            var targetObject = property.serializedObject.targetObject;
            var obj = targetObject as MonoBehaviour;
            GameObject myGameObject = obj == null ? null : obj.gameObject;

            if (targetObject == null) return;

            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(property.propertyPath);
            if (field == null)
            {
                EditorGUI.HelpBox(position,"Field is null", MessageType.Error);
                EditorGUI.EndProperty();
                return;
            }

            TweenBuild tweenBuild = field.GetValue(targetObject) as TweenBuild;
            
            if (tweenBuild == null)
            {
                EditorGUI.HelpBox(position, "Tween build is null", MessageType.Info);
                return;
            }
            
            Rect pos = position;
            string buttonName = tweenBuild.name == default ? property.propertyPath : tweenBuild.name;
            if (GUI.Button(pos, $"{buttonName}"))
            {
                TweenBuildWindow tweenBuildWindow = TweenBuildWindow.ShowWindow();
                tweenBuildWindow.TweenBuild = tweenBuild;
                tweenBuildWindow.GameObject = myGameObject;
            }

            EditorGUI.EndProperty();
        }
    }
}