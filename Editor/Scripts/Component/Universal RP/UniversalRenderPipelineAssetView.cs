using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

/// <summary>
/// Katsumasa.Kimura
/// </summary>
namespace Utj.UnityChoseKun.URP
{


    public class UniversalRenderPipelineAssetView
    {
        static class Styles
        {
            public static readonly string Rendering = "Rendering";
            public static readonly string RendererList = "Renderer List";
            public static readonly string DepthTexture = "Depth Texture";
            public static readonly string OpaqueTexture = "Opaque Texture";
            public static readonly string OpaqueDowsampling = "Opaque Dowsampling";
            public static readonly string TerrainHoles = "Terrain Holes";

            public static readonly string Quality = "Quality";
            public static readonly string HDR = "HDR";
            public static readonly string MSAA = "Anti Aliasing(MSAA)";
            public static readonly string RenderScale = "Render Scale";

            public static readonly string Lighting = "Lighting";
            public static readonly string MainLight = "Main Light";
            public static readonly string CastShadows = "Cast Shadows";
            public static readonly string ShadowResolution = "Shadow Resolution";
            public static readonly string AdditionalLights = "Additional Lights";
            public static readonly string PerObjectLimit = "Per Object Limit";
            public static readonly string ShadowAtlasReslosution = "Shadow Atlas Reslosution";
            public static readonly string ShadowResolutionTiers = "Shadow Resolution Tiers";
            public static readonly string Low = "Low";
            public static readonly string Miedium = "Miedium";
            public static readonly string Hight = "Hight";
            public static readonly string CookieAtlasResolution = "CookieAtlasResolution";
            public static readonly string CookieAtlasFormat = "Cookie Atlas Format";
            public static readonly string ReflectionProbes = "Reflection Probes";
            public static readonly string ProbeBlending = "Probe Blending";
            public static readonly string BoxProjection = "Box Projection";

            public static readonly string Shadows = "Shadows";
            public static readonly string MaxDistance = "Max Distance";
            public static readonly string WorkingUnit = "Working Unit";
            public static readonly GUIContent CascadeCount = new GUIContent("Cascade Count");
            public static readonly string LastBorder = "Last Border";
            public static readonly string DepthBias = "Depth Bias";
            public static readonly string NormalBias = "Normal Bias";
            public static readonly GUIContent SoftShadows = new GUIContent("Soft Shadows");

            public static readonly string PostProcessing = "Post-processing";
            public static readonly string GradingMode = "Grading-Mode";
            public static readonly string LUTSize = "LUT size";
            public static readonly string FastsRGBLinearConversions = "Fast sRGB/Linear conversions";

        }

        static readonly string WorkingUnitKey = "Universal_Shadow_Setting_Unit:UI_State";



        enum Unit
        {
            Metric,
            Percent,
        }

        public static UniversalRenderPipelineAssetView mInspector;

        public static UniversalRenderPipelineAssetView Inspector{
            get
            {
                if (mInspector == null)
                {
                    mInspector = new UniversalRenderPipelineAssetView();
                }
                return mInspector;
            }
        }

        bool mDrawFoldout;
        bool mRenderingFoldout;
        bool mQualityFoldout;
        bool mLightFoldout;
        bool mShadowFoldout;
        bool mPostProcessingFoldout;


        public bool Draw(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun)
        {
            var label = string.Format("{0} (Universal Render Pipeline Asset)", universalRenderPipelineAssetKun.name);
            mDrawFoldout = EditorGUILayout.Foldout(mDrawFoldout,label);
            if (mDrawFoldout)
            {
                bool dirty = false;
                EditorGUI.indentLevel++;
                EditorGUI.BeginChangeCheck();
                dirty |= DrawRendering(universalRenderPipelineAssetKun);
                dirty |= DrawQuality(universalRenderPipelineAssetKun);
                dirty |= DrawLighting (universalRenderPipelineAssetKun);
                dirty |= DrawShadow(universalRenderPipelineAssetKun);
                dirty |= DrawPostProcessing (universalRenderPipelineAssetKun);
                EditorGUI.indentLevel--;
                universalRenderPipelineAssetKun.dirty = dirty;
                return dirty;
            }
            return false;
        }


