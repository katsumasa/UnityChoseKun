using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MaterialView 
    {                
        public static readonly Texture2D MaterialIcon = (Texture2D)EditorGUIUtility.Load("d_Material Icon");
        public static readonly Texture2D TextureIcon = (Texture2D)EditorGUIUtility.Load("d_Texture Icon");
        public static readonly Texture2D ShaderIcon = (Texture2D)EditorGUIUtility.Load("d_Shader Icon");

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
            EditorGUILayout.LabelField(new GUIContent(materialKun.name,MaterialIcon));
            if(ShadersView.shaderNames == null){
                EditorGUILayout.HelpBox("If you want to change shaders,you need to Shader->Pull.",MessageType.Info);
            }
            EditorGUILayout.BeginHorizontal();
            foldout = EditorGUILayout.Foldout(foldout,new GUIContent("Shader",ShaderIcon));
            if(ShadersView.shaderNames != null){
                var idx = 0;
                for(var i = 0; i < ShadersView.shaderNames.Length; i++){
                    if(ShadersView.shaderNames[i] == shader.name){
                        idx = i;
                        break;
                    }
                }
                EditorGUI.BeginChangeCheck();
                idx = EditorGUILayout.Popup("",idx,ShadersView.shaderNames);
                if(EditorGUI.EndChangeCheck()){
                    shader = ShadersView.shaderKuns[idx];
                    materialKun.dirty = true;                           
                }
            } else {

                EditorGUILayout.TextField(shader.name);
            }
            EditorGUILayout.EndHorizontal();
            return m_foldout;
        }


        void DrawProperty(){
            #if UNITY_2019_1_OR_NEWER
            foreach(var prop in materialKun.propertys){
                if(prop.flags ==  UnityEngine.Rendering.ShaderPropertyFlags.HideInInspector){
                    continue;
                }
                var displayName = prop.displayName + "(" + prop.name + ")";
                switch(prop.flags){
                    case UnityEngine.Rendering.ShaderPropertyFlags.Normal:
                    {
                        displayName += "[Normal]";
                    }
                    break;
                    
                    case UnityEngine.Rendering.ShaderPropertyFlags.HDR:
                    {
                        displayName += "[HDR]";
                    }
                    break;

                    case UnityEngine.Rendering.ShaderPropertyFlags.Gamma:
                    {
                        displayName += "[Gamma]";
                    }
                    break;

                    case UnityEngine.Rendering.ShaderPropertyFlags.PerRendererData:
                    {
                        displayName += "[PerRenderData]";
                    }
                    break;
                }
                EditorGUI.BeginChangeCheck();
                switch(prop.type){
                    case UnityEngine.Rendering.ShaderPropertyType.Color:
                    {
                        prop.colorValue = EditorGUILayout.ColorField(displayName,prop.colorValue);
                    }
                    break;

                    case UnityEngine.Rendering.ShaderPropertyType.Range:
                    {
                       prop.floatValue = EditorGUILayout.Slider(displayName,prop.floatValue,prop.rangeLimits.x,prop.rangeLimits.y);
                    }
                    break;

                    case UnityEngine.Rendering.ShaderPropertyType.Texture:
                    {                        
                        var content = new GUIContent(displayName,TextureIcon);
                        if(TexturesView.textureNames == null){
                            EditorGUILayout.HelpBox("If you want to change Texture,you need to Texture->Pull.",MessageType.Info);

                            if((prop.textureValue == null)||string.IsNullOrEmpty(prop.textureValue.name)){
                                EditorGUILayout.LabelField(content,new GUIContent("None"));
                            } else {
                                EditorGUILayout.LabelField(content,new GUIContent(prop.textureValue.name));
                            }
                        } else {
                            int selectIdx = -1;
                            for(var i = 0; i < TexturesView.textureNames.Length; i++){
                                if(TexturesView.textureNames[i] == prop.textureValue.name){
                                    selectIdx = i;
                                    break;
                                }
                            }
                            //if(selectIdx == -1){
                            //    EditorGUILayout.LabelField(content,new GUIContent("None"));
                            //} else 
                            {
                                EditorGUI.BeginChangeCheck();
                                selectIdx = EditorGUILayout.Popup(content,selectIdx,TexturesView.textureNames);
                                if(EditorGUI.EndChangeCheck()){
                                    prop.textureValue = TexturesView.textureKuns[selectIdx];
                                    prop.dirty = true;
                                }       
                            }                     
                        }  
                        if(prop.flags != UnityEngine.Rendering.ShaderPropertyFlags.NoScaleOffset){
                            using (new EditorGUI.IndentLevelScope()){
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Tiling");
                                prop.scale = EditorGUILayout.Vector2Field("",prop.scale);
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Offset");
                                prop.offset = EditorGUILayout.Vector2Field("",prop.offset);
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }   
                    break;

                    case UnityEngine.Rendering.ShaderPropertyType.Float:
                    {
                        prop.floatValue = EditorGUILayout.FloatField(displayName,prop.floatValue);
                    }   
                    break;

                    case UnityEngine.Rendering.ShaderPropertyType.Vector:
                    {
                        prop.vectorValue = EditorGUILayout.Vector4Field(displayName,prop.vectorValue);
                    }
                    break;
                }
                if(EditorGUI.EndChangeCheck()){
                    prop.dirty = true;
                }
            }          
            #endif  
        }


        void DrawShaderKeyWords(){                        
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
                    DrawProperty();
                    DrawShaderKeyWords();
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