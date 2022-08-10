//#define ENABLE_PROFILER_STATES

using UnityEngine;
using UnityEditor;
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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
#endif

#if UNITY_2019_1_OR_NEWER
#else
using UnityEngine.Experimental.UIElements;
using UnityEditor.Experimental.UIElements;
#endif

using UnityEditor.IMGUI.Controls;





namespace Utj.UnityChoseKun
{
    using Engine;
    using Engine.Rendering;
    using Engine.Rendering.Universal;


    namespace Editor
    {
        /// <summary>
        /// Hierarchyを表示する為のClass
        /// </summary>
        [System.Serializable]
        public class PlayerHierarchyWindow : EditorWindow
        {
            public static class Styles
            {
                public static readonly GUIContent TitleContent = new GUIContent("Player Hierarchy", (Texture2D)EditorGUIUtility.Load("d_UnityEditor.SceneHierarchyWindow"));
#if UNITY_2019_1_OR_NEWER
                public static readonly GUIContent NetworkMessages = new GUIContent("Reload", (Texture2D)EditorGUIUtility.Load("d_Profiler.NetworkMessages@2x"), "Hierarchyの情報を取得");
#else
            public static readonly GUIContent NetworkMessages = new GUIContent("Reload","Hierarchyの情報を取得");
#endif
                public static readonly GUIContent Rename = new GUIContent("Rename");
                public static readonly GUIContent Duplicate = new GUIContent("Duplicate");
                public static readonly GUIContent Delete = new GUIContent("Delete");
                public static readonly GUIContent CreateEmpty = new GUIContent("CreateEmpty");
            }


            //
            // delegateの宣言
            //
            delegate void Task();
            delegate void OnMessageFunc(string json);


            [SerializeField] SearchField m_searchField;
            [SerializeField] TreeViewState m_treeViewState;
            HierarchyTreeView m_hierarchyTreeView;
            [SerializeField] HierarchyTreeView.SelectionChangedCB m_selectionChangedCB;


#if ENABLE_PROFILER_STATES
        IConnectionState m_attachProfilerState;
#endif

            TreeViewState treeViewState
            {
                get { if (m_treeViewState == null) { m_treeViewState = new TreeViewState(); } return m_treeViewState; }
                set { m_treeViewState = value; }
            }


            HierarchyTreeView hierarchyTreeView
            {
                get
                {
                    if (m_hierarchyTreeView == null)
                    {
                        m_hierarchyTreeView = new HierarchyTreeView(treeViewState);
                    }
                    return m_hierarchyTreeView;
                }

                set { m_hierarchyTreeView = value; }
            }


            public HierarchyTreeView.SelectionChangedCB selectionChangedCB
            {
                private get { return m_selectionChangedCB; }
                set
                {
                    m_selectionChangedCB = value;
                    hierarchyTreeView.selectionChangeCB = value;
                }
            }

            public SceneManagerKun sceneManagerKun
            {
                set
                {
                    hierarchyTreeView.sceneManagerKun = value;
                }

                get
                {
                    if (hierarchyTreeView.sceneManagerKun == null)
                    {
                        hierarchyTreeView.sceneManagerKun = new SceneManagerKun(false);
                    }
                    return hierarchyTreeView.sceneManagerKun;
                }
            }



            public int lastClickedID
            {
                get
                {
                    if (treeViewState == null)
                    {
                        return -1;
                    }
                    return treeViewState.lastClickedID;
                }
            }


            // 関数の定義

            /// <summary>
            /// EditorWindowの生成
            /// </summary>
            [MenuItem("Window/UTJ/UnityChoseKun/Player Hierarchy")]
            public static void Create()
            {
                var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
                window.titleContent = Styles.TitleContent;
                window.wantsMouseMove = true;
                window.autoRepaintOnSceneChange = true;
                
                window.OnEnable();

                window.Show();
            }


            /// <summary>
            /// 表示内容のリロード
            /// </summary>
            public void Reload()
            {
                //if(GraphicsSettingsKun.instance == null)
                {
                    Debug.Log("Reload");
                    UnityChoseKunEditor.SendMessage<LayerMaskKun>(UnityChoseKun.MessageID.LayerMaskPull, null);
                    UnityChoseKunEditor.SendMessage<GraphicsSettingsKun>(UnityChoseKun.MessageID.GraphicsSettingsPull, null);
#if UNITY_2021_2_OR_NEWER
                    UnityChoseKunEditor.SendMessage<UniversalRenderPipelineGlobalSettingsKun>(UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull,null);
#endif
                    UnityChoseKunEditor.SendMessage<UniversalRenderPipelineKun>(UnityChoseKun.MessageID.UniversalRenderPipelinePull, null);
                }
                

                if (hierarchyTreeView != null)
                {
                    hierarchyTreeView.selectionChangeCB = selectionChangedCB;
                    hierarchyTreeView.Reload();
                }
                Repaint();
            }


