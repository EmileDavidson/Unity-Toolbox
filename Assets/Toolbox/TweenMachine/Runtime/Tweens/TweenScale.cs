using System;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenScale : TweenBase
    {
        private Vector3 startScale;
        private Vector3 targetScale;
        private Vector3 scaleDirection;


        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenScale(){}
        
        public TweenScale(GameObject gameObject, Vector3 targetScale, float speed)
        {
            this.gameObject = gameObject;
            this.startScale = gameObject.transform.localScale;
            this.targetScale = targetScale;

            scaleDirection = targetScale - startScale;

            this.speed = speed;
            this.percent = 0;
            this.easeMethode = Easing.Linear;
        }

        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            gameObject.transform.localScale = startScale + (scaleDirection * step);
        }

        protected override void TweenEnd()
        {
            gameObject.transform.localScale = targetScale;
        }
    }
}

