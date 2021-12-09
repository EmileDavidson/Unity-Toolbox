using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenController : MonoBehaviour
    {
        #region SINGLETON PATTERN
        private static TweenController _instance;
        public static TweenController Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = GameObject.FindObjectOfType<TweenController>();
                if (_instance != null) return _instance;

                GameObject container = new GameObject("TweenMachine");
                _instance = container.AddComponent<TweenController>();
                return _instance;
            }
        }
        
        #endregion
        
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
