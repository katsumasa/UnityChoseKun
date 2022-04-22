using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;
    using Engine.Rendering;
    using Engine.Rendering.Universal;
    

    namespace Editor
    {
        
        using Rendering.Universal;


        [System.Serializable]
        public class QualitySettingsView
        {
            public static class Styles
            {
                public static readonly string[] AntiAliasingLabels = { "Disable", "2x Multi Sampling", "4x Multi Sampling", "8x Multi Sampling" };
                public static readonly int[] AntiAliasingValuess = { 0, 2, 4, 8 };
                public static readonly string[] ShadowCascadeLabels = { "No Cascades", "Two Cascades", "For Cascades" };
                public static readonly int[] ShadowCascadeValues = { 1, 2, 4 };
                public static readonly string[] VSyncCountLabels = { "Don't Sync", "Every V Blank", "Every Secont V Blank" };
                public static readonly int[] VSyncCountValues = { 0, 1, 2 };
            }


            UniversalRenderPipelineAssetView mUniversalRenderPipelineAssetView;            
            [SerializeField] Vector2 mScrollPos;
            
            
            static bool mIsInit;           
            static QualitySettingsView mInstance;
            public static QualitySettingsView instance
            {
                get
                {
                    if(mInstance == null)
                    {
                        mInstance = new QualitySettingsView();                             
                    }
                    return mInstance;
                }
            }


            public void OnGUI()
            {
                if (mIsInit)
                {
                    mScrollPos = EditorGUILayout.BeginScrollView(mScrollPos);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.LabelField("Rendering", EditorStyles.boldLabel);
#if UNITY_2019_1_OR_NEWER
                    if (QualitySettingsKun.renderPipelineType == RenderPipelineAssetType.URP)
                    {
                        if(mUniversalRenderPipelineAssetView == null)
                        {
                            mUniversalRenderPipelineAssetView = new UniversalRenderPipelineAssetView();
                        }
                        mUniversalRenderPipelineAssetView.DrawContent(QualitySettingsKun.renderPipeline as UniversalRenderPipelineAssetKun);
                    }
                    else if (QualitySettingsKun.renderPipelineType == RenderPipelineAssetType.RP)
                    {
                        DrawRenderPipelineAsset(QualitySettingsKun.renderPipeline);
                    }
#endif



                    QualitySettingsKun.pixelLightCount = EditorGUILayout.IntField("Pixel Light Count", QualitySettingsKun.pixelLightCount);
                    QualitySettingsKun.anisotropicFiltering = (AnisotropicFiltering)EditorGUILayout.EnumPopup("Anisotropic Textures", QualitySettingsKun.anisotropicFiltering);
                    QualitySettingsKun.antiAliasing = EditorGUILayout.IntPopup("Anti Aliasing", QualitySettingsKun.antiAliasing, Styles.AntiAliasingLabels, Styles.AntiAliasingValuess);
                    QualitySettingsKun.softParticles = EditorGUILayout.Toggle("Soft Particles", QualitySettingsKun.softParticles);
                    QualitySettingsKun.realtimeReflectionProbes = EditorGUILayout.Toggle("Realtime Reflection Probes", QualitySettingsKun.realtimeReflectionProbes);
                    QualitySettingsKun.billboardsFaceCameraPosition = EditorGUILayout.Toggle("Billboards Face Camera Position", QualitySettingsKun.billboardsFaceCameraPosition);

                    EditorGUILayout.Space();
                    EditorGUILayout.HelpBox("To enable this feature, we need to change the value of Player -> Resolution and Presentation -> Resolution Scaling Mode from Disable to Fixed DPI.", MessageType.Info);
                    QualitySettingsKun.resolutionScalingFixedDPIFactor = EditorGUILayout.Slider("Resolution Scaling Fixed DPI Factor", QualitySettingsKun.resolutionScalingFixedDPIFactor, 0.001f, 5.0f, GUILayout.ExpandWidth(true));
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Textures", EditorStyles.boldLabel);
                    QualitySettingsKun.streamingMipmapsActive = EditorGUILayout.Toggle("Texture Streaming", QualitySettingsKun.streamingMipmapsActive);
                    if (QualitySettingsKun.streamingMipmapsActive)
                    {
                        EditorGUI.indentLevel++;
                        QualitySettingsKun.streamingMipmapsAddAllCameras = EditorGUILayout.Toggle("Add All Cameras", QualitySettingsKun.streamingMipmapsAddAllCameras);
                        QualitySettingsKun.streamingMipmapsMemoryBudget = EditorGUILayout.FloatField("Memory Buget", QualitySettingsKun.streamingMipmapsMemoryBudget);
                        QualitySettingsKun.streamingMipmapsRenderersPerFrame = EditorGUILayout.IntField("Memory Per Frame", QualitySettingsKun.streamingMipmapsRenderersPerFrame);
                        QualitySettingsKun.streamingMipmapsMaxLevelReduction = EditorGUILayout.IntField("Max Level Reduction", QualitySettingsKun.streamingMipmapsMaxLevelReduction);
                        QualitySettingsKun.streamingMipmapsMaxFileIORequests = EditorGUILayout.IntField("Max IO Requests", QualitySettingsKun.streamingMipmapsMaxFileIORequests);

                        EditorGUI.indentLevel--;
                    }

                    GUILayout.Space(10);
                    EditorGUILayout.LabelField("Particles", EditorStyles.boldLabel);
                    QualitySettingsKun.particleRaycastBudget = EditorGUILayout.IntField("Particle Raycast Budget", QualitySettingsKun.particleRaycastBudget);


                    GUILayout.Space(10);
                    EditorGUILayout.LabelField("Terrain", EditorStyles.boldLabel);

                    GUILayout.Space(10);
                    GUILayout.Label("Shadows", EditorStyles.boldLabel);
                    QualitySettingsKun.shadowmaskMode = (ShadowmaskMode)EditorGUILayout.EnumPopup("Shadowmask Mode", QualitySettingsKun.shadowmaskMode);

#if UNITY_2019_1_OR_NEWER
                    if (QualitySettingsKun.renderPipelineType != RenderPipelineAssetType.URP)
#endif
                    {
                        QualitySettingsKun.shadows = (UnityEngine.ShadowQuality)EditorGUILayout.EnumPopup("Shadows", QualitySettingsKun.shadows);
                        QualitySettingsKun.shadowResolution = (UnityEngine.ShadowResolution)EditorGUILayout.EnumPopup("Shadow Resolution", QualitySettingsKun.shadowResolution);
                        QualitySettingsKun.shadowProjection = (UnityEngine.ShadowProjection)EditorGUILayout.EnumPopup("Shadow Projection", QualitySettingsKun.shadowProjection);
                        QualitySettingsKun.shadowDistance = EditorGUILayout.FloatField("Shadow Distance", QualitySettingsKun.shadowDistance);
                        QualitySettingsKun.shadowNearPlaneOffset = EditorGUILayout.FloatField("Shadow Near Plane Offset", QualitySettingsKun.shadowNearPlaneOffset);
                        QualitySettingsKun.shadowCascades = EditorGUILayout.IntPopup("Shadow Cascades", QualitySettingsKun.shadowCascades, Styles.ShadowCascadeLabels, Styles.ShadowCascadeValues);
                    }


                    GUILayout.Space(10);
                    GUILayout.Label("Other", EditorStyles.boldLabel);
#if UNITY_2019_1_OR_NEWER
                    QualitySettingsKun.skinWeights = (SkinWeights)EditorGUILayout.EnumPopup("Skin Weights", QualitySettingsKun.skinWeights);
#endif
                    QualitySettingsKun.vSyncCount = EditorGUILayout.IntPopup("VSync Count", QualitySettingsKun.vSyncCount, Styles.VSyncCountLabels, Styles.VSyncCountValues);
                    QualitySettingsKun.lodBias = EditorGUILayout.FloatField("LOD Bias", QualitySettingsKun.lodBias);
                    QualitySettingsKun.maximumLODLevel = EditorGUILayout.IntField("Maximum LOD Level", QualitySettingsKun.maximumLODLevel);

                    QualitySettingsKun.asyncUploadTimeSlice = EditorGUILayout.IntSlider("Async Upload Time Slice", QualitySettingsKun.asyncUploadTimeSlice, 1, 33);
                    QualitySettingsKun.asyncUploadBufferSize = EditorGUILayout.IntSlider("Async Upload Buffer Size", QualitySettingsKun.asyncUploadBufferSize, 2, 512);
                    QualitySettingsKun.asyncUploadPersistentBuffer = EditorGUILayout.Toggle("Async Upload Persistent Buffer", QualitySettingsKun.asyncUploadPersistentBuffer);

#if UNITY_2019_1_OR_NEWER
                    if (EditorGUI.EndChangeCheck() || (QualitySettingsKun.renderPipeline != null && QualitySettingsKun.renderPipeline.dirty))
                    {
                        
                        UnityChoseKunEditor.SendMessage<QualitySettingsKun>(UnityChoseKun.MessageID.QualitySettingsPush, QualitySettingsKun.instance);
                        QualitySettingsKun.dirty = false;
                    }
#endif
                    EditorGUILayout.EndScrollView();
                }

                if (GUILayout.Button("Pull"))
                {
                    UnityChoseKunEditor.SendMessage<QualitySettingsKun>(UnityChoseKun.MessageID.QualitySettingsPull, null);
                }
            }



            void DrawRenderPipelineAsset(RenderPipelineAssetKun renderPipelineAssetKun)
            {
                EditorGUILayout.LabelField("Scriptable Render Pipeline Settings");
                if (renderPipelineAssetKun == null)
                {
                    EditorGUILayout.LabelField("None");
                }
                else
                {
                    EditorGUILayout.LabelField(renderPipelineAssetKun.name);
                }

                EditorGUILayout.Space(5);
            }

            public void OnMessageEvent(BinaryReader binaryReader)
            {
                QualitySettingsKun.instance.Deserialize(binaryReader);
                mIsInit = true;
            }
        }
    }
}