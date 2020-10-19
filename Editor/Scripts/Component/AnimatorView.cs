using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utj.UnityChoseKun {
    public class AnimatorView : BehaviourView
    {
        static class Style
        {
            
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_Animator Icon");
            public static readonly Texture2D ControllerIcon = (Texture2D)EditorGUIUtility.Load("d_AnimatorController Icon");
            public static readonly Texture2D AvatorIcon = (Texture2D)EditorGUIUtility.Load("d_Avatar Icon");
            public static GUIContent Controller = new GUIContent("Controller");
            public static GUIContent Avator = new GUIContent("Avator");
            public static GUIContent ApplyRootMotion = new GUIContent("Apply Root Motion");
            public static GUIContent UpdateMode = new GUIContent("Update Mode");
            public static GUIContent CullingMode = new GUIContent("Culling Mode");
        }


        AnimatorKun animatorKun
        {
            get { return componentKun as AnimatorKun; }
            set { componentKun = value as AnimatorKun;}
        }


        public AnimatorView()
        {
            mComponentIcon = Style.ComponentIcon;
        }



        public override void OnGUI()
        {
            base.OnGUI();

            using (new EditorGUI.IndentLevelScope())
            {

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.LabelField(Style.Controller, new GUIContent(animatorKun.runtimeAnimatorController,Style.ControllerIcon));
                EditorGUILayout.LabelField(Style.Avator, new GUIContent(animatorKun.avatar,Style.AvatorIcon));
                animatorKun.applyRootMotion = EditorGUILayout.Toggle(Style.ApplyRootMotion, animatorKun.applyRootMotion);
                EditorGUILayout.EnumPopup(Style.UpdateMode, animatorKun.updateMode);
                animatorKun.cullingMode = (AnimatorCullingMode)EditorGUILayout.EnumPopup(Style.CullingMode, animatorKun.cullingMode);
                if (EditorGUI.EndChangeCheck())
                {
                    animatorKun.dirty = true;
                }
            }
        }
    }
}