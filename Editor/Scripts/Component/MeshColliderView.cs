using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        /// <summary>
        /// MeshCplliderを表示する為のClass
        /// Programed by Katsumasa.Kimura
        /// </summary>
        public class MeshColliderView : ColliderView
        {
            private static class Styles
            {
                public static Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_MeshCollider Icon");

                public static GUIContent ColliderToggleContent = new GUIContent("Mesh Collider");
            }

            MeshColliderKun meshColliderKun
            {
                get { return colliderKun as MeshColliderKun; }
                set { colliderKun = value; }
            }


            public MeshColliderView()
            {
                componentIcon = Styles.ComponentIcon;
                foldout = true;
            }




            public override bool OnGUI()
            {
                if (DrawHeader())
                {
                    EditorGUI.BeginChangeCheck();
                    using (new EditorGUI.IndentLevelScope())
                    {
                        meshColliderKun.convex = EditorGUILayout.Toggle("Convex", meshColliderKun.convex);
                        using (new EditorGUI.IndentLevelScope())
                        {
                            EditorGUI.BeginDisabledGroup(!meshColliderKun.convex);
                            colliderKun.isTrigger = EditorGUILayout.Toggle("Is Trigger", colliderKun.isTrigger);
                            EditorGUI.EndDisabledGroup();
                        }
                        meshColliderKun.cookingOptions = (MeshColliderCookingOptions)EditorGUILayout.EnumFlagsField("Cooking Options", meshColliderKun.cookingOptions);
                        EditorGUILayout.LabelField("Material", meshColliderKun.material);
                        EditorGUILayout.LabelField("Mesh", meshColliderKun.sharedMesh);
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        colliderKun.dirty = true;
                    }
                }

                return true;
            }
        }
    }
}