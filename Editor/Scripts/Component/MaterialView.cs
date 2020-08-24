using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MaterialView 
    {                
        [SerializeField] MaterialKun m_materialKun;
        public MaterialKun materialKun {
            get{if(m_materialKun == null){m_materialKun = new MaterialKun();}return m_materialKun;}
            set{m_materialKun = value;}
        }

        string shader {
            get{if(materialKun.shader!=null){return materialKun.shader.name;}return "None";}
        }            
        int renderQueue {
            get{return materialKun.renderQueue;}            
        }
        bool enableInstancing {
            get{return materialKun.enableInstancing;}
        }
        string[] shaderKeyWords {
            get{return materialKun.shaderKeywords;}                    
        }

        [SerializeField] bool m_foldout;
        bool foldout{
            get{return m_foldout;}
            set{m_foldout = value;}
        }

        [SerializeField] bool m_shaderKeywordFoldout = false;
        bool shaderKeywordFoldout{
            get{return m_shaderKeywordFoldout;}
            set{m_shaderKeywordFoldout = value;}
        }        


        bool DrawTitle()
        {
            foldout = EditorGUILayout.Foldout(foldout,materialKun.name);
            return m_foldout;
        }


        void DrawShader(){
            EditorGUILayout.TextField("Shader",shader);
            using (new EditorGUI.IndentLevelScope()){
                shaderKeywordFoldout = EditorGUILayout.Foldout(shaderKeywordFoldout,"Shader Key Words");
                if(shaderKeywordFoldout){
                    using (new EditorGUI.IndentLevelScope()){
                        if(shaderKeyWords != null && shaderKeyWords.Length > 0){                                                    
                            foreach(var keyword in shaderKeyWords){
                                EditorGUILayout.TextField(keyword);
                            }
                        } else {
                            EditorGUILayout.TextField("None");
                        }
                    }
                }
            }
        }


        void DrawBody()
        {
            EditorGUILayout.IntField("Render Queue",renderQueue);   
            EditorGUILayout.Toggle("Enable GUP Instancing",enableInstancing);
        }                            


        public  void OnGUI(){
            if(DrawTitle()){
                using (new EditorGUI.IndentLevelScope()){
                    DrawShader();
                    DrawBody();
                }
            }
        }            
    }
}