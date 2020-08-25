using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun{
    public class BehaviourView : ComponentView
    {
        private sealed class Settings{
            private static class Styles {
                public static readonly Texture CSIcon = (Texture2D)EditorGUIUtility.Load("d_TextAsset Icon");
            }

            [SerializeField] BehaviourKun m_behaviourKun;
            public BehaviourKun behaviourKun {
                get {if(m_behaviourKun == null){m_behaviourKun = new BehaviourKun();}return m_behaviourKun;}
                set {m_behaviourKun = value;}
            }

            public Settings():this(null){}
            public Settings(string json)
            {
                if(!string.IsNullOrEmpty(json)){
                    behaviourKun = JsonUtility.FromJson<BehaviourKun>(json);                    
                }
            }            
            
            public void DrawTitle()
            {
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));                            
                var label = new GUIContent(behaviourKun.name,Styles.CSIcon);
                behaviourKun.enabled = EditorGUILayout.ToggleLeft(label,behaviourKun.enabled);                
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            }
        }

        [SerializeField] Settings m_settings;
        Settings settings {
            get{if(m_settings == null){m_settings = new Settings();}return m_settings;}
            set{m_settings = value;}
        }

        public override void SetJson(string json)
        {
            settings = new Settings(json);
        }

        // <summary> JSONを設定する</summary>
        public override string GetJson()
        {
            return JsonUtility.ToJson(settings.behaviourKun);
        }
        
        // <summary> OnGUIから呼び出す処理 </summary>
        public override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            settings.DrawTitle();
            if(EditorGUI.EndChangeCheck()){
                settings.behaviourKun.dirty = true;
            }
        }                  
    }
}
