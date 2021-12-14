using Toolbox.Animation;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild();
        private GameObject _gameObject;
        public AnimationCurve easeCurve = new AnimationCurve().ChainToCurve(EasingTools.easingCurve[EasingTools.EasingType.OutElastic]);

        
        private void Awake()
        {
            _gameObject = gameObject;
            tweenBuild.CreateAndAddTween<TweenPosition>().ChainSetTarget(new Vector3(5,5,5)).ChainSetGameObject(_gameObject).ChainSetCurve(easeCurve);
            tweenBuild.CreateAndAddTween<TweenRotation>(_gameObject, new Quaternion(30, 30, 30, 1));
            tweenBuild.CreateAndAddTween<TweenScale>(_gameObject, new Vector3(5, 5, 5));
            tweenBuild.StartTween();
        }
    }
}
