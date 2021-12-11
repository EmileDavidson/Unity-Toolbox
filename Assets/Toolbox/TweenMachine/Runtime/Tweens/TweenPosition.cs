using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenPosition : TweenBase
    {
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private Vector3 _direction;

        public override void Setup(GameObject aGameObject, float aSpeed)
        {
            this.gameObject = aGameObject;
            this.speed = aSpeed;

            this._startPosition = gameObject.transform.position;
            this._targetPosition = _startPosition;
        }
        
        /// <summary>
        /// Constructor for when you create it without the use of TweenBuild class and add it to the tweens in TweenBuild
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetPos"></param>
        /// <param name="speed"></param>
        public TweenPosition(GameObject gameObject,float speed, Vector3 targetPos)
        {
            this.gameObject = gameObject;
            this._targetPosition = targetPos;
            this.speed = speed;

            this._startPosition = gameObject.transform.position;
            this._direction = targetPos - _startPosition;
            this.percent = 0;
        }

        /// <summary>
        /// When we update the tween we calculate where whe should be and move towards it by using the current %
        /// </summary>
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            gameObject.transform.position = _startPosition + (_direction * step);
        }

        /// <summary>
        /// Sets the current position to target position for precision 
        /// </summary>
        protected override void TweenEnd()
        {
            gameObject.transform.position = _targetPosition;
        }

        //======== CHAIN SETTERS ========
        
        public TweenPosition ChainSetTarget(Vector3 targetPos)
        {
            this._targetPosition = targetPos;

            this._startPosition = gameObject.transform.position;
            this._direction = targetPos - _startPosition;
            this.percent = 0;
            return this;
        }

        public TweenPosition ChainReset()
        {
            this._startPosition = gameObject.transform.position;
            this.percent = 0;
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
