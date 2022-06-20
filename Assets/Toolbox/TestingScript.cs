using System;
using Toolbox.Optional.TweenMachine;
using UnityEngine;

namespace Toolbox
{
    public class TestingScript : MonoBehaviour
    {
        public int test;
        public TweenBuild tweenBuild = new TweenBuild();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                tweenBuild?.StartTween();
            }
        }

        public void Test(string debugText)
        {
            print(debugText);
        }
    }
}