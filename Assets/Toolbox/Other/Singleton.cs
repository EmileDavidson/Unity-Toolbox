using UnityEngine;

namespace Toolbox.Other
{
    public class Singleton<T> where T : MonoBehaviour
    {
        private T _instance;
        private GameObject _container;
        private readonly string _name;

        public Singleton(string objectName = null)
        {
            _name = objectName;
        }

        public T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = Object.FindObjectOfType<T>();
                if (_instance != null) return _instance;

                GameObject singletonObject = new GameObject(_name ?? "Singleton: " + typeof(T).Name);
                singletonObject.transform.parent = Container.transform;
                _instance = singletonObject.AddComponent<T>();
                return _instance;
            }
        }

        public GameObject Container
        {
            get
            {
                if (_container != null) return _container;

                _container = GameObject.Find("/Singletons");
            
                if (_container != null) return _container;

                _container = new GameObject("Singletons");
                return _container;
            }
        }
    }
}