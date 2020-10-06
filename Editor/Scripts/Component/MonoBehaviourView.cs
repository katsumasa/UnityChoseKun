using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
            EditorGUI.BeginChangeCheck();
            behaviourKun.enabled = EditorGUILayout.ToggleLeft(label,behaviourKun.enabled);                
            if(EditorGUI.EndChangeCheck()){
                behaviourKun.dirty = true;
            }
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
        }
#if false
        public override void SetJson(string json){
            behaviourKun = JsonUtility.FromJson<MonoBehaviourKun>(json);
        }

        public override string GetJson()
        {
            return JsonUtility.ToJson(behaviourKun);
        }
#else
        public override void SetBytes(byte[] bytes)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(bytes);

            behaviourKun = (MonoBehaviourKun)bf.Deserialize(ms);
            ms.Close();
        }

        public override byte[] GetBytes()
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();

            bf.Serialize(ms, behaviourKun);
            var bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }
#endif
        public override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawTitle();
            if(EditorGUI.EndChangeCheck()){
                behaviourKun.dirty = true;
            }
        }                  
    }
}