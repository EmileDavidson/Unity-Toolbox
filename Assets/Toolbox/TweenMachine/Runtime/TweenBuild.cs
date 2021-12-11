using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenBuild
    {
        [SerializeReference] private GameObject gameObject;
        [SerializeReference] private float defaultSpeed = 1;
        [SerializeReference] private AnimationCurve curve = new AnimationCurve();
        [SerializeReference] public List<TweenBase> tweens = new List<TweenBase>();
        
        public UnityEvent onTweenBuildFinish = new UnityEvent();
        public UnityEvent onTweenBuildUpdate = new UnityEvent();
        public UnityEvent onTweenBuildStart = new UnityEvent();

        /// <summary>
        /// creates and add tween when there is a constructor containing all given parameters in the right order.
        /// </summary>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateAndAddTween<T>(object[] parameters) where T : TweenBase
        {
            if (parameters.IsEmpty())
            {
                var emptyTween = (T)Activator.CreateInstance(typeof(T));
                tweens.Add(emptyTween);
                return emptyTween;
            }
            
            if (!typeof(T).ContainsConstructorWithTheseParams(parameters)) return null;
            var tween = (T)Activator.CreateInstance(typeof(T), parameters);
            tweens.Add(tween);
            return tween;
        }

        /// <summary>
        /// Create and add new tween of given type using the given parameters if there was no constructor found with these parameters it created an empty one.
        /// without the target value it still uses the targetObj and speed
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="speed"></param>
        /// <param name="targetValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateAndAddTween<T>(GameObject targetObj, float speed, object targetValue = null) where T : TweenBase
        {
            if (HasConstructorWithTargetParameter<T>() && targetValue != null && targetValue.GetType() == GetTargetType<T>())
            {
                var tweenWithTarget = (T) Activator.CreateInstance(typeof(T), targetObj, speed, targetValue);
                tweens.Add(tweenWithTarget);
                return tweenWithTarget;
            }
     
            var tween = (T)Activator.CreateInstance(typeof(T));
            tween.Setup(targetObj, speed);
            tweens.Add(tween);
            return tween;   
        }

        public T Create<T>(object[] parameters) where T : TweenBase
        {
            if (parameters.IsEmpty())
            {
                var emptyTween = (T)Activator.CreateInstance(typeof(T));
                tweens.Add(emptyTween);
                return emptyTween;
            }

            var tween = (T)Activator.CreateInstance(typeof(T), parameters);
            tweens.Add(tween);
            return tween;
        }

        /// <summary>
        /// Create new tween of given type using the given parameters if there was no constructor found with these parameters it created an empty one.
        /// without the target value it still uses the targetObj and speed
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="speed"></param>
        /// <param name="targetValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>(GameObject targetObj, float speed, object targetValue = null) where T : TweenBase
        {
            if (HasConstructorWithTargetParameter<T>() && targetValue != null && targetValue.GetType() == GetTargetType<T>())
            {
                var tweenWithTarget = (T) Activator.CreateInstance(typeof(T), targetObj, speed, targetValue);
                tweens.Add(tweenWithTarget);
                return tweenWithTarget;
            }
     
            var tween = (T)Activator.CreateInstance(typeof(T));
            tween.Setup(targetObj, speed);
            return tween;
        }
        
        /// <summary>
        /// adds the given tween to the list
        /// </summary>
        /// <param name="tween"></param>
        /// <returns></returns>
        public TweenBuild ChainAdd(TweenBase tween)
        {
            tweens.Add(tween);
            return this;
        }

        /// <summary>
        /// Checks if the derived class has a function with 3 parameters (First: GameObject, Second: float, third: Object)
        /// if there is more then one constructor with 3 parameters we check if the name of third parameter equals to "targetValue" 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasConstructorWithTargetParameter<T>()
        {
            var possibleConstructors = typeof(T).GetConstructors().Where(info => info.GetParameters().Length == 3).ToArray();
            foreach (var constructorInfo in possibleConstructors)
            {
                var parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length < 3) continue;
                if (parameterInfos[0].ParameterType != typeof(GameObject)) continue;
                if (parameterInfos[1].ParameterType != typeof(float)) continue;
                if (possibleConstructors.Count() > 1 && !parameterInfos[2].Name.Equals("targetValue")) continue;

                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the index of all constructors with the right parameters / naming 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int GetConstructorWithTargetParameterIndex<T>()
        {
            var possibleConstructors = typeof(T).GetConstructors().Where(info => info.GetParameters().Length == 3).ToArray();
            int index = -1;
            foreach (var constructorInfo in possibleConstructors)
            {
                index++;
                var parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length < 3) continue;
                if (parameterInfos[0].ParameterType != typeof(GameObject)) continue;
                if (parameterInfos[1].ParameterType != typeof(float)) continue;
                if (possibleConstructors.Count() > 1 && !parameterInfos[2].Name.Equals("targetValue")) continue;

                return index;
            }
            return -1;
        }

        /// <summary>
        /// Gets the target type from the constructor 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Type GetTargetType<T>() where T : TweenBase
        {
            if (!HasConstructorWithTargetParameter<T>()) return null;
            var parameterInfos = typeof(T).GetConstructors()[GetConstructorWithTargetParameterIndex<T>()].GetParameters();
            
            var type = parameterInfos[2].ParameterType;
            return type;
        }

        /// <summary>
        /// removes all the tweens in the build of the given type
        /// </summary>
        /// <param name="removeType"></param>
        public void RemoveAllTweensOfType(Type removeType)
        {
            tweens.RemoveAll(tween => tween.GetType() == removeType);
        }
        
        /// <summary>
        /// Updates the tweens that are not finished yet.
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateTween(float dt)
        {
            onTweenBuildUpdate.Invoke();
            if (IsFinished) return;

            foreach (var tween in tweens.Where(tween => !tween.IsFinished))
            {
                tween.UpdateTween(dt);
            }

            CheckComplete();
        }

        /// <summary>
        /// Adds the tween to the controller so it start running until all tweens are finished or the build is paused.
        /// </summary>
        public void StartTween()
        {
            foreach (var tween in tweens.Where(tween => tween.gameObject == null))
            {
                tween.gameObject = this.GameObject;
            }

            TweenController.Instance.activeBuilds.Add(this);
            onTweenBuildStart.Invoke();
        }

        /// <summary>
        /// Checks if all tweens in this build are completed. and invokes event if it is. 
        /// </summary>
        private void CheckComplete()
        {
            if (IsFinished) return;
            onTweenBuildFinish?.Invoke();
        }

        /// <summary>
        /// returns if there is still a tween running in this build.
        /// </summary>
        public bool IsFinished => tweens.Where(tween => !tween.IsFinished).ToArray().IsEmpty();
        
        //======= getters && setter
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

        public List<TweenBase> Tweens
        {
            get => tweens;
            private set => tweens = value;
        }
    }
}