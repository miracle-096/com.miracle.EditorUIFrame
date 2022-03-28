using System;
using System.Collections.Generic;
using UIFramework.Editor.Extends;
using UIFramework.GenUICode.Component;
using UIFramework.Utility.GenUICode;
using UnityEngine.UIElements;

namespace UIFramework.Editor.CustomElement.Foldout
{
    [UI(Uxml = "Packages/com.uitoolkit.uiframework/Editor/CustomElement/Foldout/FoldoutHeader.uxml",
        Uss = "Packages/com.uitoolkit.uiframework/Editor/CustomElement/Foldout/FoldoutHeader.uss")]
    public class FoldoutHeader : Editor.Core.TUIElement
    {
        public GenUIPanel toggleParent;

        public VisualElement Root;
        public VisualElement Icon;
        public VisualElement FocusBg;
        public Label label;
        public string elementName;
        public Type elementType;
        public bool isChecked;
        public bool isFold = true;
        public List<VisualElement> CustomElements;
        
        public FoldoutHeader(TemplateContainer templateContainer) : base(templateContainer)
        {
            CustomElements = new List<VisualElement>();
            RootContainer.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            Root = Q<VisualElement>("root");
            Icon = Q<VisualElement>("icon");
            FocusBg = Q<VisualElement>("focus_bg");
            label = Q<Label>("text");
        }

        public void Init(Editor.Core.TUIElement parent, string text,string elementName = null,Type type = null)
        {
            toggleParent = parent as GenUIPanel;
            label.text = text;
            this.elementName = elementName;
            elementType = type;
        }

        protected override void InitComponent()
        {
            
        }

        protected override void OnCreate(params object[] objs)
        {
            base.OnCreate(objs);
            Root.AddComponent<LabelComponent>(this);
        }

        public void ChangeLabelClass(string className)
        {
            label.ClearClassList();
            label.AddToClassList(className);
        }

        public void SwitchFocusBg(bool toShow)
        {
            FocusBg.style.visibility = toShow
                ? new StyleEnum<Visibility>(Visibility.Visible)
                : new StyleEnum<Visibility>(Visibility.Hidden);
        }

        public void HideIcon()
        {
            Icon.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
        }

        public void SetRootPadding(int pixel, int heirarchy)
        {
            Root.style.paddingLeft = pixel * heirarchy;
        }
    }
}