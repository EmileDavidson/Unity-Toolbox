using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Toolbox.Animation;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenBuild
    {
        [SerializeReference] private readonly bool _drawer; 
        
        [SerializeReference] private GameObject gameObject;
        [SerializeReference] protected AnimationCurve curve = new AnimationCurve().ChainToCurve(EasingTools.easingCurve[EasingTools.EasingType.Linear]);

        [SerializeReference] public List<TweenBase> tweenList = new List<TweenBase>();
        [SerializeReference] public bool paused = false;

        [SerializeReference] public UnityEvent onTweenBuildFinish = new UnityEvent();
        [SerializeReference] public UnityEvent onTweenBuildUpdate = new UnityEvent();
        [SerializeReference] public UnityEvent onTweenBuildStart = new UnityEvent();

        public TweenBuild(bool aDrawer = true)
        {
            _drawer = aDrawer;
        }

        #region ========= Creators ========

        public T CreateAndAddTween<T>() where T : TweenBase
        {
            if (!typeof(T).HasEmptyConstructor()) return null;
            var tween = (T)Activator.CreateInstance(typeof(T));
            tweenList.Add(tween);
            return tween;
        }
        
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
                if (!typeof(T).HasEmptyConstructor()) return null;
                var emptyTween = (T)Activator.CreateInstance(typeof(T));
                tweenList.Add(emptyTween);
                return emptyTween;
            }

            if (!typeof(T).ContainsConstructorWithTheseParams(parameters)) return null;
            var tween = (T)Activator.CreateInstance(typeof(T), parameters);
            tweenList.Add(tween);
            return tween;
        }

        /// <summary>
        /// Create and add new tween of given type using the given parameters if there was no constructor found with these parameters it created an empty one.
        /// without the target value it still uses the targetObj and speed
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="targetValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateAndAddTween<T>(GameObject targetObj, [NotNull] object targetValue = null!) where T : TweenBase
        {
            if (HasConstructorWithTargetParameter<T>() && targetValue != null &&
                targetValue?.GetType() == GetTargetType<T>())
            {
                var tweenWithTarget = (T)Activator.CreateInstance(typeof(T), targetObj, targetValue);
                tweenList.Add(tweenWithTarget);
                return tweenWithTarget;
            }

            if (!typeof(T).HasEmptyConstructor()) return null;
            var tween = (T)Activator.CreateInstance(typeof(T));
            tween.gameObject = gameObject;
            tweenList.Add(tween);
            return tween;
        }
        
        public T Create<T>(object[] parameters) where T : TweenBase
        {
            if (parameters.IsEmpty())
            {
                if (!typeof(T).HasEmptyConstructor()) return null;
                var emptyTween = (T)Activator.CreateInstance(typeof(T));
                tweenList.Add(emptyTween);
                return emptyTween;
            }

            if (!typeof(T).ContainsConstructorWithTheseParams(parameters)) return null;
            var tween = (T)Activator.CreateInstance(typeof(T), parameters);
            tweenList.Add(tween);
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
            if (HasConstructorWithTargetParameter<T>() && targetValue != null &&
                targetValue.GetType() == GetTargetType<T>())
            {
                var tweenWithTarget = (T)Activator.CreateInstance(typeof(T), targetObj, speed, targetValue);
                tweenList.Add(tweenWithTarget);
                return tweenWithTarget;
            }

            var tween = (T)Activator.CreateInstance(typeof(T));
            tween.gameObject = this.gameObject;
            return tween;
        }

        #endregion

        #region ========== Checkers ==========

        /// <summary>
        /// Checks if the derived class has a function with 3 parameters (First: GameObject, Second: float, third: Object)
        /// if there is more then one constructor with 3 parameters we check if the name of third parameter equals to "targetValue" 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasConstructorWithTargetParameter<T>()
        {
            var possibleConstructors =
                typeof(T).GetConstructors().Where(info => info.GetParameters().Length == 2).ToArray();
            foreach (var constructorInfo in possibleConstructors)
            {
                var parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length < 2) continue;
                if (parameterInfos[0].ParameterType != typeof(GameObject)) continue;
                if (possibleConstructors.Count() > 1 && !parameterInfos[1].Name.Equals("targetValue")) continue;
                return true;
            }

            return false;
        }

        #endregion

        #region ======== helping methods =========

        /// <summary>
        /// removes all the tweens in the build of the given type
        /// </summary>
        /// <param name="removeType"></param>
        public void RemoveAllTweensOfType(Type removeType)
        {
            tweenList.RemoveAll(tween => tween.GetType() == removeType);
        }

        #endregion

        #region ===== tween logic =======

        /// <summary>
        /// Updates the tweens that are not finished yet.
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateTween(float dt)
        {
            if (paused) return;

            onTweenBuildUpdate.Invoke();
            if (IsFinished) return;

            foreach (var tween in tweenList.Where(tween => !tween.IsFinished))
            {
                tween.Update(dt);
            }

            CheckComplete();
        }

        /// <summary>
        /// Adds the tween to the controller so it start running until all tweens are finished or the build is paused.
        /// </summary>
        public void StartTween()
        {
            if (gameObject != null)
            {
                foreach (var tween in tweenList.Where(tween => tween.gameObject == null))
                {
                    tween.gameObject = this.GameObject;
                }
            }

            TweenController.MonoSingleton.Instance.activeBuilds.Add(this);
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
        public bool IsFinished => tweenList.Where(tween => !tween.IsFinished).ToArray().IsEmpty();

        #endregion

        #region ========= Chain Setters =========

        /// <summary>
        /// adds the given tween to the list
        /// </summary>
        /// <param name="tween"></param>
        /// <returns></returns>
        public TweenBuild ChainAdd(TweenBase tween)
        {
            tweenList.Add(tween);
            return this;
        }

        /// <summary>
        /// adds the given tweenList to currentList
        /// </summary>
        /// <param name="tweens"></param>
        /// <returns></returns>
        public TweenBuild ChainAddList(List<TweenBase> tweens)
        {
            tweenList.AddList(tweens);
            return this;
        }

        #endregion

        #region ====== Getters & Setters ========
        
        public bool Drawer => _drawer;


        /// <summary>
        /// Get the index of all constructors with the right parameters / naming 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int GetConstructorWithTargetParameterIndex<T>()
        {
            var possibleIndexes = new List<int>();
            int index = 0;
            foreach (var constructorInfo in typeof(T).GetConstructors())
            {
                var parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length <= 1) continue;
                if (parameterInfos[0].ParameterType != typeof(GameObject)) continue;
                possibleIndexes.Add(index);
                index++;
            }

            if (possibleIndexes.Count <= 1) return possibleIndexes[0];
            for (var i = 0; i < possibleIndexes.Count; i++)
            {
                if (!typeof(T).GetConstructors()[i].GetParameters()[1].Name.Equals("targetValue")) continue;
                return i;
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
            var parameterInfos = typeof(T).GetConstructors()[GetConstructorWithTargetParameterIndex<T>() + 1]
                .GetParameters();
            var type = parameterInfos[1].ParameterType;
            return type;
        }

        public GameObject GameObject
        {
            get => gameObject;
            set => gameObject = value;
        }

        public List<TweenBase> Tweens
        {
            get => tweenList;
            private set => tweenList = value;
        }

        public AnimationCurve Curve
        {
            get => curve;
            set => curve = value;
        }

        #endregion
    }
}