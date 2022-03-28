using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    public static class WindowManager
    {
        private static Dictionary<Type, TUIWindow> windows = new Dictionary<Type, TUIWindow>();

        private static Stack<TUIWindow> windowStack = new Stack<TUIWindow>();
        public static void OpenWindow(Type type)
        {
            TUIWindow window = null;
            if (windows.ContainsKey(type))
            {
                window = windows[type];
            }
            else
            {
                var windows = Resources.FindObjectsOfTypeAll(type);
                if (windows.Length > 0)
                    window = windows[0] as TUIWindow;
                else
                    window = ScriptableObject.CreateInstance(type) as TUIWindow;
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

        public static void RegisterWindow(Type type, TUIWindow window)
        {
            if (windows.ContainsKey(type)) return;
            windows.Add(type, window);
            EditorApplication.wantsToQuit += () =>
            {
                window.Close();
                return true;
            };
        }

        public static T GetWindow<T>() where T:TUIWindow
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
        public static void OpenWindow<T>() where T : TUIWindow
        {
            OpenWindow(typeof(T));
        }

        public static void HideWindow<T>() where T : TUIWindow
        {
            HideWindow(typeof(T));
        }

        // [MenuItem("Test/BlockUserInput")]
        // public static void BlockUserInput()
        // {
        //     foreach (var kv in windows)
        //     {
        //         kv.Value.BlockUserInput();
        //     }
        //
        //     var scenviews = Resources.FindObjectsOfTypeAll<SceneView>();
        //     for (int i = 0; i < scenviews.Length; ++i)
        //     {
        //         var sceneview = scenviews[i];
        //         var image = sceneview.rootVisualElement.Q<Image>("image_input_block") ?? new Image();
        //         image.name = "image_input_block";
        //         TUIWindow.BlockInput(sceneview.rootVisualElement, image);
        //     }
        // }

        public static void ResumeUserInput()
        {
            foreach (var kv in windows)
            {
                kv.Value.ResumeUserInput();
            }
            var scenviews = Resources.FindObjectsOfTypeAll<SceneView>();
            for (int i = 0; i < scenviews.Length; ++i)
            {
                var sceneview = scenviews[i];
                var image = sceneview.rootVisualElement.Q<Image>("image_input_block");
                if(image != null)
                {
                    image.style.display = DisplayStyle.None;
                }
            }
        }
    }
}