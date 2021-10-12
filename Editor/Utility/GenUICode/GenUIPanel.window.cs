using UIFramework.Editor.Core;
using UIFramework.Utility;
using UIFramework.Utility.GenUICode;
using UnityEngine;

namespace UIFramework.Editor.Utility.GenUICode
{
    public class GenUIPanel_window : TUIWindow
    {
        public GenUIPanel uiPage;
        public static string ParsUxmlPath;
        private Vector2 preSize;

        public override TUIElement MakeView()
        {
            rootVisualElement.Clear();
            uiPage = UILoader.LoadElement<GenUIPanel>(rootVisualElement,ParsUxmlPath);
            preSize = rootVisualElement.localBound.size;
            uiPage.ChangeSize(preSize);
            return uiPage;
        }

        protected override void Update()
        {
            base.Update();
            if (uiPage!=null && rootVisualElement.localBound.size != preSize)
            {
                preSize = rootVisualElement.localBound.size;
                uiPage.ChangeSize(preSize);
            }
        }
    }
}