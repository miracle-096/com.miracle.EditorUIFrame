using UIFramework.Editor;
using UIFramework.Editor.Extends;
using UIFramework.EventHandlers.Interface;
using UnityEngine.UIElements;

namespace UIFramework.EventHandlers
{
    public class DragEventHandler
    {
        private static DragEventHandler _instance;

        public static void RegistDragEvent<T>(T panel,VisualElement target) where T:TDragEvent
        {
            target?.RegisterCallback<DragEnterEvent>(panel.OnDragEnter);
            target?.RegisterCallback<DragUpdatedEvent>(panel.OnDragUpdatedExtends);
            target?.RegisterCallback<DragPerformEvent>(panel.OnReceivedDrag);
            target?.RegisterCallback<DragLeaveEvent>(panel.OnDragLeave);
        }
        
        public static void UnRegistDragEvent<T>(T panel,VisualElement target) where T:TDragEvent
        {
            target?.UnregisterCallback<DragEnterEvent>(panel.OnDragEnter);
            target?.UnregisterCallback<DragUpdatedEvent>(panel.OnDragUpdated);
            target?.UnregisterCallback<DragPerformEvent>(panel.OnReceivedDrag);
            target?.UnregisterCallback<DragLeaveEvent>(panel.OnDragLeave);
        }
    }
}