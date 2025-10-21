using UIFramework.Editor.Core.Events.Interface;
using UIFramework.Editor.Extensions;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core.Events.Handlers
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
            target?.UnregisterCallback<DragUpdatedEvent>(panel.OnDragUpdatedExtends);
            target?.UnregisterCallback<DragPerformEvent>(panel.OnReceivedDrag);
            target?.UnregisterCallback<DragLeaveEvent>(panel.OnDragLeave);
        }
    }
}