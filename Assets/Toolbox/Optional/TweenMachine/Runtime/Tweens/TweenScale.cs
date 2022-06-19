using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Optional.TweenMachine
{
    [Serializable]
    public class TweenScale : TweenBase
    {
        private Vector3 _startScale;
        [SerializeReference] private Vector3 targetScale;
        private Vector3 _scaleDirection;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenScale()
        {
        }
        
        /// <summary>
        /// Constructor with targeted GameObject and target value
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetScale"></param>
        public TweenScale(GameObject gameObject, Vector3 targetScale)
        {
            this.gameObject = gameObject;
            this.targetScale = targetScale;
        }
        
        //========== Tween logic functions ==========
        public override void TweenStart()
        {
            _startScale = gameObject.transform.localScale;
            
            _scaleDirection = targetScale - _startScale; 
            this.percent = 0;
        }

        protected override void UpdateTween()
        {
            float step = GetStep();
            gameObject.transform.localScale = _startScale + (_scaleDirection * step);
        }

        protected override void TweenEnd()
        {
            gameObject.transform.localScale = _startScale + (_scaleDirection * GetLastCurveValue());
        }
        
        //======== CHAIN SETTERS ========
        
        public TweenScale ChainSetTarget(Vector3 target)
        {
            this.targetScale = target;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => targetScale;
            set => targetScale = value;
        }
        
#if UNITY_EDITOR
        
        public override void DrawProperties(Rect currentPosition, out int addedHeight, out Rect newCurrentPosition)
        {
            addedHeight = 0;
            newCurrentPosition = currentPosition;
            
            base.DrawProperties(currentPosition, out addedHeight, out newCurrentPosition);
            newCurrentPosition.y += 16;
            addedHeight += 16;

            targetScale = EditorGUI.Vector3Field(newCurrentPosition, "Target Vector", targetScale);
        }
        
#endif
    }
}

