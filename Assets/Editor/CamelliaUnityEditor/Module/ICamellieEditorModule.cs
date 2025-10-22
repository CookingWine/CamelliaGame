using UnityEngine.UIElements;

namespace CamelliaUnityEditor
{
    public interface ICamellieEditorModule
    {
        /// <summary>
        /// 模块名称（用于Tab和左侧列表显示）
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// 构建模块主内容
        /// </summary>
        VisualElement CreateContent( );

        /// <summary>
        /// 模块初始化逻辑
        /// </summary>
        void OnInitialize( );

        /// <summary>
        /// 模块绘制刷新逻辑
        /// </summary>
        void OnRefresh( );

        /// <summary>
        /// 模块隐藏
        /// </summary>
        void OnHide( );

        /// <summary>
        /// 模块数据保存
        /// </summary>
        void OnSave( );

        /// <summary>
        /// 模块销毁逻辑
        /// </summary>
        void OnDestroy( );
    }
}
