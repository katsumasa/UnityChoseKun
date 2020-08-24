﻿namespace  Utj.UnityChoseKun
{   
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;


    // <summary> Componentの表示を行う基底クラス </summary>
    [System.Serializable]
    public class ComponentView
    {        

        static Dictionary<ComponentKun.ComponentKunType,System.Type> componentViewTbls = new Dictionary<BehaviourKun.ComponentKunType, System.Type>{
            {ComponentKun.ComponentKunType.Transform,               typeof(TransformView)},
            {ComponentKun.ComponentKunType.Camera,                  typeof(CameraView)},
            {ComponentKun.ComponentKunType.Light,                   typeof(LightView)},
            {ComponentKun.ComponentKunType.Renderer,                typeof(RendererView)},
            {ComponentKun.ComponentKunType.MeshRenderer,            typeof(MeshRendererView)},
            {ComponentKun.ComponentKunType.SkinnedMeshMeshRenderer, typeof(SkinnedMeshRendererView)},
            {ComponentKun.ComponentKunType.Behaviour,               typeof(BehaviourView)},
            {ComponentKun.ComponentKunType.Component,               typeof(ComponentView)},
        };
        
        public static System.Type GetComponentViewSyetemType(BehaviourKun.ComponentKunType componentType)
        {        
            return componentViewTbls[componentType];
        }

        [System.Serializable]
        private class Settings
        {
            private static class Styles {
                public static readonly Texture2D Icon = (Texture2D)EditorGUIUtility.Load("d_Prefab Icon");
            }


            [SerializeField]ComponentKun m_componentKun;
            public ComponentKun componentKun{
                get{if(m_componentKun == null){m_componentKun = new ComponentKun();}return m_componentKun;}
                set{m_componentKun = value;}
            }

            public void DrawName()
            {
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));                            
                var label = new GUIContent(componentKun.name,Styles.Icon);
                EditorGUILayout.LabelField(label);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            }


        }        
        [SerializeField] Settings m_settings;
        Settings settings
        {
            get{if(m_settings == null){m_settings = new Settings();}return m_settings;}
            set{m_settings = value;}
        }

        // <summary>JSONデータを設定する</summary>
        public virtual void SetJson(string json)
        {
            settings.componentKun = JsonUtility.FromJson<ComponentKun>(json);
        }
        // <summary> JSONを設定する</summary>
        public virtual string GetJson()
        {
            return JsonUtility.ToJson(settings.componentKun);
        }
        // <summary> OnGUIから呼び出す処理 </summary>
        public virtual void OnGUI()
        {
            settings.DrawName();
        }
    }
}