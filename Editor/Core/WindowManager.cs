using System;
using System.Collections.Generic;
using UIFramework.Core;
using UnityEditor;
using UnityEngine;

namespace UIFramework.Editor.Core
{
    public static class WindowManager
    {
        private static Dictionary<Type, UIWindow> windows = new Dictionary<Type, UIWindow>();

        private static Stack<UIWindow> windowStack = new Stack<UIWindow>();
        public static void OpenWindow(Type type)
        {
            UIWindow window = null;
            if (windows.ContainsKey(type))
            {
                window = windows[type];
            }
            else
            {
                var windows = Resources.FindObjectsOfTypeAll(type);
                if (windows.Length > 0)
                    window = windows[0] as UIWindow;
                else
                    window = ScriptableObject.CreateInstance(type) as UIWindow;
            }
            
            window.Show();
        }
        public static void HideWindow(Type type)
        {
            if (windows.ContainsKey(type))
            {
                var window = windows[type];
                window.Close();
            }
        }

        public static void RegisterWindow(Type type, UIWindow window)
        {
            if (windows.ContainsKey(type)) return;
            windows.Add(type, window);
            EditorApplication.wantsToQuit += () =>
            {
                window.Close();
                return true;
            };
        }

        public static T GetWindow<T>() where T:UIWindow
        {
            return windows[typeof(T)] as T;
        }

        public static void UnRegisterWindow(Type type)
        {
            if (windows.ContainsKey(type))
            {
                windows.Remove(type);
            }
        }
        public static void OpenWindow<T>() where T : UIWindow
        {
            OpenWindow(typeof(T));
        }

        public static void HideWindow<T>() where T : UIWindow
        {
            HideWindow(typeof(T));
        }
    }
}