
namespace Utj.UnityChoseKun
{

    using System.Collections;
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
        delegate void OnMessageFunc(byte[] data);
        
        
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
        /// Singletoneチェック用
        /// </summary>
        //static bool m_isCreated;

        



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
            UnityChoseKun.Log("OnDestroy");

            if(onMessageFuncDict != null)
            {
                onMessageFuncDict.Clear();
                onMessageFuncDict = null;
            }
        }


        //
        void OnEnable()
        {
            UnityChoseKun.Log("OnEnable");
            PlayerConnection.instance.Register(UnityChoseKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        //
        void OnDisable()
        {
            UnityChoseKun.Log("OnDisable");
            PlayerConnection.instance.Unregister(UnityChoseKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        //
        void OnMessageEvent(MessageEventArgs args)
        {
            UnityChoseKun.Log("UnityChoseKun::OnMessageEvent");
            if (args.data == null)
            {
                UnityChoseKun.LogError("args.data == null");
            }
            else
            {
                var message = UnityChoseKun.GetObject<UnityChoseKunMessageData>(args.data);
                if (message == null)
                {
                    UnityChoseKun.LogWarning("mesage == null");
                    return;
                }
                UnityChoseKun.Log("message.id " + message.id);                
                var func = onMessageFuncDict[message.id];
                func(message.bytes);
            }
        }

        
        public static void SendMessage<T>(UnityChoseKun.MessageID id, T obj)
        {
            Debug.Log("UnityChoseKunPlayer.SendMessage<T>("+ id + ",obj)" );
            
            var message = new UnityChoseKunMessageData();
            message.id = id;
            UnityChoseKun.ObjectToBytes<T>(obj, out message.bytes);

            byte[] bytes;
            UnityChoseKun.ObjectToBytes<UnityChoseKunMessageData>(message, out bytes);            
            PlayerConnection.instance.Send(UnityChoseKun.kMsgSendPlayerToEditor, bytes);
        }
    }
}