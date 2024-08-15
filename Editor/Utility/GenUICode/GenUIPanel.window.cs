using UIFramework.Core;
using UnityEngine;

namespace UIFramework.Editor.Utility.GenUICode
{
    public class GenUIWindow : UIWindow
    {
        public GenUIPanel uiPage;
        public static string ParsUxmlPath;
        private Vector2 preSize;

        protected override UIElement MakeView(params object[] objs)
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