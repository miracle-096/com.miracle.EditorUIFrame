using UIFramework.Core.UIEvent.Interface;
using UIFramework.Extends;
using UnityEngine.UIElements;

namespace UIFramework.Core.UIEvent.handler
{
    public class ReceiveDragEventHandler
    {
        private static ReceiveDragEventHandler _instance;

        public static void RegisterReceiveDragEvent<T>(T panel,VisualElement target) where T:IReceiveDragUIEvent
        {
            target?.RegisterCallback<DragEnterEvent>(panel.OnDragEnter);
            target?.RegisterCallback<DragUpdatedEvent>(panel.OnDragUpdatedExtends);
            target?.RegisterCallback<DragPerformEvent>(panel.OnReceivedDrag);
            target?.RegisterCallback<DragLeaveEvent>(panel.OnDragLeave);
        }
        
        public static void UnRegisterReceiveDragEvent<T>(T panel,VisualElement target) where T:IReceiveDragUIEvent
        {
            target?.UnregisterCallback<DragEnterEvent>(panel.OnDragEnter);
            target?.UnregisterCallback<DragUpdatedEvent>(panel.OnDragUpdated);
            target?.UnregisterCallback<DragPerformEvent>(panel.OnReceivedDrag);
            target?.UnregisterCallback<DragLeaveEvent>(panel.OnDragLeave);
        }
    }
}