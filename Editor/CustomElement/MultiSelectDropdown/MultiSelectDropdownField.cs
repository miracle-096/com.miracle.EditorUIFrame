using System;
using System.Collections.Generic;
using UIFramework.Editor.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Core
{
    public class MultiSelectDropdownField : VisualElement
    {
        private TextField displayField;
        private Button dropdownButton;
        private List<string> allOptions = new List<string>();
        private HashSet<string> selectedOptions = new HashSet<string>();
        public Action<List<string>> OnValueChanged;

        public new class UxmlFactory : UxmlFactory<MultiSelectDropdownField, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlStringAttributeDescription _options = new UxmlStringAttributeDescription
                { name = "options", defaultValue = "" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                var control = (MultiSelectDropdownField)ve;
                var optionsStr = _options.GetValueFromBag(bag, cc);
                control.name = m_Name.GetValueFromBag(bag, cc);

                if (!string.IsNullOrEmpty(optionsStr))
                {
                    control.SetOptions(new List<string>(optionsStr.Split(',')));
                }
            }
        }

        public MultiSelectDropdownField()
        {
            var dropdownField = UILoader.LoadElement<MultiSelectDropdownView>();
            dropdownButton = dropdownField.DropdownButton;
            displayField = dropdownField.DisplayField;
            if (dropdownButton != null)
            {
                dropdownButton.clickable.clicked += OnDropdownButtonClicked;
            }

            if (displayField != null)
            {
                displayField.isReadOnly = true;
                displayField.RegisterCallback<ClickEvent>(OnDisplayFieldClicked);
            }

            Add(dropdownField.RootContainer);
        }

        private void OnDropdownButtonClicked()
        {
            ShowDropdownMenu();
        }

        private void OnDisplayFieldClicked(ClickEvent evt)
        {
            ShowDropdownMenu();
        }

        private void ShowDropdownMenu()
        {
            if (allOptions == null || allOptions.Count == 0)
                return;

            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("全选"), false, SelectAllOptions);
            menu.AddItem(new GUIContent("取消全选"), false, DeselectAllOptions);
            foreach (var option in allOptions)
            {
                bool isSelected = selectedOptions.Contains(option);
                menu.AddItem(new GUIContent(option), isSelected, ToggleOption, option);
            }

            var worldBound = contentContainer.worldBound; // Get the worldBound of displayField
            var rect = new Rect(worldBound.x, worldBound.yMax, 0, 0); // Display below displayField with the same width

            menu.DropDown(rect);
        }

        private void SelectAllOptions()
        {
            selectedOptions.UnionWith(allOptions);
            UpdateDisplayText();
            OnValueChanged?.Invoke(new List<string>(selectedOptions));
        }

        private void DeselectAllOptions()
        {
            selectedOptions.Clear();
            UpdateDisplayText();
            OnValueChanged?.Invoke(new List<string>(selectedOptions));
        }

        private void ToggleOption(object option)
        {
            string selectedOption = option as string;

            if (selectedOptions.Contains(selectedOption))
            {
                selectedOptions.Remove(selectedOption);
            }
            else
            {
                selectedOptions.Add(selectedOption);
            }

            UpdateDisplayText();
            OnValueChanged?.Invoke(new List<string>(selectedOptions));
        }

        public void SetOptions(List<string> options)
        {
            if (options == null)
                return;

            allOptions = options;
            selectedOptions.Clear();
            UpdateDisplayText();
        }

        private List<string> sortedSelectedOptions = new();

        private void UpdateDisplayText()
        {
            sortedSelectedOptions.Clear();
            foreach (var option in allOptions)
            {
                if (selectedOptions.Contains(option))
                {
                    sortedSelectedOptions.Add(option);
                }
            }

            displayField.value = selectedOptions.Count == 0 ? "请选择脚本" : string.Join(", ", sortedSelectedOptions);
        }


        public void RegisterValueChangedCallback(Action<List<string>> callback)
        {
            OnValueChanged += callback;
        }

        public void UnregisterValueChangedCallback(Action<List<string>> callback)
        {
            OnValueChanged -= callback;
        }

        public List<string> value
        {
            get => new List<string>(selectedOptions);
            set
            {
                selectedOptions = value != null ? new HashSet<string>(value) : new HashSet<string>();
                UpdateDisplayText();
                OnValueChanged?.Invoke(new List<string>(selectedOptions));
            }
        }
    }
}