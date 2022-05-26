using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using Utj.UnityChoseKun.Editor.Rendering;
using Utj.UnityChoseKun.Editor.Rendering.Universal;
using Utj.UnityChoseKun.Engine;


namespace  Utj.UnityChoseKun.Editor
{
    
    

    /// <summary>
    /// Componentの表示を行う基底クラス 
    /// Programed by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    public class ComponentView
    {        
        // [NOTE] ComponentKun型のClassが追加されたらここに追加する
        // ComponentKunType,ComponentViewのSystem.Type
        static Dictionary<ComponentKun.ComponentKunType,System.Type> componentViewTbls = new Dictionary<BehaviourKun.ComponentKunType, System.Type>{
            {ComponentKun.ComponentKunType.Transform,               typeof(TransformView)},
            {ComponentKun.ComponentKunType.Camera,                  typeof(CameraView)},
            {ComponentKun.ComponentKunType.Light,                   typeof(LightView)},

            {ComponentKun.ComponentKunType.SpriteRenderer,typeof(SpriteRendererView) },
            
            {ComponentKun.ComponentKunType.SkinnedMeshMeshRenderer, typeof(SkinnedMeshRendererView)},
            {ComponentKun.ComponentKunType.MeshRenderer,            typeof(MeshRendererView)},
            {ComponentKun.ComponentKunType.Renderer,                typeof(RendererView)},

            {ComponentKun.ComponentKunType.Rigidbody,               typeof(RigidbodyView)},

            {ComponentKun.ComponentKunType.MeshCollider,            typeof(MeshColliderView) },
            {ComponentKun.ComponentKunType.CapsuleCollider,         typeof(CapsuleColliderView) },
            {ComponentKun.ComponentKunType.Collider,                typeof(ColliderView) },

            {ComponentKun.ComponentKunType.Animator,                typeof(AnimatorView) },
            {ComponentKun.ComponentKunType.ParticleSystem,          typeof(ParticleSystemView) },

            {ComponentKun.ComponentKunType.Canvas,                  typeof(CanvasView) },

            // ===

            {ComponentKun.ComponentKunType.Volume,typeof(VolumeView) },
            {ComponentKun.ComponentKunType.UniversalAdditionalCameraData,   typeof(UniversalAdditionalCameraDataView) },
            {ComponentKun.ComponentKunType.UniversalAdditionalLightData,    typeof(UniversalAdditionalLightDataView)},
            {ComponentKun.ComponentKunType.MonoBehaviour,                   typeof(MonoBehaviourView)},
            {ComponentKun.ComponentKunType.Behaviour,                       typeof(BehaviourView)},
            {ComponentKun.ComponentKunType.Component,                       typeof(ComponentView)},
            
            {ComponentKun.ComponentKunType.MissingMono,            typeof(MissingMonoView) },

          
        };
        

        public static System.Type GetComponentViewSyetemType(BehaviourKun.ComponentKunType componentType)
        {
            System.Type type;

            if(componentViewTbls.TryGetValue(componentType, out type))
            {
                return type;
            }
            return typeof(ComponentView);            
        }

        private static class Styles
        {
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_Prefab Icon");
        }


        [SerializeField] ComponentKun m_componentKun;
        protected ComponentKun componentKun
        {
            get { if (m_componentKun == null) { m_componentKun = new ComponentKun(); } return m_componentKun; }
            set { m_componentKun = value; }
        }

        /// <summary>
        /// Class名の横に表示されるアイコン
        /// </summary>
        protected Texture2D mComponentIcon;
        protected Texture2D componentIcon
        {
            get { return mComponentIcon;}
            set { mComponentIcon = value;}
        }
        
        /// <summary>
        /// Foldoutの値
        /// </summary>
        [SerializeField] bool mFoldout;
        protected bool foldout
        {
            get { return mFoldout; }
            set { mFoldout = value; }
        }

        /// <summary>
        /// ComponentKunを設定する
        /// </summary>
        /// <param name="componentKun">設定されるComponentKun</param>
        public virtual void SetComponentKun(ComponentKun componentKun)
        {
            this.componentKun = componentKun;
        }


        /// <summary>
        /// ComponentKunを取得する
        /// </summary>
        /// <returns>ComponentKun</returns>
        public virtual ComponentKun GetComponentKun()
        {
            return componentKun;
        }


        public ComponentView()
        {
            componentIcon = Styles.ComponentIcon;

            foldout = false;
        }


        /// <summary>
        /// OnGUIから呼び出す処理
        /// </summary>
        public virtual bool OnGUI()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.BeginHorizontal();                        
            var iconContent = new GUIContent(mComponentIcon);
            foldout = EditorGUILayout.Foldout(foldout, iconContent);
                         
            EditorGUI.BeginChangeCheck();
            var rect = EditorGUILayout.GetControlRect();
            var content = new GUIContent(componentKun.name);
            EditorGUI.LabelField(new Rect(rect.x - 8,rect.y,rect.width,rect.height),content);
            //EditorGUILayout.LabelField(content);

            if (EditorGUI.EndChangeCheck()){
                componentKun.dirty = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            return foldout;
        }
    }
}