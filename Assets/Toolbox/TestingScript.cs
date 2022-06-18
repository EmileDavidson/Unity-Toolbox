using Toolbox.Optional.TweenMachine;
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