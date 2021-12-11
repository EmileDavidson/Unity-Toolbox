using System;
using System.Collections.Generic;

public class EasingDictonary {
    public static Dictionary<EasingType, Func<float, float>> dict = new Dictionary<EasingType, Func<float, float>>
    {
        {EasingType.Linear, Easing.Linear},
        {EasingType.EaseInSine, Easing.InSine},
        {EasingType.EaseOutSine, Easing.OutSine},
        {EasingType.EaseInOutSine, Easing.InOutSine},
        {EasingType.EaseInCubic, Easing.InCubic},
        {EasingType.EaseOutCubic, Easing.OutCubic},
        {EasingType.EaseInOutCubic, Easing.InOutCubic},
        {EasingType.EaseInQuint, Easing.InQuint},
        {EasingType.EaseOutQuint, Easing.OutQuint},
        {EasingType.EaseInOutQuint, Easing.InOutQuint},
        {EasingType.EaseInCirc, Easing.InCirc},
        {EasingType.EaseOutCirc, Easing.OutCirc},
        {EasingType.EaseInOutCirc, Easing.InOutCirc},
        {EasingType.EaseInElastic, Easing.InElastic},
        {EasingType.EaseOutElastic, Easing.OutElastic},
        {EasingType.EaseInOutElastic, Easing.InOutElastic},
        {EasingType.EaseInQuad, Easing.InQuad},
        {EasingType.EaseOutQuad, Easing.OutQuad},
        {EasingType.EaseInOutQuad, Easing.InOutQuad},
        {EasingType.EaseInQuart, Easing.InQuart},
        {EasingType.EaseOutQuart, Easing.OutQuart},
        {EasingType.EaseInOutQuart, Easing.InOutQuart},
        {EasingType.EaseInExpo, Easing.InExpo},
        {EasingType.EaseOutExpo, Easing.OutExpo},
        {EasingType.EaseInOutExpo, Easing.InOutExpo},
        {EasingType.EaseInBack, Easing.InBack},
        {EasingType.EaseOutBack, Easing.OutBack},
        {EasingType.EaseInOutBack, Easing.InOutBack},
        {EasingType.EaseInBounce, Easing.InBounce},
        {EasingType.EaseOutBounce, Easing.OutBounce},
        {EasingType.EaseInOutBounce, Easing.InOutBounce}
    };
}