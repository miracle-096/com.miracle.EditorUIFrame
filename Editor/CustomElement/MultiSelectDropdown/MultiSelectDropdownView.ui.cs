using System;
using UIFramework.Core;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[UIFramework.UIAttribute(Uxml = "Packages/com.uitoolkit.uiframework/Editor/CustomElement/MultiSelectDropdown/MultiSelectDropdownView.uxml",Uss="Packages/com.uitoolkit.uiframework/Editor/CustomElement/MultiSelectDropdown/MultiSelectDropdownView.uss")]
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
