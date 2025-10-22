using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace CamelliaUnityEditor.Generate
{
    public class LubanConfigs:ICamellieEditorModule
    {
        public string ModuleName => "LubanConfigs";

        private VisualElement _RootVisualElement;

        /// <summary>
        /// 配置数据
        /// </summary>
        private LubanConfigsData _ConfigsData;

        /// <summary>
        /// 配置json生成的位置
        /// </summary>
        /// <remarks>这里可以根据环境生成不同的地方</remarks>
        private string CONFIGS_PATH
        {
            get
            {
                return "Assets/Editor/CamelliaUnityEditor/GenerateConfigs~/LubanConfigs.json";
            }
        }

        /// <summary>
        /// 样式的uxml文件
        /// </summary>
        private const string VISUAL_TREE_ASSET_PATH = "Assets/Editor/CamelliaUnityEditor/Generate/LubanConfigs/LubanConfigs.uxml";
        /// <summary>
        /// 样式的uss文件
        /// </summary>
        private const string STYLE_SHEET_PATH = "Assets/Editor/CamelliaUnityEditor/Generate/LubanConfigs/LubanConfigs.uss";
        /// <summary>
        /// 数据表所在位置
        /// </summary>
        private TextField _dataTablePathField;
        /// <summary>
        /// luban.dll所在位置
        /// </summary>
        private TextField _LubanDllPath;

        /// <summary>
        /// 生成数据文件的位置
        /// </summary>
        private TextField _OutputDataDirGeneratePathFieId;

        /// <summary>
        /// luban数据表生成配置
        /// </summary>
        private class LubanConfigsData
        {
            /// <summary>
            /// 数据表存放的位置
            /// </summary>
            public string DataTablePath;

            /// <summary>
            /// Luban的dll文件存放位置
            /// </summary>
            public string LuBanDllPath;

            /// <summary>
            /// 输出的数据表位置
            /// </summary>
            public string OutputDataDir;

            /// <summary>
            /// 生成的代码位置
            /// </summary>
            public string OutputCodeDir;

        }

        public VisualElement CreateContent( )
        {
            return _RootVisualElement;
        }

        public void OnInitialize( )
        {
            if(_RootVisualElement == null)
            {
                _RootVisualElement = ExpandUtility.CreatorVisualElement(VISUAL_TREE_ASSET_PATH , STYLE_SHEET_PATH);

                //加载Json数据
                OnLoadConfig( );

                //绑定
                BuildViewData(_RootVisualElement);
                //
                OnRefresh( );
            }
        }
        public void OnRefresh( )
        {
            //显示路径并禁止输入
            _dataTablePathField.value = _ConfigsData.DataTablePath;
            _dataTablePathField.SetTextFieldAttribute( );

            //绘制dll文件路径
            _LubanDllPath.value = _ConfigsData.LuBanDllPath;
            _LubanDllPath.SetTextFieldAttribute( );

            //绘制输出文件
            _OutputDataDirGeneratePathFieId.value = _ConfigsData.OutputDataDir;
            _OutputDataDirGeneratePathFieId.SetTextFieldAttribute( );

        }
        public void OnHide( )
        {

        }
        public void OnSave( )
        {
            Utility.Json.SaveJsonData(_ConfigsData , CONFIGS_PATH);
        }
        public void OnDestroy( )
        {
            _RootVisualElement = null;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="root"></param>
        private void BuildViewData(VisualElement root)
        {
            //绘制数据表文件路径
            _dataTablePathField = root.Q<TextField>("data-table-path");
            //数据表路径选择按钮
            root.ButtonBuildTouchData("select-file-button" , ( ) =>
            {
                _dataTablePathField.value = _ConfigsData.DataTablePath = GetSelectPath( );
            });
            //Luban.dll文件路径
            _LubanDllPath = root.Q<TextField>("LubanDllDirPath");
            root.ButtonBuildTouchData("LubanDllDirPath-button" , ( ) =>
            {
                _LubanDllPath.value = _ConfigsData.LuBanDllPath = GetSelectPath( );
            });

            //生成的数据路径
            _OutputDataDirGeneratePathFieId = root.Q<TextField>("OutputDataDirGeneratePathFieIdText");
            root.ButtonBuildTouchData("OutputDataDirGeneratePathFieIdButton" , ( ) =>
            {
                _OutputDataDirGeneratePathFieId.value = _ConfigsData.OutputDataDir = GetSelectPath( );
            });


        }

        /// <summary>
        /// 获取选择的路径
        /// </summary>
        /// <returns></returns>
        private string GetSelectPath( )
        {
            // 打开文件夹选择窗口，返回绝对路径
            string fullPath = EditorUtility.OpenFolderPanel("选择Luban数据表路径" , Application.dataPath , "");
            return fullPath;

            //string selectedPath = EditorUtility.OpenFolderPanel("选择路径" , "Assets/" , "");
            //if(!string.IsNullOrEmpty(selectedPath))
            //{
            //    // 转为相对路径
            //    if(selectedPath.StartsWith(Application.dataPath))
            //    {
            //        selectedPath = "Assets" + selectedPath[Application.dataPath.Length..];
            //    }
            //}
            //else
            //{
            //    selectedPath = Application.dataPath;
            //}
            //return selectedPath;
        }

        /// <summary>
        /// 加载json数据
        /// </summary>
        private void OnLoadConfig( )
        {
            _ConfigsData = Utility.Json.LoadJsonData<LubanConfigsData>(CONFIGS_PATH);
        }
    }
}
