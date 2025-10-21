using UIFramework.Editor.Core;
using UnityEngine.UIElements;
[UIFramework.UIAttribute(Uxml = "Packages/com.miracle.EditorUIFrame/Editor/CustomElement/MultiSelectDropdown/MultiSelectDropdownView.uxml",Uss="Packages/com.miracle.EditorUIFrame/Editor/CustomElement/MultiSelectDropdown/MultiSelectDropdownView.uss")]
public partial class MultiSelectDropdownView : EPanel
{
	public TextField DisplayField;
	public Button DropdownButton;

    
    public MultiSelectDropdownView(TemplateContainer container) : base(container)
    {
		DisplayField = Q<TextField>("displayField");
		DropdownButton = Q<Button>("dropdownButton");

    }
}
