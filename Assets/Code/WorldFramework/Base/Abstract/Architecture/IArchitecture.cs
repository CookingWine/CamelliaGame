namespace WorldFramework
{
    /// <summary>
    /// 架构
    /// </summary>
    public interface IArchitecture
    {
        /// <summary>
        /// 销毁框架【释放资源】
        /// </summary>
        void DeInit( );

        /// <summary>
        /// 注册模块组件
        /// </summary>
        /// <param name="model">模块</param>
        void RegisterModel<T>(T model) where T : IModel;


        /// <summary>
        /// 获取模块组件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>具体模块</returns>
        T GetModel<T>( ) where T : class, IModel;
    }
}
