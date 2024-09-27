using System;
using UIFramework.Core;
using UnityEditor;
using UnityEngine;

namespace UIFramework.Editor.Core.Popup
{
    public class WorkPopupWindow: EditorWindow
    {
        private Action UpdateCallback;
        public EPanel PopupElement;
        
        public void SetContent(EPanel popElement, Vector2 fixedSize, Action update)
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
            
            //Repaint();
        }

        private void OnDestroy()
        {
            PopupElement.OnDestroy();
        }
    }
}