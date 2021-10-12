using System;
using UnityEngine.UIElements;

namespace UIFramework.EventHandlers.Interface
{
    public enum DragState
    {
        WaitDrag,Dragging,DragOver
    }
    public interface TDragableEvent:TEvent
    {
        public DragState DragState { get; set; }
        public Action OnDragOver { get; set; }
        public object DragData { get;}
        public void OnStartDrag(MouseDownEvent evt);
        public void OnStopDrag();
    }
}