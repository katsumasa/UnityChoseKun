using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class SkinnedMeshRendererView : ComponentView
    {
        [System.Serializable]
        public class Settings {
            private static class Styles {
                public static readonly GUIContent Name = new GUIContent((Texture2D)EditorGUIUtility.Load("d_SkinnedMeshRenderer Icon"));                
            }
            [SerializeField] SkinnedMeshRendererKun m_skinnedMeshRendererKun;
            public SkinnedMeshRendererKun skinnedMeshRendererKun{
                get{return m_skinnedMeshRendererKun;}
                set{m_skinnedMeshRendererKun = value;}
            }
            [SerializeField] bool m_skinnedMeshRendererFoldout = false;
            public bool skinnedMeshRendererFoldout{
                get{return m_skinnedMeshRendererFoldout;}
                set{m_skinnedMeshRendererFoldout = value;}
            }
            public bool enabled {
                get{return skinnedMeshRendererKun.enabled;}
                set{skinnedMeshRendererKun.enabled = value;}
            }

            public Bounds bounds {
                get{return skinnedMeshRendererKun.localBounds;}
                set{skinnedMeshRendererKun.localBounds = value;}
            }

            public SkinQuality quality {
                get{return skinnedMeshRendererKun.quality;}
                set{skinnedMeshRendererKun.quality = value;}
            }
            public bool updateWhenOffscreen {
                get{return skinnedMeshRendererKun.updateWhenOffscreen;}
                set{skinnedMeshRendererKun.updateWhenOffscreen = value;}
            }            
            public string mesh {
                get{return skinnedMeshRendererKun.sharedMesh;}
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
                get{return skinnedMeshRendererKun.shadowCastingMode;}
                set{skinnedMeshRendererKun.shadowCastingMode = value;}
            }

            bool receiveShadows{
                get{return skinnedMeshRendererKun.receiveShadows;}
                set {skinnedMeshRendererKun.receiveShadows = value;}
            }

            [SerializeField] bool m_probsFoldout = true;
            bool probsFoldout{
                get{return m_probsFoldout;}
                set{m_probsFoldout = value;}
            }

            UnityEngine.Rendering.LightProbeUsage lightProbeUsage{
                get{return skinnedMeshRendererKun.lightProbeUsage;}
                set{skinnedMeshRendererKun.lightProbeUsage = value;}
            }

            TransformKun anchorOverride {
                get{return skinnedMeshRendererKun.probeAnchor;}
            }
             UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage {
                 get{return skinnedMeshRendererKun.reflectionProbeUsage;}
                 set{skinnedMeshRendererKun.reflectionProbeUsage = value;}
             }

            [SerializeField] bool m_additionalSettingsFoldout = false;
            bool additionalSettingsFoldout{
                get{return m_additionalSettingsFoldout;}
                set{m_additionalSettingsFoldout = value;}
            }
            bool motionVectors {
                get {return skinnedMeshRendererKun.skinnedMotionVectors;}
                set {skinnedMeshRendererKun.skinnedMotionVectors = value;}
            }

            bool dynamicOcclusion {
                get{return skinnedMeshRendererKun.allowOcclusionWhenDynamic;}
                set{skinnedMeshRendererKun.allowOcclusionWhenDynamic = value;}
            }




            public bool DrawTitle()
            {                
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
                EditorGUILayout.BeginHorizontal();
                skinnedMeshRendererFoldout = EditorGUILayout.Foldout(skinnedMeshRendererFoldout,Styles.Name);                
                enabled = EditorGUILayout.ToggleLeft("Skinned Mesh Renderer",enabled);                
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));         
                return skinnedMeshRendererFoldout;           
            }

            public void DrawBounds()
            {
                EditorGUILayout.LabelField("Bounds");
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Vector3Field("Center",bounds.center);
                EditorGUILayout.Vector3Field("Center",bounds.extents);
                EditorGUILayout.EndVertical();

                quality = (SkinQuality)EditorGUILayout.EnumPopup("Quality",quality);
                updateWhenOffscreen  = EditorGUILayout.Toggle("Update When Offscreen",updateWhenOffscreen);
                EditorGUILayout.TextField("Mesh",mesh);
            }
            public void DrawMaterials()
            {
                materialsFoldout = EditorGUILayout.Foldout(materialsFoldout,"Materials");
                if(materialsFoldout){
                    using (new EditorGUI.IndentLevelScope()){
                        if(skinnedMeshRendererKun.materials != null){
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Size");
                            EditorGUILayout.TextField(skinnedMeshRendererKun.materials.Length.ToString());
                            EditorGUILayout.EndHorizontal();
                            for(var i = 0; i < skinnedMeshRendererKun.materials.Length; i++){
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Element "+i);
                                EditorGUILayout.TextField(skinnedMeshRendererKun.materials[i].name);
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
                motionVectors = EditorGUILayout.Toggle("Skinned Motion Vectors",motionVectors);
                dynamicOcclusion = EditorGUILayout.Toggle("Dynamic Occluson",dynamicOcclusion);
            }

            public Settings(){}
            public Settings(string json)
            {
                skinnedMeshRendererKun = JsonUtility.FromJson<SkinnedMeshRendererKun>(json);
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
            return JsonUtility.ToJson(settings.skinnedMeshRendererKun);
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
