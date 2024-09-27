using UIFramework.Extends;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public class DoubleClickHandler
    {
        /// <summary>
        /// 注册ClickEvent 触发的 双击逻辑事件
        /// </summary>
        /// <param name="panel">页面对象</param>
        /// <param name="target">元素对象</param>
        /// <param name="millisecond">毫秒值</param>
        /// <typeparam name="T">页面对象类型</typeparam>
        public static void RegisterDoubleClickEvent<T>(T panel, VisualElement target, int millisecond)
            where T : IDoubleClickUIEvent
        {
            target?.RegisterCallback<ClickEvent, int>(panel.OnDoubleClickExtends, millisecond);
        }

        public static void UnRegisterDoubleClickEvent<T>(T panel, VisualElement target) where T : IDoubleClickUIEvent
        {
            target?.UnregisterCallback<ClickEvent, int>(panel.OnDoubleClickExtends);
        }
    }
}