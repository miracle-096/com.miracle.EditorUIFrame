using UIFramework.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Utility.GenUICode.Component
{
    /// <summary>
    /// 分割线组件
    /// </summary>
    public class DivideLineUIComponent: UIComponent
    {
        private GenUIPanel _panel;

        public GenUIPanel Panel
        {
            get => _panel ??= GetPanel<GenUIPanel>();
            set => _panel = value;
        }

        private bool _clickDown;
        private Vector2 mosPos; 
        public override void Start()
        {
            element.RegisterCallback<MouseDownEvent>(OnMouseDown);
            element.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }
        
        private void OnMouseUp(MouseUpEvent evt)
        {
            _clickDown = false;
            Panel.RootContainer.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            Panel.RootContainer.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        }

        private void OnMouseDown(MouseDownEvent evt)
        {
            _clickDown = true;
            Panel.RootContainer.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            Panel.RootContainer.RegisterCallback<MouseUpEvent>(OnMouseUp);
            mosPos = evt.mousePosition;
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            if (_clickDown)
            {
                float deltaWidth = evt.mousePosition.x - mosPos.x;
                float hierarchyW = Panel.Hierarchy.style.width.value.value;
                float inspectorW = Panel.Inspector.style.width.value.value;
                Panel.Hierarchy.style.width = hierarchyW + deltaWidth;
                Panel.Inspector.style.width = inspectorW - deltaWidth;
                mosPos = evt.mousePosition;
            }
        }
        
    }
}