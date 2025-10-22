namespace WorldFramework
{
    /// <summary>
    /// 游戏框架的入口
    /// </summary>
    public sealed class WorldFrameworkEntry
    {
        private WorldFrameworkEntry( ) { }

        /// <summary>
        /// 实例
        /// </summary>
        private static WorldFrameworkEntry mWorldFramework;

        /// <summary>
        /// 实例
        /// </summary>
        public static WorldFrameworkEntry Instance
        {
            get
            {
                if(null == mWorldFramework)
                {
                    mWorldFramework = new WorldFrameworkEntry( );
                }
                return mWorldFramework;
            }
        }

        /// <summary>
        /// 所有游戏框架模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void OnUpdate(float elapseSeconds , float realElapseSeconds)
        {

        }

        /// <summary>
        /// 关闭并清理所有游戏框架模块
        /// </summary>
        public void OnShutdown( )
        {

        }

    }
}
