using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{
    public class ColliderView : ComponentView
    {
        private static class Styles
        {
            public static GUIContent ColliderFoldoutContent = new GUIContent((Texture2D)EditorGUIUtility.Load("d_BoxCollider Icon"));            
            public static GUIContent ColliderToggleContent = new GUIContent("Collider");
        }

        ColliderKun colliderKun;
        bool foldout = true;



        public override void SetComponentKun(ComponentKun componentKun)
        {
            colliderKun = componentKun as ColliderKun;
        }


        public override ComponentKun GetComponentKun()
        {
            return colliderKun;
        }


        public override bool OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.BeginHorizontal();
            foldout = EditorGUILayout.Foldout(foldout, Styles.ColliderFoldoutContent);
            
            colliderKun.enabled = EditorGUILayout.ToggleLeft(Styles.ColliderToggleContent, colliderKun.enabled);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            if (foldout)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    colliderKun.isTrigger = EditorGUILayout.Toggle("Is Trigger", colliderKun.isTrigger);                    
                    EditorGUILayout.LabelField("Material", colliderKun.material);

                    colliderKun.contactOffset = EditorGUILayout.FloatField("Contact Offset", colliderKun.contactOffset);
                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Center");
                    colliderKun.boundsKun.center = EditorGUILayout.Vector3Field("", colliderKun.boundsKun.center);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Size");
                    colliderKun.boundsKun.size = EditorGUILayout.Vector3Field("", colliderKun.boundsKun.size);
                    EditorGUILayout.EndHorizontal();
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                colliderKun.dirty = true;
            }

            return false;
        }
    }
}
