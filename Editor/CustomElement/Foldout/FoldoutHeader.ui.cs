using UIFramework.Editor.Core;
using UnityEngine.UIElements;

[UIFramework.UIAttribute(Uxml = "Packages/com.miracle.EditorUIFrame/Editor/CustomElement/Foldout/FoldoutHeader.uxml",
    Uss = "Packages/com.miracle.EditorUIFrame/Editor/CustomElement/Foldout/FoldoutHeader.uss")]
public partial class FoldoutHeader : EPanel
{
    public VisualElement FocusBg;
    public VisualElement Root;
    public VisualElement Icon;
    public Label Text;


    public FoldoutHeader(TemplateContainer container) : base(container)
    {
        FocusBg = Q<VisualElement>("focus_bg");
        Root = Q<VisualElement>("root");
        Icon = Q<VisualElement>("icon");
        Text = Q<Label>("text");
    }
}