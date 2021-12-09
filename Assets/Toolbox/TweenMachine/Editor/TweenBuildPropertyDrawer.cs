using Toolbox.MethodExtensions;
using UnityEditor;
using UnityEngine;

namespace Toolbox.TweenMachine.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuild))]
    public class TweenBuildPropertyDrawer : PropertyDrawer
    {
        private float _totalPropertyHeight = 0;
        private float _standardPropertyHeight = 16;

        private SerializedProperty _property;
        private Rect _position;
        private Rect _currentPosition;
        private TweenBuild _tweenBuild;
        private GameObject _myGameObject;

        private Color _guiColor;
        private Color _guiBackgroundColor;
        private Color _guiContentColor;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _totalPropertyHeight + _standardPropertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Setup(position, property, label);
            if (CheckNull()) return;

            EditorGUI.BeginProperty(position, label, property);

            Draw(position, property, label);

            EditorGUI.EndProperty();
        }

        private void Setup(Rect position, SerializedProperty property, GUIContent label)
        {
            _guiColor = GUI.color;
            _guiBackgroundColor = GUI.backgroundColor;
            _guiContentColor = GUI.contentColor;
            
            _property = property;
            _position = position;
            _currentPosition = _position;
            _totalPropertyHeight = 0;

            _myGameObject = property.GetGameObject();
            _tweenBuild = property.To<TweenBuild>();
        }

        private void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
            //TODO: DRAW EVERYTHING THAT NEEDS TO BE DRAWN.
        }

        private bool CheckNull()
        {
            bool nullStatus = false;

            if (_tweenBuild == null)
            {
                EditorGUI.HelpBox(_position, "TweenBuild is null", MessageType.Error);
                nullStatus = true;
            }

            if (_myGameObject == null)
            {
                EditorGUI.HelpBox(_position, "myGameObject is null", MessageType.Error);
                nullStatus = true;
            }

            if (nullStatus)
            {
                EditorGUI.EndProperty();
                return true;
            }

            return false;
        }
    }
}