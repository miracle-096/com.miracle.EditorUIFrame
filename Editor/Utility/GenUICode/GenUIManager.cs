using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UIFramework.Core;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Editor.Utility.GenUICode
{
    public class GenUIManager
    {
        /// <summary>
        /// 右键.uxml文件时打开 UIBuilder 的同时打开生成代码用的 GenWindow
        /// </summary>
        /// <returns></returns>
        [MenuItem("Assets/UIToolkit脚本生成器")]
        private static void OpenGenWindow()
        {
            var selectedObject = Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(selectedObject);
            if (!path.EndsWith(".uxml"))
            {
                Debug.LogWarning("Selected object is not a .uxml file.");
                return;
            }
            var parsUxmlPath = path.Replace('\\', '/');
            GenUIWindow uiWindow = null;
            if (Path.GetExtension(path) == ".uxml")
            {
                if (EditorWindow.HasOpenInstances<GenUIWindow>())
                {
                    uiWindow = EditorWindow.GetWindow<GenUIWindow>();
                    uiWindow.Close();
                }

                uiWindow = EditorWindow.GetWindow<GenUIWindow>(typeof(SceneView));
            }

            uiWindow.OpenPanel(parsUxmlPath);
        }

        /// <summary>
        /// 勾选元素后生成cs文件
        /// </summary>
        /// <param name="outPath"></param>
        /// <param name="nameSpace"></param>
        /// <param name="className"></param>
        /// <param name="fields"></param>
        /// <param name="fileExist"></param>
        public static void PreBuildClass(string outPath, string nameSpace, string className,
            List<Tuple<Type, string, string>> fields, bool fileExist)
        {
            if (fileExist)
            {
            }

            try
            {
                BuildClass(outPath, nameSpace, className, fields);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static void BuildClass(string outPath, string nameSpace, string className,
            List<Tuple<Type, string, string>> fields)
        {
            var nsTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Packages/com.miracle.EditorUIFrame/Editor/Utility/GenUICode/TempleteNamespace.txt");
            string templateNamespace = nsTextAsset.text;
            var classTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Packages/com.miracle.EditorUIFrame/Editor/Utility/GenUICode/TempleteClass.txt");
            string templateClass = classTextAsset.text;
            StringBuilder templateNS = new StringBuilder(templateNamespace);
            StringBuilder templateCS = new StringBuilder(templateClass);
            StringBuilder members = new StringBuilder();
            StringBuilder constructs = new StringBuilder();
            foreach (var (fieldType, fieldName, elementName) in fields)
            {
                members.AppendLine($"\tpublic {fieldType.Name} {fieldName};");
                constructs.AppendLine($"\t\t{fieldName} = Q<{fieldType.Name}>(\"{elementName}\");");
            }

            string fileName = Path.GetFileNameWithoutExtension(outPath).Split('.')[0];
            string uxml = Path.Combine(Path.GetDirectoryName(outPath)!, fileName + ".uxml");
            uxml = uxml.Replace('\\', '/');
            string uss = Path.Combine(Path.GetDirectoryName(outPath)!, fileName + ".uss");
            uss = uss.Replace('\\', '/');
            string ns = string.IsNullOrEmpty(nameSpace)
                ? "@ClassContent"
                : "namespace " + nameSpace + "\n{" + "\n\t@ClassContent" + "\n}";
            templateNS.Replace("@Namespace", ns);
            templateCS.Replace("@Uxml", uxml);
            templateCS.Replace("@Uss", uss);
            templateCS.Replace("@ClassName", className);
            templateCS.Replace("@Members", members.ToString());
            templateCS.Replace("@Constructs", constructs.ToString());
            templateNS.Replace("@ClassContent", templateCS.ToString());

            if (File.Exists(outPath))
            {
                File.Delete(outPath);
            }

            File.WriteAllText(outPath, templateNS.ToString());
            AssetDatabase.Refresh();
            EditorUtility.DisplayProgressBar("自动生成脚本", "正在生成.cs", 100);
            EditorUtility.ClearProgressBar();
        }
    }
}