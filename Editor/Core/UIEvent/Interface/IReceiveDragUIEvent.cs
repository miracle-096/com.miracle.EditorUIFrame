using UnityEngine.UIElements;

namespace UIFramework.Core.UIEvent.Interface
{
    public interface IReceiveDragUIEvent:IUIEvent
    {
        public void OnDragEnter(DragEnterEvent evt);
        public void OnDragLeave(DragLeaveEvent evt);
        public void OnDragUpdated(DragUpdatedEvent evt);
        public void OnReceivedDrag(DragPerformEvent evt);
        
    }
}