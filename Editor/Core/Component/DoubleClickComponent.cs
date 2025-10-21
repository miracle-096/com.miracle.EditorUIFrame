using System;
using UIFramework.Editor.Core.Events.Interface;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core.Component
{
    public class DoubleClickComponent: EComponent,IDoubleClickUIEvent
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