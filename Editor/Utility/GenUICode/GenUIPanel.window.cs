using UIFramework.Core;
using UIFramework.Editor.Core;
using UnityEngine;

namespace UIFramework.Editor.Utility
{
    public class GenUIWindow : UIWindow
    {
        public GenUIPanel uiPage;
        private Vector2 preSize;

        protected override EPanel MakeView(params object[] objs)
        {
            rootVisualElement.Clear();
            uiPage = UILoader.LoadElement<GenUIPanel>(rootVisualElement,objs);
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