using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace UIFramework.Editor.CustomElement.Foldout
{
    public class TToggle : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TToggle, Traits> { }
    
        private const string _stylePath = "Packages/com.tal.unity.uiframework/Editor/CustomElement/Foldout/TToggle.uss";
        private const string _ussClassName = "tal-toggle";
        private const string _toggleLabelClassName = "tal-toggle__label";
        private const string _contentContainerClassName = "tal-toggle__checkMark";
        public Label ToggleLabel;
    
        private VisualElement _container;
    
        public VisualElement CheckMark;
    
    
        public TToggle()
        {
            AddToClassList(_ussClassName);
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(_stylePath));

            _container = new VisualElement();
            hierarchy.Add(_container);
            CheckMark = new VisualElement();
            CheckMark.name = "tal-toggle-checkMark";
            CheckMark.AddToClassList(_contentContainerClassName);
            _container.Add(CheckMark);
            ToggleLabel = new Label();
            ToggleLabel.name = "tal-toggle-label";
            ToggleLabel.AddToClassList(_toggleLabelClassName);
            _container.Add(ToggleLabel);

            RegisterCallback<AttachToPanelEvent>(ProcessEvent);
        }

        private void ProcessEvent(AttachToPanelEvent e)
        {
            for (int i = 0; i < ToggleLabel.childCount; ++i)
            {
                VisualElement element = ToggleLabel[i];

            }
        }


        public class Traits : UxmlTraits
        {
            private UxmlStringAttributeDescription Title = new UxmlStringAttributeDescription {name = "toggle label"};

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield return new UxmlChildElementDescription(typeof(VisualElement)); }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((TToggle) ve).ToggleLabel.text = Title.GetValueFromBag(bag, cc);
            }
        }
    }
}