using System;

namespace UIFramework.Runtime.AppEvent
{
    public class AppEvent
    {
        public static Dispatcher Dispatcher = null;
        public static void Dispatch(object action)
        {
            Dispatcher ??= new Dispatcher();
            Dispatcher.Dispatch(action);
        }

        public static void RegisterCallback<T>(Action<object> callback, bool exclude = false, object listener = null)
        {
            RegisterCallback(typeof(T), callback, exclude, listener);
        }

        public static void RegisterCallback(Type type, Action<object> callback, bool exclude = false, object listener = null)
        {
            Dispatcher ??= new Dispatcher();
            Dispatcher.RegisteCallback(type, callback, exclude, listener);
        }

        public static void UnregisterCallback<T>(Action<object> callback = null)
        {
            UnregisterCallback(typeof(T), callback);
        }

        public static void UnregisterCallback(Type type, Action<object> callback = null)
        {
            Dispatcher ??= new Dispatcher();
            Dispatcher.UnregisterCallback(type, callback);
        }
    }
}