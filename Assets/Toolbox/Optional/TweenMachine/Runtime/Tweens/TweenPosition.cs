using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Optional.TweenMachine
{
    [Serializable]
    public class TweenPosition : TweenBase
    {
        [SerializeReference] private Vector3 targetPosition;

        private Vector3 _startPosition;
        private Vector3 _direction;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenPosition() { }


        /// <summary>
        /// Constructor for when you create it without the use of TweenBuild class and add it to the tweens in TweenBuild
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetPos"></param>
        public TweenPosition(GameObject gameObject, Vector3 targetPos)
        {
            this.gameObject = gameObject;
            this.targetPosition = targetPos;
            this._startPosition = gameObject.transform.position;
        }
        

        //========== Tween logic functions ==========

        public override void TweenStart()
        {
            this._startPosition = gameObject.transform.position;
            this._direction = targetPosition - _startPosition;
            this.percent = 0;
        }
        
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            gameObject.transform.position = _startPosition + (_direction * step);
        }
        
        protected override void TweenEnd()
        {
            if (gameObject == null) return;
            gameObject.transform.position = _startPosition + (_direction * GetLastCurveValue());
        }

        //======== CHAIN SETTERS ========
        
        public TweenPosition ChainSetTarget(Vector3 targetPos)
        {
            this.targetPosition = targetPos;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => targetPosition;
            set => targetPosition = value;
        }
        
        #region ========== EDITOR FUNCTIONS ==========

#if UNITY_EDITOR

        public override void DrawProperties(Rect currentPosition, out int addedHeight, out Rect newCurrentPosition)
        {
            addedHeight = 0;
            newCurrentPosition = currentPosition;
            
            base.DrawProperties(currentPosition, out addedHeight, out newCurrentPosition);
            newCurrentPosition.y += 16;
            addedHeight += 16;

            targetPosition = EditorGUI.Vector3Field(newCurrentPosition, "Target vector", targetPosition);
        }

#endif

        #endregion
    }
}
