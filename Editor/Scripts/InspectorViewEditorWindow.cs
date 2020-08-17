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

   

    // <summary>UnityChoseKunのEditorWindow部分</summary>
    public class InspectorViewEditorWindow : EditorWindow
    {
        public static class Styles
        {                    
            public static readonly GUIContent TitleContent = new GUIContent("Player Inspector", (Texture2D)EditorGUIUtility.Load("d_UnityEditor.InspectorWindow"));
        }

        delegate void Task();
        delegate void OnMessageFunc(string json);

        IConnectionState m_attachProfilerState;
        bool m_registered = false;
        static InspectorViewEditorWindow m_window;

        public static InspectorViewEditorWindow window
        {
            get {               
                return m_window;
            }
            private set {m_window = value;}
        }


        [SerializeField] int toolbarIdx = 0;
        [SerializeField] ScreenEditor m_editorScrren;
        ScreenEditor editorScrren{get {if(m_editorScrren == null ){m_editorScrren = new ScreenEditor();}return m_editorScrren;}}        
        [SerializeField] TimeEditor m_editorTime;
        TimeEditor editorTime{get {if(m_editorTime == null){m_editorTime = new TimeEditor();}return m_editorTime;}}        
        [SerializeField] ComponentEditor m_editorComponent ;
        ComponentEditor editorComponent {
            get {if(m_editorComponent == null){m_editorComponent = new ComponentEditor();}return m_editorComponent;}
        }
        [SerializeField] Dictionary<string,Action> onGUILayoutFuncDict;
        Dictionary<UnityChoseKun.MessageID, OnMessageFunc> onMessageFuncDict;
        [SerializeField] Vector2 scrollPos;


        [MenuItem("Window/UnityChoseKun/Inspector")]
        static void Create()
        {
            if(window == null){
                window = (InspectorViewEditorWindow)EditorWindow.GetWindow(typeof(InspectorViewEditorWindow));
            }
            window.titleContent = Styles.TitleContent;
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }

        private void Update()
        {            
        }


        //
        private void OnGUI()
        {                        
            Initialize();
            GUILayoutConnect();            
            EditorGUILayout.Space();
            
            if (onGUILayoutFuncDict != null)
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
            if (m_attachProfilerState == null){            
                m_attachProfilerState = ConnectionUtility.GetAttachToPlayerState(this);            
            }
            Initialize();
        }

        private void OnDisable()
        {
            m_attachProfilerState.Dispose();
            m_attachProfilerState = null;

            
        }        

        private void OnDestroy()
        {
            if (m_registered == true)
            {
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(UnityChoseKun.kMsgSendPlayerToEditor, OnMessageEvent);
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.DisconnectAll();
                m_registered = false;
            }

            if(onMessageFuncDict != null)
            {
                onMessageFuncDict.Clear();
                onMessageFuncDict = null;
            }

            if(onGUILayoutFuncDict != null)
            {
                onGUILayoutFuncDict.Clear();
                onGUILayoutFuncDict = null;
            }
            window = null;
        }

        private void Initialize()
        {
            if (m_registered == false)
            {
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Initialize();
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Register(UnityChoseKun.kMsgSendPlayerToEditor, OnMessageEvent);
              
                if (onGUILayoutFuncDict == null)
                {
                    onGUILayoutFuncDict = new Dictionary<string, Action>()
                    {
                        {"Screen",      editorScrren.OnGUI },
                        { "Time",       editorTime.OnGUI},                        
                        {"Component",   editorComponent .OnGUI},
                        // 機能をここに追加していく                                              
                    };                    
                }

                if (onMessageFuncDict == null)
                {
                    onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
                    {
                        {UnityChoseKun.MessageID.ScreenPull,editorScrren.OnMessageEvent},
                        {UnityChoseKun.MessageID.TimePull,editorTime.OnMessageEvent },                        
                        {UnityChoseKun.MessageID.GameObjectPull, editorComponent.OnMessageEvent},
                        // 機能をここに追加していく                                              
                    };
                }
                m_registered = true;
            }
        }

        private void OnMessageEvent(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
        {
            Debug.Log("OnMessageEvent");
            var json = System.Text.Encoding.ASCII.GetString(args.data);
            var message = JsonUtility.FromJson<UnityChoseKunMessageData>(json);
            Debug.Log("message.id:"　+　message.id);
            if (onMessageFuncDict != null && onMessageFuncDict.ContainsKey(message.id) == true)
            {
                var func = onMessageFuncDict[message.id];
                func(message.json);
            }
        }
    }
}

