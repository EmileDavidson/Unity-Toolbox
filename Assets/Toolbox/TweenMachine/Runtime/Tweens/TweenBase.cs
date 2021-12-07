using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Toolbox.TweenMachine.Tweens
{
    [Serializable]
    public abstract class TweenBase
    {
        //variable declaration 
        [SerializeReference] protected Func<float, float> EaseMethode;
        [SerializeReference] protected float speed;
        [SerializeReference] protected float percent;
        [SerializeReference] public GameObject gameObject; 
    
        //actions
        private UnityAction _onTweenStart;
        private UnityAction _onTweenFinish;
        private UnityAction _onTweenUpdate;

        public bool IsFinished => percent >= 1;
        protected bool HasStarted => percent > 0;

        //functions
        public void UpdateTween(float dt)
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
        public virtual TweenBase SetEasing(EasingType easingType)
        {
            if ((easingType == EasingType.AnimationCurve)) return this;
            EaseMethode = EasingDictonary.dict[easingType];
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
        
        public float GetEasingStep => EaseMethode(percent);
        
    }
}
