using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// Transformを描画する為のClass
    /// </summary>
    public class TransformView : ComponentView
    {
    
        private static class Styles
        {
            public static Texture2D componentIcon = (Texture2D)EditorGUIUtility.Load("d_Transform Icon");
            public static GUIContent positionContent = EditorGUIUtility.TrTextContent("Position", "The local position of this GameObject relative to the parent.");
            public static GUIContent scaleContent = EditorGUIUtility.TrTextContent("Scale", "The local scaling of this GameObject relative to the parent.");
            public static GUIContent rotationContent = EditorGUIUtility.TrTextContent("Rotation", "The local rotation of this Game Object relative to the parent.");

        }

        
        public TransformKun transformKun{
            get {
                return componentKun as TransformKun;
                }
            set {componentKun = value;}
        }


        public TransformView():base()
        {
            componentIcon = Styles.componentIcon;
            foldout = true;
        }
        

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <returns></returns>
        public override bool OnGUI()
        {             
            if(base.OnGUI()){
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel(Styles.positionContent);
                    transformKun.localPosition = EditorGUILayout.Vector3Field("", transformKun.localPosition);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel(Styles.rotationContent);
                    transformKun.localRotation = EditorGUILayout.Vector3Field("",transformKun.localRotation);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel(Styles.scaleContent);
                    transformKun.localScale = EditorGUILayout.Vector3Field("",transformKun.localScale);
                    EditorGUILayout.EndHorizontal();
                   
                    if (EditorGUI.EndChangeCheck()){
                        transformKun.dirty = true;
                    }
                }
            }
            return true;
        }

        
    }
}