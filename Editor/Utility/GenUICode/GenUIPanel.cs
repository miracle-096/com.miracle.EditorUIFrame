using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UIFramework.Editor.CustomElement.Foldout;
using UIFramework.Editor.Utility.GenUICode.Component;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Utility.GenUICode
{
    public partial class GenUIPanel
    {
        public VisualElement _rootFold;
        public string ParseUxmlPath;
        public string UxmlName;
        public string CsName;
        public List<FoldoutHeader> toggleGroup;

        private List<Tuple<Type, string, string>> fields;

        private void OnGenerateClick()
        {
            EditorUtility.DisplayProgressBar("自动生成脚本", "正在生成.cs", 0);
            fields = new List<Tuple<Type, string, string>>();
            foreach (FoldoutHeader labelFoldout in toggleGroup)
            {
                if (string.IsNullOrEmpty(labelFoldout.elementName)) continue;
                Toggle toggle = labelFoldout.CustomElements[0] as Toggle;
                if (!toggle.value) continue;
                string elementName = labelFoldout.elementName;
                var fieldName = new StringBuilder();
                foreach (string s1 in elementName.Split('-'))
                {
                    foreach (var s2 in s1.Split('_'))
                    {
                        fieldName.Append(s2.Substring(0, 1).ToUpper());
                        fieldName.Append(s2.Substring(1));
                    }
                }

                fields.Add(new Tuple<Type, string, string>(labelFoldout.elementType, fieldName.ToString(),
                    elementName));
            }

            string csFilePath = Path.GetDirectoryName(ParseUxmlPath) + "/" + CsName;
            GenUIManager.PreBuildClass(csFilePath, NameSpace.text,
                Path.GetFileNameWithoutExtension(ParseUxmlPath), fields, File.Exists(csFilePath));
        }

        protected override void OnCreate(params object[] objs)
        {
            base.OnCreate();
            UIFramework.Extends.Extends.AddComponent<DivideLineUIComponent>(CutLine, this);

            toggleGroup = new List<FoldoutHeader>();
            RootContainer.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            RootContainer.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

            Generate.clicked += OnGenerateClick;

            ParseUxmlPath = objs[0] as string;
            //string[] spaces = null;
            //AssemblyDefinitionAsset asmdef = null;
            // if (ParseUxmlPath.Contains("/Editor/"))
            // {
            //     GetNamespace("/Editor/", out asmdef, out spaces);
            // }
            //
            // if (ParseUxmlPath.Contains("/Runtime/"))
            // {
            //     GetNamespace("/Runtime/", out asmdef, out spaces);
            // }
            // GenAssembly genAssembly = JsonUtility.FromJson<GenAssembly>(asmdef.text);

            // NameSpace.value = string.Join(".", spaces, 0, spaces.Length - 1);
            // if (!string.IsNullOrEmpty(genAssembly.rootNamespace))
            // {
            //     NameSpace.value = genAssembly.rootNamespace + "." + NameSpace.value;
            // }
            NameSpace.value = "";
            
            UxmlName = Path.GetFileName(ParseUxmlPath);
            CsName = Path.GetFileNameWithoutExtension(ParseUxmlPath) + ".ui.cs";
            TemplateContainer container = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ParseUxmlPath).CloneTree();
            _rootFold = new VisualElement();
            RootView.contentContainer.Add(_rootFold);
            _rootFold.style.paddingLeft = 2;
            _rootFold.style.paddingTop = 2;
            _rootFold.style.paddingBottom = 2;
            _rootFold.style.paddingRight = 2;
            FoldoutHeader label = UILoader.LoadElement<FoldoutHeader>(_rootFold);
            label.Init(this, UxmlName);
            rootToggle = NewToggle();
            rootToggle.RegisterCallback<ClickEvent>(SwitchAllToggle);
            label.CustomElements.Add(rootToggle);
            label.RootContainer.Add(rootToggle);
            toggleGroup.Add(label);
            AddChild(_rootFold, container, 0);
        }

        public void GetNamespace(string EditorOrRuntime,out AssemblyDefinitionAsset asmdef,
            out string[] spaces)
        {
            spaces = new string[] { };
            asmdef = null;
            var tempStrs = ParseUxmlPath.Split(new[] {EditorOrRuntime}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var file in Directory.GetFiles(tempStrs[0] + EditorOrRuntime))
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Extension == ".asmdef")
                {
                    asmdef = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(file);
                    break;
                }
            }
            spaces = tempStrs[1].Split('/');
        }

        private Toggle rootToggle;


        private void SwitchAllToggle(ClickEvent evt)
        {
            foreach (FoldoutHeader labelFoldout in toggleGroup)
            {
                Toggle toggle = labelFoldout.CustomElements[0] as Toggle;
                toggle.value = rootToggle.value;
            }
        }

        private void AddChild(VisualElement container, VisualElement child, int heirarchy)
        {
            heirarchy += 1;
            VisualElement node = new VisualElement();
            string str = child.name + $"({child.GetType().Name})";
            FoldoutHeader label = UILoader.LoadElement<FoldoutHeader>(node);
            label.Init(this, str, child.name, child.GetType());
            Toggle toggle = NewToggle();
            label.CustomElements.Add(toggle);
            label.RootContainer.Add(toggle);
            label.SetRootPadding(10, heirarchy);
            toggleGroup.Add(label);
            container.Add(node);
            if (child.childCount == 0)
            {
                label.HideIcon();
                return;
            }

            foreach (VisualElement element in child.Children())
            {
                AddChild(node, element, heirarchy);
            }
        }

        public Toggle NewToggle()
        {
            Toggle toggle = new Toggle();
            toggle.label = "";
            toggle.style.position = new StyleEnum<Position>(Position.Absolute);
            toggle.style.right = 2;
            toggle.style.top = 3;
            return toggle;
        }

        public void ChangeSize(Vector2 size)
        {
            float width = size.x * 0.6f - 2;
            Hierarchy.style.width = width;
            Inspector.style.width = size.x - width - 2;
        }
    }
}