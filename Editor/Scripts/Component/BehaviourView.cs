using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        /// <summary>
        /// Behaviourを編集するクラス
        /// Programed by Katsumasa.Kimura
        /// </summary>
        public class BehaviourView : ComponentView
        {
            private static class Styles
            {
                public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_TextAsset Icon");
            }


            protected BehaviourKun behaviourKun
            {
                get { return componentKun as BehaviourKun; }
                set { componentKun = value as BehaviourKun; }
            }



            public BehaviourView() : base()
            {
                mComponentIcon = Styles.ComponentIcon;
                foldout = true;
            }

            /// <summary> 
            /// OnGUIから呼び出す処理
            /// </summary>
            public override bool OnGUI()
            {
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                EditorGUILayout.BeginHorizontal();
                var iconContent = new GUIContent(mComponentIcon);
                foldout = EditorGUILayout.Foldout(foldout, iconContent);                          // Foldout & Icon

                EditorGUI.BeginChangeCheck();
                var content = new GUIContent(behaviourKun.name);

                var rect = EditorGUILayout.GetControlRect();
                behaviourKun.enabled = EditorGUI.ToggleLeft(new Rect(rect.x - 24, rect.y, rect.width, rect.height), content, behaviourKun.enabled);
                if (EditorGUI.EndChangeCheck())
                {
                    behaviourKun.dirty = true;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

                return foldout;
            }
        }
    }
}