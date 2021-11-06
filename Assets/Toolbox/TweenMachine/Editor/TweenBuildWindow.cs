using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.MethodExtensions;
using Toolbox.TweenMachine.Tweens;
using UnityEditor;
using UnityEngine;

namespace Toolbox.TweenMachine.Editor
{
    public class TweenBuildWindow : EditorWindow
    {
        private List<bool> foldOuts = new List<bool>();
        private bool oldPlay = false;
        private bool reopenNeeded = false;

        private TweenBuild tweenBuild;

        public static TweenBuildWindow ShowWindow(TweenBuild tweenBuild)
        {
            // Get existing open window or if none, make a new one:
            TweenBuildWindow window = GetWindow<TweenBuildWindow>();
            window.Show();
            window.reopenNeeded = false;
            window.tweenBuild = tweenBuild;
            return window;
        }

        void OnGUI()
        {
            if (reopenNeeded)
            {
                EditorGUILayout.HelpBox("Please reopen this menu to start editing again", MessageType.Info);
                return;
            }

            if (oldPlay)
            {
                EditorGUILayout.HelpBox("All changes made will not be saved when leaving playMode", MessageType.Error);
            }
            
            tweenBuild.GameObject = EditorGUILayout.ObjectField(new GUIContent("Target object", "Target object that all Tweens use except if they have there own custom target"), tweenBuild.GameObject, typeof(GameObject), true) as GameObject;
            EditorGUILayout.Space();
            
            TweenDisplayer();

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset"))
            {
                //reset logica
                tweenBuild.GameObject = null;
                tweenBuild.tweens.Clear();
            }

            if (GUILayout.Button("Copy"))
            {
                Debug.Log("Function not implemented yet.");
            }

            if (GUILayout.Button("Paste"))
            {
                Debug.Log("Function not implemented yet.");
            }

            GUILayout.EndHorizontal();
        }

        private void TweenDisplayer()
        {
            List<Type> tweenTypes = typeof(Tween).GetInheritedClassesList();
            List<string> typeNames = typeof(Tween).GetInheritedClassesNames();
            List<Type> createdTweens = new List<Type>();
            if (foldOuts.IsEmpty())
            {
                for (int i = 0; i < tweenTypes.Count; i++) foldOuts.Add(false);
            }

            if (!tweenBuild.tweens.IsEmpty())
            {
                foreach (var tween in tweenBuild.tweens.Where(tween => tween != null))
                {
                    createdTweens.Add(tween.GetType());
                }
            }

            int index = 0;
            foreach (Type type in tweenTypes)
            {
                bool contains = createdTweens.Contains(type);
                CreateFoldOuts(type, index, contains);
                index++;
            }
        }
        
        private void DrawData(Type type){}

        private void DrawAddButton(Type type)
        {
            if (GUILayout.Button($"add: {type.Name}"))
            {
                tweenBuild.tweens.Add(TweenCreator.CreateTween[type].Invoke());
            }
        }

        private void CreateFoldOuts(Type tweenType, int index, bool contains)
        {
            foldOuts[index] = EditorGUILayout.Foldout(foldOuts[index], tweenType.Name);
            if (foldOuts[index])
            {
                if (contains) DrawData(tweenType);
                else DrawAddButton(tweenType);
            }
        }
        

        private void Update()
        {
            bool newPlay = EditorApplication.isPlaying;
            if (oldPlay == newPlay) return;
            oldPlay = newPlay;
            reopenNeeded = true;
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }
    }
}