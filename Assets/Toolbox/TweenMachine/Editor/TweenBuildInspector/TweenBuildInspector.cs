using Toolbox.TweenMachine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(TweenBuild))]
public class TweenBuildInspector : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Toolbox/TweenMachine/Editor/TweenBuildMenu.uxml");
        VisualElement uxmlSource = visualTree.CloneTree();
        root.Add(uxmlSource);
        return root;
    }
}
