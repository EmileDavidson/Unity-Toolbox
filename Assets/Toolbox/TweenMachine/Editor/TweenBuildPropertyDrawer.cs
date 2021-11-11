using System;
using System.Security.Principal;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolbox.TweenMachine.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuild))]
    public class TweenBuildPropertyDrawer : PropertyDrawer
    {
        private float _totalPropertyHeight = 0;
        private float _standardPropertyHeight = 16;

        private SerializedProperty _property;
        private Rect _position;
        private TweenBuild _tweenBuild;
        private GameObject _myGameObject;

        private bool _defaultFoldout = false;
        private bool _positionFoldout = false;
        private bool _scaleFoldout = false;
        private bool _rotationFoldout = false;
        private bool _colorFoldout = false;
        private bool _tweenFoldout = false;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _totalPropertyHeight + _standardPropertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _property = property;
            _position = position;
            _totalPropertyHeight = 0;

            _myGameObject = GetGameObject(property);
            _tweenBuild = GetTweenBuild(property);
            if (CheckNull()) return;

            EditorGUI.BeginProperty(position, label, property);

            Draw(position, property, label);

            EditorGUI.EndProperty();
        }

        private void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect foldoutPos = new Rect(position.x, position.y, position.width, _standardPropertyHeight);
            Rect editPos = new Rect(position.x + (position.width - 110), position.y, 110, _standardPropertyHeight);

            if (GUI.Button(editPos, "Open Editor..."))
            {
                TweenBuildWindow.ShowWindow(_tweenBuild);
            }

            EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            property.isExpanded = EditorGUI.Foldout(foldoutPos, property.isExpanded, "");
            if (property.isExpanded)
            {
                _defaultFoldout =
                    EditorGUI.Foldout(
                        new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,
                            position.width, _standardPropertyHeight), _defaultFoldout, "Default values");
                _totalPropertyHeight += _standardPropertyHeight;
                if (_defaultFoldout) DefaultTweenValueDrawer();


                _tweenFoldout =
                    EditorGUI.Foldout(
                        new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,
                            position.width, _standardPropertyHeight), _tweenFoldout, "Tween values");
                _totalPropertyHeight += _standardPropertyHeight;
                if (_tweenFoldout) TweenValueDrawer();

                _positionFoldout =
                    EditorGUI.Foldout(
                        new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,
                            position.width, _standardPropertyHeight), _positionFoldout, "Position values");
                _totalPropertyHeight += _standardPropertyHeight;
                if (_positionFoldout) PositionValueDrawer();

                _rotationFoldout =
                    EditorGUI.Foldout(
                        new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,
                            position.width, _standardPropertyHeight), _rotationFoldout, "Rotation values");
                _totalPropertyHeight += _standardPropertyHeight;
                if (_rotationFoldout) RotationValueDrawer();

                _scaleFoldout =
                    EditorGUI.Foldout(
                        new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,
                            position.width, _standardPropertyHeight), _scaleFoldout, "Scale values");
                _totalPropertyHeight += _standardPropertyHeight;
                if (_scaleFoldout) ScaleValueDrawer();

                _colorFoldout =
                    EditorGUI.Foldout(
                        new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,
                            position.width, _standardPropertyHeight), _colorFoldout, "Color values");
                _totalPropertyHeight += _standardPropertyHeight;
                if (_colorFoldout) ColorValueDrawer();
            }
        }

        private void DefaultTweenValueDrawer()
        {
            GameObject obj = _tweenBuild.GameObject == null ? _myGameObject : _tweenBuild.GameObject;
            _tweenBuild.GameObject = InspectorTools.DrawGameObject(
                new Rect(_position.x + 16, _position.y + _totalPropertyHeight + _standardPropertyHeight,
                    _position.width - 8, _standardPropertyHeight), "GameObject", obj, true, out float height);
            _totalPropertyHeight += height + 2;

            _tweenBuild.DefaultSpeed =
                EditorGUI.FloatField(
                    new Rect(_position.x + 16, _position.y + _totalPropertyHeight + _standardPropertyHeight,
                        _position.width - 8, _standardPropertyHeight), "Speed", _tweenBuild.DefaultSpeed);
            _totalPropertyHeight += _standardPropertyHeight + 2;

            //todo check if can get the event listener count to add height to property drawer 
            SerializedProperty propertyRelative = _property.FindPropertyRelative("onTweenBuildStart");
            SerializedEvent serializedEvent = _tweenBuild.onTweenBuildStart;
            int listenerCount = serializedEvent.GetPersistentEventCount();
            int multiplier = (listenerCount <= 1) ? 0 : listenerCount - 1;

            var eventProp = EditorGUI.PropertyField(new Rect(_position.x + 16, _position.y + _totalPropertyHeight + _standardPropertyHeight, _position.width - 8, _standardPropertyHeight), propertyRelative); 
            _totalPropertyHeight += (_standardPropertyHeight * 5) + (_standardPropertyHeight * 3 * multiplier) + 2;
        }

        private void PositionValueDrawer()
        {
        }

        private void RotationValueDrawer()
        {
        }

        private void ScaleValueDrawer()
        {
        }

        private void ColorValueDrawer()
        {
        }

        private void TweenValueDrawer()
        {
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

        private GameObject GetGameObject(SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;
            var obj = targetObject as MonoBehaviour;
            GameObject myGameObject = obj == null ? null : obj.gameObject;
            return myGameObject;
        }

        private TweenBuild GetTweenBuild(SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;
            if (targetObject == null) return null;
            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(property.propertyPath);
            if (field == null) return null;

            return field.GetValue(targetObject) as TweenBuild;
        }
    }
}