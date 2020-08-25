
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
        delegate void OnMessageFunc(string json);
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
                if(m_shaderPlayer == null){m_shaderPlayer = new ShaderPlayer();}return m_shaderPlayer;}
        }

        ComponentPlayer m_componentPlayer;
        public ComponentPlayer componentPlayer {
            get {if(m_componentPlayer == null){m_componentPlayer = new ComponentPlayer();}return m_componentPlayer;}
            private set {m_componentPlayer = value;}
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
            var json = System.Text.Encoding.ASCII.GetString(args.data);
            var message = JsonUtility.FromJson<UnityChoseKunMessageData>(json);
            if(message == null){
                Debug.LogWarning("mesage == null");
                return;
            }
            Debug.Log("message.id " + message.id);
            var func = onMessageFuncDict[message.id];
            func(message.json);            
        }

        public static void SendMessage<T>(UnityChoseKun.MessageID id,object obj)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            message.json = JsonUtility.ToJson(obj);
            var json = JsonUtility.ToJson(message);
            var bytes = System.Text.Encoding.ASCII.GetBytes(json);
            PlayerConnection.instance.Send(UnityChoseKun.kMsgSendPlayerToEditor, bytes);
        }        
    }
}