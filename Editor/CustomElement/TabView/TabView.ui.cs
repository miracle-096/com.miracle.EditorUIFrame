using UIFramework.Editor.Core;
using UnityEngine.UIElements;
[UIFramework.UIAttribute(Uxml = "Packages/com.miracle.EditorUIFrame/Editor/CustomElement/TabView/TabView.uxml",Uss="Packages/com.miracle.EditorUIFrame/Editor/CustomElement/TabView/TabView.uss")]
public partial class TabView : EPanel
{
	public VisualElement TabsContainer;

    
    public TabView(TemplateContainer container) : base(container)
    {
		TabsContainer = Q<VisualElement>("TabsContainer");

    }
}
