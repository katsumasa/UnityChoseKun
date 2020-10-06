
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


    public class UnityChoseKunPlayer : MonoBehaviour
    {              
        delegate void OnMessageFunc(byte[] data);
        Dictionary<UnityChoseKun.MessageID, OnMessageFunc> onMessageFuncDict;
        ScreenPlayer m_playerScreen;
        ScreenPlayer playerScreen {
            get {if(m_playerScreen == null){m_playerScreen = new ScreenPlayer();}　return m_playerScreen;}
        }
        TimePlayer m_playerTime;
        TimePlayer playerTime {
            get {if(m_playerTime == null){m_playerTime = new TimePlayer();}return m_playerTime;}
        }
        ShaderPlayer m_shaderPlayer;
        ShaderPlayer shaderPlayer {
            get {
                if(m_shaderPlayer == null){m_shaderPlayer = new ShaderPlayer();}return m_shaderPlayer;
            }
        }

        ComponentPlayer m_componentPlayer;
        public ComponentPlayer componentPlayer {
            get {if(m_componentPlayer == null){m_componentPlayer = new ComponentPlayer();}return m_componentPlayer;}
            private set {m_componentPlayer = value;}
        }

        TexturePlayer m_texturePlayer;
        public TexturePlayer texturePlayer {
            get{if(m_texturePlayer == null){m_texturePlayer = new TexturePlayer();}return m_texturePlayer;}            
        }

        ApplicationPlayer m_applicationPlayer;
        ApplicationPlayer applicationPlayer
        {
            get { if (m_applicationPlayer == null) { m_applicationPlayer = new ApplicationPlayer(); } return m_applicationPlayer; }            
            set { m_applicationPlayer = value; }
        }

        AndroidPlayer m_androidPlayer;
        AndroidPlayer androidPlayer
        {
            get { if (m_androidPlayer == null) { m_androidPlayer = new AndroidPlayer(); } return m_androidPlayer; }
        }


        private void Awake()
        {                            
            onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
            {
                {UnityChoseKun.MessageID.ScreenPull,    playerScreen.OnMessageEventPull},
                {UnityChoseKun.MessageID.ScreenPush,    playerScreen.OnMessageEventPush},
                {UnityChoseKun.MessageID.TimePull,      playerTime.OnMessageEventPull},
                {UnityChoseKun.MessageID.TimePush,      playerTime.OnMessageEventPush},
                {UnityChoseKun.MessageID.GameObjectPull,componentPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.GameObjectPush,componentPlayer.OnMessageEventPush },
                {UnityChoseKun.MessageID.ShaderPull,    shaderPlayer.OnMessageEventPull},   
                {UnityChoseKun.MessageID.TexturePull,   texturePlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.ApplicationPull, applicationPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.ApplicationPush, applicationPlayer.OnMessageEventPush},
                {UnityChoseKun.MessageID.ApplicationQuit, applicationPlayer.OnMessageEventQuit},
                {UnityChoseKun.MessageID.AndroidPull,   androidPlayer.OnMessageEventPull},
                {UnityChoseKun.MessageID.AndroidPush,   androidPlayer.OnMessageEventPush},

            };                                    
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
        private void OnDestroy()
        {
            if(onMessageFuncDict != null)
            {
                onMessageFuncDict.Clear();
                onMessageFuncDict = null;
            }
        }

        //
        private void OnEnable()
        {
            Debug.Log("OnEnable");
            PlayerConnection.instance.Register(UnityChoseKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }

        //
        private void OnDisable()
        {
            Debug.Log("OnDisable");
            PlayerConnection.instance.Unregister(UnityChoseKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }

        //
        private void OnMessageEvent(MessageEventArgs args)
        {
            Debug.Log("UnityChoseKun::OnMessageEvent");

            var message = UnityChoseKun.GetObject<UnityChoseKunMessageData>(args.data);
            if(message == null){
                Debug.LogWarning("mesage == null");
                return;
            }
            Debug.Log("message.id " + message.id);
            var func = onMessageFuncDict[message.id];
            func(message.bytes);            
        }

        
        public static void SendMessage<T>(UnityChoseKun.MessageID id, T obj)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            UnityChoseKun.ObjectToBytes<T>(obj, out message.bytes);

            byte[] bytes;
            UnityChoseKun.ObjectToBytes<UnityChoseKunMessageData>(message, out bytes);            
            PlayerConnection.instance.Send(UnityChoseKun.kMsgSendPlayerToEditor, bytes);
        }
    }
}