        bool DrawRendering(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun)
        {
            mRenderingFoldout = EditorGUILayout.Foldout(mRenderingFoldout, Styles.Rendering);
            if (mRenderingFoldout)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.indentLevel++;

                EditorGUILayout.LabelField(Styles.RendererList);
                EditorGUI.indentLevel++;
                var rect = EditorGUILayout.BeginVertical();
                GUI.Box(rect,"");
                for (var i = 0; i < universalRenderPipelineAssetKun.rendererDataList.Length; i++)
                {
                    var label = string.Format("{0}:{1} (Universal Renderer Data)", i, universalRenderPipelineAssetKun.rendererDataList[i].name);
                    if (i== universalRenderPipelineAssetKun.defaultRendererIndex)
                    {
                        label += " [Default]";
                    }                                         
                    EditorGUILayout.LabelField(label);
                }
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();

                universalRenderPipelineAssetKun.supportsCameraDepthTexture = EditorGUILayout.Toggle(Styles.DepthTexture, universalRenderPipelineAssetKun.supportsCameraDepthTexture);
                universalRenderPipelineAssetKun.supportsCameraOpaqueTexture = EditorGUILayout.Toggle(Styles.OpaqueTexture,universalRenderPipelineAssetKun.supportsCameraOpaqueTexture);
                EditorGUI.BeginDisabledGroup(universalRenderPipelineAssetKun.supportsCameraOpaqueTexture == false);
                {
                    EditorGUILayout.EnumPopup(Styles.OpaqueDowsampling,universalRenderPipelineAssetKun.opaqueDownsampling);
                }
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.Toggle(Styles.TerrainHoles,universalRenderPipelineAssetKun.supportsTerrainHoles);

                EditorGUI.indentLevel--;
                return EditorGUI.EndChangeCheck();
            }
            return false;
        }


