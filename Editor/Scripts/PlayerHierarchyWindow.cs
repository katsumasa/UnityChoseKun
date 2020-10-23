using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
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
using UnityEditor.IMGUI.Controls;


namespace Utj.UnityChoseKun
{        
    /// <summary>
    /// Hierarchyを表示する為のClass
    /// </summary>
    [System.Serializable]
    public class PlayerHierarchyWindow : EditorWindow
    {
        public static class Styles
        {                                
            public static readonly GUIContent TitleContent      = new GUIContent("Player Hierarchy", (Texture2D)EditorGUIUtility.LoadRequired("d_UnityEditor.SceneHierarchyWindow"));            
            public static readonly GUIContent NetworkMessages   = new GUIContent((Texture2D)EditorGUIUtility.LoadRequired("d_Profiler.NetworkMessages@2x"),"Hierarchyの情報を取得");
            public static readonly GUIContent Rename            = new GUIContent("Rename");
            public static readonly GUIContent Duplicate         = new GUIContent("Duplicate");
            public static readonly GUIContent Delete            = new GUIContent("Delete");
            public static readonly GUIContent CreateEmpty       = new GUIContent("CreateEmpty");

        }
        //
        // delegateの宣言
        //
        delegate void Task();
        delegate void OnMessageFunc(string json);
        
        

        [SerializeField] SearchField m_searchField;          
        [SerializeField] TreeViewState m_treeViewState;
        [SerializeField] HierarchyTreeView m_hierarchyTreeView;
        [SerializeField] HierarchyTreeView.SelectionChangedCB m_selectionChangedCB;
        [SerializeField] SceneKun m_sceneKun;


        IConnectionState m_attachProfilerState;

        TreeViewState treeViewState {
            get{if(m_treeViewState == null){m_treeViewState = new TreeViewState();}return m_treeViewState;}
            set {m_treeViewState = value;}
        }


        HierarchyTreeView hierarchyTreeView{
            get {
                if(m_hierarchyTreeView == null){
                    m_hierarchyTreeView = new HierarchyTreeView(treeViewState);
                }                
                return m_hierarchyTreeView;
            }

            set {m_hierarchyTreeView = value;}
        }

        
        public HierarchyTreeView.SelectionChangedCB selectionChangedCB{
            private get {return m_selectionChangedCB;}
            set {
                m_selectionChangedCB = value;
                hierarchyTreeView.selectionChangeCB = value;
            }
        }
       

        public SceneKun sceneKun {
            set {                
                hierarchyTreeView.sceneKun = value;
            }
        }

        public int lastClickedID {
            get {
                if(treeViewState == null){
                    return -1;
                }
                return treeViewState.lastClickedID;
            }
        }                


        // 関数の定義

