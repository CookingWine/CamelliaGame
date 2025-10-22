using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
namespace CamelliaEditor
{


    public static class ProtoGenerator
    {
        private static readonly string ProtocPath = Application.dataPath + "\\Tools~\\Protoc\\protoc.exe";
        private static readonly string ProtoDir = Application.dataPath + "\\GameConfigTable~\\Protobufs";
        private static readonly string OutDir = Application.dataPath + "\\Code\\Hotfix\\Generated\\Protobuf";
        private static readonly string MsgIdPath = Path.Combine(OutDir , "MsgId.cs");
        private static readonly string RegistryPath = Path.Combine(OutDir , "MsgRegistry.cs");

        [MenuItem("Tools/生成 Protobuf 代码")]
        public static void Generate( )
        {
            UnityEngine.Debug.Log(Application.dataPath + "---" + ProtoDir);
            if(!File.Exists(ProtocPath))
            {
                EditorUtility.DisplayDialog("错误" , $"未找到 protoc.exe\n路径: {ProtocPath}" , "确定");
                return;
            }

            if(!Directory.Exists(ProtoDir))
            {
                EditorUtility.DisplayDialog("错误" , $"未找到 Protobufs 目录: {ProtoDir}" , "确定");
                return;
            }

            if(!Directory.Exists(OutDir))
                Directory.CreateDirectory(OutDir);

            var protoFiles = Directory.GetFiles(ProtoDir , "*.proto" , SearchOption.AllDirectories);
            if(protoFiles.Length == 0)
            {
                EditorUtility.DisplayDialog("提示" , "没有找到任何 .proto 文件" , "确定");
                return;
            }

            int startId = 1000;
            var msgNames = new List<string>( );

            // 调用 protoc 生成 C#
            foreach(var proto in protoFiles)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ProtocPath ,
                    Arguments = $"--csharp_out=\"{OutDir}\" \"{proto}\"" ,
                    UseShellExecute = false ,
                    RedirectStandardOutput = true ,
                    RedirectStandardError = true ,
                    CreateNoWindow = true ,
                    StandardOutputEncoding = Encoding.UTF8 ,
                    StandardErrorEncoding = Encoding.UTF8
                };

                using(var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd( );
                    string error = process.StandardError.ReadToEnd( );
                    process.WaitForExit( );

                    if(!string.IsNullOrEmpty(output))
                        UnityEngine.Debug.Log($"[protoc output] {output}");

                    if(!string.IsNullOrEmpty(error))
                        UnityEngine.Debug.LogError($"[protoc error] {error}");
                }

            }

            // 写 MsgId.cs
            var sb = new StringBuilder( );
            sb.AppendLine("// 自动生成，请勿修改");
            sb.AppendLine("public enum MsgId");
            sb.AppendLine("{");
            int id = startId;
            foreach(var name in msgNames)
            {
                sb.AppendLine($"    {name} = {id},");
                id++;
            }
            sb.AppendLine("}");
            File.WriteAllText(MsgIdPath , sb.ToString( ) , Encoding.UTF8);

            // 写 MsgRegistry.cs
            var reg = new StringBuilder( );
            reg.AppendLine("// 自动生成，请勿修改");
            reg.AppendLine("using System.Collections.Generic;");
            reg.AppendLine("using Google.Protobuf;");
            reg.AppendLine( );
            reg.AppendLine("public static class MsgRegistry");
            reg.AppendLine("{");
            reg.AppendLine("    public static readonly Dictionary<MsgId, MessageParser> Parsers = new()");
            reg.AppendLine("    {");
            foreach(var name in msgNames)
            {
                reg.AppendLine($"        {{ MsgId.{name}, {name}.Parser }},");
            }
            reg.AppendLine("    };");
            reg.AppendLine("}");
            File.WriteAllText(RegistryPath , reg.ToString( ) , Encoding.UTF8);

            AssetDatabase.Refresh( );
            EditorUtility.DisplayDialog("完成" , $"共生成 {msgNames.Count} 个协议\n输出目录: {OutDir}" , "确定");
            UnityEngine.Debug.Log($"✅ Protobuf 生成完成，共 {msgNames.Count} 个 message");
        }
    }

}
