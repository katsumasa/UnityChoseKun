namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEditor;
#if UNITY_2018_1_OR_NEWER
    using UnityEngine.Networking.PlayerConnection;
    using ConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;
    using ConnectionGUILayout = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUILayout;
    using UnityEngine.Experimental.Networking.PlayerConnection;
    using UnityEditor.Networking.PlayerConnection;
    using System;
    using System.Text;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
#endif

   

    /// <summary>
    /// UnityChoseKunのEditorWindow
    /// </summary>
    public class UnityChoseKunEditorWindow : EditorWindow
    {
        private static class Styles
        {                    
            public static readonly GUIContent TitleContent = new GUIContent("Player Inspector", (Texture2D)EditorGUIUtility.Load("d_UnityEditor.InspectorWindow"));
        }

        delegate void Task();
        delegate void OnMessageFunc(byte[] bytes);

        IConnectionState m_attachProfilerState;
        
        

        [SerializeField] int toolbarIdx = 0;
        [SerializeField] ScreenView m_screenView;
        [SerializeField] TimeView m_timeView;
        [SerializeField] InspectorView m_inspectorView;
        [SerializeField] ShadersView m_shadersView;
        [SerializeField] TexturesView m_texturesView;
        [SerializeField] ApplicationView m_applicationView;
        [SerializeField] AndroidView m_androidView;
        [SerializeField] Dictionary<string, Action> onGUILayoutFuncDict;
        [SerializeField] Dictionary<UnityChoseKun.MessageID, OnMessageFunc> onMessageFuncDict;
        [SerializeField] Vector2 scrollPos;



        ScreenView screenView
        {
            get {
                if(m_screenView == null ){
                    m_screenView = new ScreenView();
                }
                return m_screenView;
            }
            set {
                m_screenView = value;
            }
        }        
        
        TimeView timeView{
            get {
                if(m_timeView == null){
                    m_timeView = new TimeView();
                }
                return m_timeView;
            }
            set {
                m_timeView = value;
            }
        }        

        InspectorView inspectorView {
            get {if(m_inspectorView == null){m_inspectorView = new InspectorView();}return m_inspectorView;}
        }
        
        ShadersView shaderView {
            get{if(m_shadersView == null){m_shadersView = new ShadersView();}return m_shadersView;}
        }
        
        TexturesView texturesView{
            get{if(m_texturesView == null){m_texturesView = new TexturesView();}return m_texturesView;}            
        }
        
        public ApplicationView applicationView
        {
            get {
                if (m_applicationView == null) {
                    m_applicationView = new ApplicationView();
                }
                return m_applicationView;
            }
        }

        public AndroidView androidView
        {
            get { if (m_androidView == null) { m_androidView = new AndroidView(); } return m_androidView; }            
        }




        [MenuItem("Window/UnityChoseKun/Player Inspector")]
        static void Create()
        {            
            var window = (UnityChoseKunEditorWindow)EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));            
            window.titleContent = Styles.TitleContent;
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }


        public RuntimePlatform GetRuntimePlatform()
        {
            if (applicationView.applicationKun != null)
            {
                return applicationView.applicationKun.platform;
            }
            return RuntimePlatform.WindowsEditor;
        }


        private void Update()
        {            
        }


        //
        private void OnGUI()
        {                        
            GUILayoutConnect();            
            EditorGUILayout.Space();
            
            //if (onGUILayoutFuncDict != null)
            {
                var texts = onGUILayoutFuncDict.Keys.ToArray();                
                toolbarIdx = GUILayout.Toolbar(
                    toolbarIdx,
                    texts,
                    EditorStyles.toolbarButton);
                EditorGUILayout.Space();

                var key = texts[toolbarIdx];
                
                Action onGUI;
                onGUILayoutFuncDict.TryGetValue(key,out onGUI);
                if (onGUI != null)
                {
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                    onGUI();
                    EditorGUILayout.EndScrollView();
                }
            }            
        }

        private void GUILayoutConnect()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Connect To");
            if (m_attachProfilerState != null)
            {
                ConnectionGUILayout.AttachToPlayerDropdown(m_attachProfilerState, EditorStyles.toolbarDropDown);
            }
            EditorGUILayout.EndHorizontal();

            var playerCount = EditorConnection.instance.ConnectedPlayers.Count;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("{0} players connected.", playerCount));
            int i = 0;
            foreach (var p in EditorConnection.instance.ConnectedPlayers)
            {
                builder.AppendLine(string.Format("[{0}] - {1} {2}", i++, p.name, p.playerId));
            }
            EditorGUILayout.HelpBox(builder.ToString(), MessageType.Info);
        }        


        // SampleのようにOnEnable/OnDisableで処理すると通信が不安体になる
        private void OnEnable()
        {
            Debug.Log("OnEnable");


            m_attachProfilerState = ConnectionUtility.GetAttachToPlayerState(this);            

            screenView = new ScreenView();
            timeView = new TimeView();


            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Initialize();
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Register(UnityChoseKun.kMsgSendPlayerToEditor, OnMessageEvent);

            onGUILayoutFuncDict = new Dictionary<string, Action>()
            {
                {"Inspector",   inspectorView.OnGUI},
                {"Texture",     texturesView.OnGUI},
                {"Shader",      shaderView.OnGUI},
                {"Screen",      screenView.OnGUI },
                {"Time",        timeView.OnGUI},
                {"Application", applicationView.OnGUI},
                {"Android",     androidView.OnGUI},
                // 機能をここに追加していく                                              
            };
                        
            onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
            {
                {UnityChoseKun.MessageID.ScreenPull,screenView.OnMessageEvent},
                {UnityChoseKun.MessageID.TimePull,timeView.OnMessageEvent },
                {UnityChoseKun.MessageID.GameObjectPull, inspectorView.OnMessageEvent},
                {UnityChoseKun.MessageID.ShaderPull,shaderView.OnMessageEvent},
                {UnityChoseKun.MessageID.TexturePull,texturesView.OnMessageEvent},
                {UnityChoseKun.MessageID.ApplicationPull,applicationView.OnMessageEvent },
                {UnityChoseKun.MessageID.AndroidPull,androidView.OnMessageEvent },

                // 機能をここに追加していく                                              
            };
            
        }

        private void OnDisable()
        {

            Debug.Log("OnDisable");

            if (onMessageFuncDict != null)
            {
                onMessageFuncDict.Clear();
                onMessageFuncDict = null;
            }
            
            if (onGUILayoutFuncDict != null)
            {
                onGUILayoutFuncDict.Clear();
                onGUILayoutFuncDict = null;
            }


            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(UnityChoseKun.kMsgSendPlayerToEditor, OnMessageEvent);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.DisconnectAll();

            m_attachProfilerState.Dispose();
            m_attachProfilerState = null;            
        }        


        private void OnDestroy()
        {
        }

       
        

        /// <summary>
        /// メッセージの受信CB
        /// </summary>
        /// <param name="args">メッセージデータ</param>
        private void OnMessageEvent(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
        {            
            var message = UnityChoseKun.GetObject<UnityChoseKunMessageData>(args.data);
            Debug.Log("UnityChosekunEditorWindow.OnMessageEvent(playerID: " + args.playerId + ",message.id" + message.id +")");            
            if (onMessageFuncDict != null && onMessageFuncDict.ContainsKey(message.id) == true)
            {
                var func = onMessageFuncDict[message.id];
                func(message.bytes);
            }
        }
    }
}

