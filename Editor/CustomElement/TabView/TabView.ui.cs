using System;
using UIFramework.Core;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[UIFramework.UIAttribute(Uxml = "Packages/com.uitoolkit.uiframework/Editor/CustomElement/TabView/TabView.uxml",Uss="Packages/com.uitoolkit.uiframework/Editor/CustomElement/TabView/TabView.uss")]
public partial class TabView : EPanel
{
	public VisualElement TabsContainer;

    
    public TabView(TemplateContainer container) : base(container)
    {
		TabsContainer = Q<VisualElement>("TabsContainer");

    }
}
