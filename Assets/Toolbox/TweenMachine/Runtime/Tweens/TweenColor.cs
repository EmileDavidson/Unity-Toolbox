using System;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    [Serializable]
    public class TweenColor : TweenBase
    {
        [SerializeReference] private Color targetColor;
        [SerializeReference] private Color startingColor;
        [SerializeReference] private Renderer renderer;

        [SerializeReference] private float directionR;
        [SerializeReference] private float directionG;
        [SerializeReference] private float directionB;
        [SerializeReference] private float directionA;
        
        /// <summary>
        /// empty constructor
        /// </summary>
        public TweenColor(){}
        
        /// <summary>
        /// Constructor with all information needed to work and do want you want. 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="targetColor"></param>
        /// <param name="speed"></param>
        public TweenColor(GameObject gameObject, Color targetColor, float speed)
        {
            this.gameObject = gameObject;
            this.targetColor = targetColor;
            this.speed = speed;

            renderer = gameObject.GetOrAddComponent<Renderer>();
            if (renderer != null)
            {
                startingColor = renderer.material.color;

                directionR = targetColor.r - startingColor.r;
                directionG = targetColor.g - startingColor.g;
                directionB = targetColor.b - startingColor.b;
                directionA = targetColor.a - startingColor.a;
            }

            percent = 0;
            easeMethode = Easing.Linear;
        }
        
        protected override void UpdateTween()
        {
            if (gameObject == null) return;
            float step = GetStep();
        
            float r = startingColor.r + (directionR * step);
            float g = startingColor.g + (directionG * step);
            float b = startingColor.b + (directionB * step);
            float a = startingColor.a + (directionA * step);
        
            renderer.material.color = new Color(r, g, b, a);
        }

        protected override void TweenEnd()
        {
            renderer.material.color = targetColor;
        }
    
    }
}