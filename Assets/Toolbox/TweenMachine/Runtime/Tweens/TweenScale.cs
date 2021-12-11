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

        public TweenScale(GameObject gameObject, float speed, Vector3 targetScale)
        {
            this.gameObject = gameObject;
            this._startScale = gameObject.transform.localScale;
            this._targetScale = targetScale;

            _scaleDirection = targetScale - _startScale;

            this.speed = speed;
            this.percent = 0;
        }

        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            gameObject.transform.localScale = _startScale + (_scaleDirection * step);
        }

        protected override void TweenEnd()
        {
            gameObject.transform.localScale = _targetScale;
        }
    }
}

