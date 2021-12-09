using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild();

        private void Awake()
        {
            TweenPosition tweenPosition = new TweenPosition(gameObject, new Vector3(0,0,0), 1);
            
            tweenBuild.CreateAndAddTween<TweenPosition>(this.gameObject, 1)
                .ChainSetTarget(new Vector3(1, 1, 1))
                .UseCurve = true;
        }
    }
}
