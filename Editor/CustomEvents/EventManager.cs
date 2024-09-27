using System;

namespace UIFramework.UIEvent
{
    public class EventManager
    {
        private static EventHandler _eventHandler;

        public static EventHandler eventHandler
        {
            get { return _eventHandler; }
        }

        public static void Reset()
        {
            _eventHandler = new EventHandler();
        }

        public static void FireNow<T>(object sender, T action) where T : CustomEvent
        {
            eventHandler.FireNow(sender, action);
        }

        public static void Subcribe(int id, Action<object, CustomEvent> callback)
        {
            eventHandler.Subcribe(id, callback);
        }

        public static void Unsubcribe(int id, Action<object, CustomEvent> callback = null)
        {
            eventHandler.Unsubcribe(id, callback);
        }
    }
}