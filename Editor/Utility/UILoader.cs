using System;
using System.Reflection;
using UIFramework.Core;
using UnityEditor;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Utility
{
    public class UILoader
    {
        public static EPanel LoadElement(Type type, params object[] objs)
        {
            var attribute = type.GetCustomAttribute<UIAttribute>();
            if (attribute == null || string.IsNullOrEmpty(attribute.Uxml))
                throw new Exception($"UIAttribute.Uxml is Empty <Type = {type}>");
            var container = LoadByPath(attribute.Uxml, attribute.Uss, type.Name);
            return EPanel.Create(type, container, objs);
        }

        public static EPanel LoadElement(Type type, VisualElement Parent, params object[] objs)
        {
            var attribute = type.GetCustomAttribute<UIAttribute>();
            Assert.IsFalse(attribute == null || string.IsNullOrEmpty(attribute.Uxml),$"UIAttribute.Uxml is Empty <Type = {type}>"); 
            var container = LoadByPath(attribute.Uxml, attribute.Uss, type.Name);
            Parent?.Add(container);
            return EPanel.Create(type, container, objs);
        }

        public static T LoadElement<T>(params object[] objs) where T : EPanel
        {
            var type = typeof(T);
            return LoadElement(type, objs) as T;
        }

        public static T LoadElement<T>(VisualElement Parent, params object[] objs) where T : EPanel
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