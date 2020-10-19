using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun{
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


        [SerializeField] bool mFoldout = true;


        protected BehaviourKun behaviourKun
        {
            get { return componentKun as BehaviourKun; }
            set { componentKun = value as BehaviourKun; }
        }

                        

        public BehaviourView():base()
        {
            mComponentIcon = Styles.ComponentIcon;
        }

        /// <summary> 
        /// OnGUIから呼び出す処理
        /// </summary>
        public override bool OnGUI()
        {

            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            
            EditorGUILayout.BeginHorizontal();
            var iconContent = new GUIContent(mComponentIcon);
            mFoldout = EditorGUILayout.Foldout(mFoldout, iconContent);                          // Foldout & Icon
            
            EditorGUI.BeginChangeCheck();
            var content = new GUIContent(behaviourKun.name);
            behaviourKun.enabled = EditorGUILayout.ToggleLeft(content, behaviourKun.enabled);   // CheckBox & Label
            if (EditorGUI.EndChangeCheck())
            {
                behaviourKun.dirty = true;
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

            return mFoldout;            
        }                  
    }
}
