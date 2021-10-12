using System;
using System.Reflection;
using UIFramework.Runtime.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace UIFramework.Runtime.Utility
{
    public class UILoader
    {
        public static T LoadElement<T>(VisualElement Parent,params object[] objs) where T : TUIElement
        {
            var type = typeof(T);
            var r = type.GetCustomAttribute<UIAttribute>();
            if (r == null || string.IsNullOrEmpty(r.Uxml))
                throw new Exception($"UIAttribute.Uxml is Empty <Type = {type}>");
            var container = LoadByPath(r.Uxml, r.Uss);
            Parent.Add(container);
            return TUIManager.Create<T>(container, objs);
        }

        private static TemplateContainer LoadByPath(string uxml, string uss)
        {
            var tree = Addressables.LoadAsset<VisualTreeAsset>(uxml).Result.CloneTree();
            if (!string.IsNullOrEmpty(uss))
            {
                tree.styleSheets.Add(Addressables.LoadAsset<StyleSheet>(uss).Result);
            }

            return tree;
        }
        
        public static void Show(TUIElement ui)
        {
            TUIManager.Show(ui);
        }
        public static void Hide(TUIElement ui)
        {
            TUIManager.Hide(ui);
        }
    }
}