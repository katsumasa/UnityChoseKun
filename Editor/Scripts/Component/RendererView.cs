using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class RendererView : ComponentView
    { 
        private static class Styles {
            public static readonly GUIContent Icon = new GUIContent((Texture2D)EditorGUIUtility.Load("d_MeshRenderer Icon"));
            public static  GUIContent RendererName = new GUIContent("Renderer");
        }

        
        [SerializeField] bool m_titleFoldout = false;
        protected bool titleFoldout{
            get{return m_titleFoldout;}
            set{m_titleFoldout = value;}
        }

        [SerializeField] bool m_materialsFoldout;
         bool materialsFoldout {
            get {return m_materialsFoldout;}
            set {m_materialsFoldout = value;}
        }
        [SerializeField] bool m_lightingFoldout = true;
        bool lightingFoldout{
            get{return m_lightingFoldout;}
            set{m_lightingFoldout = value;}
        }        

        [SerializeField]MaterialView m_materialView;
        protected MaterialView materialView
        {
            get{return m_materialView;}
            set{m_materialView = value;}
        }

        [SerializeField]MaterialView[] m_materialViews;
        protected MaterialView[] materialViews
        {
            get{return m_materialViews;}
            set{m_materialViews = value;}
        }

        [SerializeField]RendererKun m_rendererKun;
        RendererKun rendererKun{
            get{return m_rendererKun;}
            set{m_rendererKun = value;}
        }


        protected virtual bool DrawTitle(RendererKun rendererKun)
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
            
        protected virtual void DrawMaterials(RendererKun rendererKun)
        {            
            materialsFoldout = EditorGUILayout.Foldout(materialsFoldout,"Materials");
            if(materialsFoldout){
                using (new EditorGUI.IndentLevelScope()){                    
                    if(rendererKun.materials != null){   
                        EditorGUILayout.IntField("Size",rendererKun.materials.Length);                                                                 
                        foreach(var materia in rendererKun.materials)
                        {
                            EditorGUILayout.TextField("Material",materia.name);
                        }
                    } else {
                        EditorGUILayout.IntField("Size",1);
                        EditorGUILayout.TextField("Material","None(Material)");
                    }
                }
            }
        }

        protected virtual void DrawLighting(RendererKun rendererKun)
        {
            lightingFoldout = EditorGUILayout.Foldout(lightingFoldout,"Lighting");
            if(lightingFoldout){
                using (new EditorGUI.IndentLevelScope()){   
                    rendererKun.shadowCastingMode = (UnityEngine.Rendering.ShadowCastingMode)EditorGUILayout.EnumPopup("Cast Shadows",rendererKun.shadowCastingMode);
                    rendererKun.receiveShadows =  EditorGUILayout.Toggle("Receive Shadows",rendererKun.receiveShadows);
                }
            }
        }


        public override void SetComponentKun(ComponentKun componentKun)
        {
            rendererKun = componentKun as RendererKun;
            if (rendererKun.material != null)
            {
                materialView = new MaterialView();
                materialView.materialKun = rendererKun.material;
            }

            if (rendererKun.materials != null)
            {
                materialViews = new MaterialView[rendererKun.materials.Length];
                for (var i = 0; i < materialViews.Length; i++)
                {
                    materialViews[i] = new MaterialView();
                    materialViews[i].materialKun = rendererKun.materials[i];
                }
            }
        }


        public override ComponentKun GetComponentKun()
        {
            return rendererKun;
        }


        public override bool OnGUI()
        {
            if(rendererKun != null){
                if(DrawTitle(rendererKun)){
                    using (new EditorGUI.IndentLevelScope()){
                        EditorGUI.BeginChangeCheck();
                        
                        DrawMaterials(rendererKun);
                        DrawLighting(rendererKun);
                        
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

            return true;
        }



        
    }
}
