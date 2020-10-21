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
            public static readonly GUIContent TitleContent = new GUIContent("Player Hierarchy", (Texture2D)EditorGUIUtility.Load("d_UnityEditor.SceneHierarchyWindow"));

            public static readonly GUIContent Rename        = new GUIContent("Rename");
            public static readonly GUIContent Duplicate     = new GUIContent("Duplicate");
            public static readonly GUIContent Delete        = new GUIContent("Delete");
            public static readonly GUIContent CreateEmpty   = new GUIContent("CreateEmpty");

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
                    menu.AddItem(Styles.Rename,false, RenameCB, lastClickedID);
                    menu.AddItem(Styles.Duplicate,false, DuplicateCB, lastClickedID);
                    menu.AddItem(Styles.Delete,false,DeleteCB,lastClickedID);
                    menu.AddItem(Styles.CreateEmpty,false,CreateEmptyCB,lastClickedID);
                    menu.ShowAsContext();
                    evt.Use();
                }
            }


            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar)) {                
                GUILayout.FlexibleSpace();
                hierarchyTreeView.searchString = m_searchField.OnToolbarGUI(hierarchyTreeView.searchString);   
            }
            var rect = EditorGUILayout.GetControlRect(false,position.height - 16); 
            hierarchyTreeView.OnGUI(rect);            
        }

        private void RenameCB(object obj)
        {
            
        }


        private void DuplicateCB(object obj)
        {
            var message = new HierarchyMessage();
            message.messageID = HierarchyMessage.MessageID.Duplicate;
            message.baseID = (int)obj;

            UnityChoseKunEditor.SendMessage<HierarchyMessage>(UnityChoseKun.MessageID.HierarchyPush, message);
        }


        private void DeleteCB(object obj)
        {

        }

        private void CreateEmptyCB(object obj)
        {

        }

        private void MenuCallback(object obj)
        {
            int id = (int)obj;
            Debug.Log("Select ID " + id);
        }
        
    }
}