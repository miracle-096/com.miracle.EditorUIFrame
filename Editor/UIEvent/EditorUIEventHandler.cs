using System;

namespace UIFramework.UIEvent
{
    public class EditorUIEventHandler
    {
        public Type actionType;
        public Action<object> callback;

        public void register()
        {
            EditorUIEventManager.RegisterCallback(actionType, callback);
        }
        public void unregister()
        {
            EditorUIEventManager.UnregisterCallback(actionType, callback);
        }
    }
}