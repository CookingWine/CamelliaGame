namespace WorldFramework
{
    /// <summary>
    /// 不带返回值命令的抽象类
    /// </summary>
    public abstract class AbstractCommand:ICommand
    {
        /// <summary>
        /// 所归属的架构
        /// </summary>
        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture( )
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        void ICommand.Execute( )
        {
            OnExecute( );
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        protected abstract void OnExecute( );
    }

    /// <summary>
    /// 带返回值命令的抽象类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class AbstractCommand<TResult>:ICommand<TResult>
    {
        /// <summary>
        /// 所归属的架构
        /// </summary>
        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture( )
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        TResult ICommand<TResult>.Execute( )
        {
            return OnExecute( );
        }
        /// <summary>
        /// 执行命令并返回结果
        /// </summary>
        /// <returns>结果</returns>
        protected abstract TResult OnExecute( );
    }
}
