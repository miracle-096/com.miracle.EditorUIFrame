using System;
using System.Reflection;
using UIFramework.Core;
using UnityEditor;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Utility
{
    public class UILoader
    {
        public static UIElement LoadElement(Type type, params object[] objs)
        {
            var r = type.GetCustomAttribute<UIAttribute>();
            if (r == null || string.IsNullOrEmpty(r.Uxml))
                throw new Exception($"UIAttribute.Uxml is Empty <Type = {type}>");
            var container = LoadByPath(r.Uxml, r.Uss, type.Name);
            return UIElement.Create(type, container, objs);
        }

        public static UIElement LoadElement(Type type, VisualElement Parent, params object[] objs)
        {
            var r = type.GetCustomAttribute<UIAttribute>();
            if (r == null || string.IsNullOrEmpty(r.Uxml))
                throw new Exception($"UIAttribute.Uxml is Empty <Type = {type}>");
            var container = LoadByPath(r.Uxml, r.Uss, type.Name);
            Parent?.Add(container);
            return UIElement.Create(type, container, objs);
        }

        public static T LoadElement<T>(params object[] objs) where T : UIElement
        {
            var type = typeof(T);
            return LoadElement(type, objs) as T;
        }

        public static T LoadElement<T>(VisualElement Parent, params object[] objs) where T : UIElement
        {
            var type = typeof(T);
            return LoadElement(type, Parent, objs) as T;
        }

        private static TemplateContainer LoadByPath(string uxml, string uss = null, string clsName = null)
        {
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxml).CloneTree();
            if (!string.IsNullOrEmpty(uss))
            {
                tree.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(uss));
            }

            if (!string.IsNullOrEmpty(clsName))
            {
                var guid = Guid.NewGuid().ToString();
                tree.name = $"{clsName}_{guid}";
            }

            return tree;
        }
    }
}