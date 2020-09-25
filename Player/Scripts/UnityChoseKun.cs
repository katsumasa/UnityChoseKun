namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Reflection;
    using System.Runtime.InteropServices;


    [System.Serializable]    
    public class UnityChoseKunMessageData
    {
        public UnityChoseKun.MessageID id;
        public string json;
    }

    public class UnityChoseKun
    {          
        public enum MessageID {
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
    }

}