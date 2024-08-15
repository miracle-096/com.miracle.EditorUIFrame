using UnityEngine.UIElements;

namespace UIFramework.Core.UIEvent.Interface
{
    public interface IDoubleClickUIEvent : IUIEvent
    {
        public long PreClickTime { get; set; }
        public bool StopPropagation { get; set; }
        public void OnDoubleClick(ClickEvent evt);
    }
}