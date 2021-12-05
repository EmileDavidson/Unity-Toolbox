using UnityEditor;
using UnityEngine;

namespace Toolbox.Editor
{
    [CustomEditor(typeof(Tester))]
    public class TesterInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Try"))
            {
                Tester tester = target as Tester;
                if(tester != null) tester.Test();
            }
        }
    }
}