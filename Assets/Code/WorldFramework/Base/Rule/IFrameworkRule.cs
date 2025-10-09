namespace WorldFramework
{
    /// <summary>
    /// 获取架构接口
    /// </summary>
    /// <remarks>用来获取归属的架构内与<seealso cref="ICanSetArchitecture"/>形成对应关系</remarks>
    public interface IBelongToArchitecture
    {
        /// <summary>
        /// 获取架构
        /// </summary>
        /// <returns>所属的架构</returns>
        IArchitecture GetArchitecture( );
    }

    /// <summary>
    /// 设置架构接口
    /// </summary>
    /// <remarks>用来设置归属于哪个架构内与<seealso cref="IBelongToArchitecture"/>形式对应关系</remarks>
    public interface ICanSetArchitecture
    {
        /// <summary>
        /// 设置所属架构
        /// </summary>
        /// <param name="architecture">架构</param>
        void SetArchitecture(IArchitecture architecture);
    }

    /// <summary>
    /// 初始化接口
    /// </summary>
    /// <remarks>继承于接口标记着可以进行初始化以及销毁</remarks>
    public interface ICanInit
    {
        /// <summary>
        /// 标记初始化状态
        /// </summary>
        bool Initialized { get; set; }

        /// <summary>
        /// 初始化创建时操作
        /// </summary>
        void Init();

        /// <summary>
        /// 销毁前的清理操作
        /// </summary>
        void Deinit( );
    }
}
