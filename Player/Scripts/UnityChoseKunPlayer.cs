
namespace Utj.UnityChoseKun
{    
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
        
        public ComponentPlayer componentPlayer {
            get {if(m_componentPlayer == null){m_componentPlayer = new ComponentPlayer();}return m_componentPlayer;}
            private set {m_componentPlayer = value;}
        }
        
        public TexturePlayer texturePlayer {
            get{if(m_texturePlayer == null){m_texturePlayer = new TexturePlayer();}return m_texturePlayer;}            
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
                {UnityChoseKun.MessageID.ScreenPull,        playerScreen.OnMessageEventPull},
                {UnityChoseKun.MessageID.ScreenPush,        playerScreen.OnMessageEventPush},
                {UnityChoseKun.MessageID.TimePull,          playerTime.OnMessageEventPull},
                {UnityChoseKun.MessageID.TimePush,          playerTime.OnMessageEventPush},
                {UnityChoseKun.MessageID.GameObjectPull,    componentPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.GameObjectPush,    componentPlayer.OnMessageEventPush },
                {UnityChoseKun.MessageID.ShaderPull,        shaderPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.TexturePull,       texturePlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.ApplicationPull,   applicationPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.ApplicationPush,   applicationPlayer.OnMessageEventPush},
                {UnityChoseKun.MessageID.ApplicationQuit,   applicationPlayer.OnMessageEventQuit},
                {UnityChoseKun.MessageID.AndroidPull,       androidPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.AndroidPush,       androidPlayer.OnMessageEventPush},
                {UnityChoseKun.MessageID.HierarchyPush,     hierarchyPlayer.OnMessageEventPush },
                {UnityChoseKun.MessageID.QualitySettingsPull,qualitySettingsPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.QualitySettingsPush,qualitySettingsPlayer.OnMessageEventPush},
                {UnityChoseKun.MessageID.OnDemandRenderingPull, OnDemandRenderingPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.OnDemandRenderingPush,OnDemandRenderingPlayer.OnMessageEventPush},
                    {UnityChoseKun.MessageID.ScalableBufferManagerPull,ScalableBufferManagerPlayer.OnMessageEventPull },
                    {UnityChoseKun.MessageID.ScalableBufferManagerPush,ScalableBufferManagerPlayer.OnMessageEventPush },

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


        // Start is called before the first frame update
        void Start()
        {            
        }


        // Update is called once per frame
        void Update()
        {
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