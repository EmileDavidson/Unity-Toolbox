using System;
using System.Reflection;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenColor : TweenBase
    {
        [SerializeReference] private Color targetColor;
        
        private Color _startingColor;
        private Renderer _renderer;
        private float _directionR;
        private float _directionG;
        private float _directionB;
        private float _directionA;


        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenColor()
        {
        }

        /// <summary>
        /// Constructor with all information needed to work and do want you want. 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetColor"></param>
        public TweenColor(GameObject gameObject, Color targetColor)
        {
            this.gameObject = gameObject;
            this.targetColor = targetColor;
        }

        //========== Tween logic functions ==========
        public override void TweenStart()
        {
            _renderer = gameObject.GetOrAddComponent<Renderer>();
            if (_renderer != null)
            {
                _startingColor = _renderer.material.color;

                _directionR = targetColor.r - _startingColor.r;
                _directionG = targetColor.g - _startingColor.g;
                _directionB = targetColor.b - _startingColor.b;
                _directionA = targetColor.a - _startingColor.a;
            }

            percent = 0;
        }

        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
            float r = _startingColor.r + (_directionR * step);
            float g = _startingColor.g + (_directionG * step);
            float b = _startingColor.b + (_directionB * step);
            float a = _startingColor.a + (_directionA * step);
        
            _renderer.material.color = new Color(r, g, b, a);
        }

        protected override void TweenEnd()
        {
            float r = _startingColor.r + (_directionR * GetLastCurveValue());
            float g = _startingColor.g + (_directionG * GetLastCurveValue());
            float b = _startingColor.b + (_directionB * GetLastCurveValue());
            float a = _startingColor.a + (_directionA * GetLastCurveValue());
            
            _renderer.material.color = new Color(r,g,b,a);
        }
        
        
        //======== CHAIN SETTERS ========
        
        public TweenColor ChainSetTarget(Color target)
        {
            this.targetColor = target;
            return this;
        }

        //getters & setter
        public Color Target
        {
            get => targetColor;
            set => targetColor = value;
        }

    }
}