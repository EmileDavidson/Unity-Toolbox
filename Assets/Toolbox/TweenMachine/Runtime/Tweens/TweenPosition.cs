using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenPosition : TweenBase
    {
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Vector3 direction;
        
        /// <summary>
        /// empty constructor for generic creation. 
        /// </summary>
        public TweenPosition() { }

        public override void Setup(GameObject aGameObject, float aSpeed)
        {
            this.gameObject = aGameObject;
            this.speed = aSpeed;

            this.startPosition = gameObject.transform.position;
            this.targetPosition = startPosition;
        }
        
        /// <summary>
        /// Constructor for when you create it without the use of TweenBuild class and add it to the tweens in TweenBuild
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetPos"></param>
        /// <param name="speed"></param>
        public TweenPosition(GameObject gameObject, Vector3 targetPos, float speed)
        {
            this.gameObject = gameObject;
            this.targetPosition = targetPos;
            this.speed = speed;

            this.startPosition = gameObject.transform.position;
            this.direction = targetPos - startPosition;
            this.percent = 0;
        
            this.easeMethode = Easing.Linear;
        }

        /// <summary>
        /// When we update the tween we calculate where whe should be and move towards it by using the current %
        /// </summary>
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            gameObject.transform.position = startPosition + (direction * step);
        }

        /// <summary>
        /// Sets the current position to target position for precision 
        /// </summary>
        protected override void TweenEnd()
        {
            gameObject.transform.position = targetPosition;
        }

        //======== CHAIN SETTERS ========
        
        public TweenPosition ChainSetTarget(Vector3 targetPos)
        {
            this.targetPosition = targetPos;

            this.startPosition = gameObject.transform.position;
            this.direction = targetPos - startPosition;
            this.percent = 0;
            return this;
        }

        public TweenPosition ChainReset()
        {
            this.startPosition = gameObject.transform.position;
            this.percent = 0;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => targetPosition;
            set => targetPosition = value;
        }
    }
}
