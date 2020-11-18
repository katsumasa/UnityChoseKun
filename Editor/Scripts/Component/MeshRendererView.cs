using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun {    
    /// <summary>
    /// MeshRendererを描画する為のClass
    /// Prpgramed by Katsumasamura
    /// </summary>
    [System.Serializable]
    public class MeshRendererView : RendererView
    {
        private static class Styles {
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_MeshRenderer Icon");
        }

        
        MeshRendererKun meshRendererKunn{
            get{return rendererKun as MeshRendererKun;}
            set{rendererKun = value;}
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
    

        public MeshRendererView() : base()
        {
            componentIcon = Styles.ComponentIcon;
            foldout = true;
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


        

        public override bool OnGUI()
        {
            if (DrawHeader()) {            
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
                
                EditorGUI.BeginChangeCheck();
                foreach(var materialView in materialViews)
                {
                    materialView.OnGUI();
                } 
                if(EditorGUI.EndChangeCheck()){
                    rendererKun.dirty = true;
                }
            }

            return true;
        }        
    }    
}