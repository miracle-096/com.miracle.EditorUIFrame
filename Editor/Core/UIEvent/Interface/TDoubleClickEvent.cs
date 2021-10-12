using UnityEngine.UIElements;

namespace UIFramework.EventHandlers.Interface
{
    public interface TDoubleClickEvent : TEvent
    {
        public long PreClickTime { get; set; }
        public bool StopPropagation { get; set; }
        public void OnDoubleClick(ClickEvent evt);
    }
}