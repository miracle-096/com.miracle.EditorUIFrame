using System;
using System.Collections.Generic;

namespace UIFramework.UIEvent
{
    public class EventHandler
    {
        private Dictionary<int, List<Action<object, CustomEvent>>> _handlers = new();

        public void FireNow<T>(object sender, T args) where T : CustomEvent
        {
            if (!_handlers.ContainsKey(args.Id)) return;

            var handlerList = _handlers[args.Id];
            if (handlerList != null)
            {
                foreach (var action in handlerList)
                {
                    action.Invoke(sender, args);
                }
            }
        }

        public void Subcribe(int eventId, Action<object, CustomEvent> handler)
        {
            if (_handlers.TryGetValue(eventId, out var list))
                list.Add(handler);
            else
                _handlers.Add(eventId, new List<Action<object, CustomEvent>> { handler });
        }

        public void Unsubcribe(int eventId,Action<object, CustomEvent> handler)
        {
            if (handler == null)
            {
                _handlers.Remove(eventId);
            }
            else
            {
                _handlers.TryGetValue(eventId, out var list);
                list?.Remove(handler);
            }
        }
    }
}