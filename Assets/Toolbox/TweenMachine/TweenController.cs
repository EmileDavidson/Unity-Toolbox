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
        
        public List<TweenBuild> acitveTweens = new List<TweenBuild>();
        public List<TweenBuild> doneTweens = new List<TweenBuild>();
        private bool paused = false;
    
        private void Update()
        {
            if (acitveTweens.Count <= 0) Destroy(this.gameObject);
            if (paused) return;
            
            UpdateActiveTweens();
        }
    
        private void UpdateActiveTweens()
        {
            for (int i = 0; i < acitveTweens.Count; i++)
            {
                acitveTweens[i].UpdateTween(Time.deltaTime);
                if (acitveTweens[i].tweenBuildFinished)
                {
                    acitveTweens.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SetPaused()
        {
            if (acitveTweens.Count >= 1)
            {
                paused = true;
            }
        }
    }
}
