using System;
using System.Collections.Generic;

namespace UIFramework.Editor.Core
{
    public class TComponent : VisualObject
    {
        public VisualObject gameobject;

        private KeyValuePair<Type, TUIElement> UIPanel { get; set; }

        public void SetPanel<T>(T panel) where T : TUIElement
        {
            UIPanel = new KeyValuePair<Type, TUIElement>(panel.GetType(), panel);
        }

        protected T GetPanel<T>() where T : TUIElement
        {
            if (UIPanel.Key != typeof(T)) throw new NullReferenceException("Panel 中 Key不存在");
            return UIPanel.Value as T;
        }

        protected TUIElement GetPanel(out Type type)
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