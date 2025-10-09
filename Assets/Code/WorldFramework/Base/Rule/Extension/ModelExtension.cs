namespace WorldFramework
{
    /// <summary>
    /// 访问模块接口
    /// </summary>
    /// <remarks>标记可访问模块</remarks>
    public interface ICanGetModel:IBelongToArchitecture
    {

    }

    /// <summary>
    /// 模块访问的扩展类
    /// </summary>
    /// <remarks>为<seealso cref="ICanGetModel"/>提供获取模块的方法</remarks>
    public static class ModelExtension
    {
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <returns>模块</returns>
        public static T GetModel<T>(this ICanGetModel self) where T : class, IModel
        {
            return self.GetArchitecture( ).GetModel<T>( );
        }
    }
}
