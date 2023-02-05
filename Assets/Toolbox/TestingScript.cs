using System;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox
{
    public class TestingScript : MonoBehaviour
    {
        
        
        [Button]
        public void Test()
        {
            gameObject.transform.position.SetX(10).SetY(10).SetZ(10);
        }
    }
}