using System;
using System.Collections.Generic;
using UIFramework.Editor.Core;
using UnityEngine.UIElements;

namespace UIFramework.Editor.CustomComponent
{
    public class ToggleGroup: TComponent
    {
        public List<VisualElement> toggleGroup { get; } = new List<VisualElement>();

        private TUIElement panel;

        public TUIElement Panel
        {
            get => panel??=GetPanel(out Type type);
            set => panel = value;
        }

        public bool AddToGroup(VisualElement visualElement)
        {
            toggleGroup.Add(visualElement);
            return true;
        }
    }
}