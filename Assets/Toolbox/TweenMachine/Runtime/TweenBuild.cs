using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenBuild
    {
        //default values for all tweens except if customized
        [SerializeReference] private GameObject gameObject;
        [SerializeReference] private float defaultSpeed = 1;
        [SerializeReference] private EasingType defaultEasingType = EasingType.Linear;
        [SerializeReference] public List<TweenBase> tweens = new List<TweenBase>();

        //complete tweens
        public UnityEvent onTweenBuildFinish = new UnityEvent();
        public UnityEvent onTweenBuildUpdate = new UnityEvent();
        public UnityEvent onTweenBuildStart = new UnityEvent();

        /// <summary>
        /// Create and add new tween of given type.
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="speed"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateAndAddTween<T>(GameObject targetObj, float speed = 0) where T : TweenBase
        {
            var tween = (T)Activator.CreateInstance(typeof(T));
            tween.Setup(targetObj, speed);
            tweens.Add(tween);
            return tween;
        }

        /// <summary>
        /// Creates a new tween but does not add it to the build yet.
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="speed"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>(GameObject targetObj, float speed = 0) where T : TweenBase
        {
            var tween = (T)Activator.CreateInstance(typeof(T));
            tween.Setup(gameObject, speed);
            return tween;
        }

        /// <summary>
        /// adds the given tween to the list.
        /// </summary>
        /// <param name="tween"></param>
        /// <returns></returns>
        public TweenBuild AddTween(TweenBase tween)
        {
            tweens.Add(tween);
            return this;
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

        public List<TweenBase> Tweens
        {
            get => tweens;
            private set => tweens = value;
        }
    }
}