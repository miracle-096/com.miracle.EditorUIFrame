using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    public class VisualObject: VisualElement
    {
        public VisualElement element;
        public Dictionary<Type, TComponent> AllComponents { get; set; }

        public T GetComponent<T>() where T : TComponent, new()
        {
            if (!AllComponents.ContainsKey(typeof(T))) return null;
            return AllComponents[typeof(T)] as T;
        }
    }
}