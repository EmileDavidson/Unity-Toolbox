using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox.Optional.TweenMachine
{
    public class Tween : TweenBase
    {
        public Tween(){}
        
        //========== Tween logic functions ==========

        public override void TweenStart() { }

        protected override void UpdateTween()
        {
            onTweenStepUpdate?.Invoke(GetStep());
        }

        protected override void TweenEnd() { }

        public override void DrawProperties(Rect currentPosition,SerializedProperty property, out int addedHeight, out Rect newCurrentPosition)
        {
            addedHeight = 0;
            newCurrentPosition = currentPosition;
            
            base.DrawProperties(currentPosition, property, out addedHeight, out newCurrentPosition);
            newCurrentPosition.y += 16;
            addedHeight += 16;
            
            //draw unity events
            DrawEventProperties(newCurrentPosition, property, out var eventHeight, out var eventNewPosition);
            addedHeight += eventHeight;
            newCurrentPosition = eventNewPosition;
        }
    }
}