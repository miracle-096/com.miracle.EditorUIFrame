using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Core
{
    public class VisualObject: VisualElement
    {
        public VisualElement element;
        public Dictionary<Type, EComponent> AllComponents { get; set; }

        public T GetComponent<T>() where T : EComponent, new()
        {
            if (!AllComponents.ContainsKey(typeof(T))) return null;
            return AllComponents[typeof(T)] as T;
        }
    }
}