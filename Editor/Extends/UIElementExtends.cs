using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Extends
{
    public static class UIElementExtends
    {
        public static void SetWidthAndHeight(this VisualElement element, float width, LengthUnit widthUnit,
            float height, LengthUnit heightUnit)
        {
            element.style.width = new Length(width, widthUnit);
            element.style.height = new Length(height, heightUnit);
        }

        public static float GetWidth(this VisualElement element)
        {
            return element.style.width.value.value;
        }

        public static void SetWidth(this VisualElement element, float width, LengthUnit widthUnit)
        {
            element.style.width = new StyleLength(new Length(width, widthUnit));
        }

        public static void SetWidth(this VisualElement element, StyleKeyword sk)
        {
            element.style.width = new StyleLength(sk);
        }

        public static float GetHeight(this VisualElement element)
        {
            return element.style.height.value.value;
        }

        public static void SetBorder(this VisualElement element, float width)
        {
            element.style.borderLeftWidth = width;
            element.style.borderRightWidth = width;
            element.style.borderBottomWidth = width;
            element.style.borderTopWidth = width;
        }

        public static void SetHeight(this VisualElement element, float height, LengthUnit heightUnit)
        {
            element.style.height = new Length(height, heightUnit);
        }

        public static void SetBackgroundImage(this VisualElement element, Texture2D texture2D)
        {
            element.style.backgroundImage = texture2D;
        }

        public static void CleanAllChild(this VisualElement element,Func<int,VisualElement,bool> validateFunc = null)
        {
            for (int i = element.childCount-1; i >=0 ; i--)
            {
                if (validateFunc==null)
                {
                    element.RemoveAt(i);
                }
                else
                {
                    if (validateFunc.Invoke(i,element[i]))
                    {
                        element.RemoveAt(i);
                    }
                }
            }
        }
        public static void DisplayNoneChild(this VisualElement element,Func<int,VisualElement,bool> validateFunc = null)
        {
            for (int i = element.childCount-1; i >=0 ; i--)
            {
                if (validateFunc==null)
                {
                    element.ElementAt(i).style.display = DisplayStyle.None;
                }
                else
                {
                    if (validateFunc.Invoke(i,element[i]))
                    {
                        element.ElementAt(i).style.display = DisplayStyle.None;
                    }
                }
            }
        }
    }
}