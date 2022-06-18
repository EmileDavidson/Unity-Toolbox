using System;
using JetBrains.Annotations;
using Toolbox.Animation;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class AnimationCurveExtensions
    {
    
        public static float GetSurface(this AnimationCurve targetCurve, float interval = 0.01f)
        {
            return targetCurve.GetSurface(0, targetCurve.GetDuration());
        }
    
        public static float GetSurface(this AnimationCurve targetCurve, float start, float end, float interval = 0.01f)
        {
            var duration = end - start;
            var surface = 0f;
            var previousCurve = targetCurve.Evaluate(start);
            for (int i = 0; i < duration/interval; i++)
            {
                var currentCurve = targetCurve.Evaluate(start + interval * (i + 1));
                var avgCurve = (currentCurve + previousCurve) / 2;
                surface += avgCurve * interval;
                previousCurve = currentCurve;
            }
            return surface;
        }
    
        public static float GetDuration(this AnimationCurve targetCurve)
        {
            var lastKey = targetCurve.keys[targetCurve.keys.Length - 1];
            return lastKey.time;
        }

        public static void CurveFromMethod(this AnimationCurve curve, EasingTools.EasingFunction function, int resolution = 15)
        {
            curve = EasingTools.GenerateCurve(function, resolution);
        }

        public static AnimationCurve ChainToCurve(this AnimationCurve curve, AnimationCurve newCurve)
        {
            curve = new AnimationCurve(newCurve.keys);
            return curve;
        }

        public static AnimationCurve Clone(this AnimationCurve oldCurve)
        {
            var clonedCurve = new AnimationCurve();
            clonedCurve.keys = (Keyframe[]) oldCurve.keys.Clone();

            return clonedCurve;
        }
    }
}