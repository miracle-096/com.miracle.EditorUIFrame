using System;
using System.Collections.Generic;
using UIFramework.Editor.Core;
using UIFramework.Editor.CustomElement.Foldout.Component;
using UIFramework.Editor.Extensions;
using UIFramework.Editor.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public partial class FoldoutHeader : EPanel
{
    public GenUIPanel toggleParent;
    public string elementName;
    public Type elementType;
    public bool isChecked;
    public bool isFold = true;
    public List<VisualElement> CustomElements;
    public static Texture2D blurFoldIcon;
    public static Texture2D blurUnFoldIcon;
    public static Texture2D focusFoldIcon;
    public static Texture2D focusUnFoldIcon;

    public bool IsChecked
    {
        get => isChecked;
        set
        {
            isChecked = value;
            Texture2D img;
            if (isChecked)
            {
                img = isFold ? focusFoldIcon : focusUnFoldIcon;
            }
            else
            {
                img = isFold ? blurFoldIcon : blurUnFoldIcon;
            }

            Icon.style.backgroundImage = new StyleBackground(img);
        }
    }

    public void Init(EPanel parent, string text, string elementName = null, Type type = null)
    {
        toggleParent = parent as GenUIPanel;
        Text.text = text;
        this.elementName = elementName;
        elementType = type;
        isFold = true;
        IsChecked = false;
    }

    protected override void OnCreate(params object[] objs)
    {
        base.OnCreate(objs);
        RootContainer.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        CustomElements = new List<VisualElement>();
        Root.AddComponent<LabelEComponent>(this);
        blurFoldIcon ??= LoadImg("foldout_blur_on");
        blurUnFoldIcon ??= LoadImg("foldout_blur_off");
        focusFoldIcon ??= LoadImg("foldout_focus_on");
        focusUnFoldIcon ??= LoadImg("foldout_focus_off");
    }

    public void ChangeLabelClass(string className)
    {
        Text.ClearClassList();
        Text.AddToClassList(className);
    }

    public void SwitchFocusBg(bool toShow)
    {
        FocusBg.style.visibility = toShow
            ? new StyleEnum<Visibility>(Visibility.Visible)
            : new StyleEnum<Visibility>(Visibility.Hidden);
    }

    public void HideIcon()
    {
        Icon.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
    }

    public void SetRootPadding(int pixel, int heirarchy)
    {
        Root.style.paddingLeft = pixel * heirarchy;
    }

    public static Texture2D LoadImg(string imgName)
    {
        string path = "Packages/com.miracle.EditorUIFrame/Editor/CustomElement/icon/" + imgName + ".png";
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (texture == null)
        {
            Debug.LogWarning($"Failed to load texture: {path}");
        }
        return texture;
    }
}