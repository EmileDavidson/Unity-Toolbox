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

        public TweenRotation(GameObject gameObject, float speed, Quaternion targetRotation)
        {
            this.gameObject = gameObject;
            this._startRotation = gameObject.transform.eulerAngles;
            this._targetRotation = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
    
            this._direction.x = targetRotation.x - _startRotation.x;
            this._direction.y = targetRotation.x - _startRotation.y;
            this._direction.z = targetRotation.x - _startRotation.z;

            this.speed = speed;
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
            gameObject.transform.eulerAngles = new Vector3(_targetRotation.x, _targetRotation.y, _targetRotation.z);
        }
    
    }
}
