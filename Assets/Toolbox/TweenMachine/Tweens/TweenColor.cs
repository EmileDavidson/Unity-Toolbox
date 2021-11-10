using System;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox.TweenMachine.Tweens
{
    [Serializable]
    public class TweenColor : TweenBase
    {
        private Color targetColor;
        private Color startingColor;
        [SerializeReference]private Renderer _renderer;

        [SerializeReference]private float _directionR;
        [SerializeReference]private float _directionG;
        [SerializeReference]private float _directionB;
        [SerializeReference] private float _directionA;

        //constructor
        public TweenColor(){}
        public TweenColor(GameObject gameObject, Color targetColor, float speed)
        {
            this.gameObject = gameObject;
            this.targetColor = targetColor;
            this.speed = speed;

            _renderer = gameObject.GetComponent<Renderer>();
            if(_renderer is null)
            {
                Debug.Log("Trying to add tween color to GameObject that does not have a renderer ");
                _renderer = gameObject.AddComponent<Renderer>();
            }
            if (_renderer != null)
            {
                startingColor = _renderer.material.color;

                _directionR = targetColor.r - startingColor.r;
                _directionG = targetColor.g - startingColor.g;
                _directionB = targetColor.b - startingColor.b;
                _directionA = targetColor.a - startingColor.a;
            }

            percent = 0;
            EaseMethode = Easing.Linear;
        }

        //update

        protected override void UpdateTween()
        {
            float easingstep = EaseMethode(percent);
        
            float r = startingColor.r + (_directionR * easingstep);
            float g = startingColor.g + (_directionG * easingstep);
            float b = startingColor.b + (_directionB * easingstep);
            float a = startingColor.a + (_directionA * easingstep);
        
            _renderer.material.color = new Color(r, g, b, a);
        }

        protected override void TweenEnd()
        {
            _renderer.material.color = targetColor;
        }
    
    }
}