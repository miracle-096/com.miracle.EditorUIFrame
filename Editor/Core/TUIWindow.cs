using System;
using System.Collections.Generic;
using UIFramework.EventHandlers;
using UIFramework.EventHandlers.Interface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    public abstract class TUIWindow: EditorWindow
    {
        public static Action<UnityEngine.KeyCode> OnKeyDown;
        public static Action<UnityEngine.KeyCode> OnKeyUp;

        public static Dictionary<VisualElement, VisualObject>
            AllObjects = new Dictionary<VisualElement, VisualObject>();

        private static Dictionary<Type, Action<TComponent, VisualElement>> eventHandlerFactory;

        public static Dictionary<Type, Action<TComponent, VisualElement>> EventHandlerFactory
        {
            get
            {
                eventHandlerFactory ??= new Dictionary<Type, Action<TComponent, VisualElement>>();
                var ass = typeof(DragState).Assembly;
                var types = ass.GetTypes(); 
                foreach (var item in types)
                {
                    if (!item.IsInterface || item.GetInterface("TEvent") == null ||
                        eventHandlerFactory.ContainsKey(item)) continue;
                    if (item == typeof(TDoubleClickEvent))
                        eventHandlerFactory.Add(item,
                            (component, target) =>
                            {
                                DoubleClickHandler.RegistDoubleClickEvent(component as TDoubleClickEvent, target, 300);
                            });
                    else if (item == typeof(TDragEvent))
                        eventHandlerFactory.Add(item,
                            (component, target) =>
                            {
                                DragEventHandler.RegistDragEvent(component as TDragEvent, target);
                            });
                    else if (item == typeof(TDragableEvent))
                        eventHandlerFactory.Add(item,
                            (component, target) =>
                            {
                                DragableEventHandler.RegistDragableEvent(component as TDragableEvent, target);
                            });
                }

                return eventHandlerFactory;
            }
        }

        public TUIElement RootUIElement { get; private set; }

        protected virtual void OnEnable()
        {
            WindowManager.RegisterWindow(GetType(), this);
            var view = MakeView();
            RootUIElement = view;
        }

        protected virtual void OnDisable()
        {
            if (RootUIElement != null)
            {
                TUIElement.Destroy(RootUIElement);
                RootUIElement = null;
            }
            //To-do: Same as the above.
            WindowManager.UnRegisterWindow(GetType());
        }

        protected virtual void OnDestroy()
        {
        }
        public static VisualObject Find(VisualElement element)
        {
            if (AllObjects.ContainsKey(element)) return AllObjects[element];
            var vo = new VisualObject();
            vo.element = element;
            AllObjects.Add(element, vo);
            return vo;
        }

        public abstract TUIElement MakeView();

        private VisualElement InputBlockImage = null;
        public void BlockUserInput()
        {
            InputBlockImage ??= new Image();
            InputBlockImage.name = "image_input_block";
            BlockInput(rootVisualElement, InputBlockImage);
        }


        public void ResumeUserInput()
        {
            if (InputBlockImage == null)
            {
                return;
            }
            InputBlockImage.style.display = DisplayStyle.None;
        }

        public static void BlockInput(VisualElement element, VisualElement image)
        {
            image.style.width = 10000;
            image.style.height = 10000;
            element.Add(image);
            image.BringToFront();
            image.style.position = Position.Absolute;
            image.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.3f));
            image.style.display = DisplayStyle.Flex;
        }

        protected virtual void OnGUI()
        {

            var currentEvent = Event.current;
            if (currentEvent != null)
            {
                var keyCode = currentEvent.keyCode;

                if (keyCode != KeyCode.None)
                {
                    if(currentEvent.type == EventType.KeyDown)
                    {
                        OnKeyDown?.Invoke(keyCode);
                    } else if(currentEvent.type == EventType.KeyUp)
                    {
                        OnKeyUp?.Invoke(keyCode);
                    }
                }
            }

            foreach (var visualObject in AllObjects)
            {
                if (rootVisualElement.Contains(visualObject.Key))
                {
                    foreach (var component in visualObject.Value.AllComponents)
                    {
                        component.Value.OnGUI();
                    }
                }
            }
        }

        protected virtual void Update()
        {
            foreach (var visualObject in AllObjects)
            {
                if (rootVisualElement.Contains(visualObject.Key))
                {
                    foreach (var component in visualObject.Value.AllComponents)
                    {
                        component.Value.Update();
                    }
                }
            }
        }
    }
}