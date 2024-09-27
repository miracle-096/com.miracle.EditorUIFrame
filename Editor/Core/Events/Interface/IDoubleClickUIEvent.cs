using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public interface IDoubleClickUIEvent : IUIEvent
    {
        public long PreClickTime { get; set; }
        public bool StopPropagation { get; set; }
        public void OnDoubleClick(ClickEvent evt);
    }
}