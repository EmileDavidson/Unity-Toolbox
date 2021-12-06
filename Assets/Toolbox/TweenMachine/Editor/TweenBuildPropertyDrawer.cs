// using System.Security.Principal;
// using UnityEditor;
// using UnityEngine;
//
// namespace Toolbox.TweenMachine.Editor
// {
//     [CustomPropertyDrawer(typeof(TweenBuild))]
//     public class TweenBuildPropertyDrawer : PropertyDrawer
//     {
//         private float _totalPropertyHeight = 0;
//         private float _standardPropertyHeight = 16;
//
//         private TweenBuild _tweenBuild;
//         private GameObject _myGameObject;
//
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             return _totalPropertyHeight + _standardPropertyHeight;
//         }
//
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             _totalPropertyHeight = 0;
//             EditorGUI.BeginProperty(position, label, property);
//
//             _myGameObject = GetGameObject(property);
//             _tweenBuild = GetTweenBuild(property);
//             if (CheckNull(position, property, label)) return;
//
//             Draw(position, property, label);
//             
//             EditorGUI.EndProperty();
//         }
//
//         private void Draw(Rect position, SerializedProperty property, GUIContent label)
//         {
//             Rect foldoutPos = new Rect(position.x, position.y, position.width, _standardPropertyHeight);
//             Rect editPos = new Rect(position.x + position.width / 2, position.y, position.width / 2, _standardPropertyHeight);
//             Rect gameObjectPos = new Rect(position.x + 8, position.y + _standardPropertyHeight, position.width - 8, 16);
//             
//             property.isExpanded = EditorGUI.Foldout(foldoutPos, property.isExpanded, label);
//             if (GUI.Button(editPos, "Editor Window"))
//             {
//                 TweenBuildMenu.ShowWindow();
//             }
//             if (property.isExpanded)
//             {
//                 GameObject obj = _tweenBuild.GameObject == null ? _myGameObject : _tweenBuild.GameObject;
//                 _tweenBuild.GameObject = DrawGameObject(gameObjectPos, "GameObject", obj, true);
//             } 
//         }
//
//         private GameObject DrawGameObject(Rect pos, string labelName, GameObject oldObj, bool allowSceneObjects)
//         {
//             _totalPropertyHeight += _standardPropertyHeight;
//             GameObject newGameObject = EditorGUI.ObjectField(pos, labelName, oldObj, typeof(GameObject), allowSceneObjects) as GameObject;
//             return newGameObject;  
//         }
//         
//         private bool CheckNull(Rect position, SerializedProperty property, GUIContent label)
//         {
//             bool nullStatus = false;
//             
//             if (_tweenBuild == null)
//             {
//                 EditorGUI.HelpBox(position, "TweenBuild is null", MessageType.Error);
//                 nullStatus = true;
//             }
//             
//             if (_myGameObject == null)
//             {
//                 EditorGUI.HelpBox(position, "myGameObject is null", MessageType.Error);
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
//         private GameObject GetGameObject(SerializedProperty property)
//         {
//             var targetObject = property.serializedObject.targetObject;
//             var obj = targetObject as MonoBehaviour;
//             GameObject myGameObject = obj == null ? null : obj.gameObject;
//             return myGameObject;
//         }
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