        bool DrawQuality(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun)
        {
            mQualityFoldout = EditorGUILayout.Foldout(mQualityFoldout,Styles.Quality);
            if(mQualityFoldout)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.indentLevel++;

                universalRenderPipelineAssetKun.supportsHDR = EditorGUILayout.Toggle(Styles.HDR,universalRenderPipelineAssetKun.supportsHDR);
                universalRenderPipelineAssetKun.msaaSampleCount = (int)(MsaaQuality)EditorGUILayout.EnumPopup(Styles.MSAA,(MsaaQuality)(universalRenderPipelineAssetKun.msaaSampleCount));
                universalRenderPipelineAssetKun.renderScale = EditorGUILayout.Slider(Styles.RenderScale,universalRenderPipelineAssetKun.renderScale,0f,2f);

                EditorGUI.indentLevel--;
                if (EditorGUI.EndChangeCheck())
                {
                    return true;
                }
            }
            return false;
        }


        bool DrawLighting(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun)
        {
            mLightFoldout = EditorGUILayout.Foldout(mLightFoldout,Styles.Lighting);
            if (mLightFoldout)
            {
                EditorGUI.BeginChangeCheck();

                EditorGUI.indentLevel++;

                bool disableGroup = false;
                EditorGUI.BeginDisabledGroup(disableGroup);
                EditorGUILayout.EnumPopup(Styles.MainLight,universalRenderPipelineAssetKun.mainLightRenderingMode);
                EditorGUI.EndDisabledGroup();

                disableGroup |= universalRenderPipelineAssetKun.mainLightRenderingMode == LightRenderingMode.Disabled ? true : false;

                EditorGUI.indentLevel++;
                {
                    EditorGUI.BeginDisabledGroup(disableGroup);
                    EditorGUILayout.Toggle(Styles.CastShadows,universalRenderPipelineAssetKun.supportsMainLightShadows);
                    EditorGUI.EndDisabledGroup();

                    disableGroup |= !universalRenderPipelineAssetKun.supportsMainLightShadows;

                    EditorGUI.BeginDisabledGroup(disableGroup);                    
                    EditorGUILayout.EnumPopup(Styles.ShadowResolution,(ShadowResolution)universalRenderPipelineAssetKun.mainLightShadowmapResolution);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();

                EditorGUILayout.EnumPopup(Styles.AdditionalLights,universalRenderPipelineAssetKun.additionalLightsRenderingMode);
                EditorGUI.indentLevel++;
                {
                    disableGroup = (universalRenderPipelineAssetKun.additionalLightsRenderingMode == LightRenderingMode.Disabled);
                    EditorGUI.BeginDisabledGroup(disableGroup);
                    universalRenderPipelineAssetKun.maxAdditionalLightsCount = EditorGUILayout.IntSlider(Styles.PerObjectLimit,universalRenderPipelineAssetKun.maxAdditionalLightsCount, 0, 8);
                    EditorGUI.EndDisabledGroup();

                    disableGroup |= (universalRenderPipelineAssetKun.maxAdditionalLightsCount == 0 || universalRenderPipelineAssetKun.additionalLightsRenderingMode != LightRenderingMode.PerPixel);
                    EditorGUI.BeginDisabledGroup(disableGroup);
                    EditorGUILayout.Toggle(Styles.CastShadows,universalRenderPipelineAssetKun.supportsAdditionalLightShadows);
                    EditorGUI.EndDisabledGroup();

                    disableGroup |= !universalRenderPipelineAssetKun.supportsAdditionalLightShadows;
                    EditorGUI.BeginDisabledGroup(disableGroup);
                    EditorGUILayout.EnumPopup(Styles.ShadowAtlasReslosution,(ShadowResolution)universalRenderPipelineAssetKun.additionalLightsShadowmapResolution);
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(Styles.ShadowResolutionTiers);
                        EditorGUILayout.IntField(Styles.Low,universalRenderPipelineAssetKun.additionalLightsShadowResolutionTierLow);
                        EditorGUILayout.IntField(Styles.Miedium,universalRenderPipelineAssetKun.additionalLightsShadowResolutionTierMedium);
                        EditorGUILayout.IntField(Styles.Hight,universalRenderPipelineAssetKun.additionalLightsShadowResolutionTierHigh);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.EndDisabledGroup();

                    EditorGUILayout.Space();
                    disableGroup = (universalRenderPipelineAssetKun.additionalLightsRenderingMode == LightRenderingMode.Disabled);
                    EditorGUI.BeginDisabledGroup(disableGroup);
                    EditorGUILayout.EnumPopup(Styles.CookieAtlasResolution,universalRenderPipelineAssetKun.additionalLightsCookieResolution);
                    EditorGUILayout.EnumPopup(Styles.CookieAtlasFormat,universalRenderPipelineAssetKun.additionalLightsCookieFormat);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.LabelField(Styles.ReflectionProbes);
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.Toggle(Styles.ProbeBlending,universalRenderPipelineAssetKun.reflectionProbeBlending);
                    EditorGUILayout.Toggle(Styles.BoxProjection,universalRenderPipelineAssetKun.reflectionProbeBoxProjection);
                }
                EditorGUI.indentLevel--;

                EditorGUI.indentLevel--;
                if (EditorGUI.EndChangeCheck())
                {
                    return true;
                }
            }

            return false;

        }


        bool DrawShadow(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun)
        {
            mShadowFoldout = EditorGUILayout.Foldout(mShadowFoldout ,Styles.Shadows);
            if(mShadowFoldout)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.indentLevel++;               
                universalRenderPipelineAssetKun.shadowDistance = EditorGUILayout.FloatField(Styles.MaxDistance, universalRenderPipelineAssetKun.shadowDistance);

                Unit unit = Unit.Metric;
                if (EditorPrefs.HasKey(WorkingUnitKey))
                {
                    unit = EditorPrefs.GetBool(WorkingUnitKey) ? Unit.Percent : Unit.Metric;
                }
                EditorGUI.BeginChangeCheck();
                unit = (Unit)EditorGUILayout.EnumPopup(Styles.WorkingUnit,unit);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorPrefs.SetBool(WorkingUnitKey, (unit == Unit.Percent) ? true : false);
                }

                universalRenderPipelineAssetKun.shadowCascadeCount = EditorGUILayout.IntSlider(Styles.CascadeCount,universalRenderPipelineAssetKun.shadowCascadeCount, UniversalRenderPipelineAssetKun.k_ShadowCascadeMinCount,UniversalRenderPipelineAssetKun.k_ShadowCascadeMaxCount);

                int cascadeCount = universalRenderPipelineAssetKun.shadowCascadeCount;
                EditorGUI.indentLevel++;
                bool useMetric = unit == Unit.Metric;
                float baseMetric = universalRenderPipelineAssetKun.shadowDistance;
                int cascadeSplitCount = cascadeCount - 1;

                DrawCascadeSliders(universalRenderPipelineAssetKun, cascadeSplitCount, useMetric, baseMetric);

                universalRenderPipelineAssetKun.shadowDepthBias = EditorGUILayout.Slider(Styles.DepthBias, universalRenderPipelineAssetKun.shadowDepthBias, 0.0f, 10);
                universalRenderPipelineAssetKun.shadowNormalBias = EditorGUILayout.Slider(Styles.NormalBias, universalRenderPipelineAssetKun.shadowNormalBias, 0.0f,10);
                EditorGUILayout.Toggle(Styles.SoftShadows,universalRenderPipelineAssetKun.supportsSoftShadows);
                EditorGUI.indentLevel--;

                EditorGUI.indentLevel--;
                if (EditorGUI.EndChangeCheck())
                {
                    return true;
                }
            }
            return false;
        }


        bool DrawPostProcessing(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun)
        {
            mPostProcessingFoldout = EditorGUILayout.Foldout(mPostProcessingFoldout,Styles.PostProcessing);
            if (mPostProcessingFoldout)
            {
                EditorGUI.indentLevel++;

                //universalRenderPipelineAssetKun.colorGradingMode = (ColorGradingMode)EditorGUILayout.EnumPopup(Styles.GradingMode,universalRenderPipelineAssetKun.colorGradingMode);
                EditorGUILayout.EnumPopup(Styles.GradingMode, universalRenderPipelineAssetKun.colorGradingMode);
                universalRenderPipelineAssetKun.colorGradingLutSize = EditorGUILayout.IntField(Styles.LUTSize,universalRenderPipelineAssetKun.colorGradingLutSize);
                if(universalRenderPipelineAssetKun.colorGradingLutSize < 0)
                {
                    universalRenderPipelineAssetKun.colorGradingLutSize = 0;
                }
                EditorGUILayout.Toggle(Styles.FastsRGBLinearConversions,universalRenderPipelineAssetKun.useFastSRGBLinearConversion);

                EditorGUI.indentLevel--;
            }
            return false;
        }


        static void DrawCascadeSliders(UniversalRenderPipelineAssetKun universalRenderPipelineAssetKun, int splitCount, bool useMetric, float baseMetric)
        {
            Vector4 shadowCascadeSplit = Vector4.one;
            if (splitCount == 3)
                shadowCascadeSplit = new Vector4(universalRenderPipelineAssetKun.cascade4Split.x, universalRenderPipelineAssetKun.cascade4Split.y, universalRenderPipelineAssetKun.cascade4Split.z, 1);
            else if (splitCount == 2)
                shadowCascadeSplit = new Vector4(universalRenderPipelineAssetKun.cascade3Split.x, universalRenderPipelineAssetKun.cascade3Split.y, 1, 0);
            else if (splitCount == 1)
                shadowCascadeSplit = new Vector4(universalRenderPipelineAssetKun.cascade2Split, 1, 0, 0);

            float splitBias = 0.001f;
            float invBaseMetric = baseMetric == 0 ? 0 : 1f / baseMetric;

            shadowCascadeSplit[0] = Mathf.Clamp(shadowCascadeSplit[0], 0f, shadowCascadeSplit[1] - splitBias);
            shadowCascadeSplit[1] = Mathf.Clamp(shadowCascadeSplit[1], shadowCascadeSplit[0] + splitBias, shadowCascadeSplit[2] - splitBias);
            shadowCascadeSplit[2] = Mathf.Clamp(shadowCascadeSplit[2], shadowCascadeSplit[1] + splitBias, shadowCascadeSplit[3] - splitBias);

            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < splitCount; ++i)
            {
                float value = shadowCascadeSplit[i];

                float minimum = i == 0 ? 0 : shadowCascadeSplit[i - 1] + splitBias;
                float maximum = i == splitCount - 1 ? 1 : shadowCascadeSplit[i + 1] - splitBias;

                if (useMetric)
                {
                    float valueMetric = value * baseMetric;
                    valueMetric = EditorGUILayout.Slider(EditorGUIUtility.TrTextContent($"Split {i + 1}", "The distance where this cascade ends and the next one starts."), valueMetric, 0f, baseMetric, null);

                    shadowCascadeSplit[i] = Mathf.Clamp(valueMetric * invBaseMetric, minimum, maximum);
                }
                else
                {
                    float valueProcentage = value * 100f;
                    valueProcentage = EditorGUILayout.Slider(EditorGUIUtility.TrTextContent($"Split {i + 1}", "The distance where this cascade ends and the next one starts."), valueProcentage, 0f, 100f, null);

                    shadowCascadeSplit[i] = Mathf.Clamp(valueProcentage * 0.01f, minimum, maximum);
                }
            }

