using System;
using UIFramework.Core.UIEvent.Interface;
using UnityEngine.UIElements;

namespace UIFramework.Core.Component
{
    public class DoubleClickComponent: UIComponent,IDoubleClickUIEvent
    {
        public long PreClickTime { get; set; }
        public bool StopPropagation { get; set; }
        
        public Action<ClickEvent> OnDoubleClickAction;
        public void OnDoubleClick(ClickEvent evt)
        {
            OnDoubleClickAction?.Invoke(evt);
        }
    }
}