using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
#if false
        public override void SetJson(string json)
        {
            settings = new Settings(json);
        }

        // <summary> JSONを設定する</summary>
        public override string GetJson()
        {
            return JsonUtility.ToJson(settings.behaviourKun);
        }
#else
        public override void SetBytes(byte[] bytes)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(bytes);

            settings.behaviourKun = (BehaviourKun)bf.Deserialize(ms);
            ms.Close();
        }

        public override byte[] GetBytes()
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();

            bf.Serialize(ms, settings.behaviourKun);
            var bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }
#endif
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
