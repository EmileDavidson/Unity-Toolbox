using System;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenRotation : TweenBase
    {
        private Vector3 startRotation;
        private Vector3 targetRotation;
        private Vector3 direction;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenRotation(){}

        public TweenRotation(GameObject gameObject, Quaternion targetRotation, float speed)
        {
            this.gameObject = gameObject;
            this.startRotation = gameObject.transform.eulerAngles;
            this.targetRotation = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
    
            this.direction.x = targetRotation.x - startRotation.x;
            this.direction.y = targetRotation.x - startRotation.y;
            this.direction.z = targetRotation.x - startRotation.z;

            this.speed = speed;
            this.percent = 0;
            this.easeMethode = Easing.Linear;
        }

        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            
            float x = startRotation.x + (direction.x * step);
            float y = startRotation.y + (direction.y * step);
            float z = startRotation.z + (direction.z * step);
            
            Vector3 newRotation = new Vector3(x, y, z);
            
            gameObject.transform.eulerAngles = newRotation;
        }

        protected override void TweenEnd()
        {
            gameObject.transform.eulerAngles = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
        }
    
    }
}
