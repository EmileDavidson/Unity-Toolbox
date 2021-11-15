using System;
using System.Linq;
using System.Security.Principal;
using NUnit.Framework.Internal;
using Toolbox.MethodExtensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolbox.TweenMachine.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuild))]
    public class TweenBuildPropertyDrawer2 : PropertyDrawer
    {
        private float _totalPropertyHeight = 0;
        private SerializedProperty _property;
        private Rect _position;
        private TweenBuild _tweenBuild;
        private GameObject _myGameObject;
        private GUIContent _label;

        private Color guiColor;
        private Color guiBackgroundColor;
        private Color guiContentColor;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _totalPropertyHeight + 16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            guiColor = GUI.color;
            guiBackgroundColor = GUI.backgroundColor;
            guiContentColor = GUI.contentColor;
            
            _property = property;
            _position = position;
            _label = label;
            _totalPropertyHeight = 0;

            _myGameObject = _property.GetGameObject();
            _tweenBuild = _property.ToProperty<TweenBuild>();
            if (CheckNull()) return;

            EditorGUI.BeginProperty(position, label, property);

            Draw(position, property, label);

            EditorGUI.EndProperty();
        }

        private void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect drawPosition = _position;
            // if (GUI.Button(editPos, "Open Editor..."))
            // {
            //     TweenBuildWindow.ShowWindow(_tweenBuild);
            // }

            DrawLabel(out var labelHeight);
            _totalPropertyHeight += labelHeight;
        }

        private bool CheckNull()
        {
            if (_tweenBuild == null)
            {
                EditorGUI.HelpBox(_position, "TweenBuild is null", MessageType.Error);
                return true;
            }

            if (_myGameObject == null)
            {
                EditorGUI.HelpBox(_position, "myGameObject is null", MessageType.Error);
                return true;
            }
            return false;
        }

        private void DrawLabel(out float addedHeight)
        {
            GUIStyle guiStyle = new GUIStyle();
            guiStyle.fontStyle = FontStyle.Bold;
            ColorUtility.TryParseHtmlString("#cdcdcd", out var textColor);
            guiStyle.normal.textColor = textColor;
            ColorUtility.TryParseHtmlString("#515151", out var color);
            guiStyle.normal.background = CreateSolidTexture2D(color);
            guiStyle.fixedHeight = 16;
            guiStyle.fixedWidth = _position.width;
            guiStyle.padding.left = 16;
            Rect pos = new Rect(_position.x, _position.y, guiStyle.fixedWidth, 16);
            EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), _label, guiStyle);
            addedHeight = 16;
        }
        
        private Texture2D CreateSolidTexture2D(Color color)
        {
            var texture = new Texture2D(1,1);
            Color[] pixels = Enumerable.Repeat(color, 1 * 1).ToArray();
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
}