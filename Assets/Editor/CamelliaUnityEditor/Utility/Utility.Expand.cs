using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace CamelliaUnityEditor
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExpandUtility
    {
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="field"></param>
        ///<remarks>禁止输入并且灰掉输入框但保留内容显示</remarks>
        public static void SetTextFieldAttribute(this TextField field)
        {
            //禁止输入
            field.isReadOnly = true;
            //灰掉输入框但保留内容显示
            field.SetEnabled(false);
        }

      
        /// <summary>
        ///按钮绑定事件
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public static void ButtonBuildTouchData(this VisualElement root , string name , Action action)
        {
            root.Q<Button>(name).clicked -= action;
            root.Q<Button>(name).clicked += action;
        }

        /// <summary>
        /// 创建VisualElement数据
        /// </summary>
        /// <param name="visualTreeAsset">visualTree数据文件</param>
        /// <param name="styleSheet">style文件</param>
        /// <returns>VisualElement</returns>
        public static VisualElement CreatorVisualElement(string visualTreeAsset , string styleSheet)
        {
            if(!File.Exists(visualTreeAsset) || !File.Exists(styleSheet))
            {
                throw new Exception("未能加载数据文件");
            }
            try
            {
                var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(visualTreeAsset);
                VisualElement root = visualTree.CloneTree( );
                var style = AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheet);
                root.styleSheets.Add(style);
                return root;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
