using System;
using System.Collections.Generic;
using Toolbox.TweenMachine.Tweens;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenCreator
    {
        public static readonly Dictionary<System.Type, Func<Tween>> CreateTween = new Dictionary<Type, Func<Tween>>
        {
            {typeof(TweenColor), (()=> new TweenColor()) },
            {typeof(TweenPosition), (()=> new TweenPosition()) },
            {typeof(TweenRotation), (()=> new TweenRotation()) },
            {typeof(TweenScale), (()=> new TweenScale()) }
        };
    }
}