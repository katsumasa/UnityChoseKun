using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{        
    /// <summary>
    /// 
    /// </summary>
    public class InspectorView
    {
        public sealed class Settings{
            public  static class Styles {
                public static GUIContent gameObject = new GUIContent("", (Texture2D)EditorGUIUtility.Load("d_GameObject Icon"));
                public static GUIContent AutoPush = new GUIContent("Auto","Not Require Push.");
            }
            [SerializeField] bool isDraw;
            [SerializeField] bool isActive;
            [SerializeField] string name;
            [SerializeField] bool isStatic;
            [SerializeField] string tag;
            [SerializeField] int layer;
            

            public Settings(){}
            public void Set(GameObjectKun gameObjectKun){
                if(gameObjectKun == null){
                    isDraw = false;
                } else {
                    isDraw = true;
                    isActive = gameObjectKun.activeSelf;
                    name = gameObjectKun.name;
                    isStatic = gameObjectKun.isStatic;
                    tag = gameObjectKun.tag;
                    layer = gameObjectKun.layer;
                }
            }

            public void Writeback(GameObjectKun gameObjectKun){
                gameObjectKun.activeSelf = isActive;
                gameObjectKun.name = name;
                gameObjectKun.isStatic = isStatic;
                gameObjectKun.tag = tag;
                gameObjectKun.layer = layer;
            }

            public void DrawGameObject(){
                if(isDraw == false){
                    return;
                }
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
                EditorGUILayout.BeginHorizontal();                     
                Styles.gameObject.text = name;
                isActive = EditorGUILayout.ToggleLeft(Styles.gameObject,isActive);                
                GUILayout.FlexibleSpace();
                isStatic = EditorGUILayout.ToggleLeft("Static",isStatic);
                EditorGUILayout.EndHorizontal();
                                
                EditorGUI.indentLevel += 1;
                EditorGUILayout.BeginHorizontal();
                tag = EditorGUILayout.TagField("Tag",tag);
                layer = EditorGUILayout.LayerField("Layer",layer);
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel --;
            }            
        }



        [SerializeField] Settings m_settings;
        [SerializeField] private List<ComponentView> m_componentViews;
        [SerializeField] Dictionary<int, GameObjectKun> m_gameObjectKuns;
        [SerializeField] int m_selectGameObujectKunID;
        [SerializeField] SceneKun m_sceneKun;


        SceneKun sceneKun
        {
            get
            {
                if(m_sceneKun == null)
                {
                    m_sceneKun = new SceneKun();
                }
                return m_sceneKun;
            }

            set
            {
                m_sceneKun = value;
            }
        }

        Settings settings {
            get{if(m_settings == null){m_settings = new Settings();}return m_settings;}
            set{m_settings = value;}
        }


        List<ComponentView> componentViews{
            get {
                if(m_componentViews == null){
                    m_componentViews = new List<ComponentView>();
                }
                return m_componentViews;
            }
            set {m_componentViews = value;}
        }                
        

        Dictionary<int,GameObjectKun> gameObjectKuns {
            get {if(m_gameObjectKuns == null){m_gameObjectKuns = new Dictionary<int, GameObjectKun>();}return m_gameObjectKuns;}
        }                
        
        


        public InspectorView() {
            if (!EditorWindow.HasOpenInstances<PlayerHierarchyWindow>())
            {
                PlayerHierarchyWindow.Create();
            }
            var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
            if (window != null){
                window.selectionChangedCB = SelectionChangedCB;
            }
            m_selectGameObujectKunID = -1;
        }


        void BuildComponentView(GameObjectKun gameObjectKun)
        {                        
            componentViews.Clear();            
            if(gameObjectKun!=null) {
                m_selectGameObujectKunID = gameObjectKun.instanceID;                
                for(var i = 0; i < gameObjectKun.componentKuns.Length; i++)
                {
                    var type = ComponentView.GetComponentViewSyetemType(gameObjectKun.componentKunTypes[i]);
                    var componentView = System.Activator.CreateInstance(type) as ComponentView;
                    componentView.SetComponentKun(gameObjectKun.componentKuns[i]);                    
                    componentViews.Add(componentView);
                }
            }else{
                m_selectGameObujectKunID = -1;
            }
        }

        
        public void OnGUI() {
            var isChange = false;

                
            EditorGUI.BeginChangeCheck();
            settings.DrawGameObject();
            if(EditorGUI.EndChangeCheck()){
                if(m_gameObjectKuns.ContainsKey(m_selectGameObujectKunID)){
                    var gameObjectKun = m_gameObjectKuns[m_selectGameObujectKunID];
                    gameObjectKun.dirty = true;
                }
            }

            EditorGUI.BeginChangeCheck();
            foreach (var componentView in componentViews)
            {
                componentView.OnGUI();
            }
            if (EditorGUI.EndChangeCheck())
            {
                isChange = true;
            }
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(4));




            if (sceneKun != null)
            {
                if (isChange)
                {
                    if (m_gameObjectKuns.ContainsKey(m_selectGameObujectKunID))
                    {
                        var gameObjectKun = m_gameObjectKuns[m_selectGameObujectKunID];
                        settings.Writeback(gameObjectKun);
                        UnityChoseKunEditor.SendMessage<GameObjectKun>(UnityChoseKun.MessageID.GameObjectPush, gameObjectKun);
                        gameObjectKun.ResetDirty();                        
                    }
                }          
            }

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Button(new GUIContent("Add Component"));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void OnMessageEvent(BinaryReader binaryReader)
        {            
            gameObjectKuns.Clear();
            sceneKun.Deserialize(binaryReader);
            
            for(var i = 0; i < sceneKun.gameObjectKuns.Length; i++){
                gameObjectKuns.Add(sceneKun.gameObjectKuns[i].instanceID,sceneKun.gameObjectKuns[i]);
            }

            if (!EditorWindow.HasOpenInstances<PlayerHierarchyWindow>())
            {
                PlayerHierarchyWindow.Create();
            }
            
            var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
            if (window != null)
            {
                window.selectionChangedCB = SelectionChangedCB;
                window.sceneKun = sceneKun;
                window.Reload();
            }
            
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedIds"></param>
        void SelectionChangedCB(IList<int> selectedIds)
        {
            var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));
            var id = window.lastClickedID;
            if(gameObjectKuns.ContainsKey(id)){
                var gameObjectKun = gameObjectKuns[id];
                settings.Set(gameObjectKun);
                BuildComponentView(gameObjectKun);   
            } else {
                settings.Set(null);
                BuildComponentView(null);
            }            

            var choseKunEditorWindow = (UnityChoseKunEditorWindow)EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));
            if (choseKunEditorWindow != null){
                choseKunEditorWindow.Repaint();
            }
        }
    }
}