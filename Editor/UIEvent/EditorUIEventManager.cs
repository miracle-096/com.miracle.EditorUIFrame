using System;

namespace UIFramework.UIEvent
{
    public class EditorUIEventManager
    {
        private static Dispatcher _dispatcher;
        public static Dispatcher Dispatcher
        {
            get
            {
                return _dispatcher;
            }
        }

        public static void Reset()
        {
            _dispatcher = new Dispatcher();
        }
        public static void Dispatch(object action)
        {
            Dispatcher.Dispatch(action);
        }

        public static void RegisterCallback<T>(Action<object> callback, bool exclude = false)
        {
            RegisterCallback(typeof(T), callback, exclude);
        }

        public static void RegisterCallback(Type type, Action<object> callback, bool exclude = false)
        {
            Dispatcher.RegisterCallback(type, callback, exclude);
        }

        public static void UnregisterCallback<T>(Action<object> callback = null)
        {
            UnregisterCallback(typeof(T), callback);
        }

        public static void UnregisterCallback(Type type, Action<object> callback = null)
        {
            Dispatcher.UnregisterCallback(type, callback);
        }
    }
}