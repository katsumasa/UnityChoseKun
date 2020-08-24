using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MonoBehaviourView : BehaviourView
    {
     
        static readonly Texture Icon = (Texture2D)EditorGUIUtility.Load("d_cs Script Icon");
        [SerializeField]bool m_foldout = false;     
        bool titleFoldout {
            get{return m_foldout;}
            set {m_foldout = value;}
        }

        [SerializeField] MonoBehaviourKun m_monoBehaviourKun; 
        MonoBehaviourKun behaviourKun {
            get{return m_monoBehaviourKun;}
            set{m_monoBehaviourKun = value;}
        }

        public void DrawTitle(){
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));                            
            var label = new GUIContent(behaviourKun.name,Icon);
            behaviourKun.enabled = EditorGUILayout.ToggleLeft(label,behaviourKun.enabled);                
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
        }

        public override void SetJson(string json){
            behaviourKun = JsonUtility.FromJson<MonoBehaviourKun>(json);
        }

        public override string GetJson()
        {
            return JsonUtility.ToJson(behaviourKun);
        }

        public override void OnGUI()
        {
            DrawTitle();
        }                  
    }
}