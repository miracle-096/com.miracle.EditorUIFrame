using UnityEngine.UIElements;

namespace UIFramework.EventHandlers.Interface
{
    public interface TDragEvent:TEvent
    {
        public void OnDragEnter(DragEnterEvent evt);
        public void OnDragLeave(DragLeaveEvent evt);
        public void OnDragUpdated(DragUpdatedEvent evt);
        public void OnReceivedDrag(DragPerformEvent evt);
        
    }
}