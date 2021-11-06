using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class TweenBuildMenu : EditorWindow
{
    [MenuItem("Window/UI Toolkit/TweenBuildMenu")]
    public static void ShowWindow()
    {
        TweenBuildMenu wnd = GetWindow<TweenBuildMenu>();
        wnd.titleContent = new GUIContent("TweenBuildMenu");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Toolbox/TweenMachine/Editor/TweenBuildMenu.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Toolbox/TweenMachine/Editor/TweenBuildMenu.uss");
    }
}