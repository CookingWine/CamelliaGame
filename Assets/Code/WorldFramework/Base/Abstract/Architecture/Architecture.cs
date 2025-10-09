using System;
using System.Linq;

namespace WorldFramework
{
    /// <summary>
    /// 架构抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T>:IArchitecture where T : Architecture<T>, new()
    {
        /// <summary>
        /// 初始化状态标记
        /// </summary>
        private bool mInitialize = false;

        /// <summary>
        /// ioc容器
        /// </summary>
        protected IOCContainer mContainer = new IOCContainer( );

        /// <summary>
        /// 实例
        /// </summary>
        protected static T mArchitecture;

        /// <summary>
        /// 实例
        /// </summary>
        public static IArchitecture Instance
        {
            get
            {
                if(mArchitecture == null)
                {
                    InitializeArchitecture( );
                }
                return mArchitecture;
            }
        }

        /// <summary>
        /// 注册补丁
        /// </summary>
        /// <remarks>用于在初始化时扩展注册逻辑</remarks>
        public static Action<T> OnRegisterPatch = architecture => { };

        /// <summary>
        /// 初始化架构
        /// </summary>
        public static void InitializeArchitecture( )
        {
            if(mArchitecture == null)
            {
                mArchitecture = new T( );
                //初始化
                mArchitecture.OnInit( );

                //执行注册补丁
                OnRegisterPatch?.Invoke(mArchitecture);

                foreach(var model in mArchitecture.mContainer.GetInstancesByType<IModel>( ).Where(m => !m.Initialized))
                {
                    model.Init( );
                    model.Initialized = true;
                }

                mArchitecture.mInitialize = true;
            }
        }
        public void RegisterModel<TModel>(TModel model) where TModel : IModel
        {
            model.SetArchitecture(this);
            mContainer.Register(model);
            if(mInitialize)
            {
                model.Init( );
                model.Initialized = true;
            }
        }

        public TModel GetModel<TModel>( ) where TModel : class, IModel
        {
            return mContainer.Get<TModel>( );
        }

        public void DeInit( )
        {
            OnDeinit( );

            //释放掉所有的模块
            foreach(var mode in mContainer.GetInstancesByType<IModel>( ).Where(m => m.Initialized))
            {
                mode.Deinit( );
            }
            //释放掉ioc容器
            mContainer.Clear( );

            //释放单例
            mArchitecture = null;
        }



        #region 子类实现方法

        /// <summary>
        /// 初始化架构
        /// </summary>
        /// <remarks>子类需实现具体的注册逻辑</remarks>
        protected abstract void OnInit( );

        /// <summary>
        /// 销毁架构
        /// </summary>
        /// <remarks>子类按需实现销毁逻辑</remarks>
        protected virtual void OnDeinit( ) { }

        #endregion
    }
}
