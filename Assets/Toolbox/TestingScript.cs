using System;
using System.Collections.Generic;
using Toolbox.TweenMachine;
using UnityEngine;

namespace Toolbox
{
    public class TestingScript : MonoBehaviour
    {
        public TweenBuild TweenBuild = new TweenBuild();

        private void Awake()
        {
            TweenBuild.StartTween();
        }
    }
}