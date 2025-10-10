using System;

namespace CamelliaGame.Runtime
{
    public sealed partial class Global
    {
        /// <summary>
        /// 路径相关
        /// </summary>
        public sealed class Path
        {
            /// <summary>
            /// 获取和设置当前目录（即该进程从中启动的目录）的完全限定路径
            /// </summary>
            public static string CurrentDirectory
            {
                get
                {
                    return Environment.CurrentDirectory;
                }
            }
        }
    }
}
