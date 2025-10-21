using System;
using System.Collections.Generic;
using LitJson;
using UIFramework.Editor.Core;
using UIFramework.Editor.Core.Events.Handlers;
using UIFramework.Editor.Core.Events.Interface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    /// <summary>
    /// 编辑器ui panel容器
    /// </summary>
    public abstract class UIWindow : EditorWindow
    {
        private static UIWindow instance;
        public string cacheJson;
        public object[] cacheDatas;
        public virtual Vector2 MIN_SIZE => new Vector2(0, 0);
        public static Action<KeyCode> OnKeyDown;
        public static Action<KeyCode> OnKeyUp;
        public static Action<string> OnCommand;

        public static Dictionary<VisualElement, VisualObject>
            AllObjects = new Dictionary<VisualElement, VisualObject>();

        private static Dictionary<Type, Action<EComponent, VisualElement>> eventHandlerFactory;

        public static Dictionary<Type, Action<EComponent, VisualElement>> EventHandlerFactory
        {
            get
            {
                eventHandlerFactory ??= new Dictionary<Type, Action<EComponent, VisualElement>>();
                var ass = typeof(DragState).Assembly;
                var types = ass.GetTypes();
                foreach (var item in types)
                {
                    if (!item.IsInterface || item.GetInterface("IUIEvent") == null ||
                        eventHandlerFactory.ContainsKey(item)) continue;
                    if (item == typeof(IDoubleClickUIEvent))
                        eventHandlerFactory.Add(item,
                            (component, target) =>
                            {
                                DoubleClickHandler.RegisterDoubleClickEvent(component as IDoubleClickUIEvent, target,
                                    300);
                            });
                    else if (item == typeof(IReceiveDragUIEvent))
                        eventHandlerFactory.Add(item,
                            (component, target) =>
                            {
                                ReceiveDragEventHandler.RegisterReceiveDragEvent(component as IReceiveDragUIEvent,
                                    target);
                            });
                    else if (item == typeof(IDraggableUIEvent))
                        eventHandlerFactory.Add(item,
                            (component, target) =>
                            {
                                DraggableEventHandler.RegisterDragEvent(component as IDraggableUIEvent, target);
                            });
                }

                return eventHandlerFactory;
            }
        }

        public EPanel rootEPanel { get; private set; }

        protected virtual void OnEnable()
        {
            //WindowManager.RegisterWindow(GetType(), this);
            titleContent = new GUIContent(GetType().Name);
            Focus();
            instance = this;
            LoadCache();
            EPanel.Window = this;
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // 在编译完成后恢复窗口
            EditorApplication.delayCall += () =>
            {
                if (instance != null) 
                {
                    instance.OpenPanel(instance.cacheDatas);
                }
            };
        }

        private void OnDisable()
        {
            if (rootEPanel != null)
            {
                SaveCache();
            }
        }

        protected virtual void OnDestroy()
        {
            instance = null;
            if (rootEPanel != null)
            {
                rootEPanel.OnDestroy();
                EPanel.Destroy(rootEPanel);
                rootEPanel = null;
            }
        }

        public static VisualObject Find(VisualElement element)
        {
            if (AllObjects.ContainsKey(element)) return AllObjects[element];
            var vo = new VisualObject();
            vo.element = element;
            AllObjects.Add(element, vo);
            return vo;
        }


        public void OpenPanel(params object[] objs)
        {
            cacheDatas = objs;
            var view = MakeView(objs);
            rootEPanel = view;
        }

        protected abstract EPanel MakeView(params object[] objs);

        protected virtual void OnGUI()
        {
            var currentEvent = Event.current;
            if (currentEvent != null)
            {
                if (currentEvent.type == EventType.ValidateCommand)
                {
                    OnCommand?.Invoke(currentEvent.commandName);
                }

                var keyCode = currentEvent.keyCode;
                if (keyCode != KeyCode.None)
                {
                    if (currentEvent.type == EventType.KeyDown)
                    {
                        OnKeyDown?.Invoke(keyCode);
                    }
                    else if (currentEvent.type == EventType.KeyUp)
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

        private void SaveCache()
        {
            if (cacheDatas != null)
            {                
                cacheJson = JsonMapper.ToJson(cacheDatas);
                EditorPrefs.SetString(GetType().Name + "_cache", cacheJson);
            }
            
        }

        private void LoadCache()
        {
            if (EditorPrefs.HasKey(GetType().Name + "_cache"))
            {
                cacheJson = EditorPrefs.GetString(GetType().Name + "_cache");
                if (!string.IsNullOrEmpty(cacheJson))
                    cacheDatas = JsonMapper.ToObject<object[]>(cacheJson);
            }
        }

        [Serializable]
        private class SerializableObjects
        {
            public object[] Objects;
        }
    }
}