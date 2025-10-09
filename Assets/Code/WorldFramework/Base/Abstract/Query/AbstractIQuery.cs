namespace WorldFramework
{
    /// <summary>
    /// 查询抽象类
    /// </summary>
    public abstract class AbstractIQuery<T>:IQuery<T>
    {
        /// <summary>
        /// 所属架构
        /// </summary>
        private IArchitecture mArchitecture;

        /// <summary>
        /// 执行入口：调用子类的OnDo并返回结果
        /// </summary>
        /// <returns></returns>
        public T Do( )
        {
            return OnDo( );
        }

        /// <summary>
        /// 抽象方法：子类需实现具体的查询逻辑
        /// </summary>
        /// <returns></returns>
        protected abstract T OnDo( );

        /// <summary>
        ///  获取所属架构
        /// </summary>
        /// <returns></returns>
        public IArchitecture GetArchitecture( ) => mArchitecture;

        /// <summary>
        /// 设置所属架构
        /// </summary>
        /// <param name="architecture"></param>
        public void SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
    }
}
