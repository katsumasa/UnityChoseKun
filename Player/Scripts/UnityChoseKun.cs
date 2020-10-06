using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    [System.Serializable]    
    public class UnityChoseKunMessageData
    {
        [SerializeField] public UnityChoseKun.MessageID id;        
        [SerializeField] public byte[] bytes;
    }

    public class UnityChoseKun
    {
        public enum MessageID
        {
            ScreenPull,
            ScreenPush,
            TimePull,
            TimePush,
            GameObjectPull,
            GameObjectPush,
            ShaderPull,
            TexturePull,
            ApplicationPull,
            ApplicationPush,
            ApplicationQuit,
            AndroidPull,
            AndroidPush,
        }
        public static readonly System.Guid kMsgSendEditorToPlayer = new System.Guid("a819fa0823134ed9bfc6cf17eac8a232");
        public static readonly System.Guid kMsgSendPlayerToEditor = new System.Guid("5b9b9d37e331433cbd31c6cf8093d8da");


        public static void ObjectToBytes<T>(T src, out byte[] dst)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            try
            {
                bf.Serialize(ms, src);
                dst = ms.ToArray();
            }
            finally
            {
                ms.Close();
            }
        }


        public static void BytesToObject<T>(byte[] src, out T dst)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(src);
            try
            {
                dst = (T)bf.Deserialize(ms);
            }
            finally
            {
                ms.Close();
            }
        }

        public static T GetObject<T>(byte[] src)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(src);
            try
            {
                return (T)bf.Deserialize(ms);
            }
            finally
            {
                ms.Close();
            }
        }
    }

}