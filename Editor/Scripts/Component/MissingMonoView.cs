using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    public class MissingMonoView : ComponentView
    {
        private static class Styles
        {
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_DefaultAsset Icon");
        }


        public MissingMonoView() : base()
        {
            
            mComponentIcon = Styles.ComponentIcon;
            foldout = true;            
        }


        public override bool OnGUI()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.BeginHorizontal();
            var iconContent = new GUIContent(mComponentIcon);
            foldout = EditorGUILayout.Foldout(foldout, iconContent);

            EditorGUI.BeginChangeCheck();
            var rect = EditorGUILayout.GetControlRect();
            var content = new GUIContent("Script");
            EditorGUI.LabelField(new Rect(rect.x - 8, rect.y, rect.width, rect.height), content);
            //EditorGUILayout.LabelField(content);

            if (EditorGUI.EndChangeCheck())
            {
                componentKun.dirty = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

            if (foldout)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Script");
                    EditorGUILayout.TextField("Missing(Mono Script)");
                    EditorGUILayout.EndHorizontal();
                }
            }
            return foldout;
        }
    }
}