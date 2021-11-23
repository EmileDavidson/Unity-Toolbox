using System;
using Toolbox.MethodExtensions;
using Toolbox.TweenMachine;
using UnityEngine;

namespace Toolbox
{
    public class Tester : MonoBehaviour
    {
        private void Start()
        {
            var list = gameObject.GetOrAddComponentAllChildren<tester2>();
            Debug.Log(list.Count);
            foreach (var component in list)
            {
                // Debug.Log(component);
            }
        }
    }
}