using System;
using System.Linq;
using GameFramework.GameUtility;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace GameFramework.GameException
{
    [Serializable]
    public class GameFrameworkException:Exception, IGameException
    {
        public int ErrorCode { get; private set; }
        public ExceptionSeverity Severity { get; private set; }
        public bool CanRetry { get; private set; }

        /// <summary>
        /// 内部使用可变字典，外部只读暴露
        /// </summary>
        private readonly Dictionary<string , string> _context = new Dictionary<string , string>( );
        public IReadOnlyDictionary<string , string> Context => _context;

        public GameFrameworkException(int errorCode , string message = null , ExceptionSeverity severity = ExceptionSeverity.Error , bool canRetry = false , Exception inner = null)
            : base(message ?? Text.Format("GameException {{0}}" , errorCode) , inner)
        {
            ErrorCode = errorCode;
            Severity = severity;
            CanRetry = canRetry;
        }

        // 支持序列化（可选）
        protected GameFrameworkException(SerializationInfo info , StreamingContext context) : base(info , context)
        {
            ErrorCode = info.GetInt32(nameof(ErrorCode));
            Severity = (ExceptionSeverity)info.GetInt32(nameof(Severity));
            CanRetry = info.GetBoolean(nameof(CanRetry));

            // 恢复 context（若有）
            try
            {
                var ctx = (KeyValuePair<string , string>[])info.GetValue("ContextPairs" , typeof(KeyValuePair<string , string>[]));
                if(ctx != null)
                {
                    foreach(var kv in ctx) _context[kv.Key] = kv.Value;
                }
            }
            catch { /* 忽略 */ }
        }

        public void AddContext(string key , string value)
        {
            if(string.IsNullOrEmpty(key)) return;
            _context[key] = value;
        }

        // 支持序列化数据写入
        public override void GetObjectData(SerializationInfo info , StreamingContext context)
        {
            base.GetObjectData(info , context);
            info.AddValue(nameof(ErrorCode) , ErrorCode);
            info.AddValue(nameof(Severity) , (int)Severity);
            info.AddValue(nameof(CanRetry) , CanRetry);

            var pairs = _context.ToArray( );
            info.AddValue("ContextPairs" , pairs , typeof(KeyValuePair<string , string>[]));
        }

        public override string ToString( )
        {
            // 简洁的输出：包含错误码与严重级别
            return Text.Format("[GameException] Code={0},Severity={1},Message={2},{3}" , ErrorCode , Severity , Message , base.ToString( ));
        }
    }
}
