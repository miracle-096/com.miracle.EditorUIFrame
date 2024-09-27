using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UIFramework.Core.Component
{
    public class ToggleGroup: EComponent
    {
        public List<VisualElement> toggleGroup { get; } = new List<VisualElement>();

        private EPanel _panel;

        public EPanel Panel
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