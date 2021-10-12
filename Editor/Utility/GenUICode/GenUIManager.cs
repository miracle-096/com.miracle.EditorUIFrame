using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UIFramework.Editor.Utility.GenUICode;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIFramework.Utility.GenUICode
{
    public class GenUIManager
    {
        /// <summary>
        /// 打开uxml时打开uibuilder同时打开生成代码用的window
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(-1)]
        private static bool OpenGenWindow(int instanceID, int line)
        {
            string path = AssetDatabase.GetAssetPath(instanceID);
            GenUIPanel_window.ParsUxmlPath = path.Replace('\\', '/');
            if (Path.GetExtension(path) == ".uxml")
            {
                GenUIPanel_window uiPanelWindow;
                if (EditorWindow.HasOpenInstances<GenUIPanel_window>())
                {
                    uiPanelWindow = EditorWindow.GetWindow<GenUIPanel_window>();
                    uiPanelWindow.Close();
                }

                uiPanelWindow = ScriptableObject.CreateInstance<GenUIPanel_window>();
                uiPanelWindow.titleContent = new GUIContent("生成UI代码");
                uiPanelWindow.Show();
            }

            return false;
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
            //准备一个代码编译器单元
            CodeCompileUnit unit = new CodeCompileUnit();

            //设置命名空间（这个是指要生成的类的空间）
            CodeNamespace myNamespace = new CodeNamespace(nameSpace);

            //导入必要的命名空间引用
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));
            myNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine"));

            //Code:代码体
            CodeTypeDeclaration myClass = new CodeTypeDeclaration(className);
            //指定为类
            myClass.IsClass = true;
            myClass.IsPartial = true;
            //设置类的访问类型
            myClass.TypeAttributes = TypeAttributes.Public;

            myClass.BaseTypes.Add(typeof(Editor.Core.TUIElement));
            //把这个类放在这个命名空间下
            myNamespace.Types.Add(myClass);
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);


            //添加构造方法
            CodeConstructor constructor = new CodeConstructor();
            constructor.Parameters.Add(
                new CodeParameterDeclarationExpression(typeof(TemplateContainer), "container"));
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("container"));
            constructor.Attributes = MemberAttributes.Public;
            foreach (var (fieldType, fieldName, elementName) in fields)
            {
                //添加字段
                CodeMemberField field = new CodeMemberField(fieldType, fieldName);
                //设置访问类型
                field.Attributes = MemberAttributes.Public;
                //构造方法中添加语句
                constructor.Statements.Add(new CodeExpressionStatement(
                    new CodeSnippetExpression($"{fieldName} = Q<{fieldType}>(\"{elementName}\")")));
                //添加到myClass类中
                myClass.Members.Add(field);
            }

            //将构造方法添加到myClass类中
            myClass.Members.Add(constructor);

            //添加特特性
            string fileName = Path.GetFileNameWithoutExtension(outPath).Split('.')[0];
            string uxml = Path.Combine(Path.GetDirectoryName(outPath)!, fileName + ".uxml");
            string uss = Path.Combine(Path.GetDirectoryName(outPath)!, fileName + ".uss");
            myClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(UIAttribute)),
                new CodeAttributeArgument("Uxml", new CodeSnippetExpression($"\"{uxml}\"")),
                new CodeAttributeArgument("Uss", new CodeSnippetExpression($"\"{uss}\""))));

            //生成C#脚本
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            //代码风格:大括号的样式{}
            options.BracingStyle = "C";
            //是否在字段、属性、方法之间添加空白行
            options.BlankLinesBetweenMembers = true;
            //保存
            using (StreamWriter sw = new StreamWriter(outPath))
            {
                //为指定的代码文档对象模型(CodeDOM) 编译单元生成代码并将其发送到指定的文本编写器，使用指定的选项。(官方解释)
                //将自定义代码编译器(代码内容)、和代码格式写入到sw中
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }

            AssetDatabase.Refresh();
            EditorUtility.DisplayProgressBar("自动生成脚本", "正在生成.cs", 100);
            EditorUtility.ClearProgressBar();
        }
    }
}