        /// <summary>
        /// EditorWindowの生成
        /// </summary>
        [MenuItem("Window/UnityChoseKun/Player Hierarchy")]
        public static void Create()
        {            
            var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));                        
            window.titleContent = Styles.TitleContent;
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
            window.OnEnable();                            
        }


        /// <summary>
        /// 表示内容のリロード
        /// </summary>
        public void Reload()
        {
            if(hierarchyTreeView != null){
                hierarchyTreeView.selectionChangeCB = selectionChangedCB;                
                hierarchyTreeView.Reload();
            }
            Repaint();
        }


        private void OnEnable() 
        {

            m_attachProfilerState = ConnectionUtility.GetAttachToPlayerState(this);

            if (treeViewState == null){
                treeViewState = new TreeViewState();
            }
            
            Reload();
            if(m_searchField == null){
                m_searchField = new SearchField();
                m_searchField.downOrUpArrowKeyPressed += hierarchyTreeView.SetFocusAndEnsureSelectedItem;
            }            
        }


        private void OnDisable()
        {
            if (m_attachProfilerState != null)
            {
                m_attachProfilerState.Dispose();
                m_attachProfilerState = null;
            }
        }        


        private void OnDestroy()
        {
        }


        private void OnGUI() {

            var evt= Event.current;
            if(evt.type == EventType.ContextClick)
            {
                // MouseがWindow無いに入っていればMenuを表示する
                var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
                if (window.rootVisualElement.localBound.Contains(evt.mousePosition))
                {
                    var menu = new GenericMenu();
                    menu.AddDisabledItem(Styles.Rename);
                    menu.AddItem(Styles.Duplicate,false, DuplicateCB, lastClickedID);
                    menu.AddItem(Styles.Delete,false,DeleteCB,lastClickedID);
                    menu.AddItem(Styles.CreateEmpty,false,CreateEmptyCB,lastClickedID);
                    menu.AddItem(new GUIContent("3D Object/Cube"),false,CreateCubeCB, lastClickedID);
                    menu.AddItem(new GUIContent("3D Object/Sphere"),false,CreateSphereCB,lastClickedID);
                    menu.AddItem(new GUIContent("3D Object/Capsule"),false,CreateCapsuleCB,lastClickedID);
                    menu.AddItem(new GUIContent("3D Object/Clyinder"),false,CreateCylinderCB,lastClickedID);
                    menu.AddItem(new GUIContent("3D Object/Plane"),false,CreatePlaneCB,lastClickedID);
                    menu.AddDisabledItem(new GUIContent("3D Object/Quad"));
                    menu.AddDisabledItem(new GUIContent("3D Object/Text-TextMeshPro"));
                    menu.AddDisabledItem(new GUIContent("3D Object/Ragdoll"));
                    menu.AddDisabledItem(new GUIContent("3D Object/Terrain"));
                    menu.AddDisabledItem(new GUIContent("3D Object/Tree"));
                    menu.AddDisabledItem(new GUIContent("3D Object/WindZone"));
                    
                    menu.AddDisabledItem(new GUIContent("2D Object/Sprite"));
                    menu.AddDisabledItem(new GUIContent("2D Object/Sprite Mask"));
                    menu.AddDisabledItem(new GUIContent("2D Object/Tilemap"));
                    menu.AddDisabledItem(new GUIContent("2D Object/Hexagonal Point Top Tilemap"));
                    menu.AddDisabledItem(new GUIContent("2D Object/Hexagonal Flat Top Tilemap"));
                    menu.AddDisabledItem(new GUIContent("2D Object/Isometric Tilemap"));
                    menu.AddDisabledItem(new GUIContent("2D Object/Isometric Z As Y Tilemap"));

                    menu.AddDisabledItem(new GUIContent("Effect/Particle System"));
                    menu.AddDisabledItem(new GUIContent("Effect/Particle System ForceField"));
                    menu.AddDisabledItem(new GUIContent("Effect/Trail"));
                    menu.AddDisabledItem(new GUIContent("Effect/Line"));

                    menu.ShowAsContext();
                    evt.Use();
                }
            }


            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar)) {                                
                var contents = new GUIContent(Styles.NetworkMessages);
                var v2 = EditorStyles.label.CalcSize(contents);
                if (GUILayout.Button(contents, EditorStyles.toolbarButton,GUILayout.Width(v2.x+8)))
                {
                    UnityChoseKunEditor.SendMessage(UnityChoseKun.MessageID.GameObjectPull);
                }
                EditorGUILayout.Space();
                contents = new GUIContent("Connect To");
                v2 = EditorStyles.label.CalcSize(contents);
                EditorGUILayout.LabelField("Connect To",GUILayout.Width(v2.x));
                if (m_attachProfilerState != null)
                {
                    ConnectionGUILayout.AttachToPlayerDropdown(m_attachProfilerState, EditorStyles.toolbarDropDown);
                }                
                hierarchyTreeView.searchString = m_searchField.OnToolbarGUI(hierarchyTreeView.searchString);   
            }
            var playerCount = EditorConnection.instance.ConnectedPlayers.Count;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("{0} players connected.", playerCount));
            int i = 0;
            foreach (var p in EditorConnection.instance.ConnectedPlayers)
            {
                builder.AppendLine(string.Format("[{0}] - {1} {2}", i++, p.name, p.playerId));
            }
            EditorGUILayout.HelpBox(builder.ToString(), MessageType.Info);


            var rect = EditorGUILayout.GetControlRect(false,position.height - 16); 
            hierarchyTreeView.OnGUI(rect);            
        }


        /// <summary>
        /// 接続先選択用GUI
        /// </summary>
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




        /// <summary>
        /// Duplicate
        /// </summary>
        /// <param name="obj"></param>
        private void DuplicateCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.Duplicate;
            message.baseID = (int)obj;

            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.Delete;
            message.baseID = (int)obj;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="obj"></param>
        private void CreateEmptyCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.CreateEmpty;                                    
            message.baseID = (int)obj;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }

        private void CreateCubeCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.CreatePrimitive;
            message.baseID = (int)obj;
            message.primitiveType = PrimitiveType.Cube;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }

        private void CreatePlaneCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.CreatePrimitive;
            message.baseID = (int)obj;
            message.primitiveType = PrimitiveType.Plane;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }

        private void CreateSphereCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.CreatePrimitive;
            message.baseID = (int)obj;
            message.primitiveType = PrimitiveType.Sphere;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }

        private void CreateCapsuleCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.CreatePrimitive;
            message.baseID = (int)obj;
            message.primitiveType = PrimitiveType.Capsule;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }

        private void CreateCylinderCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.CreatePrimitive;
            message.baseID = (int)obj;
            message.primitiveType = PrimitiveType.Cylinder;
            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }

        
    }
}