using UIFramework.Core;
using UIFramework.Editor.Utility;
using UnityEditor;

namespace UIFramework.Editor.Demo.SimpleWindow
{
    public class SimpleWindow : UIWindow
    {
        private SimplePanel _uiPage;

        [MenuItem("Tools/Editor_Tools/Demo/SimpleWindow", false, -1)]
        public static void OpenDemoWindow()
        {
            GetWindow<SimpleWindow>().OpenView();
        }

        protected override UIElement MakeView(params object[] objs)
        {
            rootVisualElement.Clear();
            _uiPage = UILoader.LoadElement<SimplePanel>(rootVisualElement);
            return _uiPage;
        }
    }
}