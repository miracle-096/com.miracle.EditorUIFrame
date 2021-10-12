using System;
using UIFramework.Editor;
using UIFramework.Editor.Extends;
using UIFramework.EventHandlers.Interface;
using UnityEngine.UIElements;

namespace UIFramework.EventHandlers
{
    public class DragableEventHandler
    {
        private static DragableEventHandler _instance;
        public static UnityEngine.Event currentEvt;
        public static  int controlId;
        public static event Action OnDragOutWindow;
        public static DragableEventHandler Instance
        {
            get { return _instance ??= new DragableEventHandler(); }
        }

        public static void RegistDragableEvent<T>(T panel,VisualElement target) where T: TDragableEvent
        {
            target?.RegisterCallback<MouseDownEvent>(panel.OnStartDragableExtends);
            panel.OnDragOver = panel.OnStopDrag;
        }
        
        public static void UnRegistDragableEvent<T>(T panel,VisualElement target) where T:TDragableEvent
        {
            panel.OnDragOver = null;
            target?.UnregisterCallback<MouseDownEvent>(panel.OnStartDragableExtends);
        }

    }
}