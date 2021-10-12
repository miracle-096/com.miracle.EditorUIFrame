using UIFramework.Runtime.Core;
using UnityEngine.UIElements;

namespace UIFramework.Runtime.Extends
{
    public static class CoreExtends
    {
        public static T Q<T>(this TUIElement panel, string name = null, params string[] classNames)
            where T : VisualElement
        {
            return panel.RootContainer.Q<T>(name, classNames);
        }
        public static void Destroy(this TUIElement element)
        {
            TUIManager.Destroy(element);
        }
    }
}