            private void OnEnable()
            {
                var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));

                this.titleContent = Styles.TitleContent;
                this.wantsMouseMove = true;
                this.autoRepaintOnSceneChange = true;


#if ENABLE_PROFILER_STATES
            m_attachProfilerState = ConnectionUtility.GetAttachToPlayerState(this);
#endif

                if (m_treeViewState == null)
                {
                    m_treeViewState = new TreeViewState();
                }
                m_hierarchyTreeView = new HierarchyTreeView(m_treeViewState);


                Reload();
                if (m_searchField == null)
                {
                    m_searchField = new SearchField();
                    m_searchField.downOrUpArrowKeyPressed += hierarchyTreeView.SetFocusAndEnsureSelectedItem;
                }
            }


            private void OnDisable()
            {
#if ENABLE_PROFILER_STATES
            m_attachProfilerState.Dispose();
#endif
            }


            private void OnDestroy()
            {
            }


            private void OnGUI()
            {

                var evt = Event.current;
                if (evt.type == EventType.ContextClick)
                {
                    // MouseがWindow無いに入っていればMenuを表示する
                    var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
#if UNITY_2019_1_OR_NEWER
                    if (window.rootVisualElement.localBound.Contains(evt.mousePosition))
#else
                
                if (this.GetRootVisualContainer().localBound.Contains(evt.mousePosition))
#endif
                    {
                        var menu = new GenericMenu();
                        menu.AddDisabledItem(Styles.Rename);
                        menu.AddItem(Styles.Duplicate, false, DuplicateCB, lastClickedID);
                        menu.AddItem(Styles.Delete, false, DeleteCB, lastClickedID);
                        menu.AddItem(Styles.CreateEmpty, false, CreateEmptyCB, lastClickedID);
                        menu.AddItem(new GUIContent("3D Object/Cube"), false, CreateCubeCB, lastClickedID);
                        menu.AddItem(new GUIContent("3D Object/Sphere"), false, CreateSphereCB, lastClickedID);
                        menu.AddItem(new GUIContent("3D Object/Capsule"), false, CreateCapsuleCB, lastClickedID);
                        menu.AddItem(new GUIContent("3D Object/Clyinder"), false, CreateCylinderCB, lastClickedID);
                        menu.AddItem(new GUIContent("3D Object/Plane"), false, CreatePlaneCB, lastClickedID);
                        menu.AddDisabledItem(new GUIContent("3D Object/Quad"));
                        menu.AddDisabledItem(new GUIContent("3D Object/Text-TextMeshPro"));
                        menu.AddDisabledItem(new GUIContent("3D Object/Ragdoll"));
                        menu.AddDisabledItem(new GUIContent("3D Object/Terrain"));
                        menu.AddDisabledItem(new GUIContent("3D Object/Tree"));
                        menu.AddDisabledItem(new GUIContent("3D Object/WindZone"));

                        menu.AddItem(new GUIContent("2D Object/Sprite"), false, CreateSpriteCB, lastClickedID);
                        menu.AddDisabledItem(new GUIContent("2D Object/Sprite Mask"));
                        menu.AddDisabledItem(new GUIContent("2D Object/Tilemap"));
                        menu.AddDisabledItem(new GUIContent("2D Object/Hexagonal Point Top Tilemap"));
                        menu.AddDisabledItem(new GUIContent("2D Object/Hexagonal Flat Top Tilemap"));
                        menu.AddDisabledItem(new GUIContent("2D Object/Isometric Tilemap"));
                        menu.AddDisabledItem(new GUIContent("2D Object/Isometric Z As Y Tilemap"));

                        menu.AddItem(new GUIContent("Effect/Particle System"), false, CreateParticleSystem, lastClickedID);
                        menu.AddDisabledItem(new GUIContent("Effect/Particle System ForceField"));
                        menu.AddDisabledItem(new GUIContent("Effect/Trail"));
                        menu.AddDisabledItem(new GUIContent("Effect/Line"));

                        menu.ShowAsContext();
                        evt.Use();
                    }
                }


                using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
                {
                    var contents = new GUIContent(Styles.NetworkMessages);
                    var v2 = EditorStyles.label.CalcSize(contents);
                    if (GUILayout.Button(contents, EditorStyles.toolbarButton, GUILayout.Width(v2.x + 8)))
                    {
                        // ChoseKunWindowを開く
                        EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));
                        UnityChoseKunEditor.SendMessage(UnityChoseKun.MessageID.GameObjectPull);
                    }
                    EditorGUILayout.Space();

#if ENABLE_PROFILER_STATES
                contents = new GUIContent("Connect To");
                v2 = EditorStyles.label.CalcSize(contents);
                EditorGUILayout.LabelField(contents, GUILayout.Width(v2.x));
                if (m_attachProfilerState != null){
                    ConnectionGUILayout.AttachToPlayerDropdown(m_attachProfilerState, EditorStyles.toolbarDropDown);
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
                }
#endif