#if false
            if (EditorGUI.EndChangeCheck())
            {
                switch (splitCount)
                {
                    case 3:
                        universalRenderPipelineAssetKun.cascade4Split.x = shadowCascadeSplit.x;
                        universalRenderPipelineAssetKun.cascade4Split.y = shadowCascadeSplit.y;
                        universalRenderPipelineAssetKun.cascade4Split.z = shadowCascadeSplit.z;
                        break;
                    case 2:
                        universalRenderPipelineAssetKun.cascade3Split.x = shadowCascadeSplit.x;
                        universalRenderPipelineAssetKun.cascade3Split.y = shadowCascadeSplit.y;
                        break;
                    case 1:
                        universalRenderPipelineAssetKun.cascade2Split = shadowCascadeSplit.x;
                        break;
                }
            }
#endif

            var borderValue = universalRenderPipelineAssetKun.cascadeBorder;
            EditorGUI.BeginChangeCheck();
            if (useMetric)
            {
                var lastCascadeSplitSize = splitCount == 0 ? baseMetric : (1.0f - shadowCascadeSplit[splitCount - 1]) * baseMetric;
                var invLastCascadeSplitSize = lastCascadeSplitSize == 0 ? 0 : 1f / lastCascadeSplitSize;
                float valueMetric = borderValue * lastCascadeSplitSize;
                valueMetric = EditorGUILayout.Slider(EditorGUIUtility.TrTextContent("Last Border", "The distance of the last cascade."), valueMetric, 0f, lastCascadeSplitSize, null);

                borderValue = valueMetric * invLastCascadeSplitSize;
            }
            else
            {
                float valueProcentage = borderValue * 100f;
                valueProcentage = EditorGUILayout.Slider(EditorGUIUtility.TrTextContent("Last Border", "The distance of the last cascade."), valueProcentage, 0f, 100f, null);

                borderValue = valueProcentage * 0.01f;
            }

            if (EditorGUI.EndChangeCheck())
            {
                universalRenderPipelineAssetKun.cascadeBorder = borderValue;
            }
        }


    }
}