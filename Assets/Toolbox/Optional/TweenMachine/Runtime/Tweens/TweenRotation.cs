using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Optional.TweenMachine
{
    [Serializable]
    public class TweenRotation : TweenBase
    {
        [SerializeReference] private Vector3 targetRotation;

        private Vector3 _startRotation;
        private Vector3 _direction;

        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenRotation() { }

        /// <summary>
        /// Constructor with targeted GameObject and target value
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetRotation"></param>
        public TweenRotation(GameObject gameObject, Quaternion targetRotation)
        {
            this.gameObject = gameObject;
            this._startRotation = gameObject.transform.eulerAngles;
            this.targetRotation = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
        }

        //========== Tween logic functions ==========
        public override void TweenStart()
        {
            _startRotation = gameObject.transform.rotation.eulerAngles;
            
            this._direction.x = targetRotation.x - _startRotation.x;
            this._direction.y = targetRotation.y - _startRotation.y;
            this._direction.z = targetRotation.z - _startRotation.z;

            this.percent = 0;
        }
        
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            float x = _startRotation.x + (_direction.x * step);
            float y = _startRotation.y + (_direction.y * step);
            float z = _startRotation.z + (_direction.z * step);
            
            Vector3 newRotation = new Vector3(x, y, z);
            
            gameObject.transform.eulerAngles = newRotation;
        }

        protected override void TweenEnd()
        {
            gameObject.transform.eulerAngles = _startRotation + (new Vector3(targetRotation.x, targetRotation.y, targetRotation.z) * GetLastCurveValue());
        }
        
        //======== CHAIN SETTERS ========
        
        public TweenRotation ChainSetTarget(Vector3 target)
        {
            this.targetRotation = target;
            return this;
        }

        //getters & setter
        public Vector3 Target
        {
            get => targetRotation;
            set => targetRotation = value;
        }

#if UNITY_EDITOR
        
        public override void DrawProperties(Rect currentPosition,SerializedProperty property, out int addedHeight, out Rect newCurrentPosition)
        {
            addedHeight = 0;
            newCurrentPosition = currentPosition;
            
            base.DrawProperties(currentPosition, property, out addedHeight, out newCurrentPosition);
            newCurrentPosition.y += 16;
            addedHeight += 16;

            targetRotation = EditorGUI.Vector3Field(newCurrentPosition, "Target Vector",targetRotation);
        }
        
#endif
    }
}
