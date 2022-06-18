using System;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox.Other
{
    public class Singleton<T>
    {

        private T _instance;

        public T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                if (!typeof(T).HasEmptyConstructor()) return _instance;
                _instance = (T) Activator.CreateInstance(typeof(T));

                return _instance;
            }
        }
    }
}