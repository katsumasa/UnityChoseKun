using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        [System.Serializable]
        public class SkinnedMeshRendererView : RendererView
        {
            private static class Styles
            {
                public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_SkinnedMeshRenderer Icon");
                public static readonly GUIContent RendererName = new GUIContent("Skinned Mesh Renderer");
            }


            SkinnedMeshRendererKun skinnedMeshRendererKun
            {
                get { return rendererKun as SkinnedMeshRendererKun; }
                set { rendererKun = value; }
            }

            [SerializeField] bool m_probsFoldout = true;
            bool probsFoldout
            {
                get { return m_probsFoldout; }
                set { m_probsFoldout = value; }
            }
            [SerializeField] bool m_additionalSettingsFoldout = true;
            bool additionalSettingsFoldout
            {
                get { return m_additionalSettingsFoldout; }
                set { m_additionalSettingsFoldout = value; }
            }


            public SkinnedMeshRendererView()
            {
                componentIcon = Styles.ComponentIcon;
                foldout = true;
            }



            public void DrawBounds(RendererKun rendererKun)
            {
                var skinnedMeshRendererKun = rendererKun as SkinnedMeshRendererKun;
                EditorGUILayout.LabelField("Bounds");
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Vector3Field("Center", skinnedMeshRendererKun.bounds.center);
                EditorGUILayout.Vector3Field("Center", skinnedMeshRendererKun.bounds.extents);
                EditorGUILayout.EndVertical();

                skinnedMeshRendererKun.quality = (SkinQuality)EditorGUILayout.EnumPopup("Quality", skinnedMeshRendererKun.quality);
                skinnedMeshRendererKun.updateWhenOffscreen = EditorGUILayout.Toggle("Update When Offscreen", skinnedMeshRendererKun.updateWhenOffscreen);
                EditorGUILayout.TextField("Mesh", skinnedMeshRendererKun.sharedMesh);
            }

            public void DrawProbs(RendererKun rendererKun)
            {
                var skinnedMeshRendererKun = rendererKun as SkinnedMeshRendererKun;

                probsFoldout = EditorGUILayout.Foldout(probsFoldout, "Probs");
                if (probsFoldout)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        rendererKun.lightProbeUsage = (UnityEngine.Rendering.LightProbeUsage)EditorGUILayout.EnumPopup("Light Probes", rendererKun.lightProbeUsage);
                        rendererKun.reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)EditorGUILayout.EnumPopup("Reflection Probs", rendererKun.reflectionProbeUsage);
                        if (rendererKun.probeAnchor != null)
                        {
                            EditorGUILayout.TextField("Anchor Override", rendererKun.probeAnchor.name);
                        }
                        else
                        {
                            EditorGUILayout.TextField("Anchor Override", "None(Transform)");
                        }
                    }
                }
            }

            public void DrawAdditionalSettings(RendererKun rendererKun)
            {
                var skinnedMeshRendererKun = rendererKun as SkinnedMeshRendererKun;

                additionalSettingsFoldout = EditorGUILayout.Foldout(additionalSettingsFoldout, "Additional Settings");
                if (additionalSettingsFoldout)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        skinnedMeshRendererKun.skinnedMotionVectors = EditorGUILayout.Toggle("Skinned Motion Vectors", skinnedMeshRendererKun.skinnedMotionVectors);
                        skinnedMeshRendererKun.allowOcclusionWhenDynamic = EditorGUILayout.Toggle("Dynamic Occluson", skinnedMeshRendererKun.allowOcclusionWhenDynamic);
                    }
                }
            }



            public override bool OnGUI()
            {

                if (DrawHeader())
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUI.BeginChangeCheck();

                        DrawMaterials(rendererKun);
                        DrawBounds(rendererKun);
                        DrawLighting(rendererKun);
                        DrawProbs(rendererKun);
                        DrawAdditionalSettings(rendererKun);

                        if (EditorGUI.EndChangeCheck())
                        {
                            rendererKun.dirty = true;
                        }
                    }

                }
                EditorGUI.BeginChangeCheck();
                foreach (var materialView in materialViews)
                {
                    materialView.OnGUI();
                }
                if (EditorGUI.EndChangeCheck())
                {
                    rendererKun.dirty = true;
                }


                return true;
            }
        }
    }
}