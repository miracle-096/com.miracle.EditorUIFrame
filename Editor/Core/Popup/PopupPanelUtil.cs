using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core.Popup
{
    public class PopupPanelUtil
    {
        private static Dictionary<Tuple<string, string>, Action<Editor.Core.TUIElement>> CustomItemCallbacks = new Dictionary<Tuple<string, string>, Action<Editor.Core.TUIElement>>();
        
        /*
         * CustomItemCallbacks.Add(new Tuple<string, string>("文本", "自定义文本"), TextEditorPanel.ShowGUI);
         * CustomItemCallbacks.Add(new Tuple<string, string>("导出", "视频"), RecorderPanel.ShowGUI);
         * CustomItemCallbacks.Add(new Tuple<string, string>("演绎动画", "自定义动画"), ActionDriverController.StartActionDriver);
         * CustomItemCallbacks.Add(new Tuple<string, string>("删除", "自定义资源"), ConfirmRemovePanel.ShowGUI);       
         */
        public static void Init(Dictionary<Tuple<string, string>, Action<Editor.Core.TUIElement>> PopPanelActions)
        {
            CustomItemCallbacks = PopPanelActions;
        }
        

        public static T GetWindowOfPanelType<T>()  where T: Editor.Core.TUIElement
        {
            foreach (var window in Resources.FindObjectsOfTypeAll<WorkPopupWindow>())
            {
                if (window.PopupElement is T)
                {
                    return window.PopupElement as T;
                }
            }
            return null;
        }
        public static void TryShowPopup<T>(ClickEvent evt, Tuple<string, string,T> param) where T: Editor.Core.TUIElement
        {
            var (item1, item2, item3) = param;
            Tuple<string,string> key = new Tuple<string, string>(item1, item2);
            if (CustomItemCallbacks.ContainsKey(key))
            {
                CustomItemCallbacks[key]?.Invoke(item3);
            }
        }

        public static void Close(Editor.Core.TUIElement panel)
        {
            foreach (var window in Resources.FindObjectsOfTypeAll<WorkPopupWindow>())
            {
                if (window.PopupElement == panel)
                {
                    window.Close();
                }
            }
        }
    }
}