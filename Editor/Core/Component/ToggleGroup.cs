using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UIFramework.Core.Component
{
    public class ToggleGroup: UIComponent
    {
        public List<VisualElement> toggleGroup { get; } = new List<VisualElement>();

        private UIElement _panel;

        public UIElement Panel
        {
            get => _panel??=GetPanel(out Type type);
            set => _panel = value;
        }

        public bool AddToGroup(VisualElement visualElement)
        {
            toggleGroup.Add(visualElement);
            return true;
        }
    }
}