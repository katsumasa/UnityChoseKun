namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEditor;

    
    public class ComponentEditor
    {
        private static class Styles {                        
            public static GUIContent componentPopupContent = EditorGUIUtility.TrTextContent("Select Component","検索するComponentを選択して下さい");                    
            public static GUIContent objectsPopupContent = EditorGUIUtility.TrTextContent("Select Object","GameObjectを選択してください。");
            public static GUIContent[] displayOptions = {
                new GUIContent("Camera", (Texture2D)EditorGUIUtility.Load("d_Camera Icon")),
                new GUIContent("Light", (Texture2D)EditorGUIUtility.Load("d_AreaLight Icon")),                
            };            
        }

        private readonly ComponentView[] componentViews = {
            new CameraView(),  // Camera
            new LightView(), // Light
        };
        TransformView m_componentViewTransform;
        TransformView componentViewTransform {
            get {if(m_componentViewTransform == null){m_componentViewTransform = new TransformView();}return m_componentViewTransform;}
        }
        ComponentView currentComponentView {
            get {return componentViews[selectComponentdIndex];}
        }
        int selectComponentdIndex = 0;
        int selectObjectIndex = 0;
        // Key: instanceID
        Dictionary<int,GameObjectKun> m_gameObjectKuns;
        Dictionary<int,GameObjectKun> gameObjectKuns {
            get {if(m_gameObjectKuns == null){m_gameObjectKuns = new Dictionary<int, GameObjectKun>();}return m_gameObjectKuns;}
        }                
        int [] m_instanceIDs;
        int [] instanceIDs{
            get {if(m_instanceIDs == null){m_instanceIDs = new int[1];}return m_instanceIDs;}
            set {m_instanceIDs = value;}
        }
        string [] m_objectNames;
        string [] objectNames {
            get {if(m_objectNames == null){m_objectNames = new string[1];m_objectNames[0] = "";}return m_objectNames;}
            set {m_objectNames = value;}
        }

        public void OnGUI() {

            if(gameObjectKuns.Count == 0){
                EditorGUILayout.HelpBox("Pullを実行して下さい", MessageType.Info);                
            } else {
                EditorGUILayout.BeginHorizontal();
                {                                   
                    EditorGUI.BeginChangeCheck ();
                    selectComponentdIndex = EditorGUILayout.Popup(Styles.componentPopupContent,selectComponentdIndex,Styles.displayOptions);
                    if(EditorGUI.EndChangeCheck() == true){                             
                        ReloadPopupObject();
                        ReloadComponentView();
                    }
                }
                EditorGUILayout.EndHorizontal();            
                
                if(selectObjectIndex != -1)
                {
                    EditorGUI.BeginChangeCheck();    
                    selectObjectIndex = EditorGUILayout.Popup(Styles.objectsPopupContent,selectObjectIndex,objectNames);
                    if(EditorGUI.EndChangeCheck() == true){                                                    
                        ReloadComponentView();
                    }
                }

                if(selectObjectIndex == -1){            
                    
                }else{
                    // Transform
                    componentViewTransform.OnGUI();
                    
                    // 選択中のComponent
                    currentComponentView.OnGUI();                        
                }
                EditorGUILayout.Space();

            }

            EditorGUILayout.BeginHorizontal();
            {
                if(GUILayout.Button("Pull")){   
                    UnityChoseKunEditor.SendMessage(UnityChoseKun.MessageID.GameObjectPull);                    
                }
                if(GUILayout.Button("Push")){
                    if(selectComponentdIndex < 0 || selectComponentdIndex >= instanceIDs.Length){
                        Debug.LogWarning("Invalid Idx");
                    }else{
                        var id = instanceIDs[selectObjectIndex];
                        var gameObjectKun = gameObjectKuns[id]; 
                        gameObjectKun.transformJson = componentViewTransform.GetJson();
                        for(var i = 0; i < gameObjectKun.types.Length; i++){
                            if(gameObjectKun.types[i] == (ComponentKun.ComponentType)selectComponentdIndex){
                                gameObjectKun.componentDataJsons[i] = currentComponentView.GetJson();
                                break;
                            }
                        }
                        UnityChoseKunEditor.SendMessage<GameObjectKun>(UnityChoseKun.MessageID.GameObjectPush,gameObjectKun);                                     
                    }
                }
            }
            EditorGUILayout.EndHorizontal();            
        }

        public void OnMessageEvent(string json)
        {
            Debug.Log("OnMessageEvent");
            gameObjectKuns.Clear();
            var data = JsonUtility.FromJson<SceneKun>(json);
            for(var i = 0; i < data.gameObjectKuns.Length; i++){
                gameObjectKuns.Add(data.gameObjectKuns[i].instanceID,data.gameObjectKuns[i]);
            }            
            ReloadPopupObject();
            ReloadComponentView();
        }

        void ReloadPopupObject()
        {
            var list = new List<GameObjectKun>();
            ComponentKun.ComponentType type = (ComponentKun.ComponentType)selectComponentdIndex;          
            foreach(var dict in gameObjectKuns){
                if(dict.Value.IsContainComponent(type)){
                    list.Add(dict.Value);
                }
            }                                        
            instanceIDs = new int[list.Count];
            objectNames = new string[list.Count];
            for(var i = 0; i < list.Count; i++){
                instanceIDs[i] = list[i].instanceID;
                objectNames[i] = list[i].name;
            }

            if(list.Count == 0){
                selectObjectIndex = -1;                        
            } else {
                selectObjectIndex = 0;
            }
        }

        void ReloadComponentView()
        {
            if(selectObjectIndex < 0 || selectObjectIndex >= instanceIDs.Length){
                Debug.LogWarning("selectObjectIndex Invalid");
                return;
            }
            var id = instanceIDs[selectObjectIndex];
            var gameObjectKun = gameObjectKuns[id];                    
            componentViewTransform.SetJson(gameObjectKun.transformJson);                
            for(var i = 0; i < gameObjectKun.types.Length; i++){
                if(gameObjectKun.types[i] == (ComponentKun.ComponentType)selectComponentdIndex){                            
                    currentComponentView.SetJson(gameObjectKun.componentDataJsons[i]);
                    break;
                }
            }                    
        }

    }
}