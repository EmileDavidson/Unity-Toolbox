using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.CompilerServices;
using Toolbox.Required;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Optional.TweenMachine.Editor
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
        private List<Type> subclasses = new List<Type>();
        private bool initialized = false;

        private TweenBase _currentTween;

        private List<Type> _ignoredTypes = new List<Type>()
        {
    
        };

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _tweenBuild ??= property.ToProperty<TweenBuild>();
            if (!_tweenBuild.Drawer) return 0;
            return _totalPropertyHeight + _standardPropertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!initialized) Initialize(position, property, label);
            Setup(position, property, label);
            
            if (CheckNull()) return;
            EditorGUI.BeginProperty(position, label, property);

            if (_tweenBuild.Drawer)
            {
                Draw(position, property, label);
            }
            
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Runs everytime the object gets selected
        /// </summary>
        private void Initialize(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            _guiColor = GUI.color;
            _guiBackgroundColor = GUI.backgroundColor;
            _guiContentColor = GUI.contentColor;
            
            _property = serializedProperty;
            _position = position;
            _label = label;

            _myGameObject = serializedProperty.GetGameObject();
            _tweenBuild = serializedProperty.ToProperty<TweenBuild>();
            subclasses = typeof(TweenBase).GetDerrivedClasses();
            
            foreach (var subClassType in typeof(TweenBase).GetDerrivedClasses())
            {
                if (_subClassesDropdown.ContainsKey(subClassType)) continue;
                _subClassesDropdown.Add(subClassType, false);
            }
            
            foreach (var keyValuePair in _subClassesDropdown)
            {
                if (subclasses.Contains(keyValuePair.Key)) continue;
                _subClassesDropdown.Remove(keyValuePair.Key);
            }
            
            initialized = true;
        }

        private void Setup(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            _position = position;
            
            _position.height = 16;
            _currentPosition = _position;
            _totalPropertyHeight = 0;
        }

        private void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
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

            _tweenBuild.Curve = EditorGUI.CurveField(_currentPosition, "Curve", _tweenBuild.Curve);
            _totalPropertyHeight += _standardPropertyHeight;
            _currentPosition.y += _standardPropertyHeight;

            _tweenBuild.paused = EditorGUI.Toggle(_currentPosition, "Paused", _tweenBuild.paused);
            _totalPropertyHeight += _standardPropertyHeight;
            _currentPosition.y += _standardPropertyHeight;
        }



        private void DrawSubClasses()
        {
            _currentPosition.x += 8;
            _currentPosition.width -= 8;
            int index = 0;
            foreach (var subClassType in subclasses)
            {
                if(_ignoredTypes.Contains(subClassType)) continue;
                _subClassesDropdown[subClassType] = DrawUtility.DrawFoldout(_currentPosition, _subClassesDropdown[subClassType], subClassType.Name, () =>
                    {
                        _currentPosition.x += 8;
                        _currentPosition.width -= 8;

                        var tweenOfSubType = _tweenBuild.tweenList.Where(tween => tween.GetType() == subClassType).ToArray();
                        if (tweenOfSubType.IsEmpty())
                        {
                            DrawAddButton(subClassType);

                            _currentPosition.x -= 8;
                            _currentPosition.width += 8;
                            return;
                        }

                        _currentTween = tweenOfSubType.First();
                        _currentPosition.y += 16;
                        _totalPropertyHeight += 16;
                        
                        // TESTING 
                        SerializedProperty tweenSerializedProperty = null;
                        if (_tweenBuild.tweenList.ContainsSlot(index))
                        {
                            tweenSerializedProperty = _property.FindPropertyRelative("tweenList.Array.data[" + index + "]");
                        }
                        //END TESTING
                        
                        _currentTween.DrawProperties(_currentPosition, tweenSerializedProperty , out var addedHeight, out var newCurrentPosition);

                        _currentPosition = newCurrentPosition;
                        _totalPropertyHeight += addedHeight;
                        
                        _totalPropertyHeight += _standardPropertyHeight;
                        _currentPosition.y += _standardPropertyHeight;
                        
                        if(GUI.Button(_currentPosition,"Remove")) _tweenBuild.RemoveAllTweensOfType(subClassType);

                        _currentPosition.x -= 8;
                        _currentPosition.width += 8;
                        index++;
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
                    var tween = Activator.CreateInstance(type) as TweenBase;
                    if (tween is null) return;
                    tween.ResetValues(); 
                    tween.gameObject = _tweenBuild.GameObject;
                    tween.Curve = _tweenBuild.Curve.Clone();
                    tween.ResetValues();
                    _tweenBuild.ChainAdd(tween);
                    _totalPropertyHeight += 16;
                    _currentPosition.y += 16;
                }
            }
        }

        /// <summary>
        /// Checks if something of the data that is needed is null
        /// if it is it logs HelpBox in the Inspector and return true 
        /// </summary>
        /// <returns></returns>
        private bool CheckNull()
        {
            bool nullStatus = false;

            if (_tweenBuild is null)
            {
                EditorGUI.HelpBox(_position, "TweenBuild is null", MessageType.Error);
                nullStatus = true;
            }

            if (_myGameObject is null)
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