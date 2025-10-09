using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldFramework
{
    /// <summary>
    /// ico容器
    /// </summary>
    public class IOCContainer
    {
        /// <summary>
        /// 缓存类型映射
        /// </summary>
        private readonly Dictionary<Type , object> cacheMapping = new Dictionary<Type , object>( );

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="instance">类型</param>
        public void Register<T>(T instance)
        {
            var key = typeof(T);
            if(cacheMapping.ContainsKey(key))
            {
                cacheMapping[key] = instance;
            }
            else
            {
                cacheMapping.Add(key , instance);
            }
        }

        /// <summary>
        /// 获取缓存的类型
        /// </summary>
        /// <returns>具体的类型,如果未找到该类型则返回空</returns>
        public T Get<T>( ) where T : class
        {
            if(cacheMapping.TryGetValue(typeof(T) , out var instance))
            {
                return instance as T;
            }
            return null;
        }

        /// <summary>
        /// 清除映射缓存
        /// </summary>
        public void Clear( )
        {
            cacheMapping.Clear( );
        }

        /// <summary>
        /// 获取指定类型
        /// </summary>
        /// <returns>类型</returns>
        public IEnumerable<T> GetInstancesByType<T>( )
        {
            return cacheMapping.Values.Where(instance => typeof(T).IsInstanceOfType(instance)).Cast<T>( );
        }
    }
}
