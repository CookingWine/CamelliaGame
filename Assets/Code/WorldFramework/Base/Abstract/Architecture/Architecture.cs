namespace WorldFramework
{
    /// <summary>
    /// 架构抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T>:IArchitecture where T : Architecture<T>, new()
    {

    }
}
