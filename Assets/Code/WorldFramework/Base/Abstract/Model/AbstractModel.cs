namespace WorldFramework
{
    /// <summary>
    /// 模块抽象类
    /// </summary>
    /// <remarks>实现<seealso cref="IModel"/>接口</remarks>
    public abstract class AbstractModel:IModel
    {
        private IArchitecture mArchitecture;

        public bool Initialized { get; set; }

        public IArchitecture GetArchitecture( )
        {
            return mArchitecture;
        }

        public void SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void ICanInit.Init( )
        {
            OnInit( );
        }

        public void Deinit( )
        {
            OnDeinit( );
        }
        /// <summary>
        /// 初始化执行
        /// </summary>
        protected abstract void OnInit( );
        
        /// <summary>
        /// 销毁时执行
        /// </summary>
        protected virtual void OnDeinit( ) { }
    }
}
