using System;
using Toolbox.Utils.Editor;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Required
{
    /// <summary>
    /// A set of methods that simplify drawing of button controls.
    /// </summary>
    internal static class DrawUtility
    {
        private static readonly GUIContent _tempContent = new GUIContent();

        public static bool DrawInFoldout(Rect foldoutRect, bool expanded, string header, Action drawStuff)
        {
            expanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, expanded, header);

            if (expanded)
            {
                EditorGUI.indentLevel++;
                drawStuff();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            return expanded;
        }

        public static (Rect foldoutRect, Rect buttonRect) GetFoldoutAndButtonRects(string header)
        {
            const float buttonWidth = 60f;

            Rect foldoutWithoutButton = GUILayoutUtility.GetRect(TempContent(header), EditorStyles.foldoutHeader);

            var foldoutRect = new Rect(
                foldoutWithoutButton.x,
                foldoutWithoutButton.y,
                foldoutWithoutButton.width - buttonWidth,
                foldoutWithoutButton.height);

            var buttonRect = new Rect(
                foldoutWithoutButton.xMax - buttonWidth,
                foldoutWithoutButton.y,
                buttonWidth,
                foldoutWithoutButton.height);

            return (foldoutRect, buttonRect);
        }

        private static GUIContent TempContent(string text)
        {
            _tempContent.text = text;
            return _tempContent;
        }

        
        /// <summary>
        /// Draws a button with label and a dropdown arrow
        /// this button is a toggle if true. you can draw something and else you can use it in a if statement for false.
        /// </summary>
        /// <param name="foldoutRect"></param>
        /// <param name="expanded"></param>
        /// <param name="header"></param>
        /// <param name="drawStuff"></param>
        /// <returns></returns>
        public static bool DrawFoldout(Rect foldoutRect, bool expanded, string header, Action drawStuff = null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.background = Textures.DefaultTexture2D;
            style.hover.background = Textures.HoverTexture2D;

            if (GUI.Button(new Rect(foldoutRect.x - 15, foldoutRect.y, foldoutRect.width + 15, foldoutRect.height), "", style))
            {
                expanded = !expanded;
            }
            
            GUI.Label(new Rect(foldoutRect.x, foldoutRect.y, foldoutRect.width, foldoutRect.height), header);

            var arrow = expanded ?  Textures.ArrowDownTexture2D : Textures.ArrowRightTexture2D;
            GUI.DrawTexture(new Rect(foldoutRect.x - 17.5f, foldoutRect.y, 20,20), arrow);

            if (expanded)
            {
                drawStuff?.Invoke();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            return expanded;
        }


        /// <summary>
        /// Draw GameObject field an returns the value
        /// </summary>
        /// <param name="position">Rect of the position where to draw</param>
        /// <param name="label">label (text drawn next to it)</param>
        /// <param name="currentObject">the current object that is linked to this objectField</param>
        /// <param name="elementHeight">out of the height we add</param>
        /// <returns></returns>
        public static GameObject DrawGameObject(Rect position, string label, GameObject currentObject, out float elementHeight)
        {
            GameObject newGameObject = EditorGUI.ObjectField(position, label, currentObject, typeof(GameObject), false) as GameObject;
            elementHeight = position.height;
            return newGameObject;  
        }
        
        /// <summary>
        /// Draw GameObject field an returns the value
        /// </summary>
        /// <param name="position">Rect of the position where to draw</param>
        /// <param name="label">label (text drawn next to it)</param>
        /// <param name="currentObject">the current object that is linked to this objectField</param>
        /// <param name="allowSceneObjects">allow scene objects to be used</param>
        /// <param name="elementHeight">out of the height we add</param>
        /// <returns></returns>
        public static GameObject DrawGameObject(Rect position, string label, GameObject currentObject , bool allowSceneObjects, out float elementHeight)
        {
            GameObject newGameObject = EditorGUI.ObjectField(position, label, currentObject, typeof(GameObject), allowSceneObjects) as GameObject;
            elementHeight = position.height;
            return newGameObject;  
        }
        
        /// <summary>
        /// Draw GameObject field an returns the value
        /// </summary>
        /// <param name="position">Rect of the position where to draw</param>
        /// <param name="label">label (text drawn next to it)</param>
        /// <param name="currentObject">the current object that is linked to this objectField</param>
        /// <returns></returns>
        public static GameObject DrawGameObject(Rect position, string label, GameObject currentObject)
        {
            GameObject newGameObject = EditorGUI.ObjectField(position, label, currentObject, typeof(GameObject), false) as GameObject;
            return newGameObject;  
        }
        
        /// <summary>
        /// Draw GameObject field an returns the value
        /// </summary>
        /// <param name="position">Rect of the position where to draw</param>
        /// <param name="label">label (text drawn next to it)</param>
        /// <param name="currentObject">the current object that is linked to this objectField</param>
        /// <param name="allowSceneObjects">allow scene objects to be used</param>
        /// <returns></returns>
        public static GameObject DrawGameObject(Rect position, string label, GameObject currentObject, bool allowSceneObjects)
        {
            GameObject newGameObject = EditorGUI.ObjectField(position, label, currentObject, typeof(GameObject), allowSceneObjects) as GameObject;
            return newGameObject;  
        }
    }
}