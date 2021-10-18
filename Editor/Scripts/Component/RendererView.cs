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
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_MeshRenderer Icon");
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
            get{
                if(m_materialViews == null)
                {
                    m_materialViews = new MaterialView[0];
                }
                return m_materialViews;
            }
            set{m_materialViews = value;}
        }

        
        public RendererKun rendererKun{
            get{return componentKun as RendererKun;}
            set{ 
                componentKun = value;                
            }
        }


        public RendererView():base()
        {
            componentIcon = Styles.ComponentIcon;
            foldout = true;
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


        protected virtual bool DrawHeader()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.BeginHorizontal();
            var iconContent = new GUIContent(mComponentIcon);
            foldout = EditorGUILayout.Foldout(foldout, iconContent);                          // Foldout & Icon

            EditorGUI.BeginChangeCheck();
            var content = new GUIContent(rendererKun.name);

            var rect = EditorGUILayout.GetControlRect();
            rendererKun.enabled = EditorGUI.ToggleLeft(new Rect(rect.x - 24, rect.y, rect.width, rect.height), content, rendererKun.enabled);
            if (EditorGUI.EndChangeCheck())
            {
                rendererKun.dirty = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

            return foldout;
        }



        public override bool OnGUI()
        {
            if (DrawHeader()) {
                using (new EditorGUI.IndentLevelScope()){
                    EditorGUI.BeginChangeCheck();
                        
                    DrawMaterials(rendererKun);
                    DrawLighting(rendererKun);
                        
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

            return true;
        }



        
    }
}
