using System;
using System.Collections.Generic;
using UIFramework.AppEvent;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    public abstract class TUIElement
    {
        public static T Create<T>(VisualElement container, params object[] objs) where T : TUIElement
        {
            return Create(typeof(T), container, objs) as T;
        }

        public static TUIElement Create(Type uiType, VisualElement container, params object[] objs)
        {
            // container.RegisterCallback<DragEnterEvent>(OnDragEnter);
            // container.RegisterCallback<DragLeaveEvent>(OnDragLeave);
            // container.RegisterCallback<DragExitedEvent>(OnDragExit);
            if (!(Activator.CreateInstance(uiType, container) is TUIElement ui))
                throw new NullReferenceException($"{uiType} instance failure");
            ui.OnCreate(objs);
            return ui;
        }

        private static void OnDragEnter(DragEnterEvent evt)
        {
        }

        private static void OnDragLeave(DragLeaveEvent evt)
        {
        }

        private static void OnDragExit(DragExitedEvent evt)
        {
            AppEvent.AppEvent.Dispatch(new GlobalDragExitedAction());
        }

        public static void Destroy<T>(T ui) where T : TUIElement
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

        public static void Hide(TUIElement ui)
        {
            ui.RootContainer.style.display = DisplayStyle.None;
            ui.OnHide();
        }

        public static void Show(TUIElement ui)
        {
            ui.RootContainer.style.display = DisplayStyle.Flex;
            ui.OnShow();
        }


        private List<EditorActionHandler> m_handlers = new List<EditorActionHandler>();

        public void Register<T>(Action<object> callback)
        {
            var eah = new EditorActionHandler
            {
                actionType = typeof(T),
                callback = callback
            };
            eah.register();
            this.m_handlers.Add(eah);
        }

        public void Unregister<T>(Action<object> callback)
        {
            for (int i = m_handlers.Count - 1; i >= 0; i--)
            {
                var eah = m_handlers[i];
                if (eah.actionType == typeof(T) && callback == eah.callback)
                {
                    eah.unregister();
                }

                this.m_handlers.RemoveAt(i);
                break;
            }
        }

        public void UnregisterAllEventHandlers()
        {
            for (int i = m_handlers.Count - 1; i >= 0; i--)
            {
                m_handlers[i].unregister();
            }

            m_handlers.Clear();
        }


        private bool _isDestroy = false;

        public TUIElement(TemplateContainer templateContainer)
        {
            this.RootContainer = templateContainer;
            this.RootContainer.RegisterCallback<GeometryChangedEvent>(OnGeometryChangeHandler);
            _isDestroy = false;
        }

        protected abstract void InitComponent();

        protected virtual void OnGeometryChangeHandler(GeometryChangedEvent evt)
        {
        }

        public TemplateContainer RootContainer { get; }

        public void SendEvent<EventType>(EventType evt)
        {
            AppEvent.AppEvent.Dispatch(evt);
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
            InitComponent();
        }

        public virtual void OnDestroy()
        {
            this.m_handlers.ForEach((handler) => { handler.unregister(); });
            this.m_handlers.Clear();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}