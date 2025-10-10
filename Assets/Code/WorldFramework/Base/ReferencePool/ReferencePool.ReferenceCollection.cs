using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace WorldFramework
{
    public static partial class ReferencePool
    {
        /// <summary>
        /// 引用收集器
        /// </summary>
        private sealed class ReferenceCollection
        {
            /// <summary>
            /// 引用队列
            /// </summary>
            private readonly Queue<IReference> mReferences;
            /// <summary>
            /// 引用类型
            /// </summary>
            private readonly Type mReferenceType;
            /// <summary>
            /// 正使用的引用数量
            /// </summary>
            private int mUsingReferenceCount;
            /// <summary>
            /// 获取的引用数量
            /// </summary>
            private int mAcquireReferenceCount;
            /// <summary>
            /// 回收的引用数量
            /// </summary>
            private int mReleaseReferenceCount;
            /// <summary>
            /// 添加的引用数量
            /// </summary>
            private int mAddReferenceCount;
            /// <summary>
            /// 移除的引用数量
            /// </summary>
            private int mRemoveReferenceCount;

            /// <summary>
            /// 引用收集器
            /// </summary>
            /// <param name="referenceType">引用类型</param>
            public ReferenceCollection(Type referenceType)
            {
                mReferences = new Queue<IReference>( );
                mReferenceType = referenceType;
                mUsingReferenceCount = 0;
                mAcquireReferenceCount = 0;
                mReleaseReferenceCount = 0;
                mAddReferenceCount = 0;
                mRemoveReferenceCount = 0;
            }
            /// <summary>
            /// 引用类型
            /// </summary>
            public Type ReferenceType
            {
                get
                {
                    return mReferenceType;
                }
            }
            /// <summary>
            /// 未使用引用数量
            /// </summary>
            public int UnusedReferenceCount
            {
                get
                {
                    return mReferences.Count;
                }
            }
            /// <summary>
            /// 正使用的引用数量
            /// </summary>
            public int UsingReferenceCount
            {
                get
                {
                    return mUsingReferenceCount;
                }
            }
            /// <summary>
            /// 获取的引用数量
            /// </summary>
            public int AcquireReferenceCount
            {
                get
                {
                    return mAcquireReferenceCount;
                }
            }
            /// <summary>
            /// 回收的引用数量
            /// </summary>
            public int ReleaseReferenceCount
            {
                get
                {
                    return mReleaseReferenceCount;
                }
            }
            /// <summary>
            /// 添加的引用数量
            /// </summary>
            public int AddReferenceCount
            {
                get
                {
                    return mAddReferenceCount;
                }
            }
            /// <summary>
            /// 移除的引用数量
            /// </summary>
            public int RemoveReferenceCount
            {
                get
                {
                    return mRemoveReferenceCount;
                }
            }

            public T Acquire<T>( ) where T : class, IReference, new()
            {
                if(typeof(T) != mReferenceType)
                {
                    throw new Exception("Type is invalid.");
                }

                mUsingReferenceCount++;
                mAcquireReferenceCount++;
                lock(mReferences)
                {
                    if(mReferences.Count > 0)
                    {
                        return (T)mReferences.Dequeue( );
                    }
                }

                mAddReferenceCount++;
                return new T( );
            }
          
            public IReference Acquire( )
            {
                mUsingReferenceCount++;
                mAcquireReferenceCount++;
                lock(mReferences)
                {
                    if(mReferences.Count > 0)
                    {
                        return mReferences.Dequeue( );
                    }
                }

                mAddReferenceCount++;
                return (IReference)Activator.CreateInstance(mReferenceType);
            }

            public void Release(IReference reference)
            {
                reference.Clear( );
                lock(mReferences)
                {
                    if(mEnableStrictCheck && mReferences.Contains(reference))
                    {
                        throw new Exception("The reference has been released.");
                    }

                    mReferences.Enqueue(reference);
                }

                mReleaseReferenceCount++;
                mUsingReferenceCount--;
            }

            public void Add<T>(int count) where T : class, IReference, new()
            {
                if(typeof(T) != mReferenceType)
                {
                    throw new Exception("Type is invalid.");
                }

                lock(mReferences)
                {
                    mAddReferenceCount += count;
                    while(count-- > 0)
                    {
                        mReferences.Enqueue(new T( ));
                    }
                }
            }

            public void Add(int count)
            {
                lock(mReferences)
                {
                    mAddReferenceCount += count;
                    while(count-- > 0)
                    {
                        mReferences.Enqueue((IReference)Activator.CreateInstance(mReferenceType));
                    }
                }
            }

            public void Remove(int count)
            {
                lock(mReferences)
                {
                    if(count > mReferences.Count)
                    {
                        count = mReferences.Count;
                    }

                    mRemoveReferenceCount += count;
                    while(count-- > 0)
                    {
                        mReferences.Dequeue( );
                    }
                }
            }

            public void RemoveAll( )
            {
                lock(mReferences)
                {
                    mRemoveReferenceCount += mReferences.Count;
                    mReferences.Clear( );
                }
            }
        }
    }
}
