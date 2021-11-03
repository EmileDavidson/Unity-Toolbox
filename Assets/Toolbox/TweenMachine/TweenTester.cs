using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Toolbox.TweenMachine.Tweens;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild();

        private void Awake()
        {
            // tweenBuild = new TweenBuild(this.gameObject);
            //
            // Tween tweenScale = tweenBuild.SetTweenScale(new Vector3(0.5f, 0.5f, 0.5f), 5, EasingType.Linear); // new scale, speed, easingType
            //
            // tweenBuild.StartTween();
        }
    }
}
