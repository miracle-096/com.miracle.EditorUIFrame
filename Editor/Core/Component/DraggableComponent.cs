using System;
using UIFramework.Core.UIEvent.Interface;
using UnityEditor;
using UnityEngine.UIElements;

namespace UIFramework.Core.Component
{
    public class DraggableComponent: UIComponent,IDraggableUIEvent
    {
        public DragState DragState { get; set; }
        public object DragData { get; set; }
        
        public Action<DraggableComponent,MouseDownEvent> OnStartDrag;
        public Action<DraggableComponent,MouseUpEvent> OnStopDrag;
        void IDraggableUIEvent.OnStartDrag(MouseDownEvent evt)
        {
            OnStartDrag?.Invoke(this,evt);

            DragAndDrop.PrepareStartDrag();
            DragAndDrop.SetGenericData("data", DragData);
            DragAndDrop.StartDrag("");
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
        }

        void IDraggableUIEvent.OnStopDrag(MouseUpEvent evt)
        {
            OnStopDrag?.Invoke(this,evt);
        }
    }
}