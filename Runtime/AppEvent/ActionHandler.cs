using System;

namespace UIFramework.Runtime.AppEvent
{
    public class ActionHandler
    {
        public Type actionType;
        public Action<object> callback;

        public void register()
        {
            Runtime.AppEvent.AppEvent.RegisterCallback(actionType, callback);
        }
        public void unregister()
        {
            Runtime.AppEvent.AppEvent.UnregisterCallback(actionType, callback);
        }
    }
}