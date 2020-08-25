using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class SkinnedMeshRendererView : RendererView
    {
        private static class Styles {
            public static readonly GUIContent Icon = new GUIContent((Texture2D)EditorGUIUtility.Load("d_SkinnedMeshRenderer Icon"));
            public static readonly GUIContent RendererName = new GUIContent("Skinned Mesh Renderer"); 
        }

        [SerializeField] SkinnedMeshRendererKun m_skinnedMeshRendererKun;
        SkinnedMeshRendererKun rendererKun {
            get{return m_skinnedMeshRendererKun;}
            set{m_skinnedMeshRendererKun = value;}
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


        public void DrawBounds(RendererKun rendererKun)
        {
            var skinnedMeshRendererKun = rendererKun as SkinnedMeshRendererKun;
            EditorGUILayout.LabelField("Bounds");
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Vector3Field("Center",skinnedMeshRendererKun.bounds.center);
            EditorGUILayout.Vector3Field("Center",skinnedMeshRendererKun.bounds.extents);
            EditorGUILayout.EndVertical();

            skinnedMeshRendererKun.quality = (SkinQuality)EditorGUILayout.EnumPopup("Quality",skinnedMeshRendererKun.quality);
            skinnedMeshRendererKun.updateWhenOffscreen  = EditorGUILayout.Toggle("Update When Offscreen",skinnedMeshRendererKun.updateWhenOffscreen);
            EditorGUILayout.TextField("Mesh",skinnedMeshRendererKun.sharedMesh);
        }

        public void DrawProbs(RendererKun rendererKun)
        {
            var skinnedMeshRendererKun = rendererKun as SkinnedMeshRendererKun;

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
            var skinnedMeshRendererKun = rendererKun as SkinnedMeshRendererKun;

            additionalSettingsFoldout = EditorGUILayout.Foldout(additionalSettingsFoldout,"Additional Settings");
            if(additionalSettingsFoldout){
                 using (new EditorGUI.IndentLevelScope()){
                    skinnedMeshRendererKun.skinnedMotionVectors = EditorGUILayout.Toggle("Skinned Motion Vectors",skinnedMeshRendererKun.skinnedMotionVectors);
                    skinnedMeshRendererKun.allowOcclusionWhenDynamic = EditorGUILayout.Toggle("Dynamic Occluson",skinnedMeshRendererKun.allowOcclusionWhenDynamic);
                 }
            }
        }

        public override void SetJson(string json)
        {
            rendererKun =  JsonUtility.FromJson<SkinnedMeshRendererKun>(json);
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
                        DrawBounds(rendererKun);
                        DrawLighting(rendererKun);
                        DrawProbs(rendererKun);
                        DrawAdditionalSettings(rendererKun);

                        if(EditorGUI.EndChangeCheck()){
                            rendererKun.dirty = true;
                        }
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
