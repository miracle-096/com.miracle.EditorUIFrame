using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UIFramework.Runtime.Core
{
    public sealed class TUIManager
    {
        private static List<TUIElement> _panels = new List<TUIElement>();
        public static T Create<T>(VisualElement container, params object[] objs) where T : TUIElement
        {
            return Create(typeof(T), container, objs) as T;
        }

        public static TUIElement Create(Type uiType, VisualElement container, params object[] objs)
        {
            if (!(Activator.CreateInstance(uiType, container) is TUIElement ui))
                throw new NullReferenceException($"{uiType} instance failure");
            AddPanel(ui);
            ui.OnCreate(objs);
            return ui;
        }

        public static void Destroy<T>(T ui) where T : TUIElement
        {
            if (ui.IsDestroy) return;
            ui.IsDestroy = true;
            ui.OnDestroy();
            RemovePanel(ui);
        }
        public static void Show(TUIElement ui)
        {
            ui.RootContainer.style.display = DisplayStyle.Flex;
            _panels.Add(ui);
            ui.OnShow();
        }
        public static void Hide(TUIElement ui)
        {
            ui.RootContainer.style.display = DisplayStyle.None;
            ui.OnHide();
            _panels.Remove(ui);
        }

        public static bool AddPanel(TUIElement panel)
        {
            _panels.Add(panel);
            return true;
        }

        public static bool RemovePanel(TUIElement panel)
        {
            if (_panels.Contains(panel))
            {
                _panels.Remove(panel);
                return true;
            }
            return false;
        }

        public static void UpdateAllPanels()
        {
            foreach (var panel in _panels)
            {
                panel.OnUpdate();
            }
        }
    }
}