using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class Tween : TweenBase
    {
        [PublicAPI] public Action<float> onUpdateTween;
        public Tween(){}
        
        //========== Tween logic functions ==========
        public override void TweenStart() { }

        protected override void UpdateTween()
        {
            onUpdateTween?.Invoke(GetStep());
        }

        protected override void TweenEnd() { }
    }
}