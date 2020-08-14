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
        IConnectionState m_attachProfilerState;
        static PlayerHierarchyWindow m_window;
        public static PlayerHierarchyWindow window{
            get {return m_window;}            
            private set {m_window = value;}
        }

        SearchField m_searchField;  
        
        [SerializeField] TreeViewState treeViewState;
        HierarchyTreeView hierarchyTreeView;

        public SceneKun sceneKun {
            set {
                treeViewState = new TreeViewState();
                hierarchyTreeView = new HierarchyTreeView(treeViewState);
                hierarchyTreeView.sceneKun = value;                
                hierarchyTreeView.Reload();
            }
        }

        

        // 関数の定義

        [MenuItem("Window/UnityChoseKun/Hierarchy")]
        static void Create()
        {
            if (window == null)
            {
                window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
            }            
            window.titleContent = Styles.TitleContent;
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();

        }

        private void OnEnable() {            
            
            if(treeViewState == null){
                treeViewState = new TreeViewState();
            }
            hierarchyTreeView = new HierarchyTreeView(treeViewState);
            hierarchyTreeView.Reload();

            m_searchField = new SearchField();
            m_searchField.downOrUpArrowKeyPressed += hierarchyTreeView.SetFocusAndEnsureSelectedItem;
        }

        private void OnDisable()
        {
            
        }        

        private void OnDestroy()
        {
        }

        private void OnGUI() {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar)) {
                //GUILayout.Space (100);
                //GUILayout.FlexibleSpace();
                hierarchyTreeView.searchString = m_searchField.OnToolbarGUI(hierarchyTreeView.searchString);   
            }
            var rect = EditorGUILayout.GetControlRect(false,position.height - 16); 
            hierarchyTreeView.OnGUI(rect);

            
        }


    }
}