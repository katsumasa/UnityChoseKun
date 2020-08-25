using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun {
    // MeshRenderer
    [System.Serializable]
    public class MeshRendererView : RendererView
    {
        private static class Styles {
            public static readonly GUIContent Icon = new GUIContent((Texture2D)EditorGUIUtility.Load("d_MeshRenderer Icon"));
            public static readonly GUIContent RendererName = new GUIContent("Mesh Renderer");         
        }

        [SerializeField]MeshRendererKun m_meshRendererKun;
        MeshRendererKun rendererKun{
            get{return m_meshRendererKun;}
            set{m_meshRendererKun = value;}
        }
        [SerializeField] bool m_probsFoldout = true;
        bool probsFoldout{
            get{return m_probsFoldout;}
            set{m_probsFoldout = value;}
        }
        [SerializeField] bool m_additionalSettingsFoldout = true;
        bool additionalSettingsFoldout{
            get{return m_additionalSettingsFoldout;}
            set{m_additionalSettingsFoldout = value;}
        }
    

        protected override bool DrawTitle(RendererKun rendererKun)
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
            EditorGUILayout.BeginHorizontal();

            titleFoldout = EditorGUILayout.Foldout(titleFoldout,Styles.Icon);                
            
            EditorGUI.BeginChangeCheck();
            rendererKun.enabled = EditorGUILayout.ToggleLeft(Styles.RendererName,rendererKun.enabled);                
            if(EditorGUI.EndChangeCheck()){
                rendererKun.dirty = true;
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));         
            
            return titleFoldout;
        }


        public void DrawProbs(RendererKun rendererKun)
        {
            var meshRendererKun = rendererKun as MeshRendererKun;

            probsFoldout = EditorGUILayout.Foldout(probsFoldout,"Probs");
            if(probsFoldout){
                using (new EditorGUI.IndentLevelScope()){   
                    rendererKun.lightProbeUsage = (UnityEngine.Rendering.LightProbeUsage)EditorGUILayout.EnumPopup("Light Probes",rendererKun.lightProbeUsage);
                    rendererKun.reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)EditorGUILayout.EnumPopup("Reflection Probs",rendererKun.reflectionProbeUsage);
                    if(rendererKun.probeAnchor != null){
                        EditorGUILayout.TextField("Anchor Override",rendererKun.probeAnchor.name);
                    } else {
                        EditorGUILayout.TextField("Anchor Override","None(Transform)");
                    }                        
                }
            }
        }

        public void DrawAdditionalSettings(RendererKun rendererKun)        
        {
            var meshRendererKun = rendererKun as MeshRendererKun;
            additionalSettingsFoldout = EditorGUILayout.Foldout(additionalSettingsFoldout,"Additional Settings");
            if(additionalSettingsFoldout){
                using (new EditorGUI.IndentLevelScope()){
                    meshRendererKun.motionVectorGenerationMode = (MotionVectorGenerationMode)EditorGUILayout.EnumPopup("Motion Vectors",meshRendererKun.motionVectorGenerationMode);
                    meshRendererKun.allowOcclusionWhenDynamic = EditorGUILayout.Toggle("Dynamic Occluson",meshRendererKun.allowOcclusionWhenDynamic);
                }
            }
        }


        public override void SetJson(string json)
        {
            rendererKun =  JsonUtility.FromJson<MeshRendererKun>(json);
            if(rendererKun.material != null){
                materialView = new MaterialView();
                materialView.materialKun =  rendererKun.material;
            }
            
            if(rendererKun.materials != null){
                materialViews = new MaterialView[rendererKun.materials.Length];
                for(var i = 0; i < materialViews.Length; i++){
                    materialViews[i] = new MaterialView();
                    materialViews[i].materialKun = rendererKun.materials[i];
                }
            }
        }

        public override string GetJson()
        {            
            return JsonUtility.ToJson(rendererKun);
        }

        public override void OnGUI()
        {
            if(rendererKun != null){
                if(DrawTitle(rendererKun)){
                    using (new EditorGUI.IndentLevelScope()){
                        EditorGUI.BeginChangeCheck();
                        DrawMaterials(rendererKun);
                        DrawLighting(rendererKun);
                        DrawProbs(rendererKun);
                        DrawAdditionalSettings(rendererKun);
                        if(EditorGUI.EndChangeCheck()){
                            rendererKun.dirty = true;
                        }
                    }
                }
                EditorGUI.BeginChangeCheck();
                foreach(var materialView in materialViews)
                {
                    materialView.OnGUI();
                } 
                if(EditorGUI.EndChangeCheck()){
                    rendererKun.dirty = true;
                }
            }
        }        
    }    
}