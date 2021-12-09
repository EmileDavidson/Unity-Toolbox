using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class Tween : TweenBase
    {
        [PublicAPI] public Action<float> onUpdateTween;
        [PublicAPI] public Action onTweenFinish;
        
        /// <summary>
        /// empty constructor
        /// </summary>
        public Tween(){}

        protected override void UpdateTween()
        {
            onUpdateTween?.Invoke(GetStep());
        }

        protected override void TweenEnd()
        {
            onTweenFinish?.Invoke();
        }
    }
}