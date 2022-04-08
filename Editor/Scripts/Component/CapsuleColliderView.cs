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
        /// CapuselCplliderを描画する為のClass
        /// </summary>
        public class CapsuleColliderView : ColliderView
        {
            private static class Styles
            {
                public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_CapsuleCollider Icon");
                public static readonly GUIContent[] AxixContents =
                {
                    new GUIContent("X-Axis"),
                    new GUIContent("Y-Axis"),
                    new GUIContent("Z-Axis"),
                };
            }


            CapsuleColliderKun capsuleColliderKun
            {
                get { return componentKun as CapsuleColliderKun; }
                set { componentKun = value; }
            }



            public CapsuleColliderView() : base()
            {

                componentIcon = Styles.ComponentIcon;
            }


            public override bool OnGUI()
            {
                if (DrawHeader())
                {
                    EditorGUI.BeginChangeCheck();
                    using (new EditorGUI.IndentLevelScope())
                    {
                        colliderKun.isTrigger = EditorGUILayout.Toggle("Is Trigger", colliderKun.isTrigger);
                        EditorGUILayout.LabelField("Material", colliderKun.material);

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Center");
                        colliderKun.boundsKun.center = EditorGUILayout.Vector3Field("", colliderKun.boundsKun.center);
                        EditorGUILayout.EndHorizontal();

                        capsuleColliderKun.radius = EditorGUILayout.FloatField("Radius", capsuleColliderKun.radius);
                        capsuleColliderKun.height = EditorGUILayout.FloatField("Height", capsuleColliderKun.height);

                        capsuleColliderKun.direction = EditorGUILayout.Popup(capsuleColliderKun.direction, Styles.AxixContents);

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