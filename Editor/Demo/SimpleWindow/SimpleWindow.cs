using UIFramework.Editor.Core;
using UnityEditor;

namespace UIFramework.Editor.Demo
{
    public class SimpleWindow : UIWindow
    {
        private SimplePanel _uiPage;

        [MenuItem("Tools/Editor_Tools/Demo/SimpleWindow", false, -1)]
        public static void OpenDemoWindow()
        {
            GetWindow<SimpleWindow>().OpenPanel();
        }

        protected override EPanel MakeView(params object[] objs)
        {
            rootVisualElement.Clear();
            _uiPage = UILoader.LoadElement<SimplePanel>(rootVisualElement);
            return _uiPage;
        }
    }
}