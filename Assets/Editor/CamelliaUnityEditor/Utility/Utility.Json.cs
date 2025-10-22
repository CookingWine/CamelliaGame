using System.IO;
using UnityEngine;

namespace CamelliaUnityEditor
{
    public static partial class Utility
    {
        /// <summary>
        /// json读写
        /// </summary>
        /// <remarks>使用的是<see cref="JsonUtility"/>进行读写</remarks>
        public static class Json
        {
            /// <summary>
            /// 加载json数据
            /// </summary>
            /// <param name="path">路径</param>
            /// <returns>数据</returns>
            public static T LoadJsonData<T>(string path) where T : class, new()
            {
                if(!File.Exists(path))
                {
                    return new T( );
                }
                return JsonUtility.FromJson<T>(File.ReadAllText(path));
            }

            /// <summary>
            /// 保存json数据
            /// </summary>
            /// <param name="data"></param>
            /// <param name="path"></param>
            /// <exception cref="System.Exception">数据为空</exception>
            public static void SaveJsonData<T>(T data , string path)
            {
                if(data == null)
                {
                    throw new System.Exception($"name({typeof(T).Name})为空");
                }
                if(File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllText(path , JsonUtility.ToJson(data));
            }
        }
    }
}
