using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun {
        
    
    public class ShadersView
    {
        // Member変数の定義
        [SerializeField] static ShaderKun[] m_shaderKuns;
        public static ShaderKun[] shaderKuns {
            get{return m_shaderKuns;}
            private set{m_shaderKuns = value;}
        }

        [SerializeField] static string[] m_shaderNames;
        public static string[] shaderNames {
            get {return m_shaderNames;}
            private set {m_shaderNames = value;}
        }

        [SerializeField] Vector2 m_scrollPos;
        Vector2 scrollPos {
            get {return m_scrollPos;}
            set {m_scrollPos = value;}
        }

        public void OnGUI() 
        {
            int cnt = 0;
            if(shaderKuns!=null){
                cnt = shaderKuns.Length;
                EditorGUILayout.LabelField("Shader List("+cnt+")");
            }else {
                EditorGUILayout.HelpBox("Please Pull Request.",MessageType.Info);
            }

            using (new EditorGUI.IndentLevelScope()){
                if(shaderKuns != null){
                    EditorGUILayout.BeginScrollView(scrollPos);
                    for(var i = 0; i < shaderKuns.Length; i++){
                        EditorGUILayout.LabelField(shaderKuns[i].name);
                    }
                    EditorGUILayout.EndScrollView();
                }            
            }
            if (GUILayout.Button("Pull"))
            {                
                UnityChoseKunEditor.SendMessage<ShaderKunPacket>(UnityChoseKun.MessageID.ShaderPull,null);
            }
        }

        public void OnMessageEvent(string json)
        {
            var shaderKunPacket = JsonUtility.FromJson<ShaderKunPacket>(json);
            shaderKuns = shaderKunPacket.shaderKuns;
            shaderNames = new string[shaderKuns.Length];
            for(var i = 0; i < shaderKuns.Length; i++)
            {
                shaderNames[i] = shaderKuns[i].name;
            }
        }

    }
}