using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MaterialView 
    {                
        public static readonly Texture2D Icon = (Texture2D)EditorGUIUtility.Load("d_Material Icon");
          


        [SerializeField] MaterialKun m_materialKun;
        public MaterialKun materialKun {
            get{if(m_materialKun == null){m_materialKun = new MaterialKun();}return m_materialKun;}
            set{m_materialKun = value;}
        }
        
        ShaderKun shader {            
            get {return materialKun.shader;}
            set {materialKun.shader= value;}            
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
            EditorGUILayout.LabelField(new GUIContent(materialKun.name,Icon));
            EditorGUILayout.BeginHorizontal();
            foldout = EditorGUILayout.Foldout(foldout,"Shader");
            if(ShaderView.shaderNames != null){
                var idx = 0;
                for(var i = 0; i < ShaderView.shaderNames.Length; i++){
                    if(ShaderView.shaderNames[i] == shader.name){
                        idx = i;
                        break;
                    }
                }
                EditorGUI.BeginChangeCheck();
                idx = EditorGUILayout.Popup("",idx,ShaderView.shaderNames);
                if(EditorGUI.EndChangeCheck()){
                    shader = ShaderView.shaderKuns[idx];
                    materialKun.dirty = true;                           
                }
            } else {
                EditorGUILayout.TextField("Shader",shader.name);
            }
            EditorGUILayout.EndHorizontal();


            return m_foldout;
        }


        void DrawShader(){
            
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
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));         
            if(DrawTitle()){
                using (new EditorGUI.IndentLevelScope()){                    
                    EditorGUI.BeginChangeCheck();
                    DrawShader();
                    DrawBody();
                    if(EditorGUI.EndChangeCheck()){
                        materialKun.dirty = true;
                    }
                }
            }
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));         
        }            
    }
}