using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CamelliaUnityEditor
{
    internal class CamelliaMainEditorWindows:EditorWindow
    {
        /// <summary>
        /// 所有的模块
        /// </summary>
        private readonly List<ICamellieEditorModule> _modules = new List<ICamellieEditorModule>( );
        /// <summary>
        /// 当先绘制的模块
        /// </summary>
        private ICamellieEditorModule _currentModule;
        /// <summary>
        /// 模块列表
        /// </summary>
        private VisualElement _moduleList;
        /// <summary>
        /// 顶部的创建按钮
        /// </summary>
        private VisualElement _moduleHeader;
        /// <summary>
        /// 中间模块内容
        /// </summary>
        private ScrollView _moduleContent;


        /// <summary>
        /// 生成代码的根路径
        /// </summary>
        private const string GenerateRoot = "Assets/Editor/CamelliaUnityEditor/Generate";

        [MenuItem("Camellia/Configs Editor")]
        public static void ShowWindow( )
        {
            var window = GetWindow<CamelliaMainEditorWindows>( );
            window.titleContent = new GUIContent("Camellia Editor");
            window.minSize = new Vector2(700 , 400);
        }

        public void CreateGUI( )
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/CamelliaUnityEditor/CamelliaMainEditorWindows.uxml");
            VisualElement root = visualTree.CloneTree( );
            root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/CamelliaUnityEditor/CamelliaMainEditorWindows.uss"));
            rootVisualElement.Add(root);

            // 获取 UI 元素
            _moduleHeader = root.Q<VisualElement>("module-header");
            _moduleList = root.Q<ScrollView>("module-list");
            _moduleContent = root.Q<ScrollView>("module-content");

            var saveBtn = root.Q<Button>("save-button");

            // 加载模块
            LoadModules( );
            // 构建固定创建按钮
            BuildModuleHeader( );
            //构建模块列表
            BuildModuleList( );
            saveBtn.clicked += ( ) =>
            {
                _currentModule?.OnSave( );
                AssetDatabase.Refresh( );
            };
            if(_modules.Count > 0)
                SwitchModule(_modules[0]);
        }
        private void OnDestroy( )
        {
            foreach(var module in _modules)
            {
                module.OnDestroy( );
            }
            _moduleList.Clear( );
        }

        /// <summary>
        /// 扫描并实例化所有实现 ICamellieEditorModule 的类
        /// </summary>
        private void LoadModules( )
        {
            _modules.Clear( );

            // 确保目录存在
            if(!Directory.Exists(GenerateRoot))
                Directory.CreateDirectory(GenerateRoot);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies( );

            foreach(var asm in assemblies)
            {
                // 只加载编辑器程序集
                if(!asm.FullName.Contains("Editor")) continue;

                Type[] types;
                try { types = asm.GetTypes( ); }
                catch(ReflectionTypeLoadException e) { types = e.Types.Where(t => t != null).ToArray( ); }

                foreach(var type in types)
                {
                    if(typeof(ICamellieEditorModule).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        try
                        {
                            var instance = (ICamellieEditorModule)Activator.CreateInstance(type);
                            instance.OnInitialize( );
                            _modules.Add(instance);
                        }
                        catch(Exception ex)
                        {
                            Debug.LogWarning($"Camellia: 实例化失败 {type.Name}: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void BuildModuleHeader( )
        {
            _moduleHeader.Clear( );

            var createBtn = new Button(( ) => ShowCreateModuleDialog( ))
            {
                text = "+ 创建模块"
            };
            createBtn.style.unityFontStyleAndWeight = FontStyle.Bold;
            createBtn.style.color = Color.white;
            createBtn.style.backgroundColor = new Color(0.25f , 0.45f , 0.8f);
            createBtn.style.flexGrow = 1;
            _moduleHeader.Add(createBtn);
        }

        /// <summary>
        /// 构建模块列表
        /// </summary>
        private void BuildModuleList( )
        {
            _moduleList.Clear( );

            if(_modules.Count == 0)
            {
                var emptyLabel = new Label("未找到模块.");
                emptyLabel.style.color = new Color(0.8f , 0.8f , 0.8f);
                emptyLabel.style.marginTop = 6;
                _moduleList.Add(emptyLabel);
                return;
            }

            foreach(var module in _modules)
            {
                var button = new Button(( ) => SwitchModule(module))
                {
                    text = module.ModuleName
                };
                _moduleList.Add(button);
            }
        }


        /// <summary>
        /// 切换模块
        /// </summary>
        /// <param name="module"></param>
        private void SwitchModule(ICamellieEditorModule module)
        {
            if(_currentModule == module)
                return;

            _currentModule?.OnHide( );
            //切换的时候自动保存一下
            _currentModule?.OnSave( );
            _currentModule = module;
            _currentModule.OnRefresh( );
            _moduleContent.Clear( );
            _moduleContent.Add(_currentModule.CreateContent( ));

            foreach(var child in _moduleList.Children( ))
                child.RemoveFromClassList("active");
            var active = _moduleList.Children( ).FirstOrDefault(e => ( e as Button )?.text == module.ModuleName);
            active?.AddToClassList("active");
        }

        /// <summary>
        /// 显示创建模块弹窗
        /// </summary>
        private void ShowCreateModuleDialog( )
        {
            CreateModulePopup.Show("创建新模块" , moduleName =>
            {
                GenerateModuleFiles(moduleName);
                AssetDatabase.Refresh( );
                EditorUtility.DisplayDialog("成功" , $"模块 \"{moduleName}\" 创建成功!" , "确定");
                RecreateModules( );
            });
        }

        /// <summary>
        /// 重新加载模块
        /// </summary>
        private void RecreateModules( )
        {
            LoadModules( );
            BuildModuleList( );
            if(_modules.Count > 0)
                SwitchModule(_modules.Last( ));
        }

        /// <summary>
        /// 创建模块的文件
        /// </summary>
        /// <param name="moduleName"></param>
        private void GenerateModuleFiles(string moduleName)
        {
            if(string.IsNullOrWhiteSpace(moduleName))
                return;

            string moduleDir = Path.Combine(GenerateRoot , moduleName);
            Directory.CreateDirectory(moduleDir);

            // 创建.cs文件
            string csPath = Path.Combine(moduleDir , $"{moduleName}.cs");
            File.WriteAllText(csPath , GenerateModuleCode(moduleName));

            // 创建.uxml文件
            string uxmlPath = Path.Combine(moduleDir , $"{moduleName}.uxml");
            File.WriteAllText(uxmlPath , GenerateUXML(moduleName));

            // 创建.uss文件
            string ussPath = Path.Combine(moduleDir , $"{moduleName}.uss");
            File.WriteAllText(ussPath , GenerateUSS( ));
        }

        /// <summary>
        /// 生成模块代码
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        private string GenerateModuleCode(string moduleName)
        {
            return
$@"using UnityEngine;
using UnityEngine.UIElements;
namespace CamelliaUnityEditor.Generate
{{
    public class {moduleName} : ICamellieEditorModule
    {{
        public string ModuleName => ""{moduleName}"";

        private VisualElement _RootVisualElement;

        public VisualElement CreateContent()
        {{
            return _RootVisualElement;
        }}

        public void OnInitialize() 
        {{
            if(_RootVisualElement == null)
            {{
                  var visualTree = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(""{GenerateRoot}/{moduleName}/{moduleName}.uxml"");
                  _RootVisualElement = visualTree.CloneTree();
                  _RootVisualElement.styleSheets.Add(UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(""{GenerateRoot}/{moduleName}/{moduleName}.uss""));
            }}
        }}
        public void OnRefresh() 
        {{
            
        }}
        public void OnHide() 
        {{

        }}
         public void OnSave() 
        {{

        }}
        public void OnDestroy() 
        {{

        }}
    }}
}}";
        }

        /// <summary>
        /// 生成uxml内容
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        private string GenerateUXML(string moduleName)
        {
            return
$@"<ui:UXML xmlns:ui=""UnityEngine.UIElements"" xmlns:uie=""UnityEditor.UIElements"">
    <ui:VisualElement class=""root"">
        <ui:Label text=""{moduleName} 模块视图"" class=""header""/>
        <ui:Button text=""你好来自 {moduleName}!""/>
    </ui:VisualElement>
</ui:UXML>";
        }

        /// <summary>
        /// 生成uss内容
        /// </summary>
        /// <returns></returns>
        private string GenerateUSS( )
        {
            return
@".root {
    flex-direction: column;
    padding: 10px;
}

.header {
    font-size: 16px;
    font-weight: bold;
    color: white;
    margin-bottom: 8px;
}";
        }
    }

    /// <summary>
    /// 弹窗，创建模块
    /// </summary>
    internal class CreateModulePopup:EditorWindow
    {
        private string _moduleName = "";
        private Action<string> _onConfirm;

        public static void Show(string title , Action<string> onConfirm)
        {
            var wnd = CreateInstance<CreateModulePopup>( );
            wnd.titleContent = new GUIContent(title);
            wnd.position = new Rect(Screen.width / 2 , Screen.height / 2 , 300 , 100);
            wnd._onConfirm = onConfirm;
            wnd.ShowUtility( );
        }

        private void OnGUI( )
        {
            GUILayout.Label("输入模块名称:" , EditorStyles.boldLabel);
            _moduleName = EditorGUILayout.TextField(_moduleName);

            GUILayout.Space(10);

            if(GUILayout.Button("创建"))
            {
                if(string.IsNullOrEmpty(_moduleName))
                {
                    EditorUtility.DisplayDialog("错误" , "模块名称不能为空!" , "确定");
                    return;
                }

                _onConfirm?.Invoke(_moduleName);
                Close( );
            }
        }
    }
}
