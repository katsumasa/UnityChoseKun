using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utj.UnityChoseKun.Engine.Rendering;
using Utj.UnityChoseKun.Engine;

namespace Utj.UnityChoseKun.Editor.Rendering
{
    /// <summary>
    /// VolumeをInspectorに表示する為のClass
    /// </summary>
    public class VolumeView : MonoBehaviourView
    {
        public static class Styles {
            public static GUIContent Mode       = new GUIContent("Mode");
            public static GUIContent Weight     = new GUIContent("Weight");
            public static GUIContent Priority   = new GUIContent("Priority");
            public static GUIContent Profile    = new GUIContent("Profile");
            // Volumeのアイコンはビルドインではなくパッケージに含まれているので保留
            //public static readonly Texture2D VolumeIcon = (Texture2D)EditorGUIUtility.Load("Volume");
        }


        enum Mode
        {
            Local,
            Grobal,
        }

        /// <summary>
        /// 表示するVolumeKun
        /// </summary>
        VolumeKun volumeKun
        {
            get { return componentKun as VolumeKun; }
        }

        /// <summary>
        /// VolumeComponentViews
        /// </summary>
        VolumeComponentView[] m_volumeComponentViews;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VolumeView():base()
        {
            //componentIcon = Styles.VolumeIcon;
            foldout = true;            
        }

                
        public override void SetComponentKun(ComponentKun componentKun)
        {
            base.SetComponentKun(componentKun);
            m_volumeComponentViews = new VolumeComponentView[volumeKun.profile.components.Length];
            for (var i = 0; i < volumeKun.profile.components.Length; i++)
            {
                m_volumeComponentViews[i] = new VolumeComponentView(volumeKun.profile.components[i]);
            }
        }


        public override bool OnGUI()
        {
            if(base.OnGUI())
            {
                using(new EditorGUI.IndentLevelScope())
                {
                    var mode = volumeKun.isGlobal ? Mode.Grobal : Mode.Local;
                    EditorGUI.BeginChangeCheck();
                    mode = (Mode)EditorGUILayout.EnumPopup(Styles.Mode, mode);
                    if (EditorGUI.EndChangeCheck())
                    {
                        volumeKun.isGlobal = (mode == Mode.Grobal) ? true : false;
                    }

                    var weight = volumeKun.weight;
                    EditorGUI.BeginChangeCheck();
                    weight = EditorGUILayout.Slider(Styles.Weight, weight, 0f, 1.0f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        volumeKun.weight = weight;
                    }

                    var priority = volumeKun.priority;
                    EditorGUI.BeginChangeCheck();
                    priority = EditorGUILayout.FloatField(Styles.Priority,priority);
                    if (EditorGUI.EndChangeCheck())
                    {
                        volumeKun.priority = priority;
                    }

                    if (volumeKun.profile == null || volumeKun.profile.components == null || volumeKun.profile.components.Length == 0)
                    {
                        EditorGUILayout.LabelField(Styles.Profile, new GUIContent("None"));
                    }
                    else
                    {
                        EditorGUILayout.LabelField(Styles.Profile, new GUIContent(volumeKun.profile.name));
                        foreach (var volumeComponentView in m_volumeComponentViews)
                        {
                            volumeComponentView.OnGUI();
                        }
                    }
                }
            }
            return true;
        }

    }
}
