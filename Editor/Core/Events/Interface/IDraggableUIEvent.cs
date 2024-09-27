using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public enum DragState
    {
        WaitDrag,Dragging,DragOver
    }
    public interface IDraggableUIEvent:IUIEvent
    {
        public DragState DragState { get; set; }
        public object DragData { get; set; }
        public void OnStartDrag(MouseDownEvent evt);
        public void OnStopDrag(MouseUpEvent evt);
    }
}