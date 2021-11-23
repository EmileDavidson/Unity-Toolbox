using System;
using System.Linq;
using Toolbox.TweenMachine.Tweens;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild();

        private void Awake()
        {
            // Debug.Log(tweenBuild.tweens.Count);
        }
    }
}
