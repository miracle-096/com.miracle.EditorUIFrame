using System;
using System.Collections.Generic;

namespace UIFramework.Core
{
    public class UIComponent : VisualObject
    {
        public VisualObject gameobject;

        private KeyValuePair<Type, UIElement> UIPanel { get; set; }

        public void SetPanel<T>(T panel) where T : UIElement
        {
            if (panel == null) return;
            UIPanel = new KeyValuePair<Type, UIElement>(panel.GetType(), panel);
        }

        protected T GetPanel<T>() where T : UIElement
        {
            if (UIPanel.Key != typeof(T)) throw new NullReferenceException("Panel 中 Key不存在");
            return UIPanel.Value as T;
        }

        protected UIElement GetPanel(out Type type)
        {
            type = UIPanel.Key;
            return UIPanel.Value;
        }

        public virtual void Enable()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void OnGUI()
        {
        }

        public virtual void Disable()
        {
        }
    }
}