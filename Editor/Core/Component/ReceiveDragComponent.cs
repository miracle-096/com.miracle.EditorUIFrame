using System;
using UnityEngine.UIElements;

namespace UIFramework.Core.Component
{
    public class ReceiveDragComponent : EComponent, IReceiveDragUIEvent
    {
        public Action<DragEnterEvent> OnDragEnter;
        public Action<DragLeaveEvent> OnDragLeave;
        public Action<DragUpdatedEvent> OnDragUpdated;
        public Action<DragPerformEvent> OnReceivedDrag;

        void IReceiveDragUIEvent.OnDragEnter(DragEnterEvent evt)
        {
            OnDragEnter?.Invoke(evt);
        }

        void IReceiveDragUIEvent.OnDragLeave(DragLeaveEvent evt)
        {
            OnDragLeave?.Invoke(evt);
        }

        void IReceiveDragUIEvent.OnDragUpdated(DragUpdatedEvent evt)
        {
            OnDragUpdated?.Invoke(evt);
        }

        void IReceiveDragUIEvent.OnReceivedDrag(DragPerformEvent evt)
        {
            OnReceivedDrag?.Invoke(evt);
        }
    }
    
}
