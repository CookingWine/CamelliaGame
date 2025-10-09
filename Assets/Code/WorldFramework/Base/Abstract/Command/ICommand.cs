namespace WorldFramework
{
    /// <summary>
    /// 无返回值命令接口
    /// </summary>
    /// <remarks>定义命令的能力</remarks>
    public interface ICommand:IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanSendCommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        void Execute( );
    }

    /// <summary>
    /// 有返回值命令接口
    /// </summary>
    /// <remarks>继承<seealso cref="ICommand"/>的能力，增加返回值</remarks>
    public interface ICommand<TResult>:IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanSendCommand
    {
        /// <summary>
        /// 执行命令并返回结果
        /// </summary>
        /// <returns>结果</returns>
        TResult Execute( );
    }
}
