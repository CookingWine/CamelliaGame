using System;
using System.Runtime.InteropServices;

namespace WorldFramework
{
    /// <summary>
    /// 引用池信息
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ReferencePoolInfo
    {
        /// <summary>
        /// 引用池类型
        /// </summary>
        private readonly Type mType;
        /// <summary>
        /// 未使用引用数量
        /// </summary>
        private readonly int mUnusedReferenceCount;
        /// <summary>
        /// 正在使用引用数量
        /// </summary>
        private readonly int mUsingReferenceCount;
        /// <summary>
        /// 获取引用数量
        /// </summary>
        private readonly int mAcquireReferenceCount;
        /// <summary>
        /// 归还引用数量
        /// </summary>
        private readonly int mReleaseReferenceCount;
        /// <summary>
        /// 增加引用数量
        /// </summary>
        private readonly int mAddReferenceCount;
        /// <summary>
        /// 移除引用数量
        /// </summary>
        private readonly int mRemoveReferenceCount;

        /// <summary>
        /// 初始化引用池信息的新实例。
        /// </summary>
        /// <param name="type">引用池类型。</param>
        /// <param name="unusedReferenceCount">未使用引用数量。</param>
        /// <param name="usingReferenceCount">正在使用引用数量。</param>
        /// <param name="acquireReferenceCount">获取引用数量。</param>
        /// <param name="releaseReferenceCount">归还引用数量。</param>
        /// <param name="addReferenceCount">增加引用数量。</param>
        /// <param name="removeReferenceCount">移除引用数量。</param>
        public ReferencePoolInfo(Type type , int unusedReferenceCount , int usingReferenceCount , int acquireReferenceCount , int releaseReferenceCount , int addReferenceCount , int removeReferenceCount)
        {
            mType = type;
            mUnusedReferenceCount = unusedReferenceCount;
            mUsingReferenceCount = usingReferenceCount;
            mAcquireReferenceCount = acquireReferenceCount;
            mReleaseReferenceCount = releaseReferenceCount;
            mAddReferenceCount = addReferenceCount;
            mRemoveReferenceCount = removeReferenceCount;
        }

        /// <summary>
        /// 引用池类型
        /// </summary>
        public readonly Type PoolType
        {
            get
            {
                return mType;
            }
        }

        /// <summary>
        /// 获取未使用引用数量。
        /// </summary>
        public readonly int UnusedReferenceCount
        {
            get
            {
                return mUnusedReferenceCount;
            }
        }

        /// <summary>
        /// 获取正在使用引用数量。
        /// </summary>
        public readonly int UsingReferenceCount
        {
            get
            {
                return mUsingReferenceCount;
            }
        }

        /// <summary>
        /// 获取获取引用数量。
        /// </summary>
        public readonly int AcquireReferenceCount
        {
            get
            {
                return mAcquireReferenceCount;
            }
        }

        /// <summary>
        /// 获取归还引用数量。
        /// </summary>
        public readonly int ReleaseReferenceCount
        {
            get
            {
                return mReleaseReferenceCount;
            }
        }

        /// <summary>
        /// 获取增加引用数量。
        /// </summary>
        public readonly int AddReferenceCount
        {
            get
            {
                return mAddReferenceCount;
            }
        }

        /// <summary>
        /// 获取移除引用数量。
        /// </summary>
        public readonly int RemoveReferenceCount
        {
            get
            {
                return mRemoveReferenceCount;
            }
        }
    }
}
