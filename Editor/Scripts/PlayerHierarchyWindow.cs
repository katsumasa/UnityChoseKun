namespace  Utj.UnityChoseKun
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
    using UnityEditor.IMGUI.Controls;

    [System.Serializable]
    public class PlayerHierarchyWindow : EditorWindow
    {
        public static class Styles
        {                                
            public static readonly GUIContent TitleContent = new GUIContent("Player Hierarchy", (Texture2D)EditorGUIUtility.Load("d_UnityEditor.SceneHierarchyWindow"));
        }
        //
        // delegateの宣言
        //
        delegate void Task();
        delegate void OnMessageFunc(string json);
        
        // 変数の定義        
        static PlayerHierarchyWindow m_window;
        public static PlayerHierarchyWindow window{
            get {return m_window;}            
            private set {m_window = value;}
        }

        [SerializeField] SearchField m_searchField;          
        [SerializeField] TreeViewState m_treeViewState;
        TreeViewState treeViewState {
            get{if(m_treeViewState == null){m_treeViewState = new TreeViewState();}return m_treeViewState;}
            set {m_treeViewState = value;}
        }

        HierarchyTreeView m_hierarchyTreeView;        
        HierarchyTreeView hierarchyTreeView{
            get {if(m_hierarchyTreeView == null){m_hierarchyTreeView = new HierarchyTreeView(treeViewState);}return m_hierarchyTreeView;}
            set {m_hierarchyTreeView = value;}
        }
        [SerializeField] HierarchyTreeView.SelectionChangedCB m_selectionChangedCB;
        public HierarchyTreeView.SelectionChangedCB selectionChangedCB{
            private get {return m_selectionChangedCB;}
            set {
                m_selectionChangedCB = value;
                hierarchyTreeView.selectionChangeCB = value;
            }
        }

        
        [SerializeField] SceneKun m_sceneKun;

        public SceneKun sceneKun {
            set {                
                //m_sceneKun = value;
                hierarchyTreeView.sceneKun = value;
            }
            //get {return m_sceneKun;}
            //get{return hierarchyTreeView.sceneKun;}
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

        [MenuItem("Window/UnityChoseKun/Player Hierarchy")]
        public static void Create()
        {
            if (window == null)
            {
                window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
            }            
            window.titleContent = Styles.TitleContent;
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
            window.OnEnable();
                
            
        }

        public void Reload()
        {
            if(hierarchyTreeView != null){
                hierarchyTreeView.selectionChangeCB = selectionChangedCB;                
                hierarchyTreeView.Reload();
            }
            Repaint();
        }

        private void OnEnable() {            
            
            if(treeViewState == null){
                treeViewState = new TreeViewState();
            }
            hierarchyTreeView = new HierarchyTreeView(treeViewState);            
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
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar)) {                
                GUILayout.FlexibleSpace();
                hierarchyTreeView.searchString = m_searchField.OnToolbarGUI(hierarchyTreeView.searchString);   
            }
            var rect = EditorGUILayout.GetControlRect(false,position.height - 16); 
            hierarchyTreeView.OnGUI(rect);            
        }

        
    }
}