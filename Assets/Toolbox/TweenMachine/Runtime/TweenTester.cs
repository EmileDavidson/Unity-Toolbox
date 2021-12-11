using System;
using Toolbox.Attributes;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild();
        private GameObject _gameObject;
        public AnimationCurve curve = new AnimationCurve();
        
        private void Awake()
        {
            this._gameObject = gameObject;
            tweenBuild.tweens.Clear();

            tweenBuild.CreateAndAddTween<TweenPosition>(new object[] { gameObject, 1, new Vector3(2, 2, 2) });
            tweenBuild.CreateAndAddTween<TweenPosition>(gameObject, 1, new Vector3(9, 9, 9));
            tweenBuild.CreateAndAddTween<TweenRotation>(gameObject, 1, new Quaternion(1,2,3,1));
            tweenBuild.CreateAndAddTween<TweenScale>(gameObject, 1, new Vector3(4,5,6));
            tweenBuild.CreateAndAddTween<Tween>(gameObject, 1, new Vector3(1,2,3));
            

            print(tweenBuild.tweens.Count);
            tweenBuild.tweens[0].GetStep();
            
        }
    }
}
