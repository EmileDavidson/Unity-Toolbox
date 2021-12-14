using System;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenScale : TweenBase
    {
        private Vector3 _startScale;
        private Vector3 _targetScale;
        private Vector3 _scaleDirection;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenScale() { }
        
        /// <summary>
        /// Constructor with targeted GameObject and target value
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetScale"></param>
        public TweenScale(GameObject gameObject, Vector3 targetScale)
        {
            this.gameObject = gameObject;
            this._startScale = gameObject.transform.localScale;
            this._targetScale = targetScale;
        }
        
        //========== Tween logic functions ==========
        public override void TweenStart()
        {
            _scaleDirection = _targetScale - _startScale; 
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
        
        public TweenScale ChainSetTarget(Vector3 targetScale)
        {
            this._targetScale = targetScale;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => _targetScale;
            set => _targetScale = value;
        }
    }
}

