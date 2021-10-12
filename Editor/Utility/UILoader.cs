using System;
using System.Reflection;
using UIFramework.Editor.Core;
using UnityEditor;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Utility
{
    public class UILoader
    {
        public static TUIElement LoadElement(Type type, params object[] objs)
        {
            var r = type.GetCustomAttribute<UIAttribute>();
            if (r == null || string.IsNullOrEmpty(r.Uxml))
                throw new Exception($"UIAttribute.Uxml is Empty <Type = {type}>");
            var container = LoadByPath(r.Uxml, r.Uss);
            return TUIElement.Create(type, container, objs);
        }

        public static TUIElement LoadElement(Type type, VisualElement Parent, params object[] objs)
        {
            var r = type.GetCustomAttribute<UIAttribute>();
            if (r == null || string.IsNullOrEmpty(r.Uxml))
                throw new Exception($"UIAttribute.Uxml is Empty <Type = {type}>");
            var container = LoadByPath(r.Uxml, r.Uss);
            Parent?.Add(container);
            return TUIElement.Create(type, container, objs);
        }

        public static T LoadElement<T>(params object[] objs) where T : TUIElement
        {
            var type = typeof(T);
            return LoadElement(type, objs) as T;
        }

        public static T LoadElement<T>(VisualElement Parent, params object[] objs) where T : TUIElement
        {
            var type = typeof(T);
            return LoadElement(type, Parent, objs) as T;
        }

        private static TemplateContainer LoadByPath(string uxml, string uss)
        {
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxml).CloneTree();
            if (!string.IsNullOrEmpty(uss))
            {
                tree.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(uss));
            }

            return tree;
        }
    }
}