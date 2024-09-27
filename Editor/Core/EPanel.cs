using System;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public abstract class EPanel
    {
        public UIWindow Window;
        public static T Create<T>(VisualElement container, params object[] objs) where T : EPanel
        {
            return Create(typeof(T), container, objs) as T;
        }

        public static EPanel Create(Type uiType, VisualElement container, params object[] objs)
        {
            if (!(Activator.CreateInstance(uiType, container) is EPanel ui))
                throw new NullReferenceException($"{uiType} instance failure");
            ui.OnCreate(objs);
            return ui;
        }

        public static void Destroy<T>(T ui) where T : EPanel
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

        public static void Hide(EPanel ui)
        {
            ui.RootContainer.style.display = DisplayStyle.None;
            ui.OnHide();
        }

        public static void Show(EPanel ui)
        {
            ui.RootContainer.style.display = DisplayStyle.Flex;
            ui.OnShow();
        }

        private bool _isDestroy = false;

        public EPanel(TemplateContainer templateContainer)
        {
            RootContainer = templateContainer;
            RootContainer.RegisterCallback<GeometryChangedEvent>(OnGeometryChangeHandler);
            _isDestroy = false;
        }

        protected virtual void OnGeometryChangeHandler(GeometryChangedEvent evt)
        {
        }

        public TemplateContainer RootContainer { get; }

        protected T Q<T>(string search) where T : VisualElement
        {
            return RootContainer.Query<T>(search);
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
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}