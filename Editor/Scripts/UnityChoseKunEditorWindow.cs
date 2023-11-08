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

using UTJ.RemoteConnect;
using UTJ.RemoteConnect.Editor;

namespace Utj.UnityChoseKun.Editor
{    
    using Utj.UnityChoseKun.Editor.Rendering;
    using Utj.UnityChoseKun.Editor.Rendering.Universal;

    /// <summary>
    /// UnityChoseKunのEditorWindow
    /// </summary>
    public class UnityChoseKunEditorWindow : RemoteConnectEditorWindow
    {
        private static class Styles
        {                    
            public static readonly GUIContent TitleContent = new GUIContent("Player Inspector", (Texture2D)EditorGUIUtility.Load("d_UnityEditor.InspectorWindow"));
        }

        static readonly Dictionary<string, Action> onGUILayoutFuncDict = new Dictionary<string, Action>()
        {
            {"Inspector",   InspectorView.instance.OnGUI},
            {"UnityEngine.Application", ApplicationView.instance.OnGUI},
            { "UnityEngine.Android.Permission",     AndroidView.instance.OnGUI},
            { "UnityEngine.Component",   ObjectCounterView.instance.OnGUI },
            { "UnityEngine.QualitySettings", QualitySettingsView.instance.OnGUI },
            { "UnityEngine.Rendering.GraphicsSettings",GraphicsSettingsView.instance.OnGUI },
            { "UnityEngine.Rendering.OnDemandRendering",OnDemandRenderingView.instance.OnGUI },
#if UNITY_2021_2_OR_NEWER
            {"UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings",UniversalRenderPipelineGlobalSettingsView.instance.OnGUI},
#endif
            { "UnityEngine.ScalableBufferManager", ScalableBufferManagerView.instance.OnGUI},
            { "UnityEngine.Screen",      ScreenView.instance.OnGUI },
            { "UnityEngine.Shader",      ShadersView.instance.OnGUI},
            { "UnityEngine.SortingLayer",SortingLayerView.instance.OnGUI },
            { "UnityEngine.Sprite",      SpritesView.instance.OnGUI},
            { "UnityEngine.SystemInfo" , SystemInfoView.instance.OnGUI},
            { "UnityEngine.Texture",     TexturesView.instance.OnGUI},
            { "UnityEngine.Time",        TimeView.instance.OnGUI},
            // 機能をここに追加していく                                              
        };

        static readonly Dictionary<UnityChoseKun.MessageID, OnMessageFunc> onMessageFuncDict = new Dictionary<UnityChoseKun.MessageID, OnMessageFunc>()
        {
            { UnityChoseKun.MessageID.ScreenPull,                ScreenView.instance.OnMessageEvent},
            { UnityChoseKun.MessageID.TimePull,                  TimeView.instance.OnMessageEvent},
            { UnityChoseKun.MessageID.GameObjectPull,            InspectorView.instance.OnMessageEvent},
            { UnityChoseKun.MessageID.ShaderPull,                ShadersView.instance.OnMessageEvent},
            { UnityChoseKun.MessageID.TexturePull,               TexturesView.instance.OnMessageEvent},
            { UnityChoseKun.MessageID.ApplicationPull,           ApplicationView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.AndroidPull,               AndroidView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.QualitySettingsPull,       QualitySettingsView.instance.OnMessageEvent},
            { UnityChoseKun.MessageID.OnDemandRenderingPull,     OnDemandRenderingView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.ScalableBufferManagerPull, ScalableBufferManagerView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.SystemInfoPull,            SystemInfoView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.SpritePull,                SpritesView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.SortingLayerPull,          SortingLayerView.instance.OnMessageEvent },
            { UnityChoseKun.MessageID.GraphicsSettingsPull,      Engine.Rendering.GraphicsSettingsKun.OnMessageEvent },
#if UNITY_2019_1_OR_NEWER                
            { UnityChoseKun.MessageID.UniversalRenderPipelinePull,Engine.Rendering.Universal.UniversalRenderPipelineKun.OnMessageEvent },
#endif
#if UNITY_2021_2_OR_NEWER
            {UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull,UniversalRenderPipelineGlobalSettingsView.instance.OnMessageEvent},
#endif
        };

        static UnityChoseKunEditorWindow m_Instance;
        public static UnityChoseKunEditorWindow Instance
        {
            get
            {
                m_Instance = (UnityChoseKunEditorWindow)EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));
                return m_Instance;
            }
        }


        delegate void Task();
        delegate void OnMessageFunc(BinaryReader binaryReader);
        
                
        int                m_ToolBarIdx = 0;                                
        Vector2            m_ScrollPos;


        [MenuItem("Window/UTJ/UnityChoseKun/Player Inspector")]
        static void Inite()
        {
            m_Instance = (UnityChoseKunEditorWindow)EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));
            m_Instance.titleContent = Styles.TitleContent;
            m_Instance.wantsMouseMove = true;
            m_Instance.autoRepaintOnSceneChange = true;
            m_Instance.Show();            
        }

        
        /// <summary>
        /// 描画処理
        /// </summary>
        private void OnGUI()
        {
            ConnectionTargetSelectionDropdown();            
            EditorGUILayout.Space();                        
            var texts = onGUILayoutFuncDict.Keys.ToArray();
            m_ToolBarIdx = EditorGUILayout.Popup(m_ToolBarIdx, texts);
            EditorGUILayout.Space();
            var key = texts[m_ToolBarIdx];                
            Action onGUI;
            onGUILayoutFuncDict.TryGetValue(key,out onGUI);
            if (onGUI != null)
            {
                m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);
                onGUI();
                EditorGUILayout.EndScrollView();
            }                        
        }

        
        protected override void OnEnable()
        {
            kMsgSendEditorToPlayer = UnityChoseKun.kMsgSendEditorToPlayer;
            kMsgSendPlayerToEditor = UnityChoseKun.kMsgSendPlayerToEditor;
            eventMessageCB = EventMessageReciveCB;
            base.OnEnable();
            
            this.titleContent = Styles.TitleContent;
            this.wantsMouseMove = true;
            this.autoRepaintOnSceneChange = true;            
        }
    
        protected override void OnDisable()
        {            
            base.OnDisable();
        }        

               
        void EventMessageReciveCB(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    var messageID = (UnityChoseKun.MessageID)binaryReader.ReadInt32();
                    if (onMessageFuncDict != null && onMessageFuncDict.ContainsKey(messageID) == true)
                    {
                        var func = onMessageFuncDict[messageID];
                        func(binaryReader);
                    }
                }
            }            
        }        


        public static void Send(byte[] bytes)
        {
            Instance.SendRemoteMessage(bytes);
        }
    }
}

