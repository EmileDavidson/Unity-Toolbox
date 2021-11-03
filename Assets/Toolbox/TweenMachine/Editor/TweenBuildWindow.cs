using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Toolbox.MethodExtensions;
using Toolbox.TweenMachine.Tweens;
using UnityEditor;
using UnityEngine;

namespace Toolbox.TweenMachine.Editor
{
    public class TweenBuildWindow : EditorWindow
    {
        public TweenBuild TweenBuild { get; set; }
        public GameObject GameObject { get; set; }
        private int _selectionIndex = 0;
        

        public static TweenBuildWindow ShowWindow()
        {
            // Get existing open window or if none, make a new one:
            TweenBuildWindow window = GetWindow<TweenBuildWindow>();
            window.Show();
            return window;
        }

        void OnGUI()
        {
            TweenBuild.GameObject = EditorGUILayout.ObjectField("Target object", GameObject, typeof(GameObject), true) as GameObject;
            TweenBuild.name = EditorGUILayout.TextField("Name", TweenBuild.name);

            TweenChooser();
        }

        private void TweenChooser()
        {
            List<Type> tweenTypes = typeof(Tween).GetInheritedClassesList();
            List<string> typeNames = typeof(Tween).GetInheritedClassesNames();
            List<Type> tweenBuildTweenTypes = new List<Type>();

            if (!TweenBuild.tweens.IsEmpty())
            {
                foreach (var tween in TweenBuild.tweens.Where(tween => tween != null))
                {
                    tweenBuildTweenTypes.Add(tween.GetType());
                }
            }

            GUIStyle style = GUI.skin.GetStyle("popup");
            _selectionIndex = EditorGUILayout.Popup(_selectionIndex, typeNames.ToArray(), style);

            bool contains = false;
            if (!tweenBuildTweenTypes.IsEmpty())
            {
                contains = tweenBuildTweenTypes.Contains(tweenTypes.Get(_selectionIndex));
            }

            if (contains)
            {
                // you bitch!
            }
            else
            {
                if (GUILayout.Button($"add {tweenTypes.Get(_selectionIndex).Name}"))
                {
                    TweenBuild.tweens.Add(TweenBuild.Create(tweenTypes.Get(_selectionIndex), GameObject));
                }
            }

        }
    }
}