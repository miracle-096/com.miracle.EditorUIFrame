using System;
using LitJson;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    public abstract class EPanel
    {
        public static UIWindow Window;

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


        public void Show()
        {
            RootContainer.style.display = DisplayStyle.Flex;
            OnShow();
        }

        public void Hide()
        {
            RootContainer.style.display = DisplayStyle.None;
            OnHide();
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