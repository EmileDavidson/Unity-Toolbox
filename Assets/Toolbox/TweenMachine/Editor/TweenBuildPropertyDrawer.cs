// using System;
// using System.Security.Principal;
// using NUnit.Framework.Internal;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UIElements;
//
// namespace Toolbox.TweenMachine.Editor
// {
//     [CustomPropertyDrawer(typeof(TweenBuild))]
//     public class TweenBuildPropertyDrawer : PropertyDrawer
//     {
//         private float _totalPropertyHeight = 0;
//         private float _standardPropertyHeight = 16;
//
//         private SerializedProperty _property;
//         private Rect _position;
//         private TweenBuild _tweenBuild;
//         private GameObject _myGameObject;
//
//         private Color guiColor;
//         private Color guiBackgroundColor;
//         private Color guiConentColor;
//
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             return _totalPropertyHeight + _standardPropertyHeight;
//         }
//
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             guiColor = GUI.color;
//             guiBackgroundColor = GUI.backgroundColor;
//             guiConentColor = GUI.contentColor;
//             
//             _property = property;
//             _position = position;
//             _totalPropertyHeight = 0;
//
//             _myGameObject = GetGameObject(property);
//             _tweenBuild = GetTweenBuild(property);
//             if (CheckNull()) return;
//
//             EditorGUI.BeginProperty(position, label, property);
//
//             Draw(position, property, label);
//
//             EditorGUI.EndProperty();
//         }
//
//         private void Draw(Rect position, SerializedProperty property, GUIContent label)
//         {
//             Rect foldoutPos = new Rect(position.x, position.y, position.width, _standardPropertyHeight);
//             Rect editPos = new Rect(position.x + (position.width - 110), position.y, 110, _standardPropertyHeight);
//
//             // if (GUI.Button(editPos, "Open Editor..."))
//             // {
//             //     TweenBuildWindow.ShowWindow(_tweenBuild);
//             // }
//
//             EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
//
//             property.isExpanded = EditorGUI.Foldout(foldoutPos, property.isExpanded, "");
//             if (property.isExpanded)
//             {
//                 DefaultTweenValueDrawer();
//
//                 _tweenBuild.tweenFoldout = EditorGUI.Foldout(new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight, position.width, _standardPropertyHeight), _tweenBuild.tweenFoldout, "Tween values");
//                 _totalPropertyHeight += _standardPropertyHeight;
//                 if (_tweenBuild.tweenFoldout) TweenValueDrawer();
//
//                 _tweenBuild.positionFoldout = EditorGUI.Foldout(new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight, position.width, _standardPropertyHeight), _tweenBuild.positionFoldout, "Position values");
//                 _totalPropertyHeight += _standardPropertyHeight;
//                 if (_tweenBuild.positionFoldout) PositionValueDrawer();
//
//                 _tweenBuild.rotationFoldout = EditorGUI.Foldout(new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight, position.width, _standardPropertyHeight), _tweenBuild.rotationFoldout, "Rotation values");
//                 _totalPropertyHeight += _standardPropertyHeight;
//                 if (_tweenBuild.rotationFoldout) RotationValueDrawer();
//
//                 _tweenBuild.scaleFoldout = EditorGUI.Foldout(new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight,position.width, _standardPropertyHeight), _tweenBuild.scaleFoldout, "Scale values");
//                 _totalPropertyHeight += _standardPropertyHeight;
//                 if (_tweenBuild.scaleFoldout) ScaleValueDrawer();
//
//                 _tweenBuild.colorFoldout = EditorGUI.Foldout(new Rect(position.x + 8, position.y + _totalPropertyHeight + _standardPropertyHeight, position.width, _standardPropertyHeight), _tweenBuild.colorFoldout, "Color values");
//                 _totalPropertyHeight += _standardPropertyHeight;
//                 if (_tweenBuild.colorFoldout) ColorValueDrawer();
//             }
//             
//             GUI.backgroundColor = Color.red;
//             GUI.backgroundColor = guiBackgroundColor;
//         }
//
//         private void DefaultTweenValueDrawer()
//         {
//             GameObject obj = _tweenBuild.GameObject == null ? _myGameObject : _tweenBuild.GameObject;
//             _tweenBuild.GameObject = InspectorTools.DrawGameObject(new Rect(_position.x + 16, _position.y + _totalPropertyHeight + _standardPropertyHeight, _position.width - 8, _standardPropertyHeight), "GameObject", obj, true, out float height);
//             _totalPropertyHeight += height + 2;
//
//             _tweenBuild.DefaultSpeed = EditorGUI.FloatField(new Rect(_position.x + 16, _position.y + _totalPropertyHeight + _standardPropertyHeight, _position.width - 8, _standardPropertyHeight), "Speed", _tweenBuild.DefaultSpeed);
//             _totalPropertyHeight += _standardPropertyHeight + 2 + _standardPropertyHeight;
//             
//             //start event
//             SerializedProperty propertyRelative;
//             SerializedEvent serializedEvent;
//             int listenerCount = 0;
//             int multiplier = 0;
//             
//             propertyRelative = _property.FindPropertyRelative("onTweenBuildStart");
//             serializedEvent = _tweenBuild.onTweenBuildStart;
//             listenerCount = serializedEvent.GetPersistentEventCount();
//             multiplier = (listenerCount <= 1) ? 0 : listenerCount - 1;
//
//             EditorGUI.PropertyField(
//                     new Rect(_position.x + 16, _position.y + _totalPropertyHeight + _standardPropertyHeight,
//                         _position.width - 30, _standardPropertyHeight), propertyRelative);
//             _totalPropertyHeight += (_standardPropertyHeight * 5) + (_standardPropertyHeight * 3 * multiplier) + 2;
//
//             //update event
//             propertyRelative = _property.FindPropertyRelative("onTweenBuildUpdate");
//             serializedEvent = _tweenBuild.onTweenBuildUpdate;
//             listenerCount = serializedEvent.GetPersistentEventCount();
//             multiplier = (listenerCount <= 1) ? 0 : listenerCount - 1;
//
//                 EditorGUI.PropertyField(
//                     new Rect(_position.x + 16, _position.y + _totalPropertyHeight + (_standardPropertyHeight * 2),
//                         _position.width - 30, _standardPropertyHeight), propertyRelative);
//             _totalPropertyHeight += (_standardPropertyHeight * 5) + (_standardPropertyHeight * 3 * multiplier) + 2 + (_standardPropertyHeight);
//             //finish event
//             propertyRelative = _property.FindPropertyRelative("onTweenBuildFinish");
//             serializedEvent = _tweenBuild.onTweenBuildFinish;
//             listenerCount = serializedEvent.GetPersistentEventCount();
//             multiplier = (listenerCount <= 1) ? 0 : listenerCount - 1;
//
//             EditorGUI.PropertyField(
//                 new Rect(_position.x + 16, _position.y + _totalPropertyHeight + (_standardPropertyHeight * 2),
//                     _position.width - 30, _standardPropertyHeight), propertyRelative);
//             _totalPropertyHeight += (_standardPropertyHeight * 5) + (_standardPropertyHeight * 3 * multiplier) + 2;
//             _totalPropertyHeight += _standardPropertyHeight * 2;
//         }
//
//         private void PositionValueDrawer()
//         {
//         }
//
//         private void RotationValueDrawer()
//         {
//         }
//
//         private void ScaleValueDrawer()
//         {
//         }
//
//         private void ColorValueDrawer()
//         {
//         }
//
//         private void TweenValueDrawer()
//         {
//         }
//
//         private bool CheckNull()
//         {
//             bool nullStatus = false;
//
//             if (_tweenBuild == null)
//             {
//                 EditorGUI.HelpBox(_position, "TweenBuild is null", MessageType.Error);
//                 nullStatus = true;
//             }
//
//             if (_myGameObject == null)
//             {
//                 EditorGUI.HelpBox(_position, "myGameObject is null", MessageType.Error);
//                 nullStatus = true;
//             }
//
//             if (nullStatus)
//             {
//                 EditorGUI.EndProperty();
//                 return true;
//             }
//
//             return false;
//         }
//
//         private GameObject GetGameObject(SerializedProperty property)
//         {
//             var targetObject = property.serializedObject.targetObject;
//             var obj = targetObject as MonoBehaviour;
//             GameObject myGameObject = obj == null ? null : obj.gameObject;
//             return myGameObject;
//         }
//
//         private TweenBuild GetTweenBuild(SerializedProperty property)
//         {
//             var targetObject = property.serializedObject.targetObject;
//             if (targetObject == null) return null;
//             var targetObjectClassType = targetObject.GetType();
//             var field = targetObjectClassType.GetField(property.propertyPath);
//             if (field == null) return null;
//
//             return field.GetValue(targetObject) as TweenBuild;
//         }
//     }
// }