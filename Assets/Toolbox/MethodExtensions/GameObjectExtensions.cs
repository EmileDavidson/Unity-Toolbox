using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets a component attached to the given GameObject.
        /// If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.HasComponent<T>())
            {
                return gameObject.GetComponent<T>();
            }

            return gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Gets a component attached to the given GameObject parent.
        /// If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponentToParent<T>(this GameObject gameObject) where T : Component
        {
            var parent = gameObject.transform.parent;
            return parent.HasComponent<T>() ? parent.GetComponent<T>() : parent.AddComponent<T>();
        }

        /// <summary>
        /// checks whether a GameObject has the given component or not.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// checks if the given GameObject parent has the given type 
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponentInParent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponentInParent<T>() != null;
        }

        /// <summary>
        /// Checks if GameObject has component and removes it.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="immediate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>if component is removed or not</returns>
        public static bool RemoveComponent<T>(this GameObject obj, bool immediate = false) where T : Component
        {
            if (!(obj.HasComponent<T>())) return false;
            if (immediate) Object.DestroyImmediate(obj.GetComponent<T>(), true);
            else Object.Destroy(obj.GetComponent<T>());
            return true;
        }
        

        /// <summary>
        /// gets or add script from / to add children of GameObject and returns the list of components
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<Component> GetOrAddComponentAllChildren<T>(this GameObject gameObject) where T : Component
        {
            List<Component> components = new List<Component>();
            List<GameObject> childGameObjects = GetAllChildrenGameObjects(gameObject);

            foreach (GameObject child in childGameObjects)
            {
                if (child.TryGetComponent<T>(out var comp))
                {
                    components.Add(comp);
                    continue;
                }
                var addedComp = child.AddComponent<T>();
                components.Add(addedComp);
            }

            return components;
        }

        /// <summary>
        /// returns list of all children GameObjects
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static List<GameObject> GetAllChildrenGameObjects(this GameObject gameObject)
        {
            List<Transform> childrenTransforms = new List<Transform>(gameObject.transform.GetComponentsInChildren<Transform>());
            List<GameObject> childGameObjects = new List<GameObject>();
            childrenTransforms.ForEach((objTrans => childGameObjects.Add(objTrans.gameObject)));
            return childGameObjects;
        }
        
        /// <summary>
        /// Find all children of the GameObject by tag (includes self)
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static List<GameObject> FindChildrenByTag(this GameObject gameObject, params string[] tags)
        {
            return gameObject.transform.FindChildrenByTag(tags)
                .Select(tran => tran.gameObject)
                .Where(gameOb => gameOb != null)
                .ToList();
        }
    }
}