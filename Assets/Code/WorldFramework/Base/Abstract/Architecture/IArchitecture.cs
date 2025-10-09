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
        /// 发送无返回值的命令
        /// </summary>
        /// <param name="command">命令</param>
        void SendCommand<T>(T command) where T : ICommand;

        /// <summary>
        /// 发送带返回值的命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>结果</returns>
        TResult SendCommand<TResult>(ICommand<TResult> command);

        /// <summary>
        /// 发送查询（有返回值）
        /// </summary>
        /// <returns></returns>
        TResult SendQuery<TResult>(IQuery<TResult> query);

        /// <summary>
        /// 获取模块组件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>具体模块</returns>
        T GetModel<T>( ) where T : class, IModel;
    }
}
