using System;
using System.Collections.Generic;
using UIFramework.Runtime.AppEvent;
using UnityEngine.UIElements;

namespace UIFramework.Runtime.Core
{
    public class TUIElement
    {
        public bool IsDestroy = false;
        public TemplateContainer RootContainer { get; }

        public TUIElement(TemplateContainer templateContainer)
        {
            this.RootContainer = templateContainer;
            IsDestroy = false;
        }

        private List<ActionHandler> _handlers = new List<ActionHandler>();

        public void Register<T>(Action<object> callback)
        {
            var eah = new ActionHandler
            {
                actionType = typeof(T),
                callback = callback
            };
            eah.register();
            _handlers.Add(eah);
        }

        public void Unregister<T>(Action<object> callback)
        {
            for (int i = _handlers.Count - 1; i >= 0; i--)
            {
                var eah = _handlers[i];
                if (eah.actionType == typeof(T) && callback == eah.callback)
                {
                    eah.unregister();
                }

                this._handlers.RemoveAt(i);
                break;
            }
        }

        public void UnregisterAllEventHandlers()
        {
            for (int i = _handlers.Count - 1; i >= 0; i--)
            {
                _handlers[i].unregister();
            }

            _handlers.Clear();
        }

        public void SendEvent<EventType>(EventType evt)
        {
            Runtime.AppEvent.AppEvent.Dispatch(evt);
        }

        public virtual void OnCreate(params object[] objs)
        {
        }

        public virtual void OnShow()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnHide()
        {
        }

        public virtual void OnDestroy()
        {
            _handlers.ForEach((handler) => { handler.unregister(); });
            _handlers.Clear();
        }
    }
}