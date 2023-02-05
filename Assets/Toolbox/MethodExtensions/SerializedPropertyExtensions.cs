using UnityEditor;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class SerializedPropertyExtensions
    {
        
        /// <summary>
        /// make sure to do a null check because there is a possibility it will be null
        /// </summary>
        /// <param name="property"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToProperty<T>(this SerializedProperty property) where T : class
        {
            var targetObject = property.serializedObject.targetObject;
            if (targetObject == null) return null;
            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(property.propertyPath);
            if (field == null) return null;

            return field.GetValue(targetObject) as T;
        }

        /// <summary>
        /// Gets the GameObject of the serializedProperty
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static GameObject GetGameObject(this SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;
            var obj = targetObject as MonoBehaviour;
            GameObject myGameObject = obj == null ? null : obj.gameObject;
            return myGameObject;
        }
    }
}