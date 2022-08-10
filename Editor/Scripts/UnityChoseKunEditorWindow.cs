using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;
#if UNITY_2018_1_OR_NEWER
using UnityEngine.Networking.PlayerConnection;

#if UNITY_2020_1_OR_NEWER
using ConnectionUtility = UnityEditor.Networking.PlayerConnection.PlayerConnectionGUIUtility;
using ConnectionGUILayout = UnityEditor.Networking.PlayerConnection.PlayerConnectionGUILayout;
#else
    using ConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;
    using ConnectionGUILayout = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUILayout;
    using UnityEngine.Experimental.Networking.PlayerConnection;
#endif

using UnityEditor.Networking.PlayerConnection;
using System;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
#endif


namespace Utj.UnityChoseKun.Editor
{
    using Utj.UnityChoseKun.Editor.Rendering;
    using Utj.UnityChoseKun.Editor.Rendering.Universal;

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
        delegate void OnMessageFunc(BinaryReader binaryReader);
        
                
        [SerializeField] int                toolbarIdx = 0;                                
        [SerializeField] Vector2            scrollPos;

        IConnectionState                                    m_attachProfilerState;
        Dictionary<UnityChoseKun.MessageID, OnMessageFunc>  onMessageFuncDict;
        Dictionary<string, Action>                          onGUILayoutFuncDict;
        
                
        [MenuItem("Window/UTJ/UnityChoseKun/Player Inspector")]
        static void Inite()
        {            
            var window = (UnityChoseKunEditorWindow)EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));            
            window.titleContent = Styles.TitleContent;
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }


        
        /// <summary>
        /// 描画処理
        /// </summary>
        private void OnGUI()
        {                        
            GUILayoutConnect();            
            EditorGUILayout.Space();
            
            //if (onGUILayoutFuncDict != null)
            {
                var texts = onGUILayoutFuncDict.Keys.ToArray();
#if false
                toolbarIdx = GUILayout.Toolbar(
                    toolbarIdx,
                    texts,
                    EditorStyles.toolbarButton);
#else
                toolbarIdx = EditorGUILayout.Popup(toolbarIdx, texts);
#endif
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


        /// <summary>
        /// 接続先選択用GUI
        /// </summary>
        private void GUILayoutConnect()
        {
            EditorGUILayout.BeginHorizontal();
            var contents = new GUIContent("Connect To");
            var v2 = EditorStyles.label.CalcSize(contents);
            EditorGUILayout.LabelField(contents, GUILayout.Width(v2.x));
            if (m_attachProfilerState != null)
            {
#if UNITY_2020_1_OR_NEWER
                ConnectionGUILayout.ConnectionTargetSelectionDropdown(m_attachProfilerState, EditorStyles.toolbarDropDown);
#else
                ConnectionGUILayout.AttachToPlayerDropdown(m_attachProfilerState, EditorStyles.toolbarDropDown);
#endif

#if false
                switch (m_attachProfilerState.connectedToTarget)
                {
                    case ConnectionTarget.None:
                        //This case can never happen within the Editor, since the Editor will always fall back onto a connection to itself.
                        break;
                    case ConnectionTarget.Player:
                        Profiler.enabled = GUILayout.Toggle(Profiler.enabled, string.Format("Profile the attached Player ({0})", m_attachProfilerState.connectionName), EditorStyles.toolbarButton);
                        break;
                    case ConnectionTarget.Editor:
                        // The name of the Editor or the PlayMode Player would be "Editor" so adding the connectionName here would not add anything.
                        Profiler.enabled = GUILayout.Toggle(Profiler.enabled, "Profile the Player in the Editor", EditorStyles.toolbarButton);
                        break;
                    default:
                        break;
                }
#endif
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


        
        private void OnEnable()
        {            
#if UNITY_2020_1_OR_NEWER
            m_attachProfilerState = ConnectionUtility.GetConnectionState(this);
#else
            m_attachProfilerState = ConnectionUtility.GetAttachToPlayerState(this);
#endif
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Initialize();
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Register(UnityChoseKun.kMsgSendPlayerToEditor, OnMessageEvent);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.RegisterConnection(OnConnection);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.RegisterDisconnection(OnDisConnection);


            onGUILayoutFuncDict = new Dictionary<string, Action>()
            {
                {"Inspector",   InspectorView.instance.OnGUI},
                {"UnityEngine.Application", ApplicationView.instance.OnGUI},
                {"UnityEngine.Android.Permission",     AndroidView.instance.OnGUI},
                {"UnityEngine.Component",   ObjectCounterView.instance.OnGUI },
                {"UnityEngine.QualitySettings", QualitySettingsView.instance.OnGUI },                                
                {"UnityEngine.Rendering.GraphicsSettings",GraphicsSettingsView.instance.OnGUI },
                {"UnityEngine.Rendering.OnDemandRendering",OnDemandRenderingView.instance.OnGUI },
#if UNITY_2021_2_OR_NEWER
                {"UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings",UniversalRenderPipelineGlobalSettingsView.instance.OnGUI},
#endif
                {"UnityEngine.ScalableBufferManager", ScalableBufferManagerView.instance.OnGUI},
                {"UnityEngine.Screen",      ScreenView.instance.OnGUI },
                {"UnityEngine.Shader",      ShadersView.instance.OnGUI},
                {"UnityEngine.SortingLayer",SortingLayerView.instance.OnGUI },
                {"UnityEngine.Sprite",      SpritesView.instance.OnGUI},
                {"UnityEngine.SystemInfo" , SystemInfoView.instance.OnGUI},
                {"UnityEngine.Texture",     TexturesView.instance.OnGUI},
                {"UnityEngine.Time",        TimeView.instance.OnGUI},


                // 機能をここに追加していく                                              
            };

            onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
            {
                {UnityChoseKun.MessageID.ScreenPull,                ScreenView.instance.OnMessageEvent},
                {UnityChoseKun.MessageID.TimePull,                  TimeView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.GameObjectPull,            InspectorView.instance.OnMessageEvent},
                {UnityChoseKun.MessageID.ShaderPull,                ShadersView.instance.OnMessageEvent},
                {UnityChoseKun.MessageID.TexturePull,               TexturesView.instance.OnMessageEvent},
                {UnityChoseKun.MessageID.ApplicationPull,           ApplicationView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.AndroidPull,               AndroidView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.QualitySettingsPull,       QualitySettingsView.instance.OnMessageEvent},
                {UnityChoseKun.MessageID.OnDemandRenderingPull,     OnDemandRenderingView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.ScalableBufferManagerPull, ScalableBufferManagerView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.SystemInfoPull,            SystemInfoView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.SpritePull,                SpritesView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.SortingLayerPull,          SortingLayerView.instance.OnMessageEvent },
                {UnityChoseKun.MessageID.GraphicsSettingsPull,      Engine.Rendering.GraphicsSettingsKun.OnMessageEvent },
#if UNITY_2019_1_OR_NEWER                
                {UnityChoseKun.MessageID.UniversalRenderPipelinePull,Engine.Rendering.Universal.UniversalRenderPipelineKun.OnMessageEvent },
#endif

#if UNITY_2021_2_OR_NEWER
                {UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull,UniversalRenderPipelineGlobalSettingsView.instance.OnMessageEvent},
#endif
            };            
            this.titleContent = Styles.TitleContent;
            this.wantsMouseMove = true;
            this.autoRepaintOnSceneChange = true;
        }
    
        private void OnDisable()
        {            
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

            if (m_attachProfilerState != null)
            {                
                m_attachProfilerState.Dispose();
                m_attachProfilerState = null;
            }


            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(UnityChoseKun.kMsgSendPlayerToEditor, OnMessageEvent);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.UnregisterConnection(OnConnection);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.UnregisterDisconnection(OnDisConnection);

            // Deviceと接続した状態でUnityEditorを終了させるとUnityEditorがクラッシュする。
            // スクリプトリファレンスに記載されているサンプルでもOnDisableで処理しているので謎
            // しかし、呼ばないと次に接続した時に繋がらなくなる。
            // 呼ぶ必要はあるが、呼ぶとクラッシュする謎仕様。
            //  ========== OUTPUTTING STACK TRACE ==================
            //  0x00007FF774558B79(Unity) profiling::ProfilerSession::OnConnectTo
            //  0x00007FF7753BA87C(Unity) GeneralConnection::DisconnectAll
            //  0x00007FF7733DF6B6(Unity) EditorConnectionInternal_CUSTOM_DisconnectAll
            //  0x0000023E504F1115(Mono JIT Code)(wrapper managed - to - native) UnityEditor.EditorConnectionInternal:DisconnectAll()
            //  0x0000023E504F0FB3(Mono JIT Code) UnityEditor.EditorConnectionInternal:UnityEngine.IPlayerEditorConnectionNative.DisconnectAll()
            //  0x0000023E504F0E84(Mono JIT Code) UnityEditor.Networking.PlayerConnection.EditorConnection:DisconnectAll()
            //  0x0000023E504EF963(Mono JIT Code)[D:\SandBox\unitychan - crs - master\Assets\UTJ\UnityChoseKun\Editor\Scripts\PlayerViewEditorWindow.cs:141] Utj.UnityChoseKun.PlayerViewKunEditorWindow:OnDisable()
            
            //  UnityEditor.Networking.PlayerConnection.EditorConnection.instance.DisconnectAll();                                                  
        }        

               
        /// <summary>
        /// メッセージの受信CB
        /// </summary>
        /// <param name="args">メッセージデータ</param>
        private void OnMessageEvent(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
        {
            MemoryStream memoryStream = new MemoryStream(args.data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            var messageID = (UnityChoseKun.MessageID)binaryReader.ReadInt32();


            
            UnityChoseKun.Log("UnityChosekunEditorWindow.OnMessageEvent(playerID: " + args.playerId + ",message.id" + messageID + ")");            
            if (onMessageFuncDict != null && onMessageFuncDict.ContainsKey(messageID) == true)
            {
                var func = onMessageFuncDict[messageID];
                func(binaryReader);
            }

            binaryReader.Close();
            memoryStream.Close();
        }


        private void OnConnection(int playerId)
        {
            Debug.Log("connected "+ playerId);            
        }


        private void OnDisConnection(int playerId)
        {
            Debug.Log("disconect " + playerId);            
        }

    }
}

