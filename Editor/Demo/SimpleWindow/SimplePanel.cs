using UnityEngine;

namespace UIFramework.Editor.Demo.SimpleWindow
{
    public partial class SimplePanel
    {
        protected override void OnCreate(params object[] objs)
        {
            base.OnCreate(objs);
            
            RootContainer.parent.style.backgroundColor = new Color(0.48f,0.6f,0.42f);
        }
    }
}