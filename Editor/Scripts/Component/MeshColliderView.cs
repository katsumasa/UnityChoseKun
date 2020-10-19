using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{
    public class MeshColliderView : ComponentView
    {
        private static class Styles
        {
            public static GUIContent ColliderFoldoutContent = new GUIContent((Texture2D)EditorGUIUtility.Load("d_MeshCollider Icon"));
            public static GUIContent ColliderToggleContent = new GUIContent("Mesh Collider");
        }

        MeshColliderKun colliderKun;
        bool foldout = true;



        public override void SetComponentKun(ComponentKun componentKun)
        {
            colliderKun = componentKun as MeshColliderKun;
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
                    colliderKun.convex = EditorGUILayout.Toggle("Convex", colliderKun.convex);
                    using (new EditorGUI.IndentLevelScope()) {
                        EditorGUI.BeginDisabledGroup(!colliderKun.convex);
                        colliderKun.isTrigger = EditorGUILayout.Toggle("Is Trigger", colliderKun.isTrigger);
                        EditorGUI.EndDisabledGroup();
                    }
                    colliderKun.cookingOptions = (MeshColliderCookingOptions)EditorGUILayout.EnumFlagsField("Cooking Options", colliderKun.cookingOptions);
                    EditorGUILayout.LabelField("Material", colliderKun.material);
                    EditorGUILayout.LabelField("Mesh",colliderKun.sharedMesh);                    
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                colliderKun.dirty = true;
            }

            return true;
        }
    }
}