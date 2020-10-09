using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{

    public class CapsuleColliderView : ComponentView
    {
        private static class Styles
        {
            public static GUIContent ColliderFoldoutContent = new GUIContent((Texture2D)EditorGUIUtility.Load("d_CapsuleCollider Icon"));
            public static GUIContent ColliderToggleContent = new GUIContent("CapsulColliderKun");

            public static GUIContent[] AxixContents =
            {
                new GUIContent("X-Axis"),
                new GUIContent("Y-Axis"),
                new GUIContent("Z-Axis"),
            };
        }

        CapsuleColliderKun colliderKun;
        bool foldout = true;

        

        public override void SetComponentKun(ComponentKun componentKun)
        {
            colliderKun = componentKun as CapsuleColliderKun;
        }


        public override ComponentKun GetComponentKun()
        {
            return colliderKun;
        }


        public override void OnGUI()
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
                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Center");
                    colliderKun.boundsKun.center = EditorGUILayout.Vector3Field("", colliderKun.boundsKun.center);
                    EditorGUILayout.EndHorizontal();

                    colliderKun.radius = EditorGUILayout.FloatField("Radius", colliderKun.radius);
                    colliderKun.height = EditorGUILayout.FloatField("Height", colliderKun.height);

                    colliderKun.direction = EditorGUILayout.Popup(colliderKun.direction, Styles.AxixContents);

                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                colliderKun.dirty = true;
            }
        }

    }
}
