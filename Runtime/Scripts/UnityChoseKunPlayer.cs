
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Utj.UnityChoseKun.Engine
{
    using Rendering.Universal;

    /// <summary>
    /// UnityChoseKunのPlayer側Class
    /// </summary>
    public class UnityChoseKunPlayer : MonoBehaviour
    {              
        delegate void OnMessageFunc(BinaryReader binaryReader);
        
        
        Dictionary<UnityChoseKun.MessageID, OnMessageFunc> onMessageFuncDict;
        
        // BasePlayer Classを継承するClassの定義を追加していく
        ScreenPlayer        m_playerScreen;
        TimePlayer          m_playerTime;
        ShaderPlayer        m_shaderPlayer;
        ComponentPlayer     m_componentPlayer;
        TexturePlayer       m_texturePlayer;
        ApplicationPlayer   m_applicationPlayer;
        AndroidPlayer       m_androidPlayer;
        HierarchyPlayer     m_hierarchyPlayer;
        QualitySettingsPlayer m_qualitySettingsPlayer;
        SystemInfoPlayer m_systemInfoPlayer;
        AssetPlayer<Sprite,SpriteKun> m_spritePlayer;
        SortingLayerPlayer m_sortingLayerPlayer;


        ScreenPlayer playerScreen {
            get {if(m_playerScreen == null){m_playerScreen = new ScreenPlayer();}　return m_playerScreen;}
        }

        TimePlayer playerTime {
            get {if(m_playerTime == null){m_playerTime = new TimePlayer();}return m_playerTime;}
        }
        
        ShaderPlayer shaderPlayer {
            get {
                if(m_shaderPlayer == null){m_shaderPlayer = new ShaderPlayer();}return m_shaderPlayer;
            }
        }
        
        SortingLayerPlayer sortingLayerPlayer
        {
            get
            {
                if(m_sortingLayerPlayer == null)
                {
                    m_sortingLayerPlayer = new SortingLayerPlayer();
                }
                return m_sortingLayerPlayer;
            }
        }

        public ComponentPlayer componentPlayer {
            get {if(m_componentPlayer == null){m_componentPlayer = new ComponentPlayer();}return m_componentPlayer;}
            private set {m_componentPlayer = value;}
        }
        
        public TexturePlayer texturePlayer {
            get{if(m_texturePlayer == null){m_texturePlayer = new TexturePlayer();}return m_texturePlayer;}            
        }
        
        public AssetPlayer<Sprite,SpriteKun> spritePlayer
        {
            get
            {
                if(m_spritePlayer == null)
                {
                    m_spritePlayer = new AssetPlayer<Sprite, SpriteKun>(UnityChoseKun.MessageID.SpritePull);
                }
                return m_spritePlayer;
            }
        }

        ApplicationPlayer applicationPlayer
        {
            get { if (m_applicationPlayer == null) { m_applicationPlayer = new ApplicationPlayer(); } return m_applicationPlayer; }            
            set { m_applicationPlayer = value; }
        }
        
        AndroidPlayer androidPlayer
        {
            get { if (m_androidPlayer == null) { m_androidPlayer = new AndroidPlayer(); } return m_androidPlayer; }
        }

        QualitySettingsPlayer qualitySettingsPlayer
        {
            get { if (m_qualitySettingsPlayer == null) { m_qualitySettingsPlayer = new QualitySettingsPlayer(); } return m_qualitySettingsPlayer; }
        }


        HierarchyPlayer hierarchyPlayer
        {
            get
            {
                if(m_hierarchyPlayer == null)
                {
                    m_hierarchyPlayer = new HierarchyPlayer();
                }
                return m_hierarchyPlayer;
            }
        }

        SystemInfoPlayer systemInfoPlayer
        {
            get
            {
                if(m_systemInfoPlayer == null)
                {
                    m_systemInfoPlayer = new SystemInfoPlayer();
                }
                return m_systemInfoPlayer;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            // DontDestroyOnLoad()扱いにはなるが、ダメっぽい。

            //if (m_isCreated)
            //{
            //    GameObject.DestroyImmediate(this.gameObject);
            //}
            //else
            {
                //    m_isCreated = true;
                //    DontDestroyOnLoad(this.gameObject);


                // MessageIDと対応するCBを登録する必要がある
                onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
                {
                    {UnityChoseKun.MessageID.AndroidPull,       androidPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.AndroidPush,       androidPlayer.OnMessageEventPush},
                    {UnityChoseKun.MessageID.ApplicationPull,   applicationPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.ApplicationPush,   applicationPlayer.OnMessageEventPush},
                    {UnityChoseKun.MessageID.ApplicationQuit,   applicationPlayer.OnMessageEventQuit},
                    {UnityChoseKun.MessageID.GameObjectPull,    componentPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.GameObjectPush,    componentPlayer.OnMessageEventPush },
                    {UnityChoseKun.MessageID.GraphicsSettingsPull,GraphicsSettingsPlayer.OnMessageEventPull },
                    {UnityChoseKun.MessageID.GraphicsSettingsPush,GraphicsSettingsPlayer.OnMessageEventPush },
                    {UnityChoseKun.MessageID.HierarchyPush,     hierarchyPlayer.OnMessageEventPush },
                    {UnityChoseKun.MessageID.LayerMaskPull,     LayerMaskPlayer.OnMessageEventPull },
                    {UnityChoseKun.MessageID.OnDemandRenderingPull, OnDemandRenderingPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.OnDemandRenderingPush,OnDemandRenderingPlayer.OnMessageEventPush},
                    {UnityChoseKun.MessageID.QualitySettingsPull,qualitySettingsPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.QualitySettingsPush,qualitySettingsPlayer.OnMessageEventPush},
                    {UnityChoseKun.MessageID.ScalableBufferManagerPull,ScalableBufferManagerPlayer.OnMessageEventPull },
                    {UnityChoseKun.MessageID.ScalableBufferManagerPush,ScalableBufferManagerPlayer.OnMessageEventPush },
                    {UnityChoseKun.MessageID.ScreenPull,        playerScreen.OnMessageEventPull},
                    {UnityChoseKun.MessageID.ScreenPush,        playerScreen.OnMessageEventPush},                                        
                    {UnityChoseKun.MessageID.ShaderPull,        shaderPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.SortingLayerPull, sortingLayerPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.SpritePull,spritePlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.SystemInfoPull,systemInfoPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.TexturePull,       texturePlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.TimePull,          playerTime.OnMessageEventPull},
                    {UnityChoseKun.MessageID.TimePush,          playerTime.OnMessageEventPush},
                                      
                    {UnityChoseKun.MessageID.UniversalRenderPipelinePull,UniversalRenderPipelinePlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.UniversalRenderPipelinePush,UniversalRenderPipelinePlayer.OnMessageEventPush},
#if UNITY_2021_2_OR_NEWER
                    {UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull,UniversalRenderPipelineGlobalSettingsPlayer.OnMessageEventPull},
                    {UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPush,UniversalRenderPipelineGlobalSettingsPlayer.OnMessageEventPush},
#endif
            };



                //
                // https://answers.unity.com/questions/30930/why-did-my-binaryserialzer-stop-working.html
                // https://answers.unity.com/questions/725419/filestream-binaryformatter-from-c-to-ios-doesnt-wo.html
                //
#if UNITY_IPHONE || UNITY_IOS
                {
                    System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
                }
#endif
            }
        }

        
        //
        void OnDestroy()
        {
            //UnityChoseKun.Log("OnDestroy");
            if(onMessageFuncDict != null)
            {
                onMessageFuncDict.Clear();
                onMessageFuncDict = null;
            }
        }


        //
        void OnEnable()
        {
            //UnityChoseKun.Log("OnEnable");
            PlayerConnection.instance.Register(UnityChoseKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        //
        void OnDisable()
        {
            //UnityChoseKun.Log("OnDisable");
            PlayerConnection.instance.Unregister(UnityChoseKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        /// <summary>
        /// EditorからPlayerへの受信
        /// </summary>
        /// <param name="args"></param>
        void OnMessageEvent(MessageEventArgs args)
        {
            //UnityChoseKun.Log("UnityChoseKun::OnMessageEvent");
            if (args.data == null)
            {
                UnityChoseKun.LogError("args.data == null");
            }
            else
            {
                MemoryStream memory = new MemoryStream(args.data);
                BinaryReader binaryReader = new BinaryReader(memory);

                try
                {
                    var messageID = (UnityChoseKun.MessageID)binaryReader.ReadInt32();
                    //UnityChoseKun.Log("message.id " + messageID);
                    var func = onMessageFuncDict[messageID];
                    func(binaryReader);
                }

                finally
                {
                    binaryReader.Close();
                    memory.Close();
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
            //UnityChoseKun.Log("UnityChoseKunPlayer.SendMessage<T>("+ id + ",obj)" );
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            try
            {
                binaryWriter.Write((int)id);
                obj.Serialize(binaryWriter);
                byte[] bytes = memoryStream.ToArray();
                PlayerConnection.instance.Send(UnityChoseKun.kMsgSendPlayerToEditor, bytes);
            }
            finally
            {
                binaryWriter.Close();
                memoryStream.Close();
            }
        }
    }
}