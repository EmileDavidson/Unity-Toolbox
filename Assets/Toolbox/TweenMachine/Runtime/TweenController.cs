using System.Collections.Generic;
using Toolbox.Other;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenController : MonoBehaviour
    {
        public static readonly MonoSingleton<TweenController> MonoSingleton = new MonoSingleton<TweenController>();
        
        public List<TweenBuild> activeBuilds = new List<TweenBuild>();
        public List<TweenBuild> doneBuilds = new List<TweenBuild>();
        private bool _paused = false;
    
        private void Update()
        {
            if (activeBuilds.Count <= 0) Destroy(this.gameObject);
            if (_paused) return;
            
            UpdateActiveTweens();
        }
    
        private void UpdateActiveTweens()
        {
            for (int i = 0; i < activeBuilds.Count; i++)
            {
                activeBuilds[i].UpdateTween(Time.deltaTime);
                if (activeBuilds[i].IsFinished)
                {
                    activeBuilds.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SetPaused()
        {
            if (activeBuilds.Count >= 1)
            {
                _paused = true;
            }
        }
    }
}
