using System;
using UnityEngine;

public static class Easing
{
    public static float Linear(float t) => t;
    public static float InSine(float x) 
    {
        return (float) (1 - Math.Cos((x * Math.PI) / 2));
    }
 
    public static float OutSine(float x)
    {
        return (float)(Math.Sin((x * Math.PI) / 2));
    }
 
    public static float InOutSine(float x)
    {
        return (float) (-(Math.Cos(Math.PI * x) - 1) / 2);
    }
 
 
    public static float InCubic(float x)
    {
        return (x * x * x);
    }
 
 
    public static float OutCubic(float x)
    {
        return (float) (1 - Math.Pow(1 - x, 3));
    }
 
    public static float InOutCubic(float x)
    {
        return (float) (x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2);
    }
 
    public static float InQuint(float x)
    {
        return (x * x * x * x * x);
    }
 
    public static float OutQuint(float x)
    {
        return (float) (1 - Math.Pow(1 - x, 5));
    }
 
    public static float InOutQuint(float x)
    {
        return (float) (x < 0.5 ? 16 * x * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 5) / 2);
    }
 
    public static float InCirc(float x)
    {
        return (float) (1 - Math.Sqrt(1 - Math.Pow(x, 2)));
    }
 
    public static float OutCirc(float x)
    {
        return (float) (Math.Sqrt(1 - Math.Pow(x - 1, 2)));
    }
 
    public static float InOutCirc(float x)
    {
        return (float) (x < 0.5 ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2 : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2);
    }
 
    public static float InElastic(float x)
    {
        float c4 = (float) ((2 * Math.PI) / 3);
 
        return (float) (x == 0 ? 0 : x >= 1 ? 1 : -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * c4));
    }
 
    public static float OutElastic(float x)
    {
        float c4 = (float) ((2 * Math.PI) / 3);
 
        return (float) (x == 0 ? 0 : x >= 1 ? 1 : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * c4) + 1);
    }
 
    public static float InOutElastic(float x)
    {
        float c5 = (float) ((2 * Math.PI) / 4.5);
 
        return (float) (x == 0 ? 0 : x >= 1 ? 1 : x < 0.5 ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 : (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 + 1);
    }
 
 
    // row 2
    public static float InQuad(float x)
    {
        return (x * x);
    }
 
    public static float OutQuad(float x)
    {
        return (1 - (1 - x) * (1 - x));
    }
 
    public static float InOutQuad(float x)
    {
        return (float) (x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2);
    }
 
    public static float InQuart(float x)
    {
        return (x * x * x * x);
    }
 
    public static float OutQuart(float x)
    {
        return (float) (1 - Math.Pow(1 - x, 4));
    }
 
    public static float InOutQuart(float x)
    {
        return (float) (x < 0.5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2);
    }
 
    public static float InExpo(float x)
    {
        return (float) (x == 0 ? 0 : Math.Pow(2, 10 * x - 10));
    }
 
    public static float OutExpo(float x)
    {
        return Math.Abs(x - 1f) < 0 ? 1f : 1 - Mathf.Pow(2, -10 * x);
    }
 
    public static float InOutExpo(float x)
    {
        return (float) (x == 0 ? 0 : x >= 1 ? 1 : x < 0.5 ? Math.Pow(2, 20 * x - 10) / 2 : (2 - Math.Pow(2, -20 * x + 10)) / 2);
    }
 
    public static float InBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        
        return (c3 * x * x * x - c1 * x * x);
    }
 
    public static float OutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
 
        return (float) (1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2));
    }
 
    public static float InOutBack(float x)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
 
        return (float)(x < 0.5 ? (Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2 : (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2);
    }
 
    public static float InBounce(float x)
    {
        return (1 - OutBounce(1 - x));
    }
 
    public static float OutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;
 
        if (x < 1 / d1) return (n1 * x * x);
        if (x < 2 / d1) return (float) (n1 * (x -= 1.5f / d1) * x + 0.75);
        if (x < 2.5 / d1) return (n1 * (x -= 2.25f / d1) * x + 0.9375f);
        return (n1 * (x -= 2.625f / d1) * x + 0.984375f);
    }
 
    public static float InOutBounce(float x)
    {
        return (x < 0.5 ? (1 - OutBounce(1 - 2 * x)) / 2 : (1 + OutBounce(2 * x - 1)) / 2);
    }
}
