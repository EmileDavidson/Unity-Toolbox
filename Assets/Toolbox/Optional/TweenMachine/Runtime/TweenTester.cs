using UnityEngine;

namespace Toolbox.Optional.TweenMachine
{
    public class TweenTester : MonoBehaviour
    {
        public TweenBuild tweenBuild = new TweenBuild();

        private void Awake()
        {
            tweenBuild.StartTween();
        }
    }
}
