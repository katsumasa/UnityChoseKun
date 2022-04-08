using UnityEditor;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        /// <summary>
        /// Animatorを表示する為のClass
        /// Programed by Katsuma.Kimura
        /// </summary>
        public class AnimatorView : BehaviourView
        {
            static class Styles
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
                set { componentKun = value as AnimatorKun; }
            }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public AnimatorView() : base()
            {
                componentIcon = Styles.ComponentIcon;
                foldout = true;
            }


            /// <summary>
            /// 描画処理
            /// </summary>
            /// <returns></returns>
            public override bool OnGUI()
            {
                if (base.OnGUI())
                {
                    using (new EditorGUI.IndentLevelScope())
                    {

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.LabelField(Styles.Controller, new GUIContent(animatorKun.runtimeAnimatorController, Styles.ControllerIcon));
                        EditorGUILayout.LabelField(Styles.Avator, new GUIContent(animatorKun.avatar, Styles.AvatorIcon));
                        animatorKun.applyRootMotion = EditorGUILayout.Toggle(Styles.ApplyRootMotion, animatorKun.applyRootMotion);
                        EditorGUILayout.EnumPopup(Styles.UpdateMode, animatorKun.updateMode);
                        animatorKun.cullingMode = (AnimatorCullingMode)EditorGUILayout.EnumPopup(Styles.CullingMode, animatorKun.cullingMode);
                        if (EditorGUI.EndChangeCheck())
                        {
                            animatorKun.dirty = true;
                        }
                    }
                }
                return true;
            }
        }
    }
}