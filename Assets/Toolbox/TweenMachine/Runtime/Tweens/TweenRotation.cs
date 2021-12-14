using System;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenRotation : TweenBase
    {
        private Vector3 _startRotation;
        private Vector3 _targetRotation;
        private Vector3 _direction;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenRotation() { }

        /// <summary>
        /// Constructor with targeted GameObject and target value
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetRotation"></param>
        public TweenRotation(GameObject gameObject, Quaternion targetRotation)
        {
            this.gameObject = gameObject;
            this._startRotation = gameObject.transform.eulerAngles;
            this._targetRotation = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
        }

        //========== Tween logic functions ==========
        public override void TweenStart()
        {
            this._direction.x = _targetRotation.x - _startRotation.x;
            this._direction.y = _targetRotation.y - _startRotation.y;
            this._direction.z = _targetRotation.z - _startRotation.z;

            this.percent = 0;
        }
        
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            float x = _startRotation.x + (_direction.x * step);
            float y = _startRotation.y + (_direction.y * step);
            float z = _startRotation.z + (_direction.z * step);
            
            Vector3 newRotation = new Vector3(x, y, z);
            
            gameObject.transform.eulerAngles = newRotation;
        }

        protected override void TweenEnd()
        {
            gameObject.transform.eulerAngles = _startRotation + (new Vector3(_targetRotation.x, _targetRotation.y, _targetRotation.z) * GetLastCurveValue());
        }
        
        //======== CHAIN SETTERS ========
        
        public TweenRotation ChainSetTarget(Vector3 targetRotation)
        {
            this._targetRotation = targetRotation;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => _targetRotation;
            set => _targetRotation = value;
        }
    
    }
}
