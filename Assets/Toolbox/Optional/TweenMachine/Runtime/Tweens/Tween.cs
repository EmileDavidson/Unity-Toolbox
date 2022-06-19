using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Optional.TweenMachine
{
    public class Tween : TweenBase
    {
        [SerializeReference, PublicAPI] public Action<float> onUpdateTween;
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