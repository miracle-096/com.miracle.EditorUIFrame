using UIFramework.Extends;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public class DraggableEventHandler
    {
        public static void RegisterDragEvent<T>(T panel,VisualElement target) where T: IDraggableUIEvent
        {
            target?.RegisterCallback<MouseDownEvent>(panel.OnStartDragExtends);
            target?.RegisterCallback<MouseUpEvent>(panel.OnStopDragExtends);
        }
        
        public static void UnRegisterDragEvent<T>(T panel,VisualElement target) where T:IDraggableUIEvent
        {
            target?.UnregisterCallback<MouseDownEvent>(panel.OnStartDragExtends);
            target?.UnregisterCallback<MouseUpEvent>(panel.OnStopDragExtends);
        }

    }
}