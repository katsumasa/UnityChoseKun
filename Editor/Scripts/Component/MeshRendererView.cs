using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun {
    [System.Serializable]
    public class MeshRendererView : ComponentView
    {
        [System.Serializable]
        public class Settings {
            private static class Styles {
                public static readonly GUIContent Name = new GUIContent((Texture2D)EditorGUIUtility.Load("d_MeshRenderer Icon"));                
            }

            [SerializeField] MeshRendererKun m_meshRendererKun;
            public MeshRendererKun meshRendererKun {
                get {if(m_meshRendererKun == null){m_meshRendererKun = new MeshRendererKun();}return m_meshRendererKun;}
                set {m_meshRendererKun = value;}
            }

            [SerializeField] bool m_meshRendererFoldout = false;
            bool meshRendererFoldout {
                get {return m_meshRendererFoldout;}
                set {m_meshRendererFoldout = value;}
            }

            public bool enabled {
                get{return meshRendererKun.enabled;}
                set{meshRendererKun.enabled = value;}
            }

            [SerializeField] bool m_materialsFoldout = true;
            bool materialsFoldout {
                get {return m_materialsFoldout;}
                set {m_materialsFoldout = value;}
            }

            [SerializeField] bool m_lightingFoldout = true;
            bool lightingFoldout{
                get{return m_lightingFoldout;}
                set{m_lightingFoldout = value;}
            }

            UnityEngine.Rendering.ShadowCastingMode castShadows {
                get{return meshRendererKun.shadowCastingMode;}
                set{meshRendererKun.shadowCastingMode = value;}
            }

            bool receiveShadows{
                get{return meshRendererKun.receiveShadows;}
                set {meshRendererKun.receiveShadows = value;}
            }

            [SerializeField] bool m_probsFoldout = true;
            bool probsFoldout{
                get{return m_probsFoldout;}
                set{m_probsFoldout = value;}
            }

            UnityEngine.Rendering.LightProbeUsage lightProbeUsage{
                get{return meshRendererKun.lightProbeUsage;}
                set{meshRendererKun.lightProbeUsage = value;}
            }

            TransformKun anchorOverride {
                get{return meshRendererKun.probeAnchor;}
            }
             UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage {
                 get{return meshRendererKun.reflectionProbeUsage;}
                 set{meshRendererKun.reflectionProbeUsage = value;}
             }

            [SerializeField] bool m_additionalSettingsFoldout = false;
            bool additionalSettingsFoldout{
                get{return m_additionalSettingsFoldout;}
                set{m_additionalSettingsFoldout = value;}
            }
            MotionVectorGenerationMode motionVectors {
                get {return meshRendererKun.motionVectorGenerationMode;}
                set {meshRendererKun.motionVectorGenerationMode = value;}
            }

            bool dynamicOcclusion {
                get{return meshRendererKun.allowOcclusionWhenDynamic;}
                set{meshRendererKun.allowOcclusionWhenDynamic = value;}
            }

            public bool DrawTitle()
            {                
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
                EditorGUILayout.BeginHorizontal();
                meshRendererFoldout = EditorGUILayout.Foldout(meshRendererFoldout,Styles.Name);                
                enabled = EditorGUILayout.ToggleLeft("Mesh Renderer",enabled);                
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));         
                return meshRendererFoldout;           
            }

            public void DrawMaterials()
            {
                materialsFoldout = EditorGUILayout.Foldout(materialsFoldout,"Materials");
                if(materialsFoldout){
                    using (new EditorGUI.IndentLevelScope()){
                        if(meshRendererKun.materials != null){
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Size");
                            EditorGUILayout.TextField(meshRendererKun.materials.Length.ToString());
                            EditorGUILayout.EndHorizontal();
                            for(var i = 0; i < meshRendererKun.materials.Length; i++){
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Element "+i);
                                EditorGUILayout.TextField(meshRendererKun.materials[i].name);
                                EditorGUILayout.EndHorizontal();
                            }
                        }else{
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Size");
                            EditorGUILayout.TextField("0");
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Element 0");
                            EditorGUILayout.TextField("None(Material)");
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }
            }
            
            public void DrawLighting()
            {
                lightingFoldout = EditorGUILayout.Foldout(lightingFoldout,"Lighting");
                if(lightingFoldout){
                    using (new EditorGUI.IndentLevelScope()){   
                        castShadows = (UnityEngine.Rendering.ShadowCastingMode)EditorGUILayout.EnumPopup("Cast Shadows",castShadows);
                        receiveShadows =  EditorGUILayout.Toggle("Receive Shadows",receiveShadows);
                    }
                }
            }

            public void DrawProbs()
            {
                probsFoldout = EditorGUILayout.Foldout(probsFoldout,"Probs");
                if(probsFoldout){
                    using (new EditorGUI.IndentLevelScope()){   
                        lightProbeUsage = (UnityEngine.Rendering.LightProbeUsage)EditorGUILayout.EnumPopup("Light Probes",lightProbeUsage);
                        reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)EditorGUILayout.EnumPopup("Reflection Probs",reflectionProbeUsage);
                        if(anchorOverride != null){
                            EditorGUILayout.TextField("Anchor Override",anchorOverride.name);
                        } else {
                            EditorGUILayout.TextField("Anchor Override","None(Transform)");
                        }                        
                    }
                }
            }

            public void DrawAdditionalSettings()
            {
                additionalSettingsFoldout = EditorGUILayout.Foldout(additionalSettingsFoldout,"Additional Settings");
                motionVectors = (MotionVectorGenerationMode)EditorGUILayout.EnumPopup("Motion Vectors",motionVectors);
                dynamicOcclusion = EditorGUILayout.Toggle("Dynamic Occluson",dynamicOcclusion);
            }
            public Settings(){}
            public Settings(string json)
            {
                meshRendererKun = JsonUtility.FromJson<MeshRendererKun>(json);
            }

        }

        [SerializeField] Settings m_settings;
        public Settings settings {
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
            return JsonUtility.ToJson(settings.meshRendererKun);
        }
        // <summary> OnGUIから呼び出す処理 </summary>
        public override void OnGUI()
        {
            if(settings != null && settings.DrawTitle()){
                using (new EditorGUI.IndentLevelScope()){
                    settings.DrawMaterials();
                    settings.DrawLighting();
                    settings.DrawProbs();
                    settings.DrawAdditionalSettings();
                }
            }
        }
    }
}