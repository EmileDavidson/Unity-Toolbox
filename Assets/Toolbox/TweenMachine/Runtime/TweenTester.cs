﻿using System;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild(false);

        private void Awake()
        {
            tweenBuild.StartTween();
        }
    }
}