                    hierarchyTreeView.searchString = m_searchField.OnToolbarGUI(hierarchyTreeView.searchString);
                }

#if ENABLE_PROFILER_STATES
            var playerCount = EditorConnection.instance.ConnectedPlayers.Count;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("{0} players connected.", playerCount));
            int i = 0;
            foreach (var p in EditorConnection.instance.ConnectedPlayers)
            {
                builder.AppendLine(string.Format("[{0}] - {1} {2}", i++, p.name, p.playerId));
            }
            EditorGUILayout.HelpBox(builder.ToString(), MessageType.Info);
#endif

                var rect = EditorGUILayout.GetControlRect(false, position.height - 16);
                hierarchyTreeView.OnGUI(rect);
            }

#if ENABLE_PROFILER_STATES
        /// <summary>
        /// 接続先選択用GUI
        /// </summary>
        private void GUILayoutConnect()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Connect To");                        
            ConnectionGUILayout.AttachToPlayerDropdown(m_attachProfilerState, EditorStyles.toolbarDropDown);            
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
#endif

            private void CreateObjectCommon(HierarchyMessage.MessageID messageID, int instanceID)
            {
                // ChoseKunWindowを開く
                EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));

                var message = new HierarchyMessage();
                message.messageID = messageID;
                message.baseID = instanceID;
                UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
            }


            /// <summary>
            /// Duplicate
            /// </summary>
            /// <param name="obj"></param>
            private void DuplicateCB(object obj)
            {
                CreateObjectCommon(HierarchyMessage.MessageID.Duplicate, (int)obj);
            }


            /// <summary>
            /// Delete
            /// </summary>
            /// <param name="obj"></param>
            private void DeleteCB(object obj)
            {

                CreateObjectCommon(HierarchyMessage.MessageID.Delete, (int)obj);
            }


            /// <summary>
            /// Create
            /// </summary>
            /// <param name="obj"></param>
            private void CreateEmptyCB(object obj)
            {
                CreateObjectCommon(HierarchyMessage.MessageID.CreateEmpty, (int)obj);
            }


            private void CreateParticleSystem(object obj)
            {
                CreateClassCommon(typeof(ParticleSystem), (int)obj);
            }


            private void CreateClassCommon(System.Type type, int instanceID)
            {
                var message = new HierarchyMessage();
                message.messageID = HierarchyMessage.MessageID.CreateClass;
                message.baseID = instanceID;
                message.type = type;
                UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
            }


            /// <summary>
            /// Create Primitiveの共通関数
            /// </summary>
            /// <param name="messageID"></param>
            /// <param name="instanceID"></param>
            /// <param name="primitiveType"></param>
            private void CreatePrimitiveCommon(HierarchyMessage.MessageID messageID, int instanceID, PrimitiveType primitiveType)
            {
                // ChoseKunWindowを開く
                EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));


                var message = new HierarchyMessage();
                message.messageID = HierarchyMessage.MessageID.CreatePrimitive;
                message.baseID = instanceID;
                message.primitiveType = primitiveType;
                UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
            }


            private void CreateCubeCB(object obj)
            {
                CreatePrimitiveCommon(HierarchyMessage.MessageID.CreatePrimitive, (int)obj, PrimitiveType.Cube);
            }


            private void CreatePlaneCB(object obj)
            {
                CreatePrimitiveCommon(HierarchyMessage.MessageID.CreatePrimitive, (int)obj, PrimitiveType.Plane);
            }


            private void CreateSphereCB(object obj)
            {
                CreatePrimitiveCommon(HierarchyMessage.MessageID.CreatePrimitive, (int)obj, PrimitiveType.Sphere);
            }


            private void CreateCapsuleCB(object obj)
            {
                CreatePrimitiveCommon(HierarchyMessage.MessageID.CreatePrimitive, (int)obj, PrimitiveType.Capsule);
            }


            private void CreateCylinderCB(object obj)
            {
                CreatePrimitiveCommon(HierarchyMessage.MessageID.CreatePrimitive, (int)obj, PrimitiveType.Cylinder);
            }

            private void CreateSpriteCB(object obj)
            {
                CreateClassCommon(typeof(SpriteRenderer), (int)obj);
            }
        }
    }
}