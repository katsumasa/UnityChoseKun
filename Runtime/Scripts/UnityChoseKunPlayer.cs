
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Utj.UnityChoseKun.Engine
{
    using Rendering.Universal;

    /// <summary>
    /// UnityChoseKunのPlayer側Class
    /// </summary>
    public class UnityChoseKunPlayer : UTJ.RemoteConnect.Player
    {              
        delegate void OnMessageFunc(BinaryReader binaryReader);
        

        static readonly Dictionary<UnityChoseKun.MessageID, OnMessageFunc> onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
        {
            { UnityChoseKun.MessageID.AndroidPull,       AndroidPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.AndroidPush,       AndroidPlayer.OnMessageEventPush},
            { UnityChoseKun.MessageID.ApplicationPull,   ApplicationPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.ApplicationPush,   ApplicationPlayer.OnMessageEventPush},
            { UnityChoseKun.MessageID.ApplicationQuit,   ApplicationPlayer.OnMessageEventQuit},
            { UnityChoseKun.MessageID.GameObjectPull,    ComponentPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.GameObjectPush,    ComponentPlayer.OnMessageEventPush },
            { UnityChoseKun.MessageID.GraphicsSettingsPull,GraphicsSettingsPlayer.OnMessageEventPull },
            { UnityChoseKun.MessageID.GraphicsSettingsPush,GraphicsSettingsPlayer.OnMessageEventPush },
            { UnityChoseKun.MessageID.HierarchyPush,     HierarchyPlayer.OnMessageEventPush },
            { UnityChoseKun.MessageID.LayerMaskPull,     LayerMaskPlayer.OnMessageEventPull },
            { UnityChoseKun.MessageID.OnDemandRenderingPull, OnDemandRenderingPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.OnDemandRenderingPush,OnDemandRenderingPlayer.OnMessageEventPush},
            { UnityChoseKun.MessageID.QualitySettingsPull,QualitySettingsPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.QualitySettingsPush,QualitySettingsPlayer.OnMessageEventPush},
            { UnityChoseKun.MessageID.ScalableBufferManagerPull,ScalableBufferManagerPlayer.OnMessageEventPull },
            { UnityChoseKun.MessageID.ScalableBufferManagerPush,ScalableBufferManagerPlayer.OnMessageEventPush },
            { UnityChoseKun.MessageID.ScreenPull,        ScreenPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.ScreenPush,        ScreenPlayer.OnMessageEventPush},                                        
            { UnityChoseKun.MessageID.ShaderPull,        ShaderPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.SortingLayerPull,  SortingLayerPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.SpritePull,        AssetPlayer<Sprite, SpriteKun>.OnMessageEventPull},
            { UnityChoseKun.MessageID.SystemInfoPull,    SystemInfoPlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.TexturePull,       TexturePlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.TimePull,          TimePlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.TimePush,          TimePlayer.OnMessageEventPush},                                      
            { UnityChoseKun.MessageID.UniversalRenderPipelinePull,UniversalRenderPipelinePlayer.OnMessageEventPull},
            { UnityChoseKun.MessageID.UniversalRenderPipelinePush,UniversalRenderPipelinePlayer.OnMessageEventPush},
#if UNITY_2021_2_OR_NEWER
            {UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull,UniversalRenderPipelineGlobalSettingsPlayer.OnMessageEventPull},
            {UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPush,UniversalRenderPipelineGlobalSettingsPlayer.OnMessageEventPush},
#endif
            {UnityChoseKun.MessageID.ProfilerPlayerPull, ProfilerPlayer.OnMessageEventPull},
        };


        static UnityChoseKunPlayer m_Instance;


        private void Awake()
        {
            // https://answers.unity.com/questions/30930/why-did-my-binaryserialzer-stop-working.html
            // https://answers.unity.com/questions/725419/filestream-binaryformatter-from-c-to-ios-doesnt-wo.html
            //
#if UNITY_IPHONE || UNITY_IOS
            {
                System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
            }
#endif
        }


        private void Start()
        {
            if(m_Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                m_Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        //
        void OnDestroy()
        {
            if(m_Instance == this)
            {
                m_Instance = null;
            }
        }

        protected override void OnEnable()
        {
            kMsgSendEditorToPlayer = UnityChoseKun.kMsgSendEditorToPlayer;
            kMsgSendPlayerToEditor = UnityChoseKun.kMsgSendPlayerToEditor;
            messageEventCB = MessageReciveEventCB;
            base.OnEnable();                        
        }
       

        void MessageReciveEventCB(byte[] bytes)
        {
            using (MemoryStream memory = new MemoryStream(bytes))
            {
                using (BinaryReader binaryReader = new BinaryReader(memory))
                {                   
                    var messageID = (UnityChoseKun.MessageID)binaryReader.ReadInt32();
                    if (onMessageFuncDict.ContainsKey(messageID))
                    {
                        var func = onMessageFuncDict[messageID];
                        func(binaryReader);
                    }
                    else
                    {
                        Debug.Log($"Invalid messageID:{messageID}");
                    }
                }
            }            
        }

        
        /// <summary>
        /// PlayerからEditorへの送信
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        public static void SendMessage<T>(UnityChoseKun.MessageID id, T obj) where T : ISerializerKun
        {            
            using (MemoryStream memoryStream = new MemoryStream()) 
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                { 
                    binaryWriter.Write((int)id);
                    obj.Serialize(binaryWriter);
                    byte[] bytes = memoryStream.ToArray();
                    m_Instance.SendRemoteMessage(memoryStream.GetBuffer());
                }                
            }
        }
    }
}