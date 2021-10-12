using System;
using System.Collections.Generic;
using UIFramework.Editor.Core;
using UIFramework.EventHandlers.Interface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Extends
{
    public static class Extends
    {
        public static void Destroy(this TUIElement element)
        {
            TUIElement.Destroy(element);
        }
        public static T GetComponent<T>(this VisualElement element) where T : TComponent
        {
            if (!TUIWindow.AllObjects.ContainsKey(element)) return null;
            var components = TUIWindow.AllObjects[element].AllComponents;
            if (components[typeof(T)] != null) return components[typeof(T)] as T;
            return null;
        }

        public static T AddComponent<T>(this VisualElement target, TUIElement panel)
            where T : TComponent, new()
        {
            var visualObject = TUIWindow.Find(target);
            visualObject.AllComponents ??= new Dictionary<Type, TComponent>();
            T component = new T();
            component.gameobject = visualObject;
            component.element = target;
            component.SetPanel(panel);
            visualObject.AllComponents.Add(typeof(T), component);
            Type[] ins = typeof(T).GetInterfaces();
            foreach (var ty in ins)
            {
                if (TUIWindow.EventHandlerFactory.ContainsKey(ty))
                {
                    TUIWindow.EventHandlerFactory[ty].Invoke(component, target);
                }
            }

            component.Enable();
            component.Start();
            return component;
        }

        public static void OnStartDragableExtends(this TDragableEvent handler, MouseDownEvent evt)
        {
            if (evt.button == 1) return;
            DragAndDrop.PrepareStartDrag();
            DragAndDrop.SetGenericData("data", handler.DragData);
            DragAndDrop.StartDrag("");
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
            handler.OnStartDrag(evt);
        }

        public static void OnDoubleClickExtends(this TDoubleClickEvent handler, ClickEvent evt, int millisecond)
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

        public static void OnDragUpdatedExtends(this TDragEvent handler, DragUpdatedEvent evt)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
            handler.OnDragUpdated(evt);
        }
    }
}