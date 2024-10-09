using UnityEngine;
using UnityEditor;
//using UnityEditor.UIElements;

namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        public class RigidbodyView : ComponentView
        {
            private static class Styles
            {
                public static GUIContent Rigidbody = new GUIContent("Rigidbody", (Texture2D)EditorGUIUtility.Load("d_Rigidbody Icon"));
                public static GUIContent Mass = new GUIContent("Mass", "Rigidbody の質量");
                public static GUIContent Drag = new GUIContent("Drag", "オブジェクトの Drag (抗力)");
                public static GUIContent AngularDrag = new GUIContent("AngularDrag", "オブジェクトの Angular drag (回転抗力)");
                public static GUIContent UseGravity = new GUIContent("UseGravity", "Rigidbody が重力の影響を受けるかどうか");
                public static GUIContent IsKinematic = new GUIContent("IsKinematic", "物理演算の影響を受けるかどうか");
                public static GUIContent Interpolate = new GUIContent("Interpolate", "interpolation を使用することで、固定フレームレートで物理処理実行のエフェクトをなめらかにできます。");
                public static GUIContent CollisionDetectionMode = new GUIContent("CollisionDetection", "Rigidbody の衝突検出モード");
                public static GUIContent Constraints = new GUIContent("Constraints", "Rigidbody のシミュレーションで自由に操作できる軸をコントロールします");
                public static GUIContent FreezePosition = new GUIContent("Freeze Position");
                public static GUIContent FreezeRotation = new GUIContent("Freeze Rotation");
            }

            RigidbodyKun m_rigidbodyKun;
            public RigidbodyKun rigidbodyKun
            {
                get
                {
                    if (m_rigidbodyKun == null)
                    {
                        m_rigidbodyKun = new RigidbodyKun();
                    }
                    return m_rigidbodyKun;
                }

                set
                {
                    m_rigidbodyKun = value;
                }
            }

            [SerializeField] bool m_foldoutConstraints;
            [SerializeField] bool m_rigidbodyFoldout = true;

            public override void SetComponentKun(ComponentKun componentKun)
            {
                rigidbodyKun = componentKun as RigidbodyKun;
            }


            public override ComponentKun GetComponentKun()
            {
                return rigidbodyKun;
            }

            public override bool OnGUI()
            {
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                m_rigidbodyFoldout = EditorGUILayout.Foldout(m_rigidbodyFoldout, Styles.Rigidbody);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                if (m_rigidbodyFoldout)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        rigidbodyKun.mass = EditorGUILayout.FloatField(Styles.Mass, rigidbodyKun.mass);
#if UNITY_6000_0_OR_NEWER
                        rigidbodyKun.linearDamping = EditorGUILayout.FloatField(Styles.Drag, rigidbodyKun.linearDamping);
                        rigidbodyKun.angularDamping = EditorGUILayout.FloatField(Styles.AngularDrag, rigidbodyKun.angularDamping);
#else
                        rigidbodyKun.drag = EditorGUILayout.FloatField(Styles.Drag, rigidbodyKun.drag);
                        rigidbodyKun.angularDrag = EditorGUILayout.FloatField(Styles.AngularDrag, rigidbodyKun.angularDrag);
#endif
                        rigidbodyKun.useGravity = EditorGUILayout.Toggle(Styles.UseGravity, rigidbodyKun.useGravity);
                        rigidbodyKun.isKinematic = EditorGUILayout.Toggle(Styles.IsKinematic, rigidbodyKun.isKinematic);
                        rigidbodyKun.interpolation = (RigidbodyInterpolation)EditorGUILayout.EnumPopup(Styles.Interpolate, rigidbodyKun.interpolation);
                        rigidbodyKun.collisionDetectionMode = (CollisionDetectionMode)EditorGUILayout.EnumPopup(Styles.CollisionDetectionMode, rigidbodyKun.collisionDetectionMode);
                        m_foldoutConstraints = EditorGUILayout.Foldout(m_foldoutConstraints, Styles.Constraints);
                        if (m_foldoutConstraints)
                        {
                            using (new EditorGUI.IndentLevelScope())
                            {
                                bool px = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX) ? true : false;
                                bool py = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezePositionY) == RigidbodyConstraints.FreezePositionY) ? true : false;
                                bool pz = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ) ? true : false;
                                bool rx = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezeRotationX) == RigidbodyConstraints.FreezeRotationX) ? true : false;
                                bool ry = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezeRotationY) == RigidbodyConstraints.FreezeRotationY) ? true : false;
                                bool rz = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezeRotationZ) == RigidbodyConstraints.FreezeRotationZ) ? true : false;

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(Styles.FreezePosition);
                                px = EditorGUILayout.ToggleLeft("X", px);
                                py = EditorGUILayout.ToggleLeft("Y", py);
                                pz = EditorGUILayout.ToggleLeft("Z", pz);
                                GUILayout.FlexibleSpace();
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(Styles.FreezeRotation);
                                rx = EditorGUILayout.ToggleLeft("X", rx);
                                ry = EditorGUILayout.ToggleLeft("Y", ry);
                                rz = EditorGUILayout.ToggleLeft("Z", rz);
                                GUILayout.FlexibleSpace();
                                EditorGUILayout.EndHorizontal();

                                rigidbodyKun.constraints = RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= px ? RigidbodyConstraints.FreezePositionX : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= py ? RigidbodyConstraints.FreezePositionY : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= pz ? RigidbodyConstraints.FreezePositionZ : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= rx ? RigidbodyConstraints.FreezeRotationX : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= ry ? RigidbodyConstraints.FreezeRotationY : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= rz ? RigidbodyConstraints.FreezeRotationZ : RigidbodyConstraints.None;
                            }
                        }
                    }
                }
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

                return true;
            }

        }
    }
}