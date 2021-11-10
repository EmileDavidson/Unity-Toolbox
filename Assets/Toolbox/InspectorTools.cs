using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public static class InspectorTools
    {
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