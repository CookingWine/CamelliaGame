using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CamelliaEditor
{
    /// <summary>
    /// 跨平台命令行工具类，用于在Unity编辑器中执行外部命令行指令
    /// 支持Windows、macOS、Linux系统，自动处理不同平台的命令行差异
    /// </summary>
    public static class ShellHelper
    {
        /// <summary>
        /// 执行外部命令行指令
        /// </summary>
        /// <param name="cmd">要执行的命令字符串（如"echo hello"）</param>
        /// <param name="workDirectory">命令执行的工作目录路径</param>
        /// <param name="environmentVars">可选，需要临时添加到PATH环境变量的路径列表（用于解决命令依赖）</param>
        public static void Run(string cmd , string workDirectory , List<string> environmentVars = null)
        {
            // 创建进程实例，用于执行外部命令
            Process process = new( );
            try
            {
                // 根据当前Unity编辑器运行的操作系统，配置对应的命令行外壳程序
#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
                // macOS和Linux系统使用bash作为命令行外壳
                string app = "bash";
                // 类Unix系统的环境变量路径分隔符为冒号
                string splitChar = ":";
                // bash执行字符串命令的参数
                string arguments = "-c";
#elif UNITY_EDITOR_WIN
                // Windows系统使用cmd.exe作为命令行外壳
                string app = "cmd.exe";
                // Windows系统的环境变量路径分隔符为分号
                string splitChar = ";";
                // cmd执行字符串命令的参数
                string arguments = "/c";
#endif

                // 配置进程启动信息
                ProcessStartInfo start = new ProcessStartInfo(app);

                // 如果有需要追加的环境变量，将其添加到PATH中
                if(environmentVars != null)
                {
                    foreach(string var in environmentVars)
                    {
                        start.EnvironmentVariables["PATH"] += ( splitChar + var );
                    }
                }

                // 关联进程启动信息到进程实例
                process.StartInfo = start;
                // 拼接命令参数（外壳程序参数 + 要执行的命令，命令用引号包裹避免空格问题）
                start.Arguments = arguments + " \"" + cmd + "\"";
                // 不显示命令行窗口
                start.CreateNoWindow = true;
                // 进程启动失败时显示错误对话框
                start.ErrorDialog = true;
                // 禁用操作系统外壳程序（允许重定向输入输出流）
                start.UseShellExecute = false;
                // 设置命令执行的工作目录
                start.WorkingDirectory = workDirectory;

                // 根据UseShellExecute配置流重定向（当不使用外壳程序时才支持重定向）
                if(start.UseShellExecute)
                {
                    start.RedirectStandardOutput = false;
                    start.RedirectStandardError = false;
                    start.RedirectStandardInput = false;
                }
                else
                {
                    // 启用标准输出、错误、输入流的重定向
                    start.RedirectStandardOutput = true;
                    start.RedirectStandardError = true;
                    start.RedirectStandardInput = true;
                    // 设置编码为UTF8，确保中文等特殊字符正常显示
                    start.StandardOutputEncoding = System.Text.Encoding.UTF8;
                    start.StandardErrorEncoding = System.Text.Encoding.UTF8;
                }

                // 标记输出流和错误流是否读取结束
                bool endOutput = false;
                bool endError = false;

                // 注册输出流数据接收事件（命令正常输出信息）
                process.OutputDataReceived += (sender , args) =>
                {
                    if(args.Data != null)
                    {
                        UnityEngine.Debug.Log(args.Data);
                    }
                    else
                    {
                        // 数据为null表示输出流结束
                        endOutput = true;
                    }
                };

                // 注册错误流数据接收事件（命令错误输出信息）
                process.ErrorDataReceived += (sender , args) =>
                {
                    if(args.Data != null)
                    {
                        // 错误信息通过Unity错误日志打印
                        UnityEngine.Debug.LogError(args.Data);
                    }
                    else
                    {
                        // 数据为null表示错误流结束
                        endError = true;
                    }
                };

                // 启动进程
                process.Start( );
                // 开始异步读取输出流
                process.BeginOutputReadLine( );
                // 开始异步读取错误流
                process.BeginErrorReadLine( );

                // 等待输出流和错误流都处理完毕（避免进程提前结束导致输出丢失）
                while(!endOutput || !endError)
                {

                }

                // 取消输出流和错误流的异步读取
                process.CancelOutputRead( );
                process.CancelErrorRead( );
            }
            catch(Exception e)
            {
                // 捕获并打印执行过程中的异常
                UnityEngine.Debug.LogException(e);
            }
            finally
            {
                // 确保进程资源被释放
                process.Close( );
            }
        }
    }
}
