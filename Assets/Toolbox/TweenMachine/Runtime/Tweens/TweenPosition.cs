using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenPosition : TweenBase
    {
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private Vector3 _direction;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenPosition() { }


        /// <summary>
        /// Constructor for when you create it without the use of TweenBuild class and add it to the tweens in TweenBuild
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetPos"></param>
        public TweenPosition(GameObject gameObject, Vector3 targetPos)
        {
            this.gameObject = gameObject;
            this._targetPosition = targetPos;
            this._startPosition = gameObject.transform.position;
        }
        
        //========== Tween logic functions ==========

        public override void TweenStart()
        {
            this._direction = _targetPosition - _startPosition;
            this.percent = 0;
        }
        
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            gameObject.transform.position = _startPosition + (_direction * step);
        }
        
        protected override void TweenEnd()
        {
            if (gameObject == null) return;
            gameObject.transform.position = _startPosition + (_direction * GetLastCurveValue());
        }

        //======== CHAIN SETTERS ========
        
        public TweenPosition ChainSetTarget(Vector3 targetPos)
        {
            this._targetPosition = targetPos;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => _targetPosition;
            set => _targetPosition = value;
        }
    }
}
