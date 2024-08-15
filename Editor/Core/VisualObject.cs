using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public class VisualObject: VisualElement
    {
        public VisualElement element;
        public Dictionary<Type, UIComponent> AllComponents { get; set; }

        public T GetComponent<T>() where T : UIComponent, new()
        {
            if (!AllComponents.ContainsKey(typeof(T))) return null;
            return AllComponents[typeof(T)] as T;
        }
    }
}