using System;
using System.Collections.Generic;

namespace UIFramework.Core
{
    /// <summary>
    /// visual element组件，所有组件的基类
    /// </summary>
    public class EComponent : VisualObject
    {
        public VisualObject gameobject;

        private KeyValuePair<Type, EPanel> _panelDict { get; set; }

        public void SetPanel<T>(T panel) where T : EPanel
        {
            if (panel == null) return;
            _panelDict = new KeyValuePair<Type, EPanel>(panel.GetType(), panel);
        }

        protected T GetPanel<T>() where T : EPanel
        {
            if (_panelDict.Key != typeof(T)) throw new NullReferenceException("Panel 中 Key不存在");
            return _panelDict.Value as T;
        }

        protected EPanel GetPanel(out Type type)
        {
            type = _panelDict.Key;
            return _panelDict.Value;
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
        public virtual void OnDestroy()
        {
        }
    }
}