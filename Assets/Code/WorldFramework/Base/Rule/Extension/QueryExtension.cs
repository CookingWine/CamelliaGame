namespace WorldFramework
{
    /// <summary>
    ///  查询发送接口：标记组件可发送查询
    /// </summary>
    public interface ICanSendQuery:IBelongToArchitecture
    {

    }

    /// <summary>
    /// 查询发送扩展
    /// </summary>
    /// <remarks></remarks>
    public static class QueryExtension
    {
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <returns>结果</returns>
        public static TResult SendQuery<TResult>(this ICanSendQuery self , IQuery<TResult> query)
        {
            return self.GetArchitecture( ).SendQuery(query);
        }
    }
}
