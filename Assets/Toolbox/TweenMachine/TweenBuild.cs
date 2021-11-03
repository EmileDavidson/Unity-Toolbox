using System;
using System.Collections.Generic;
using System.Numerics;
using Toolbox.TweenMachine.Tweens;
using UnityEngine;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenBuild
    {
        private GameObject _gameObject;
        public List<Tween> tweens = new List<Tween>();
        
        //complete tweens
        public string name = default;
        public UnityEvent onTweenBuildFinish = new UnityEvent();
        public UnityEvent onTweenBuildUpdate = new UnityEvent();
        public UnityEvent onTweenBuildStart = new UnityEvent();
        public bool tweenBuildFinished = false;

        public TweenBuild() { }
        public TweenBuild(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public GameObject GameObject
        {
            get => _gameObject;
            set => _gameObject = value;
        }

        struct test
        {
            public Action action;
            public System.Type type;
        }
        public Tween Create(Type value, GameObject target)
        {
            // Dictionary<System.Type, test> awomeness = new Dictionary<Type, test>();
            //
            // var test = new test();
            // test.action = () => new TweenColor(target, Color.red)
            //     awomeness.Add(typeof(TweenColor));
            //
            //
            // if (value == typeof(TweenColor))
            // {
            //     return new TweenColor(target, Color.white, 1);
            // }
            //
            // if (value == typeof(TweenPosition))
            // {
            //     return new TweenPosition(target, Vector3.zero, 1);
            // }
            //
            // return null;
            return null;
        }

        //position
        public Tween SetTweenPosition(Vector3 targetPos, float speed)
        {
            Tween newTween = new TweenPosition(_gameObject, targetPos, speed);
            tweens.Add(newTween);
            return newTween;
        }
        
        public Tween SetTweenPosition(Vector3 target, float speed, EasingType easingType)
        {
            Tween tween =  new TweenPosition(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenPosition(TweenPosition tween)
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
        public Tween SetTweenRotation(Quaternion target, float speed)
        {
            Tween tween = new TweenRotation(_gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenRotation(Quaternion target, float speed, EasingType easingType)
        {
            Tween tween = new TweenRotation(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenRotation(TweenRotation tween)
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
        public Tween SetTweenScale(Vector3 target, float speed)
        {
            Tween tween = new TweenScale(_gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenScale(Vector3 target, float speed, EasingType easingType)
        {
            Tween tween = new TweenScale(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenScale(TweenScale tween)
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
        public Tween SetTweenColor(Color target, float speed)
        {
            Tween tween = new TweenColor(_gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenColor(Color target, float speed, EasingType easingType)
        {
            Tween tween = new TweenColor(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }

        public Tween SetTweenColor(TweenColor tween)
        {
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenColor()
        {
            foreach (Tween tween in tweens)
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
            
            foreach (Tween tween in tweens){
                if(!tween.IsFinished) tween.Update(dt);
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
            foreach (Tween tween in tweens)
            {
                if (!tween.IsFinished) return;
            }

            onTweenBuildFinish?.Invoke();
            tweenBuildFinished = true;
        }
    }
}

