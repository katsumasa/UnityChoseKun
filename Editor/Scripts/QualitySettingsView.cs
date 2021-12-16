
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class QualitySettingsView
    {
        public static class Styles
        {
            public static readonly string[] AntiAliasingLabels = { "Disable", "2x Multi Sampling", "4x Multi Sampling", "8x Multi Sampling" };
            public static readonly int[] AntiAliasingValuess = { 0, 2, 4, 8 };
            public static readonly string[] ShadowCascadeLabels = { "No Cascades", "Two Cascades","For Cascades" };
            public static readonly int[] ShadowCascadeValues = {1,2,4};
            public static readonly string[] VSyncCountLabels = {"Don't Sync", "Every V Blank","Every Secont V Blank"};
            public static readonly int[] VSyncCountValues = { 0, 1, 2 };
        }


        [SerializeField] QualitySettingsKun mQualitySettingsKun;
        [SerializeField] Vector2 mScrollPos;
        static bool mIsInit;

        public QualitySettingsKun qualitySettingsKun
        {
            get {if(mQualitySettingsKun == null) { mQualitySettingsKun = new QualitySettingsKun(); } return mQualitySettingsKun; }
            set { mQualitySettingsKun = value; }
        }


        public void OnGUI()
        {
            if (mIsInit)
            {
                mScrollPos = EditorGUILayout.BeginScrollView(mScrollPos);
                EditorGUI.BeginChangeCheck();


                DrawRenderPipelineAsset(qualitySettingsKun.renderPipeline);



                EditorGUILayout.LabelField("Rendering");
                qualitySettingsKun.pixelLightCount = EditorGUILayout.IntField("Pixel Light Count", qualitySettingsKun.pixelLightCount);
                qualitySettingsKun.anisotropicFiltering = (AnisotropicFiltering)EditorGUILayout.EnumPopup("Anisotropic Textures", qualitySettingsKun.anisotropicFiltering);
                qualitySettingsKun.antiAliasing = EditorGUILayout.IntPopup("Anti Aliasing", qualitySettingsKun.antiAliasing, Styles.AntiAliasingLabels, Styles.AntiAliasingValuess);
                qualitySettingsKun.softParticles = EditorGUILayout.Toggle("Soft Particles", qualitySettingsKun.softParticles);
                qualitySettingsKun.realtimeReflectionProbes = EditorGUILayout.Toggle("Realtime Reflection Probes", qualitySettingsKun.realtimeReflectionProbes);
                qualitySettingsKun.billboardsFaceCameraPosition = EditorGUILayout.Toggle("Billboards Face Camera Position", qualitySettingsKun.billboardsFaceCameraPosition);

                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("To enable this feature, we need to change the value of Player -> Resolution and Presentation -> Resolution Scaling Mode from Disable to Fixed DPI.", MessageType.Info);
                qualitySettingsKun.resolutionScalingFixedDPIFactor = EditorGUILayout.Slider("Resolution Scaling Fixed DPI Factor", qualitySettingsKun.resolutionScalingFixedDPIFactor, 0.001f, 5.0f, GUILayout.ExpandWidth(true));
                EditorGUILayout.Space();

                qualitySettingsKun.streamingMipmapsActive = EditorGUILayout.Toggle("Texture Streaming", qualitySettingsKun.streamingMipmapsActive);
                if (qualitySettingsKun.streamingMipmapsActive)
                {
                    EditorGUI.indentLevel++;
                    qualitySettingsKun.streamingMipmapsAddAllCameras = EditorGUILayout.Toggle("Add All Cameras", qualitySettingsKun.streamingMipmapsAddAllCameras);
                    qualitySettingsKun.streamingMipmapsMemoryBudget = EditorGUILayout.FloatField("Memory Buget", qualitySettingsKun.streamingMipmapsMemoryBudget);
                    qualitySettingsKun.streamingMipmapsRenderersPerFrame = EditorGUILayout.IntField("Memory Per Frame", qualitySettingsKun.streamingMipmapsRenderersPerFrame);
                    qualitySettingsKun.streamingMipmapsMaxLevelReduction = EditorGUILayout.IntField("Max Level Reduction", qualitySettingsKun.streamingMipmapsMaxLevelReduction);
                    qualitySettingsKun.streamingMipmapsMaxFileIORequests = EditorGUILayout.IntField("Max IO Requests", qualitySettingsKun.streamingMipmapsMaxFileIORequests);

                    EditorGUI.indentLevel--;
                }

                GUILayout.Space(10);
                GUILayout.Label("Shadows", EditorStyles.boldLabel);
                qualitySettingsKun.shadowmaskMode = (ShadowmaskMode)EditorGUILayout.EnumPopup("Shadowmask Mode", qualitySettingsKun.shadowmaskMode);
                qualitySettingsKun.shadows = (ShadowQuality)EditorGUILayout.EnumPopup("Shadows", qualitySettingsKun.shadows);
                qualitySettingsKun.shadowResolution = (ShadowResolution)EditorGUILayout.EnumPopup("Shadow Resolution", qualitySettingsKun.shadowResolution);
                qualitySettingsKun.shadowProjection = (ShadowProjection)EditorGUILayout.EnumPopup("Shadow Projection", qualitySettingsKun.shadowProjection);
                qualitySettingsKun.shadowDistance = EditorGUILayout.FloatField("Shadow Distance", qualitySettingsKun.shadowDistance);
                qualitySettingsKun.shadowNearPlaneOffset = EditorGUILayout.FloatField("Shadow Near Plane Offset", qualitySettingsKun.shadowNearPlaneOffset);
                qualitySettingsKun.shadowCascades = EditorGUILayout.IntPopup("Shadow Cascades", qualitySettingsKun.shadowCascades, Styles.ShadowCascadeLabels, Styles.ShadowCascadeValues);

                GUILayout.Space(10);
                GUILayout.Label("Other", EditorStyles.boldLabel);
#if UNITY_2019_1_OR_NEWER
                qualitySettingsKun.skinWeights = (SkinWeights)EditorGUILayout.EnumPopup("Skin Weights", qualitySettingsKun.skinWeights);
#endif
                qualitySettingsKun.vSyncCount = EditorGUILayout.IntPopup("VSync Count", qualitySettingsKun.vSyncCount, Styles.VSyncCountLabels, Styles.VSyncCountValues);
                qualitySettingsKun.lodBias = EditorGUILayout.FloatField("LOD Bias", qualitySettingsKun.lodBias);
                qualitySettingsKun.maximumLODLevel = EditorGUILayout.IntField("Maximum LOD Level", qualitySettingsKun.maximumLODLevel);
                qualitySettingsKun.particleRaycastBudget = EditorGUILayout.IntField("Particle Raycast Budget", qualitySettingsKun.particleRaycastBudget);
                qualitySettingsKun.asyncUploadTimeSlice = EditorGUILayout.IntSlider("Async Upload Time Slice", qualitySettingsKun.asyncUploadTimeSlice, 1, 33);
                qualitySettingsKun.asyncUploadBufferSize = EditorGUILayout.IntSlider("Async Upload Buffer Size", qualitySettingsKun.asyncUploadBufferSize, 2, 512);
                qualitySettingsKun.asyncUploadPersistentBuffer = EditorGUILayout.Toggle("Async Upload Persistent Buffer", qualitySettingsKun.asyncUploadPersistentBuffer);

                if (EditorGUI.EndChangeCheck())
                {
                    qualitySettingsKun.isDirty = true;
                    UnityChoseKunEditor.SendMessage<QualitySettingsKun>(UnityChoseKun.MessageID.QualitySettingsPush, qualitySettingsKun);
                    qualitySettingsKun.isDirty = false;

                }
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
            if(renderPipelineAssetKun == null)
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
            qualitySettingsKun.Deserialize(binaryReader);
            mIsInit = true;
        }
    }
}