namespace Utj.UnityChoseKun {

    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using UnityEditor;

    public class TransformView : ComponentView
    {
    
        private static class Styles
        {
            public static GUIContent transformContent = new GUIContent("Transform", (Texture2D)EditorGUIUtility.Load("d_Transform Icon"));
            public static GUIContent positionContent = EditorGUIUtility.TrTextContent("Position", "The local position of this GameObject relative to the parent.");
            public static GUIContent scaleContent = EditorGUIUtility.TrTextContent("Scale", "The local scaling of this GameObject relative to the parent.");
            public static GUIContent rotationContent = EditorGUIUtility.TrTextContent("Rotation", "The local rotation of this Game Object relative to the parent.");

        }

        TransformKun m_transformKun;
        public TransformKun transformKun{
            get {
                    if(m_transformKun == null){
                        m_transformKun = new TransformKun();
                    }
                    return m_transformKun;
                }
            set {m_transformKun = value;}
        }
        
        
        bool foldout = true;


        public override void SetComponentKun(ComponentKun componentKun)
        {
            transformKun = componentKun as TransformKun;
        }


        public override ComponentKun GetComponentKun()
        {
            return transformKun;
        }


        public override void OnGUI()
        {        

            EditorGUILayout.Space();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            foldout = EditorGUILayout.Foldout(foldout,Styles.transformContent);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.Space();
            if(foldout){
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

                    if(EditorGUI.EndChangeCheck()){
                        transformKun.dirty = true;
                    }
                }
            }
        }

        
    }
}