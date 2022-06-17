using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Toolbox.MethodExtensions;
using Toolbox.Utils;
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
        private Rect _currentPosition;
        private TweenBuild _tweenBuild;
        private GameObject _myGameObject;
        private GUIContent _label;

        private Color _guiColor;
        private Color _guiBackgroundColor;
        private Color _guiContentColor;

        private Dictionary<Type, bool> _subClassesDropdown = new Dictionary<Type, bool>();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _tweenBuild ??= property.ToProperty<TweenBuild>();
            if (!_tweenBuild.Drawer) return 0;
            return _totalPropertyHeight + _standardPropertyHeight;
        }
        

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //repaint
            // EditorUtility.SetDirty(property.serializedObject.targetObject);

            //logic
            Setup(position, property, label);
            if (CheckNull()) return;

            EditorGUI.BeginProperty(position, label, property);

            if(_tweenBuild.Drawer){Draw(position, property, label);}

            EditorGUI.EndProperty();
        }

        private void Setup(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            _guiColor = GUI.color;
            _guiBackgroundColor = GUI.backgroundColor;
            _guiContentColor = GUI.contentColor;

            _property = serializedProperty;
            _position = position;
            _position.height = 16;
            _currentPosition = _position;
            _totalPropertyHeight = 0;
            _label = label;

            foreach (var subClassType in typeof(TweenBase).GetDerrivedClasses())
            {
                if (_subClassesDropdown.ContainsKey(subClassType)) continue;
                _subClassesDropdown.Add(subClassType, false);
            }

            var subClasses = typeof(TweenBase).GetDerrivedClasses();
            foreach (var keyValuePair in _subClassesDropdown)
            {
                if (subClasses.Contains(keyValuePair.Key)) continue;
                _subClassesDropdown.Remove(keyValuePair.Key);
            }

            _myGameObject = serializedProperty.GetGameObject();
            _tweenBuild = serializedProperty.ToProperty<TweenBuild>();
        }

        private void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CheckNull()) return;

            _property.isExpanded = DrawUtility.DrawFoldout(_currentPosition, _property.isExpanded, _label.text, () =>
            {
                _currentPosition.y += 16;

                DrawDefaultClassValues();
                DrawSubClasses();
            });
        }

        private void DrawDefaultClassValues()
        {
            _currentPosition.x += 8;
            _currentPosition.width -= 8;

            if (_tweenBuild.GameObject == null) _tweenBuild.GameObject = _myGameObject;
            GameObject obj = DrawUtility.DrawGameObject(_currentPosition, "GameObject", _tweenBuild.GameObject, false, out var height);
            _totalPropertyHeight += height;
            _currentPosition.y += height;

            AnimationCurve curve = EditorGUI.CurveField(_currentPosition, "Curve", _tweenBuild.Curve);
            _totalPropertyHeight += _standardPropertyHeight;
            _currentPosition.y += _standardPropertyHeight;

            bool paused = EditorGUI.Toggle(_currentPosition, "Paused", _tweenBuild.paused);
            _totalPropertyHeight += _standardPropertyHeight;
            _currentPosition.y += _standardPropertyHeight;
        }

        private void DrawSubClasses()
        {
            DrawSubClassDropdowns();
        }

        private void DrawSubClassDropdowns()
        {
            _currentPosition.x += 8;
            _currentPosition.width -= 8;
            foreach (var subClassType in typeof(TweenBase).GetDerrivedClasses())
            {
                _subClassesDropdown[subClassType] = DrawUtility.DrawFoldout(_currentPosition,
                    _subClassesDropdown[subClassType], subClassType.Name, () =>
                    {
                        _currentPosition.x += 8;
                        _currentPosition.width -= 8;

                        if (_tweenBuild.tweenList.IsEmpty())
                        {
                            DrawAddButton(subClassType);

                            _currentPosition.x -= 8;
                            _currentPosition.width += 8;
                            return;
                        }

                        var tweenOfSubType = _tweenBuild.tweenList.Where(tween => tween.GetType() == subClassType)
                            .ToArray();
                        if (tweenOfSubType.IsEmpty())
                        {
                            DrawAddButton(subClassType);

                            _currentPosition.x -= 8;
                            _currentPosition.width += 8;
                            return;
                        }

                        var myTween = tweenOfSubType.First();
                        _currentPosition.y += 16;
                        _totalPropertyHeight += 16;

                        myTween.DrawProperties(_currentPosition, out var addedHeight, out var newCurrentPosition);
                        _currentPosition = newCurrentPosition;
                        _totalPropertyHeight += addedHeight;
                        
                        _totalPropertyHeight += _standardPropertyHeight;
                        _currentPosition.y += _standardPropertyHeight;
                        if(GUI.Button(_currentPosition,"Remove!"))
                        {
                            _tweenBuild.RemoveAllTweensOfType(subClassType);
                        }

                        _currentPosition.x -= 8;
                        _currentPosition.width += 8;
                    });


                _currentPosition.y += 16;
                _totalPropertyHeight += 16;
            }
        }

        public void DrawAddButton(Type type)
        {
            _currentPosition.y += 16;
            _totalPropertyHeight += 16;
            if (GUI.Button(_currentPosition, "Add: " + type.Name))
            {
                if (type.HasEmptyConstructor())
                {
                    if (!(Activator.CreateInstance(type) is TweenBase tween)) return;
                    tween.gameObject = _tweenBuild.GameObject;
                    tween.Curve = _tweenBuild.Curve;
                    _tweenBuild.ChainAdd(tween);
                    _totalPropertyHeight += 16;
                    _currentPosition.y += 16;
                }
            }
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