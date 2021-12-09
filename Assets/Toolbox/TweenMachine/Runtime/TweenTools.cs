using System;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.TweenMachine
{
    public class TweenCreator
    {
        public static readonly Dictionary<System.Type, Func<TweenBase>> CreateTween = new Dictionary<Type, Func<TweenBase>>
        {
            {typeof(TweenColor), (()=> new TweenColor()) },
            {typeof(TweenPosition), (()=> new TweenPosition()) },
            {typeof(TweenRotation), (()=> new TweenRotation()) },
            {typeof(TweenScale), (()=> new TweenScale()) }
        };
    }
}