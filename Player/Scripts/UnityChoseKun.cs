#if DEBUG
#define UNITY_CHOSEKUN_DEBUG
#else
#undef UNITY_CHOSEKUN_DEBUG
#endif

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using System;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// UnityChoseKunの共通パケット
    /// </summary>
    [System.Serializable]    
    public class UnityChoseKunMessageData
    {
        [SerializeField] public UnityChoseKun.MessageID id;        
        [SerializeField] public byte[] bytes;
    }

    /// <summary>
    /// UnityChoseKun Class
    /// </summary>
    public class UnityChoseKun
    {
        /// <summary>
        /// MessageID
        /// </summary>
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
            HierarchyPush,
        }


        /// <summary>
        /// From Editor to Playerで使用するPlayerConnec用のGUID
        /// </summary>        
        public static readonly System.Guid kMsgSendEditorToPlayer = new System.Guid("a819fa0823134ed9bfc6cf17eac8a232");

        /// <summary>
        /// From Player to Editorで使用するPlayerConnect用のGUID
        /// </summary>
        public static readonly System.Guid kMsgSendPlayerToEditor = new System.Guid("5b9b9d37e331433cbd31c6cf8093d8da");


        /// <summary>
        /// Objectデータをbyteの配列へ変換する
        /// </summary>
        /// <typeparam name="T">変換するObjectの型</typeparam>
        /// <param name="src">変換するObject</param>
        /// <param name="dst">byte型の配列</param>
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


        /// <summary>
        /// byte配列からObjectへ変換する
        /// </summary>
        /// <typeparam name="T">変換後のオブジェクトの型</typeparam>
        /// <param name="src">byte配列</param>
        /// <param name="dst">変換されたオブジェクト</param>
        public static void BytesToObject<T>(byte[] src, out T dst)
        {
            if (src != null)
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
            else
            {
                dst = default(T);
            }
        }


        /// <summary>
        /// byte配列をオブジェクトに変換する
        /// </summary>
        /// <typeparam name="T">変換後のオブジェクトの型</typeparam>
        /// <param name="src">byte配列</param>
        /// <returns>変換されたオブジェクト</returns>
        public static T GetObject<T>(byte[] src)
        {                     
            if(src == null)
            {
                return default(T);
            }
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



        public static void Log(object obj)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.Log(obj);
#endif
        }


        public static void LogError(object obj)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.LogError(obj);
#endif
        }


        public static void LogWarning(object obj)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.LogWarning(obj);
#endif
        }


        public static void LogAssert(bool condition)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.Assert(condition);
#endif
        }
    }

}