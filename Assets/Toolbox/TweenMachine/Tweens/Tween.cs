using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Toolbox.TweenMachine.Tweens
{
    [Serializable]
    public abstract class Tween
    {
        //variable declaration 
        protected Func<float, float> EaseMethode;
        protected float speed;
        protected float percent;
        public GameObject gameObject; 
    
        //actions
        private UnityAction _onTweenStart;
        private UnityAction _onTweenFinish;
        private UnityAction _onTweenUpdate;

        public bool IsFinished
        {
            get { return percent >= 1; }
        }
    
        protected bool HasStarted
        {
            get { return percent > 0; }
        }
    
        //functions
        public void Update(float dt)
        {
            //invoke tweenstart action if not started yet
            if(!HasStarted) OnTweenStart?.Invoke();
        
            //invoke update action
            OnTweenUpdate?.Invoke();
        
            percent += dt / speed;
            if (!IsFinished)
            {
                UpdateTween();
                return;
            }
            //invoke finshed action
            OnTweenFinish?.Invoke();
            
            TweenEnd();
        }

        protected abstract void UpdateTween();
        protected abstract void TweenEnd();

        //getters & setters
        public virtual Tween SetEasing(Func<float, float> func)
        {
            EaseMethode = func;
            return this; 
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
