using System;

namespace UIFramework.AppEvent
{
    public class EditorActionHandler
    {
        public Type actionType;
        public Action<object> callback;

        public void register()
        {
            AppEvent.RegisterCallback(actionType, callback);
        }
        public void unregister()
        {
            AppEvent.UnregisterCallback(actionType, callback);
        }
    }
}