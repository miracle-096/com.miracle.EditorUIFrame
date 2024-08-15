using System;
using UIFramework.Core;
using UnityEngine;
using UnityEngine.UIElements;

[UIFramework.UIAttribute(Uxml = "Packages/com.uitoolkit.uiframework/Editor/CustomElement/Foldout/FoldoutHeader.uxml",
    Uss = "Packages/com.uitoolkit.uiframework/Editor/CustomElement/Foldout/FoldoutHeader.uss")]
public partial class FoldoutHeader : UIElement
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