using System;
using UIFramework.Editor.Utility;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public class TabButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TabButton, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
                { name = "text",defaultValue = ""};

            private readonly UxmlStringAttributeDescription m_Value = new UxmlStringAttributeDescription
                { name = "value",defaultValue = ""};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                TabButton tabButton = (TabButton)ve;
                tabButton.name = m_Name.GetValueFromBag(bag, cc);
                tabButton.m_Label.text = m_Text.GetValueFromBag(bag, cc);
                tabButton.Value = m_Value.GetValueFromBag(bag, cc);
                tabButton.MarkDirtyRepaint();
            }
        }

        static readonly string s_UssClassName = "unity-tab-button";
        static readonly string s_UssActiveClassName = s_UssClassName + "--active";

        private Label m_Label;

        public bool IsCloseable { get; set; }
        public string Value { get; private set; }

        public event Action<TabButton> OnSelect;
        public event Action<TabButton> OnClose;

        public TabButton()
        {
            m_Label = new Label("Label");
            AddToClassList(s_UssClassName);
            hierarchy.Add(m_Label);
            RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
        }


        public void Select()
        {
            AddToClassList(s_UssActiveClassName);
        }

        public void Deselect()
        {
            RemoveFromClassList(s_UssActiveClassName);
            MarkDirtyRepaint();
        }

        private void OnMouseDownEvent(MouseDownEvent e)
        {
            switch (e.button)
            {
                case 0:
                {
                    OnSelect?.Invoke(this);
                    break;
                }

                // case 2 when IsCloseable:
                // {
                //     OnClose?.Invoke(this);
                //     break;
                // }
            } // End of switch.

            e.StopImmediatePropagation();
        }
    }
}