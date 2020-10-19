namespace  Utj.UnityChoseKun
{   
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using UnityEditor;


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
            {ComponentKun.ComponentKunType.SkinnedMeshMeshRenderer, typeof(SkinnedMeshRendererView)},
            {ComponentKun.ComponentKunType.MeshRenderer,            typeof(MeshRendererView)},
            {ComponentKun.ComponentKunType.Renderer,                typeof(RendererView)},

            {ComponentKun.ComponentKunType.Rigidbody,               typeof(RigidbodyView)},

            {ComponentKun.ComponentKunType.MeshCollider,            typeof(MeshColliderView) },
            {ComponentKun.ComponentKunType.CapsuleCollider,         typeof(CapsuleColliderView) },
            {ComponentKun.ComponentKunType.Collider,                typeof(ColliderView) },

            {ComponentKun.ComponentKunType.Animator,                typeof(AnimatorView) },
            {ComponentKun.ComponentKunType.ParticleSystem,          typeof(ParticleSystemView) },


            {ComponentKun.ComponentKunType.MonoBehaviour,           typeof(MonoBehaviourView)},
            {ComponentKun.ComponentKunType.Behaviour,               typeof(BehaviourView)},
            {ComponentKun.ComponentKunType.Component,               typeof(ComponentView)},
        };
        

        public static System.Type GetComponentViewSyetemType(BehaviourKun.ComponentKunType componentType)
        {        
            return componentViewTbls[componentType];
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

        protected Texture2D mComponentIcon;


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
            mComponentIcon = Styles.ComponentIcon;
        }


        /// <summary>
        /// OnGUIから呼び出す処理
        /// </summary>
        public virtual bool OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
            var content = new GUIContent(componentKun.name, mComponentIcon);
            EditorGUILayout.LabelField(content);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
            if (EditorGUI.EndChangeCheck()){
                componentKun.dirty = true;
            }

            return true;
        }
    }
}