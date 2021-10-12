using System;
using UnityEditor;
using UnityEngine;

namespace UIFramework.Editor.Core.Popup
{
    public class WorkPopupWindow: EditorWindow
    {
        private Action UpdateCallback;
        public Editor.Core.TUIElement PopupElement;
        
        public void SetContent(Editor.Core.TUIElement popElement, Vector2 fixedSize, Action update)
        {
            rootVisualElement.Clear();
            this.PopupElement = popElement;
            rootVisualElement.Add(popElement.RootContainer);
            if (fixedSize.x > 0 && fixedSize.y > 0)
            {
                maxSize = fixedSize;
                minSize = fixedSize;
            }

            UpdateCallback = update;
        }
        
        private void Update()
        {
            if (rootVisualElement.childCount == 0)
            {
                Close();
            }
            UpdateCallback?.Invoke();
            
            Repaint(); // force to repaint to avoid text input font color bug
        }

        private void OnDestroy()
        {
            PopupElement.OnDestroy();
        }
    }
}