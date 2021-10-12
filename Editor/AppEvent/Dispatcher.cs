using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework.AppEvent
{
    public class Dispatcher
    {
        private Dictionary<Type, List<Action<object>>> _callbacks = new Dictionary<Type, List<Action<object>>>();

        public void Dispatch(object action)
        {
            var type = action.GetType();
            if (!_callbacks.ContainsKey(type))
            {
                return;
            }
            var callbackList = _callbacks[type];
            if (callbackList != null)
            {
                for (int i = 0; i < callbackList.Count; ++i)
                {
                    callbackList[i].Invoke(action);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        /// <param name="exclude"></param>
        public void RegisteCallback(Type type, Action<object> callback, bool exclude = false)
        {
            if(exclude)
            {
                _callbacks.Remove(type);
            }
            if (!_callbacks.ContainsKey(type))
            {
                _callbacks.Add(type, new List<Action<object>>());
            }
            _callbacks[type].Add(callback);
        }
        
        public void ClearCallbacks()
        {
            _callbacks.Clear();
        }

        public void UnregisterCallback(Type type, Action<object> callback)
        {
            if(callback == null)
            {
                _callbacks.Remove(type);
            } else
            {
                _callbacks.TryGetValue(type, out var list);
                if (list == null) Debug.LogWarning($"When {type} UnregisterCallback list be null!!!");
                list?.Remove(callback);
            }
        }
    }
}