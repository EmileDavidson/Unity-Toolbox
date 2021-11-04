using Toolbox.TweenMachine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolbox.TweenMachine.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuild))]
    public class TweenBuildPropertyDrawer : PropertyDrawer
    {

        public static TweenBuild tweenBuild;
        
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

            tweenBuild = field.GetValue(targetObject) as TweenBuild;
            
            if (tweenBuild == null)
            {
                tweenBuild = new TweenBuild();
            }
            
            Rect pos = position;
            if (tweenBuild.name == default || string.IsNullOrEmpty(tweenBuild.name)) tweenBuild.name = property.propertyPath;
            string buttonName = tweenBuild.name;
            
            if (GUI.Button(pos, $"{buttonName}"))
            {
                TweenBuildWindow tweenBuildWindow = TweenBuildWindow.ShowWindow();
                tweenBuildWindow.Gameobject = myGameObject;
                tweenBuildWindow.TweenBuild = tweenBuild;
            }

            EditorGUI.EndProperty();
        }
    }
}