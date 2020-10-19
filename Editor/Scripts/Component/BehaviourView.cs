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
        public override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            var label = new GUIContent(behaviourKun.name, mComponentIcon);
            behaviourKun.enabled = EditorGUILayout.ToggleLeft(label, behaviourKun.enabled);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            if (EditorGUI.EndChangeCheck())
            {
                behaviourKun.dirty = true;
            }
        }                  
    }
}
