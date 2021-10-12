using UIFramework.Editor.Core;
using UIFramework.Utility.GenUICode;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.GenUICode.Component
{
    public class CutlineComponent: TComponent
    {
        private GenUIPanel panel;

        public GenUIPanel Panel
        {
            get => panel ??= GetPanel<GenUIPanel>();
            set => panel = value;
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