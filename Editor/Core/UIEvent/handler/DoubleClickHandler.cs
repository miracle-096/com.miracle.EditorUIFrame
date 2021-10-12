using System;
using UIFramework.Editor;
using UIFramework.Editor.Extends;
using UIFramework.EventHandlers.Interface;
using UnityEngine.UIElements;

namespace UIFramework.EventHandlers
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
        public static void RegistDoubleClickEvent<T>(T panel, VisualElement target, int millisecond)
            where T : TDoubleClickEvent
        {
            target?.RegisterCallback<ClickEvent, int>(panel.OnDoubleClickExtends, millisecond);
        }

        public static void UnRegistDoubleClickEvent<T>(T panel, VisualElement target) where T : TDoubleClickEvent
        {
            target?.UnregisterCallback<ClickEvent, int>(panel.OnDoubleClickExtends);
        }
    }
}