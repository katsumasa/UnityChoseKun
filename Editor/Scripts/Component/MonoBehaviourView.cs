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


        public void DrawTitle(){
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));                            
            var label = new GUIContent(behaviourKun.name,Icon);
            EditorGUI.BeginChangeCheck();
            behaviourKun.enabled = EditorGUILayout.ToggleLeft(label, behaviourKun.enabled);                
            if(EditorGUI.EndChangeCheck()){
                behaviourKun.dirty = true;
            }
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
        }


        public override void SetComponentKun(ComponentKun componentKun)
        {
            behaviourKun = componentKun as MonoBehaviourKun;
        }

        public override ComponentKun GetComponentKun()
        {
            return behaviourKun;
        }


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