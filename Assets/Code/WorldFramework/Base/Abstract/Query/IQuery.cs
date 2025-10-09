namespace WorldFramework
{
    /// <summary>
    /// 查询接口：定义查询的能力
    /// </summary>
    public interface IQuery<TResult>:IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanSendQuery
    {
        /// <summary>
        ///  执行查询并返回结果
        /// </summary>
        /// <returns>结果</returns>
        TResult Do( );
    }
}
