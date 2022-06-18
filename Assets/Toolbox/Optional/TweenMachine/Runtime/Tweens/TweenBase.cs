using System;
using Toolbox.Animation;
using Toolbox.MethodExtensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox.Optional.TweenMachine
{
    /// <summary>
    /// TweenBase is the base of the Tween classes the generic it needs is the generic of the Inheritance class.
    /// Please do not use any other class because it might give errors. 
    /// </summary>
    [Serializable]
    public abstract class TweenBase

    {
        //variable declaration 
        [SerializeReference] protected AnimationCurve easeCurve = new AnimationCurve().ChainToCurve(EasingTools.easingCurve[EasingTools.EasingType.Linear]);

        [SerializeReference] protected float percent;
        [SerializeReference] public GameObject gameObject;
        [SerializeReference] protected bool paused = false;

        //actions
        [SerializeReference] public UnityAction onTweenStart;
        [SerializeReference] public UnityAction onTweenFinish;
        [SerializeReference] public UnityAction onTweenUpdate;

        public bool IsFinished => percent >= 1;
        protected bool HasStarted => percent > 0;


        #region ========== Constructors ==========

        /// <summary>
        /// Empty constructor so we can have constructor with different parameters in derived classes.
        /// </summary>
        protected TweenBase(){}

        /// <summary>
        /// Base Constructor that need to be in all derived classes! this is used for the generic function to create new tween. 
        /// </summary>
        /// <param name="gameObject"></param>
        protected TweenBase(GameObject gameObject, UnityAction onTweenStart)
        {
            this.gameObject = gameObject;
            this.onTweenStart = onTweenStart;
        }

        #endregion

        #region ========== Tween logic functions ==========

        public abstract void TweenStart();
        protected abstract void UpdateTween();
        protected abstract void TweenEnd();

        #endregion

        #region ========== functions callers =========

        public void Update(float dt)
        {
            if (paused) return;
            if (!HasStarted)
            {
                TweenStart();
                onTweenStart?.Invoke();
            }

            percent += dt / Curve.GetDuration();

            if (!IsFinished)
            {
                onTweenUpdate?.Invoke();
                UpdateTween();
                return;
            }

            onTweenFinish?.Invoke();

            TweenEnd();
        }

        #endregion

        #region ========== Helping functions ==========

        /// <summary>
        /// Get value out curve from current percentage
        /// </summary>
        /// <returns></returns>
        public float GetStep()
        {
            return easeCurve.Evaluate(Curve.GetDuration() / 100 * (percent * 100));
        }

        /// <summary>
        /// Gets the last value in curve this makes it so the new value equals exactly the target value and curve end value
        /// </summary>
        /// <returns></returns>
        public float GetLastCurveValue()
        {
            return (Curve[Curve.keys.Length - 1].value);
        }

        #endregion

        #region ========== ChainSetters ============

        public virtual TweenBase ChainSetGameObject(GameObject newObj)
        {
            gameObject = newObj;
            return this;
        }

        public virtual TweenBase ChainSetCurve(AnimationCurve curve)
        {
            this.Curve = curve;
            return this;
        }

        #endregion

        #region ========== Getters & Setters ============

        public AnimationCurve Curve
        {
            get => easeCurve;
            set => easeCurve = value;
        }

        public GameObject GameObject
        {
            get => gameObject;
            set => gameObject = value;
        }

        #endregion

        #region ========== EDITOR FUNCTIONS ==========

#if UNITY_EDITOR

        public virtual void DrawProperties(Rect currentPosition, out int addedHeight, out Rect newCurrentPosition)
        {
            addedHeight = 0;
            newCurrentPosition = currentPosition;
            
            addedHeight = 0;
            newCurrentPosition = currentPosition;
            
            gameObject = EditorGUI.ObjectField(newCurrentPosition, "GameObject", gameObject, typeof(GameObject), true) as GameObject;
            newCurrentPosition.y += 16;
            addedHeight += 16;

            Curve = EditorGUI.CurveField(newCurrentPosition, "Curve", this.Curve);
        }

#endif

        #endregion
    }
}