using System;
using System.Collections.Generic;
using UIFramework.UIEvent;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public abstract class UIElement
    {
        public UIWindow Window;
        public static T Create<T>(VisualElement container, params object[] objs) where T : UIElement
        {
            return Create(typeof(T), container, objs) as T;
        }

        public static UIElement Create(Type uiType, VisualElement container, params object[] objs)
        {
            if (!(Activator.CreateInstance(uiType, container) is UIElement ui))
                throw new NullReferenceException($"{uiType} instance failure");
            ui.OnCreate(objs);
            return ui;
        }

        public static void Destroy<T>(T ui) where T : UIElement
        {
            if (ui == null || ui._isDestroy) return;
            ui._isDestroy = true;
            ui.OnDestroy();
        }

        public void Destroy()
        {
            if (_isDestroy) return;
            _isDestroy = true;
            this.OnDestroy();
        }

        public static void Hide(UIElement ui)
        {
            ui.RootContainer.style.display = DisplayStyle.None;
            ui.OnHide();
        }

        public static void Show(UIElement ui)
        {
            ui.RootContainer.style.display = DisplayStyle.Flex;
            ui.OnShow();
        }


        private List<EditorUIEventHandler> _eventHandlers = new List<EditorUIEventHandler>();

        public void Register<T>(Action<object> callback)
        {
            var eah = new EditorUIEventHandler
            {
                actionType = typeof(T),
                callback = callback
            };
            eah.register();
            this._eventHandlers.Add(eah);
        }

        public void Unregister<T>(Action<object> callback)
        {
            for (int i = _eventHandlers.Count - 1; i >= 0; i--)
            {
                var eah = _eventHandlers[i];
                if (eah.actionType == typeof(T) && callback == eah.callback)
                {
                    eah.unregister();
                }

                this._eventHandlers.RemoveAt(i);
                break;
            }
        }

        public void UnregisterAllEventHandlers()
        {
            for (int i = _eventHandlers.Count - 1; i >= 0; i--)
            {
                _eventHandlers[i].unregister();
            }

            _eventHandlers.Clear();
        }


        private bool _isDestroy = false;

        public UIElement(TemplateContainer templateContainer)
        {
            this.RootContainer = templateContainer;
            this.RootContainer.RegisterCallback<GeometryChangedEvent>(OnGeometryChangeHandler);
            _isDestroy = false;
        }

        protected virtual void OnGeometryChangeHandler(GeometryChangedEvent evt)
        {
        }

        public TemplateContainer RootContainer { get; }

        public void SendEvent<EventType>(EventType evt)
        {
            EditorUIEventManager.Dispatch(evt);
        }

        protected T Query<T>(string search) where T : VisualElement
        {
            return this.RootContainer.Query<T>(search);
        }

        protected T Q<T>(string search) where T : VisualElement
        {
            return Query<T>(search);
        }


        public virtual void Show()
        {
            Show(this);
        }

        protected virtual void Hide()
        {
            Hide(this);
        }

        protected virtual void OnCreate(params object[] objs)
        {
            RootContainer.userData = this;
        }

        public virtual void OnDestroy()
        {
            this._eventHandlers.ForEach((handler) => { handler.unregister(); });
            this._eventHandlers.Clear();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}