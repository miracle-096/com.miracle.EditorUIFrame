using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace UIFramework.Editor.CustomElement.Foldout
{
    public class TFoldout : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TFoldout, Traits> { }

    
        public string title;
        private const string _styleName = "TFoldout";
        private const string _ussClassName = "tal-foldout";
        private const string _bgClassName = "tal-foldout__bg";
        private const string _toggleClassName = "tal-foldout__toggle";
        private const string _contentContainerClassName = "tal-foldout__content-container";

    
        private readonly VisualElement _bg;
        public VisualElement background => _bg;
    
        private readonly VisualElement _toggle;

        private readonly VisualElement _content;
        public override VisualElement contentContainer => _content;
    
    
        public TFoldout()
        {
            AddToClassList(_ussClassName);
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.tal.unity.uiframework/Editor/CustomElement/Foldout/TFoldout.uss"));

            _bg = new VisualElement();
            _bg.name = "tal-foldout-bg";
            _bg.AddToClassList(_bgClassName);
            hierarchy.Add(_bg);
        
            _toggle = new TToggle();
            _toggle.name = "tal-foldout-toggle";
            _toggle.AddToClassList(_toggleClassName);
            hierarchy.Add(_toggle);
        
            _content = new VisualElement();
            _content.name = "tal-foldout-container";
            _content.AddToClassList(_contentContainerClassName);
            hierarchy.Add(_content);

            RegisterCallback<AttachToPanelEvent>(ProcessEvent);
        }

        private void ProcessEvent(AttachToPanelEvent e)
        {
            for (int i = 0; i < _content.childCount; ++i)
            {
                VisualElement element = _content[i];

            }
        }


        public class Traits : UxmlTraits
        {
            private UxmlStringAttributeDescription Title = new UxmlStringAttributeDescription {name = "title"};

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield return new UxmlChildElementDescription(typeof(VisualElement)); }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((TFoldout) ve).title = Title.GetValueFromBag(bag, cc);
            }
        }
    }
}