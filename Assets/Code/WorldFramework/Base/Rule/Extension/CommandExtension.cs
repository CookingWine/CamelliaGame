namespace WorldFramework
{
    /// <summary>
    /// 命令发送接口：标记组件可发送命令
    /// </summary>
    public interface ICanSendCommand:IBelongToArchitecture
    {

    }

    /// <summary>
    /// 命令发送扩展
    /// </summary>
    /// <remarks>为<seealso cref="ICanSendCommand"/>提供发送命令的方法</remarks>
    public static class CommandExtension
    {
        /// <summary>
        /// 发送无参命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new()
        {
            self.GetArchitecture( ).SendCommand(new T( ));
        }

        /// <summary>
        /// 发送指定命令实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="command"></param>
        public static void SendCommand<T>(this ICanSendCommand self , T command) where T : ICommand
        {
            self.GetArchitecture( ).SendCommand(command);
        }

        /// <summary>
        /// 发送有返回值命令
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static TResult SendCommand<TResult>(this ICanSendCommand self , ICommand<TResult> command)
        {
            return self.GetArchitecture( ).SendCommand(command);
        }
    }
}
