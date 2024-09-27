using System;
using System.Collections.Generic;
using UIFramework.Core;
using UnityEditor;
using UnityEngine.UIElements;

namespace UIFramework.Extends
{
    public static class Extends
    {
        public static void Destroy(this EPanel element)
        {
            EPanel.Destroy(element);
        }

        public static T GetComponent<T>(this VisualElement element) where T : EComponent
        {
            if (!UIWindow.AllObjects.ContainsKey(element)) return null;
            var components = UIWindow.AllObjects[element].AllComponents;
            if (components[typeof(T)] != null) return components[typeof(T)] as T;
            return null;
        }

        public static T AddComponent<T>(this VisualElement target, EPanel panel = null)
            where T : EComponent, new()
        {
            var visualObject = UIWindow.Find(target);
            visualObject.AllComponents ??= new Dictionary<Type, EComponent>();
            T component = new T();
            component.gameobject = visualObject;
            component.element = target;
            component.SetPanel(panel);
            visualObject.AllComponents.Add(typeof(T), component);
            Type[] ins = typeof(T).GetInterfaces();
            foreach (var ty in ins)
            {
                if (UIWindow.EventHandlerFactory.ContainsKey(ty))
                {
                    UIWindow.EventHandlerFactory[ty].Invoke(component, target);
                }
            }

            component.Enable();
            component.Start();
            return component;
        }

        public static void OnStartDragExtends(this IDraggableUIEvent handler, MouseDownEvent evt)
        {
            if (evt.button == 1) return;
            handler.OnStartDrag(evt);
        }
        public static void OnStopDragExtends(this IDraggableUIEvent handler, MouseUpEvent evt)
        {
            if (evt.button == 1) return;
            handler.OnStopDrag(evt);
        }

        public static void OnDoubleClickExtends(this IDoubleClickUIEvent handler, ClickEvent evt, int millisecond)
        {
            if (handler.StopPropagation)
            {
                evt.StopPropagation();
            }

            long setInterval = millisecond * 10000L;
            var t = (DateTime.Now.Ticks - handler.PreClickTime);
            handler.PreClickTime = DateTime.Now.Ticks;
            if (t > setInterval) return;
            handler.OnDoubleClick(evt);
        }

        public static void OnDragUpdatedExtends(this IReceiveDragUIEvent handler, DragUpdatedEvent evt)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
            handler.OnDragUpdated(evt);
        }
    }
}