using System;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public abstract class TweenBase
    {
        //variable declaration 
        protected AnimationCurve easeCurve = new AnimationCurve();
        protected bool useCurve = true;
        
        protected float speed;
        protected float percent;
        public GameObject gameObject; 
    
        //actions
        private UnityAction _onTweenStart;
        private UnityAction _onTweenFinish;
        private UnityAction _onTweenUpdate;

        public bool IsFinished => percent >= 1;
        protected bool HasStarted => percent > 0;


        /// <summary>
        /// Empty constructor so we can have constructor with different parameters in derived classes.
        /// </summary>
        public TweenBase(){}
        
        /// <summary>
        /// Base Constructor that need to be in all derived classes! this is used for the generic function to create new tween. 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="speed"></param>
        public TweenBase(GameObject gameObject, float speed)
        {
            this.gameObject = gameObject;
            this.speed = speed;
        }

        public virtual void Setup(GameObject aGameObject, float aSpeed)
        {
            this.gameObject = aGameObject;
            this.speed = aSpeed;
        }
        
        //Methods 
        public virtual void TweenStart()
        {
            if (useCurve)
            {
                speed = easeCurve.GetDuration();
            }
        }
        
        public void UpdateTween(float dt)
        {
            if(!HasStarted)
            {
                TweenStart();
                OnTweenStart?.Invoke();
            }
            
            OnTweenUpdate?.Invoke();
        
            
            percent += dt / speed;
            if (!IsFinished)
            {
                UpdateTween();
                return;
            }
            OnTweenFinish?.Invoke();
            
            TweenEnd();
        }

        protected abstract void UpdateTween();
        protected abstract void TweenEnd();

        public float GetStep()
        {
            return easeCurve.Evaluate(percent);
        }
        
        //Chain Setters
        public TweenBase ChainSetGameObject(GameObject newObj)
        {
            gameObject = newObj;
            return this;
        }
        
        public TweenBase ChainSetSpeed(float newSpeed)
        {
            speed = newSpeed;
            return this;
        }

        //Getters and setters

        public AnimationCurve Curve
        {
            get => easeCurve;
            set => easeCurve = value;
        }
        
        public bool UseCurve
        {
            get => useCurve;
            set => useCurve = value;
        }
        public GameObject GameObject
        {
            get => gameObject;
            set => gameObject = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        public UnityAction OnTweenFinish
        {
            get => _onTweenFinish;
            set => _onTweenFinish = value;
        }

        public UnityAction OnTweenStart
        {
            get => _onTweenStart;
            set => _onTweenStart = value;
        }

        public UnityAction OnTweenUpdate
        {
            get => _onTweenUpdate;
            set => _onTweenUpdate = value;
        }

        public void AddListener(UnityAction action, UnityAction del)
        {
            action += del;
        }
    }
}
