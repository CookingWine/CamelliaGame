namespace GameFramework.GameException
{
    /// <summary>
    /// 异常等级
    /// </summary>
    public enum ExceptionSeverity:byte
    {
        ///<summary>一般</summary>
        Info = 0,
        ///<summary>警告</summary>
        Warning,
        ///<summary>错误</summary>
        Error,
        ///<summary>严重</summary>
        Critical
    }
}
