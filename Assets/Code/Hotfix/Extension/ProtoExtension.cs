using Google.Protobuf;
using System;
using System.ComponentModel;
using System.IO;
namespace CamelliaGame.HotfixCode
{
    /// <summary>
    /// proto扩展方法
    /// </summary>
    public static class ProtoExtension
    {
        public static byte[] ToBytes(object message)
        {
            return ( (IMessage)message ).ToByteArray( );
        }

        public static void ToStream(object message , MemoryStream stream)
        {
            ( (IMessage)message ).WriteTo(stream);
        }

        public static object FromBytes(Type type , byte[] bytes , int index , int count)
        {
            object message = Activator.CreateInstance(type);
            ( (IMessage)message ).MergeFrom(bytes , index , count);
            if(message is not ISupportInitialize iSupportInitialize)
            {
                return message;
            }
            iSupportInitialize.EndInit( );
            return message;
        }
        public static object FromBytes(object message , byte[] bytes , int index , int count)
        {
            ( (IMessage)message ).MergeFrom(bytes , index , count);
            if(message is not ISupportInitialize iSupportInitialize)
            {
                return message;
            }
            iSupportInitialize.EndInit( );
            return message;
        }

        public static object FromStream(Type type , MemoryStream stream)
        {
            object message = Activator.CreateInstance(type);
            ( (IMessage)message ).MergeFrom(stream.GetBuffer( ) , (int)stream.Position , (int)stream.Length);
            if(message is not ISupportInitialize iSupportInitialize)
            {
                return message;
            }
            iSupportInitialize.EndInit( );
            return message;
        }
        public static object FromStream(object message , MemoryStream stream)
        {
            ( (IMessage)message ).MergeFrom(stream.GetBuffer( ) , (int)stream.Position , (int)stream.Length);
            if(message is not ISupportInitialize iSupportInitialize)
            {
                return message;
            }
            iSupportInitialize.EndInit( );
            return message;
        }
    }
}
