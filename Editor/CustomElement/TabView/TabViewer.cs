using System;
using System.Collections.Generic;
using System.Linq;
using UIFramework.Editor.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public class TabViewer : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TabViewer, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                TabViewer tabViewer = (TabViewer)ve;
                tabViewer.name = m_Name.GetValueFromBag(bag, cc);
            }
        }
        public TabViewer()
        {
            var tabView = UILoader.LoadElement<TabView>();
            m_TabContent = tabView.TabsContainer;
            m_TabContent.AddToClassList(s_TabsContainerClassName);
            tabView.RootContainer.AddToClassList(s_UssClassName);
            hierarchy.Add(tabView.RootContainer);
            RegisterCallback<AttachToPanelEvent>(ProcessEvent);
        }

        private string s_UssClassName = "unity-tab-view";
        private string s_TabsContainerClassName = "unity-tab-view__tabs-container";

        private readonly VisualElement m_TabContent;
        public override VisualElement contentContainer => m_TabContent;
        
        private readonly List<TabButton> m_Tabs = new List<TabButton>();
        private TabButton m_ActiveTab;

        private string _value;
        public string Value
        {
            get => _value;
            private set
            {
                if (_value != value)
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        public event Action<string> OnValueChanged;

        public void AddTab(TabButton tabButton, bool activate)
        {
            m_Tabs.Add(tabButton);
            m_TabContent.Add(tabButton);

            tabButton.OnClose += RemoveTab;
            tabButton.OnSelect += Activate;

            if (activate)
            {
                Activate(tabButton);
            }
        }
        
        private void AddTabInternal(TabButton tabButton, bool activate)
        {
            m_Tabs.Add(tabButton);

            tabButton.OnClose += RemoveTab;
            tabButton.OnSelect += Activate;

            if (activate)
            {
                Activate(tabButton);
            }
        }

        public void RemoveTab(TabButton tabButton)
        {
            int index = m_Tabs.IndexOf(tabButton);

            // If this tab is the active one make sure we deselect it first...
            if (m_ActiveTab == tabButton)
            {
                DeselectTab(tabButton);
                m_ActiveTab = null;
            }

            m_Tabs.RemoveAt(index);
            m_TabContent.Remove(tabButton);

            tabButton.OnClose -= RemoveTab;
            tabButton.OnSelect -= Activate;

            // If we closed the active tab AND we have any tabs left - active the next valid one...
            if ((m_ActiveTab == null) && m_Tabs.Any())
            {
                int clampedIndex = Mathf.Clamp(index, 0, m_Tabs.Count - 1);
                TabButton tabToActivate = m_Tabs[clampedIndex];

                Activate(tabToActivate);
            }
        }

        private void SelectTab(TabButton tabButton)
        {
            tabButton.Select();
            Value = tabButton.Value;
        }

        private void DeselectTab(TabButton tabButton)
        {
            tabButton.Deselect();
        }

        public void Activate(TabButton button)
        {
            if (m_ActiveTab != null)
            {
                DeselectTab(m_ActiveTab);
            }

            m_ActiveTab = button;
            SelectTab(m_ActiveTab);
        }
        
        private void ProcessEvent(AttachToPanelEvent e)
        {
            for (int i = 0; i < m_TabContent.childCount; i++)
            {
                VisualElement element = m_TabContent.ElementAt(i);
                Debug.Log($"Element at index {i}: {element.name}");
                if (element is TabButton button)
                {
                    AddTabInternal(button, false);
                }
            }
            
            if (m_ActiveTab != null)
            {
                SelectTab(m_ActiveTab);
            }
            else if (m_TabContent.childCount > 0)
            {
                m_ActiveTab = (TabButton)m_TabContent[0];

                SelectTab(m_ActiveTab);
            }
        }

    }
}