using System;
using Toolbox.Attributes;
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

        [Button]
        public void TestLenght()
        {
            Debug.Log(tweenBuild.tweenList);
            foreach (var tween in tweenBuild.tweenList)
            {
                Debug.Log(tween.GetType());
            }
        }
    }
}