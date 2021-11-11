using System;
using System.Collections.Generic;
using Toolbox.TweenMachine.Tweens;
using UnityEngine;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class SerializedEvent : UnityEvent{}

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenBuild
    {
        //default values for all tweens except if customized
        [SerializeReference] private GameObject gameObject;
        [SerializeReference] private float defaultSpeed = 1;
        private EasingType defaultEasingType = EasingType.Linear;


        [SerializeReference] public List<TweenBase> tweens = new List<TweenBase>();

        //complete tweens
        public SerializedEvent onTweenBuildFinish = new SerializedEvent();
        public SerializedEvent onTweenBuildUpdate = new SerializedEvent();
        public SerializedEvent onTweenBuildStart = new SerializedEvent();
        public bool tweenBuildFinished = false;

        public TweenBuild()
        {
            
        }
        
        public TweenBuild(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public EasingType DefaultEasingType
        {
            get => defaultEasingType;
            set => defaultEasingType = value;
        }
        public float DefaultSpeed
        {
            get => defaultSpeed;
            set => defaultSpeed = value;
        }

        public GameObject GameObject
        {
            get => gameObject;
            set => gameObject = value;
        }

        //position
        public TweenBase SetTweenPosition(Vector3 targetPos, float speed)
        {
            TweenBase newTween = new TweenPosition(gameObject, targetPos, speed);
            tweens.Add(newTween);
            return newTween;
        }
        
        public TweenBase SetTweenPosition(Vector3 target, float speed, EasingType easingType)
        {
            TweenBase tween =  new TweenPosition(gameObject, target, speed).SetEasing(easingType);
            tweens.Add(tween);
            return tween;
        }
        
        public TweenBase SetTweenPosition(TweenPosition tween)
        {
            tweens.Add(tween);
            return tween;
        }

        public void RemoveAllPositionTweens()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenPosition)
                {
                    tweens.Remove(tween);
                }
            }
        }
        
        //rotation
        public TweenBase SetTweenRotation(Quaternion target, float speed)
        {
            TweenBase tween = new TweenRotation(gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public TweenBase SetTweenRotation(Quaternion target, float speed, EasingType easingType)
        {
            TweenBase tween = new TweenRotation(gameObject, target, speed).SetEasing(easingType);
            tweens.Add(tween);
            return tween;
        }
        
        public TweenBase SetTweenRotation(TweenRotation tween)
        {
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenRotations()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenRotation)
                {
                    tweens.Remove(tween);
                }
            }
        }
        
        //scale
        public TweenBase SetTweenScale(Vector3 target, float speed)
        {
            TweenBase tween = new TweenScale(gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public TweenBase SetTweenScale(Vector3 target, float speed, EasingType easingType)
        {
            TweenBase tween = new TweenScale(gameObject, target, speed).SetEasing(easingType);
            tweens.Add(tween);
            return tween;
        }
        
        public TweenBase SetTweenScale(TweenScale tween)
        {
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenScale()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenScale)
                {
                    tweens.Remove(tween);
                }
            }
        }
        
        //color
        public TweenBase SetTweenColor(Color target, float speed)
        {
            TweenBase tween = new TweenColor(gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public TweenBase SetTweenColor(Color target, float speed, EasingType easingType)
        {
            TweenBase tween = new TweenColor(gameObject, target, speed).SetEasing(easingType);
            tweens.Add(tween);
            return tween;
        }

        public TweenBase SetTweenColor(TweenColor tween)
        {
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenColor()
        {
            foreach (TweenBase tween in tweens)
            {
                if (tween is TweenColor)
                { 
                    tweens.Remove(tween);
                }
            }
        }

        
        //functional functions
        public void UpdateTween(float dt)
        {
            onTweenBuildUpdate.Invoke();
            
            if (tweenBuildFinished) return;
            
            foreach (TweenBase tween in tweens){
                if(!tween.IsFinished) tween.UpdateTween(dt);
            }

            CheckComplete();
        }
        
        public void StartTween()
        {
            foreach (var tween in tweens)
            {
                if (tween.gameObject == null) tween.gameObject = this.GameObject;
            }
            TweenController.Instance.acitveTweens.Add(this);
            onTweenBuildStart.Invoke();
        }

        private void CheckComplete()
        {
            foreach (TweenBase tween in tweens)
            {
                if (!tween.IsFinished) return;
            }

            onTweenBuildFinish?.Invoke();
            tweenBuildFinished = true;
        }
    }
}

