using UnityEngine.UIElements;

namespace UIFramework.Editor.Core.Events.Interface
{
    public interface IDoubleClickUIEvent : IUIEvent
    {
        public long PreClickTime { get; set; }
        public bool StopPropagation { get; set; }
        public void OnDoubleClick(ClickEvent evt);
    }
}