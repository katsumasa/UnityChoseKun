using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;


// Katsumasa.Kimura

namespace Utj.UnityChoseKun.Engine.Rendering.Universal
{
    public enum ShadowQuality
    {
        Disabled,
        HardShadows,
        SoftShadows,
    }

    public enum ShadowResolution
    {
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048,
        _4096 = 4096
    }

    public enum LightCookieResolution
    {
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048,
        _4096 = 4096
    }

    public enum LightCookieFormat
    {
        GrayscaleLow,
        GrayscaleHigh,
        ColorLow,
        ColorHigh,
        ColorHDR,
    }

    public enum MsaaQuality
    {
        Disabled = 1,
        _2x = 2,
        _4x = 4,
        _8x = 8
    }

    public enum Downsampling
    {
        None,
        _2xBilinear,
        _4xBox,
        _4xBilinear
    }

    internal enum DefaultMaterialType
    {
        Standard,
        Particle,
        Terrain,
        Sprite,
        UnityBuiltinDefault,
        SpriteMask,
        Decal
    }

    public enum LightRenderingMode
    {
        Disabled = 0,
        PerVertex = 2,
        PerPixel = 1,
    }

    public enum ShaderVariantLogLevel
    {
        Disabled,
        [InspectorName("Only URP Shaders")]
        OnlyUniversalRPShaders,
        [InspectorName("All Shaders")]
        AllShaders
    }

    [Obsolete("PipelineDebugLevel is unused and has no effect.", false)]
    public enum PipelineDebugLevel
    {
        Disabled,
        Profiling,
    }

    public enum RendererType
    {
        Custom,
        UniversalRenderer,
        _2DRenderer,
        [Obsolete("ForwardRenderer has been renamed (UnityUpgradable) -> UniversalRenderer", true)]
        ForwardRenderer = UniversalRenderer,
    }

    public enum ColorGradingMode
    {
        LowDynamicRange,
        HighDynamicRange
    }

    /// <summary>
    /// Defines if Unity discards or stores the render targets of the DrawObjects Passes. Selecting the Store option significantly increases the memory bandwidth on mobile and tile-based GPUs.
    /// </summary>
    public enum StoreActionsOptimization
    {
        /// <summary>Unity uses the Discard option by default, and falls back to the Store option if it detects any injected Passes.</summary>
        Auto,
        /// <summary>Unity discards the render targets of render Passes that are not reused later (lower memory bandwidth).</summary>
        Discard,
        /// <summary>Unity stores all render targets of each Pass (higher memory bandwidth).</summary>
        Store
    }

    /// <summary>
    /// Defines the update frequency for the Volume Framework.
    /// </summary>
    public enum VolumeFrameworkUpdateMode
    {
        [InspectorName("Every Frame")]
        EveryFrame = 0,
        [InspectorName("Via Scripting")]
        ViaScripting = 1,
        [InspectorName("Use Pipeline Settings")]
        UsePipelineSettings = 2,
    }




    /// <summary>
    /// Class <c>UniversalRenderPipelineAssetKun</c>
    /// </summary>
    public class UniversalRenderPipelineAssetKun : RenderPipelineAssetKun
    {
        public ScriptableRendererDataKun[] rendererDataList
        {
            get
            {
                return m_RendererDataList;
            }
        }

        public int defaultRendererIndex
        {
            get
            {
                return m_DefaultRendererIndex;
            }
        }

        public bool supportsCameraDepthTexture
        {
            get { return m_RequireDepthTexture; }
            set { m_RequireDepthTexture = value; }
        }

        public bool supportsCameraOpaqueTexture
        {
            get { return m_RequireOpaqueTexture; }
            set { m_RequireOpaqueTexture = value; }
        }

        public Downsampling opaqueDownsampling
        {
            get { return m_OpaqueDownsampling; }
        }

        public bool supportsTerrainHoles
        {
            get { return m_SupportsTerrainHoles; }
        }

        /// <summary>
        /// Returns the active store action optimization value.
        /// </summary>
        /// <returns>Returns the active store action optimization value.</returns>
        public StoreActionsOptimization storeActionsOptimization
        {
            get { return m_StoreActionsOptimization; }
            set { m_StoreActionsOptimization = value; }
        }

        public bool supportsHDR
        {
            get { return m_SupportsHDR; }
            set { m_SupportsHDR = value; }
        }

        public int msaaSampleCount
        {
            get { return (int)m_MSAA; }
            set { m_MSAA = (MsaaQuality)value; }
        }

        public float renderScale
        {
            get { return m_RenderScale; }
            set { m_RenderScale = ValidateRenderScale(value); }
        }

        public LightRenderingMode mainLightRenderingMode
        {
            get { return m_MainLightRenderingMode; }
            internal set { m_MainLightRenderingMode = value; }
        }

        public bool supportsMainLightShadows
        {
            get { return m_MainLightShadowsSupported; }
            internal set { m_MainLightShadowsSupported = value; }
        }

        public int mainLightShadowmapResolution
        {
            get { return (int)m_MainLightShadowmapResolution; }
            internal set { m_MainLightShadowmapResolution = (ShadowResolution)value; }
        }

        public LightRenderingMode additionalLightsRenderingMode
        {
            get { return m_AdditionalLightsRenderingMode; }
            internal set { m_AdditionalLightsRenderingMode = value; }
        }

        public int maxAdditionalLightsCount
        {
            get { return m_AdditionalLightsPerObjectLimit; }
            set { m_AdditionalLightsPerObjectLimit = ValidatePerObjectLights(value); }
        }

        public bool supportsAdditionalLightShadows
        {
            get { return m_AdditionalLightShadowsSupported; }
            internal set { m_AdditionalLightShadowsSupported = value; }
        }

        public int additionalLightsShadowmapResolution
        {
            get { return (int)m_AdditionalLightsShadowmapResolution; }
            internal set { m_AdditionalLightsShadowmapResolution = (ShadowResolution)value; }
        }

        /// <summary>
        /// Returns the additional light shadow resolution defined for tier "Low" in the UniversalRenderPipeline asset.
        /// </summary>
        public int additionalLightsShadowResolutionTierLow
        {
            get { return (int)m_AdditionalLightsShadowResolutionTierLow; }
            internal set { additionalLightsShadowResolutionTierLow = value; }
        }

        /// <summary>
        /// Returns the additional light shadow resolution defined for tier "Medium" in the UniversalRenderPipeline asset.
        /// </summary>
        public int additionalLightsShadowResolutionTierMedium
        {
            get { return (int)m_AdditionalLightsShadowResolutionTierMedium; }
            internal set { m_AdditionalLightsShadowResolutionTierMedium = value; }
        }

        /// <summary>
        /// Returns the additional light shadow resolution defined for tier "High" in the UniversalRenderPipeline asset.
        /// </summary>
        public int additionalLightsShadowResolutionTierHigh
        {
            get { return (int)m_AdditionalLightsShadowResolutionTierHigh; }
            internal set { additionalLightsShadowResolutionTierHigh = value; }
        }


        public int GetAdditionalLightsShadowResolution(int additionalLightsShadowResolutionTier)
        {
            if (additionalLightsShadowResolutionTier <= UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierLow /* 0 */)
                return additionalLightsShadowResolutionTierLow;

            if (additionalLightsShadowResolutionTier == UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierMedium /* 1 */)
                return additionalLightsShadowResolutionTierMedium;

            if (additionalLightsShadowResolutionTier >= UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierHigh /* 2 */)
                return additionalLightsShadowResolutionTierHigh;

            return additionalLightsShadowResolutionTierMedium;
        }


        public bool reflectionProbeBlending
        {
            get { return m_ReflectionProbeBlending; }
            internal set { m_ReflectionProbeBlending = value; }
        }

        public bool reflectionProbeBoxProjection
        {
            get { return m_ReflectionProbeBoxProjection; }
            internal set { m_ReflectionProbeBoxProjection = value; }
        }

        /// <summary>
        /// Controls the maximum distance at which shadows are visible.
        /// </summary>
        public float shadowDistance
        {
            get { return m_ShadowDistance; }
            set { m_ShadowDistance = Mathf.Max(0.0f, value); }
        }

        /// <summary>
        /// Returns the number of shadow cascades.
        /// </summary>
        public int shadowCascadeCount
        {
            get { return m_ShadowCascadeCount; }
            set
            {
                if (value < k_ShadowCascadeMinCount || value > k_ShadowCascadeMaxCount)
                {
                    throw new ArgumentException($"Value ({value}) needs to be between {k_ShadowCascadeMinCount} and {k_ShadowCascadeMaxCount}.");
                }
                m_ShadowCascadeCount = value;
            }
        }

        /// <summary>
        /// Returns the split value.
        /// </summary>
        /// <returns>Returns a Float with the split value.</returns>
        public float cascade2Split
        {
            get { return m_Cascade2Split; }
            internal set { m_Cascade2Split = value; }
        }

        /// <summary>
        /// Returns the split values.
        /// </summary>
        /// <returns>Returns a Vector2 with the split values.</returns>
        public Vector2Kun cascade3Split
        {
            get { return m_Cascade3Split; }
            internal set { m_Cascade3Split = value; }
        }

        /// <summary>
        /// Returns the split values.
        /// </summary>
        /// <returns>Returns a Vector3 with the split values.</returns>
        public Vector3Kun cascade4Split
        {
            get { return m_Cascade4Split; }
            internal set { m_Cascade4Split = value; }
        }

        /// <summary>
        /// Last cascade fade distance in percentage.
        /// </summary>
        public float cascadeBorder
        {
            get { return m_CascadeBorder; }
            set { m_CascadeBorder = value; }
        }

        /// <summary>
        /// The Shadow Depth Bias, controls the offset of the lit pixels.
        /// </summary>
        public float shadowDepthBias
        {
            get { return m_ShadowDepthBias; }
            set { m_ShadowDepthBias = ValidateShadowBias(value); }
        }

        /// <summary>
        /// Controls the distance at which the shadow casting surfaces are shrunk along the surface normal.
        /// </summary>
        public float shadowNormalBias
        {
            get { return m_ShadowNormalBias; }
            set { m_ShadowNormalBias = ValidateShadowBias(value); }
        }

        /// <summary>
        /// Supports Soft Shadows controls the Soft Shadows.
        /// </summary>
        public bool supportsSoftShadows
        {
            get { return m_SoftShadowsSupported; }
            internal set { m_SoftShadowsSupported = value; }
        }

        public bool supportsDynamicBatching
        {
            get { return m_SupportsDynamicBatching; }
            set { m_SupportsDynamicBatching = value; }
        }

        public bool supportsMixedLighting
        {
            get { return m_MixedLightingSupported; }
        }

        /// <summary>
        /// Returns true if the Render Pipeline Asset supports light layers, false otherwise.
        /// </summary>
        public bool supportsLightLayers
        {
            get { return m_SupportsLightLayers; }
        }

        public ShaderVariantLogLevel shaderVariantLogLevel
        {
            get { return m_ShaderVariantLogLevel; }
            set { m_ShaderVariantLogLevel = value; }
        }

        /// <summary>
        /// Returns the selected update mode for volumes.
        /// </summary>
        public VolumeFrameworkUpdateMode volumeFrameworkUpdateMode => m_VolumeFrameworkUpdateMode;

        [Obsolete("PipelineDebugLevel is deprecated. Calling debugLevel is not necessary.", false)]
        public PipelineDebugLevel debugLevel
        {
            get => PipelineDebugLevel.Disabled;
        }

        public bool useSRPBatcher
        {
            get { return m_UseSRPBatcher; }
            set { m_UseSRPBatcher = value; }
        }

        public ColorGradingMode colorGradingMode
        {
            get { return m_ColorGradingMode; }
            set { m_ColorGradingMode = value; }
        }

        public int colorGradingLutSize
        {
            get { return m_ColorGradingLutSize; }
            set { m_ColorGradingLutSize = Mathf.Clamp(value, k_MinLutSize, k_MaxLutSize); }
        }

        /// <summary>
        /// Returns true if fast approximation functions are used when converting between the sRGB and Linear color spaces, false otherwise.
        /// </summary>
        public bool useFastSRGBLinearConversion
        {
            get { return m_UseFastSRGBLinearConversion; }
        }

        /// <summary>
        /// Set to true to allow Adaptive performance to modify graphics quality settings during runtime.
        /// Only applicable when Adaptive performance package is available.
        /// </summary>
        public bool useAdaptivePerformance
        {
            get { return m_UseAdaptivePerformance; }
            set { m_UseAdaptivePerformance = value; }
        }

        /// <summary>
        /// Set to true to enable a conservative method for calculating the size and position of the minimal enclosing sphere around the frustum cascade corner points for shadow culling.
        /// </summary>
        public bool conservativeEnclosingSphere
        {
            get { return m_ConservativeEnclosingSphere; }
            set { m_ConservativeEnclosingSphere = value; }
        }

        /// <summary>
        /// Set the number of iterations to reduce the cascade culling enlcosing sphere to be closer to the absolute minimun enclosing sphere, but will also require more CPU computation for increasing values.
        /// This parameter is used only when conservativeEnclosingSphere is set to true. Default value is 64.
        /// </summary>
        public int numIterationsEnclosingSphere
        {
            get { return m_NumIterationsEnclosingSphere; }
            set { m_NumIterationsEnclosingSphere = value; }
        }


        public LightCookieResolution additionalLightsCookieResolution
        {
            get
            {
                return m_AdditionalLightsCookieResolution;
            } 
        }
        
        public LightCookieFormat additionalLightsCookieFormat
        {
            get
            {
                return m_AdditionalLightsCookieFormat;
            }
        }





        // Default values set when a new UniversalRenderPipeline asset is created
        [SerializeField] int k_AssetVersion = 9;
        [SerializeField] int k_AssetPreviousVersion = 9;

        // Deprecated settings for upgrading sakes
        [SerializeField] RendererType m_RendererType = RendererType.UniversalRenderer;
        [SerializeField] ScriptableRendererDataKun m_RendererData = null;

        // Renderer settings
        [SerializeField] ScriptableRendererDataKun[] m_RendererDataList = new ScriptableRendererDataKun[1];
        [SerializeField] int m_DefaultRendererIndex = 0;

        // General settings
        [SerializeField] bool m_RequireDepthTexture = false;
        [SerializeField] bool m_RequireOpaqueTexture = false;
        [SerializeField] Downsampling m_OpaqueDownsampling = Downsampling._2xBilinear;
        [SerializeField] bool m_SupportsTerrainHoles = true;
        [SerializeField] StoreActionsOptimization m_StoreActionsOptimization = StoreActionsOptimization.Auto;


        // Quality settings
        [SerializeField] bool m_SupportsHDR = true;
        [SerializeField] MsaaQuality m_MSAA = MsaaQuality.Disabled;
        [SerializeField] float m_RenderScale = 1.0f;

        // Main directional light Settings
        [SerializeField] LightRenderingMode m_MainLightRenderingMode = LightRenderingMode.PerPixel;
        [SerializeField] bool m_MainLightShadowsSupported = true;
        [SerializeField] ShadowResolution m_MainLightShadowmapResolution = ShadowResolution._2048;

        // Additional lights settings
        [SerializeField] LightRenderingMode m_AdditionalLightsRenderingMode = LightRenderingMode.PerPixel;
        [SerializeField] int m_AdditionalLightsPerObjectLimit = 4;
        [SerializeField] bool m_AdditionalLightShadowsSupported = false;
        [SerializeField] ShadowResolution m_AdditionalLightsShadowmapResolution = ShadowResolution._2048;

        [SerializeField] int m_AdditionalLightsShadowResolutionTierLow = AdditionalLightsDefaultShadowResolutionTierLow;
        [SerializeField] int m_AdditionalLightsShadowResolutionTierMedium = AdditionalLightsDefaultShadowResolutionTierMedium;
        [SerializeField] int m_AdditionalLightsShadowResolutionTierHigh = AdditionalLightsDefaultShadowResolutionTierHigh;

        // Reflection Probes
        [SerializeField] bool m_ReflectionProbeBlending = false;
        [SerializeField] bool m_ReflectionProbeBoxProjection = false;

        // Shadows Settings
        [SerializeField] float m_ShadowDistance = 50.0f;
        [SerializeField] int m_ShadowCascadeCount = 1;
        [SerializeField] float m_Cascade2Split = 0.25f;
        [SerializeField] Vector2Kun m_Cascade3Split;
        [SerializeField] Vector3Kun m_Cascade4Split;
        [SerializeField] float m_CascadeBorder = 0.2f;
        [SerializeField] float m_ShadowDepthBias = 1.0f;
        [SerializeField] float m_ShadowNormalBias = 1.0f;
        [SerializeField] bool m_SoftShadowsSupported = false;
        [SerializeField] bool m_ConservativeEnclosingSphere = false;
        [SerializeField] int m_NumIterationsEnclosingSphere = 64;

        // Light Cookie Settings
        [SerializeField] LightCookieResolution m_AdditionalLightsCookieResolution = LightCookieResolution._2048;
        [SerializeField] LightCookieFormat m_AdditionalLightsCookieFormat = LightCookieFormat.ColorHigh;

        // Advanced settings
        [SerializeField] bool m_UseSRPBatcher = true;
        [SerializeField] bool m_SupportsDynamicBatching = false;
        [SerializeField] bool m_MixedLightingSupported = true;
        [SerializeField] bool m_SupportsLightLayers = false;
        

        // Adaptive performance settings
        [SerializeField] bool m_UseAdaptivePerformance = true;

        // Post-processing settings
        [SerializeField] ColorGradingMode m_ColorGradingMode = ColorGradingMode.LowDynamicRange;
        [SerializeField] int m_ColorGradingLutSize = 32;
        [SerializeField] bool m_UseFastSRGBLinearConversion = false;

        // Deprecated settings
        [SerializeField] ShadowQuality m_ShadowType = ShadowQuality.HardShadows;
        [SerializeField] bool m_LocalShadowsSupported = false;
        [SerializeField] ShadowResolution m_LocalShadowsAtlasResolution = ShadowResolution._256;
        [SerializeField] int m_MaxPixelLights = 0;
        [SerializeField] ShadowResolution m_ShadowAtlasResolution = ShadowResolution._256;

        [SerializeField] ShaderVariantLogLevel m_ShaderVariantLogLevel = ShaderVariantLogLevel.Disabled;
        [SerializeField] VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateMode = VolumeFrameworkUpdateMode.EveryFrame;

        // Note: A lut size of 16^3 is barely usable with the HDR grading mode. 32 should be the
        // minimum, the lut being encoded in log. Lower sizes would work better with an additional
        // 1D shaper lut but for now we'll keep it simple.
        public const int k_MinLutSize = 16;
        public const int k_MaxLutSize = 65;

        public static readonly int k_ShadowCascadeMinCount = 1;
        public static readonly int k_ShadowCascadeMaxCount = 4;

        public static readonly int AdditionalLightsDefaultShadowResolutionTierLow = 256;
        public static readonly int AdditionalLightsDefaultShadowResolutionTierMedium = 512;
        public static readonly int AdditionalLightsDefaultShadowResolutionTierHigh = 1024;


        /// <summary>
        /// 
        /// </summary>
        public UniversalRenderPipelineAssetKun():this(null)
        {
        }


        /// <summary>
        /// UniversalRenderPipelineAssetKun
        /// </summary>
        /// <param name="renderPipelineAsset"></param>
        public UniversalRenderPipelineAssetKun(RenderPipelineAsset renderPipelineAsset):base(renderPipelineAsset)
        {
            if(renderPipelineAsset != null)
            {
                var t = renderPipelineAsset.GetType();
                FieldInfo fieldInfo;

                fieldInfo = t.GetField("k_AssetVersion", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    k_AssetVersion = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("k_AssetPreviousVersion", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    k_AssetPreviousVersion = (int)fieldInfo.GetValue(renderPipelineAsset);
                }
                
                fieldInfo = t.GetField("m_RendererType", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_RendererType = (RendererType)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_RendererData", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_RendererData = new ScriptableRendererDataKun((ScriptableObject)fieldInfo.GetValue(renderPipelineAsset));
                }
                
                fieldInfo = t.GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    var scriptableObjects = (ScriptableObject[])(fieldInfo.GetValue(renderPipelineAsset));
                    m_RendererDataList = new ScriptableRendererDataKun[scriptableObjects.Length];
                    for (var i = 0; i < scriptableObjects.Length; i++)
                    {
                        m_RendererDataList[i] = new ScriptableRendererDataKun(scriptableObjects[i]);
                    }
                }

                fieldInfo = t.GetField("m_DefaultRendererIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_DefaultRendererIndex = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_RequireDepthTexture", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_RequireDepthTexture = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }


                fieldInfo = t.GetField("m_RequireOpaqueTexture", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_RequireOpaqueTexture = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_OpaqueDownsampling", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_OpaqueDownsampling = (Downsampling)fieldInfo.GetValue(renderPipelineAsset);
                }
                
                fieldInfo = t.GetField("m_SupportsTerrainHoles", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_SupportsTerrainHoles = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_StoreActionsOptimization", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_StoreActionsOptimization = (StoreActionsOptimization)fieldInfo.GetValue(renderPipelineAsset);
                }

                // Quality settings
                fieldInfo = t.GetField("m_SupportsHDR", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_SupportsHDR = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_MSAA", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_MSAA = (MsaaQuality)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_RenderScale", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_RenderScale = (float)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo  = t.GetField("m_MainLightRenderingMode", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_MainLightRenderingMode = (LightRenderingMode)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_MainLightShadowsSupported", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_MainLightShadowsSupported = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo  = t.GetField("m_MainLightShadowmapResolution", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_MainLightShadowmapResolution = (ShadowResolution)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsPerObjectLimit", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsPerObjectLimit = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightShadowsSupported", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightShadowsSupported = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsShadowmapResolution", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsShadowmapResolution = (ShadowResolution)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsShadowResolutionTierLow", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsShadowResolutionTierLow = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsShadowResolutionTierMedium", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsShadowResolutionTierMedium = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsShadowResolutionTierHigh", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsShadowResolutionTierHigh = (int)fieldInfo.GetValue(renderPipelineAsset);
                }
                fieldInfo = t.GetField("m_ReflectionProbeBlending", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ReflectionProbeBlending = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ReflectionProbeBoxProjection", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ReflectionProbeBoxProjection = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowDistance", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowDistance = (float)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowCascadeCount", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowCascadeCount = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_Cascade2Split", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_Cascade2Split = (float)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_Cascade3Split", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_Cascade3Split = new Vector2Kun((Vector2)fieldInfo.GetValue(renderPipelineAsset));
                }

                fieldInfo = t.GetField("m_Cascade4Split", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_Cascade4Split = new Vector3Kun((Vector3)fieldInfo.GetValue(renderPipelineAsset));
                }

                fieldInfo = t.GetField("m_CascadeBorder", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_CascadeBorder = (float)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowDepthBias", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowDepthBias = (float)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowNormalBias", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowNormalBias = (float)fieldInfo.GetValue(renderPipelineAsset);
                }
                
                fieldInfo = t.GetField("m_SoftShadowsSupported", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_SoftShadowsSupported = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }
                

                fieldInfo = t.GetField("m_ConservativeEnclosingSphere", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ConservativeEnclosingSphere = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_NumIterationsEnclosingSphere", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_NumIterationsEnclosingSphere = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsCookieResolution", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsCookieResolution = (LightCookieResolution)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_AdditionalLightsCookieFormat", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_AdditionalLightsCookieFormat = (LightCookieFormat)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_UseSRPBatcher", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_UseSRPBatcher = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }
                
                fieldInfo = t.GetField("m_SupportsDynamicBatching", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_SupportsDynamicBatching = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_MixedLightingSupported", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_MixedLightingSupported = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_SupportsLightLayers", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_SupportsLightLayers = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_UseAdaptivePerformance", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_UseAdaptivePerformance = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }
                                
                fieldInfo = t.GetField("m_ColorGradingMode", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ColorGradingMode = (ColorGradingMode)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ColorGradingLutSize", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ColorGradingLutSize = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_UseFastSRGBLinearConversion", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_UseFastSRGBLinearConversion = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowType", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowType = (ShadowQuality)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_LocalShadowsSupported", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_LocalShadowsSupported = (bool)fieldInfo.GetValue(renderPipelineAsset);
                }
                
                fieldInfo = t.GetField("m_LocalShadowsAtlasResolution", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_LocalShadowsAtlasResolution = (ShadowResolution)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_MaxPixelLights", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_MaxPixelLights = (int)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowAtlasResolution", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowAtlasResolution = (ShadowResolution)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShadowAtlasResolution", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShadowAtlasResolution = (ShadowResolution)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_ShaderVariantLogLevel", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_ShaderVariantLogLevel = (ShaderVariantLogLevel)fieldInfo.GetValue(renderPipelineAsset);
                }

                fieldInfo = t.GetField("m_VolumeFrameworkUpdateMode", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    m_VolumeFrameworkUpdateMode = (VolumeFrameworkUpdateMode)fieldInfo.GetValue(renderPipelineAsset);
                }
            }
        }


        public override void WriteBack(RenderPipelineAsset renderPipelineAsset)
        {
            // SetValueを行う際に引数の型をリフレクション側に合わせる必要があるが、System.Convert.ChangeTypeで例外がスローされる。
            // C#ワカラン
            


            if (!dirty)
            {
                return;
            }
            dirty = false;
            base.WriteBack(renderPipelineAsset);

            var t = renderPipelineAsset.GetType();
            FieldInfo fieldInfo;
            

            // Rendering
            fieldInfo = t.GetField("m_RequireDepthTexture", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_RequireDepthTexture);
            }

            fieldInfo = t.GetField("m_RequireOpaqueTexture", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_RequireOpaqueTexture);
            }                        
            
            // Quality
            fieldInfo = t.GetField("m_SupportsHDR", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_SupportsHDR);
            }

            fieldInfo = t.GetField("m_MSAA", BindingFlags.Instance | BindingFlags.NonPublic);            
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, (int)m_MSAA);
            }

            fieldInfo = t.GetField("m_RenderScale", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_RenderScale);
            }
                                               
            fieldInfo = t.GetField("m_AdditionalLightsPerObjectLimit", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_AdditionalLightsPerObjectLimit);
            }

            // Shadows
            fieldInfo = t.GetField("m_ShadowDistance", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_ShadowDistance);
            }

            fieldInfo = t.GetField("m_ShadowCascadeCount", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_ShadowCascadeCount);
            }


            fieldInfo = t.GetField("m_CascadeBorder", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_CascadeBorder);
            }

            fieldInfo = t.GetField("m_ShadowDepthBias", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_ShadowDepthBias);
            }

            fieldInfo = t.GetField("m_ShadowNormalBias", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_ShadowNormalBias);
            }            

            fieldInfo = t.GetField("m_UseAdaptivePerformance", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_UseAdaptivePerformance);
            }

            fieldInfo = t.GetField("m_ColorGradingMode", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {                
                fieldInfo.SetValue(renderPipelineAsset, (int)m_ColorGradingMode);                
            }

            fieldInfo = t.GetField("m_ColorGradingLutSize", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, m_ColorGradingLutSize);
            }
                       

            fieldInfo = t.GetField("m_ShaderVariantLogLevel", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, (int)m_ShaderVariantLogLevel);
            }

            fieldInfo = t.GetField("m_VolumeFrameworkUpdateMode", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(renderPipelineAsset, (int)m_VolumeFrameworkUpdateMode);
            }
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);

            binaryWriter.Write((int)k_AssetVersion);
            binaryWriter.Write((int)k_AssetPreviousVersion);
            binaryWriter.Write((int)m_RendererType);

            SerializerKun.Serialize<ScriptableObjectKun>(binaryWriter,m_RendererData);
            SerializerKun.Serialize<ScriptableObjectKun>(binaryWriter,m_RendererDataList);
            binaryWriter.Write((int)m_DefaultRendererIndex);

            binaryWriter.Write((bool)m_RequireDepthTexture);
            binaryWriter.Write((bool)m_RequireOpaqueTexture);
            binaryWriter.Write((int)m_OpaqueDownsampling);
            binaryWriter.Write((bool)m_SupportsTerrainHoles);
            binaryWriter.Write((int)m_StoreActionsOptimization);

            binaryWriter.Write((bool)m_SupportsHDR);
            binaryWriter.Write((int)m_MSAA);
            binaryWriter.Write((float)m_RenderScale);
            binaryWriter.Write((int)m_MainLightRenderingMode);
            binaryWriter.Write((bool)m_MainLightShadowsSupported);
            binaryWriter.Write((int)m_MainLightShadowmapResolution);

            binaryWriter.Write((int)m_AdditionalLightsRenderingMode);
            binaryWriter.Write((int)m_AdditionalLightsPerObjectLimit);
            binaryWriter.Write((bool)m_AdditionalLightShadowsSupported);
            binaryWriter.Write((int)m_AdditionalLightsShadowmapResolution);

            binaryWriter.Write((int)m_AdditionalLightsShadowResolutionTierLow);
            binaryWriter.Write((int)m_AdditionalLightsShadowResolutionTierMedium);
            binaryWriter.Write((int)m_AdditionalLightsShadowResolutionTierHigh);

            binaryWriter.Write((bool)m_ReflectionProbeBlending);
            binaryWriter.Write((bool)m_ReflectionProbeBoxProjection);

            binaryWriter.Write((float)m_ShadowDistance);
            binaryWriter.Write((int)m_ShadowCascadeCount);
            binaryWriter.Write((float)m_Cascade2Split);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_Cascade3Split);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_Cascade4Split);
            binaryWriter.Write((float)m_CascadeBorder);
            binaryWriter.Write((float)m_ShadowDepthBias);
            binaryWriter.Write((float)m_ShadowNormalBias);
            binaryWriter.Write((bool)m_SoftShadowsSupported);
            binaryWriter.Write((bool)m_ConservativeEnclosingSphere);
            binaryWriter.Write((int)m_NumIterationsEnclosingSphere);

            binaryWriter.Write((int)m_AdditionalLightsCookieResolution);
            binaryWriter.Write((int)m_AdditionalLightsCookieFormat);

            binaryWriter.Write((bool)m_UseSRPBatcher);
            binaryWriter.Write((bool)m_SupportsDynamicBatching);
            binaryWriter.Write((bool)m_MixedLightingSupported);
            binaryWriter.Write((bool)m_SupportsLightLayers);

            binaryWriter.Write((bool)m_UseAdaptivePerformance);

            binaryWriter.Write((int)m_ColorGradingMode);
            binaryWriter.Write((int)m_ColorGradingLutSize);
            binaryWriter.Write((bool)m_UseFastSRGBLinearConversion);

            binaryWriter.Write((int)m_ShadowType);
            binaryWriter.Write((bool)m_LocalShadowsSupported);
            binaryWriter.Write((int)m_LocalShadowsAtlasResolution);
            binaryWriter.Write((int)m_MaxPixelLights);
            binaryWriter.Write((int)m_ShadowAtlasResolution);

            binaryWriter.Write((int)m_ShaderVariantLogLevel);
            binaryWriter.Write((int)m_VolumeFrameworkUpdateMode);           
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            k_AssetVersion = binaryReader.ReadInt32();
            k_AssetPreviousVersion = binaryReader.ReadInt32();
            m_RendererType = (RendererType)binaryReader.ReadInt32();

            m_RendererData = SerializerKun.DesirializeObject<ScriptableRendererDataKun>(binaryReader);
            m_RendererDataList = SerializerKun.DesirializeObjects<ScriptableRendererDataKun>(binaryReader);
            m_DefaultRendererIndex = binaryReader.ReadInt32();

            m_RequireDepthTexture = binaryReader.ReadBoolean();
            m_RequireOpaqueTexture = binaryReader.ReadBoolean();
            m_OpaqueDownsampling = (Downsampling)binaryReader.ReadInt32();
            m_SupportsTerrainHoles = binaryReader.ReadBoolean();
            m_StoreActionsOptimization = (StoreActionsOptimization)binaryReader.ReadInt32();

            m_SupportsHDR = binaryReader.ReadBoolean();
            m_MSAA = (MsaaQuality)binaryReader.ReadInt32();
            m_RenderScale = binaryReader.ReadSingle();
            m_MainLightRenderingMode = (LightRenderingMode)binaryReader.ReadInt32();
            m_MainLightShadowsSupported = binaryReader.ReadBoolean();
            m_MainLightShadowmapResolution = (ShadowResolution)binaryReader.ReadInt32();

            m_AdditionalLightsRenderingMode = (LightRenderingMode)binaryReader.ReadInt32();
            m_AdditionalLightsPerObjectLimit = binaryReader.ReadInt32();
            m_AdditionalLightShadowsSupported = binaryReader.ReadBoolean();
            m_AdditionalLightsShadowmapResolution = (ShadowResolution)binaryReader.ReadInt32();

            m_AdditionalLightsShadowResolutionTierLow = binaryReader.ReadInt32();
            m_AdditionalLightsShadowResolutionTierMedium = binaryReader.ReadInt32();
            m_AdditionalLightsShadowResolutionTierHigh = binaryReader.ReadInt32();

            m_ReflectionProbeBlending = binaryReader.ReadBoolean();
            m_ReflectionProbeBoxProjection = binaryReader.ReadBoolean();

            m_ShadowDistance = binaryReader.ReadSingle();
            m_ShadowCascadeCount = binaryReader.ReadInt32();
            m_Cascade2Split = binaryReader.ReadSingle();
            m_Cascade3Split = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
            m_Cascade4Split = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
            m_CascadeBorder = binaryReader.ReadSingle();
            m_ShadowDepthBias = binaryReader.ReadSingle();
            m_ShadowNormalBias = binaryReader.ReadSingle();
            m_SoftShadowsSupported = binaryReader.ReadBoolean();
            m_ConservativeEnclosingSphere = binaryReader.ReadBoolean();
            m_NumIterationsEnclosingSphere = binaryReader.ReadInt32();

            m_AdditionalLightsCookieResolution = (LightCookieResolution)binaryReader.ReadInt32();
            m_AdditionalLightsCookieFormat = (LightCookieFormat)binaryReader.ReadInt32();

            m_UseSRPBatcher = binaryReader.ReadBoolean();
            m_SupportsDynamicBatching = binaryReader.ReadBoolean();
            m_MixedLightingSupported = binaryReader.ReadBoolean();
            m_SupportsLightLayers = binaryReader.ReadBoolean();

            m_UseAdaptivePerformance = binaryReader.ReadBoolean();

            m_ColorGradingMode = (ColorGradingMode)binaryReader.ReadInt32();
            m_ColorGradingLutSize = binaryReader.ReadInt32();
            m_UseFastSRGBLinearConversion = binaryReader.ReadBoolean();

            m_ShadowType = (ShadowQuality)binaryReader.ReadInt32();
            m_LocalShadowsSupported = binaryReader.ReadBoolean();
            m_LocalShadowsAtlasResolution = (ShadowResolution)binaryReader.ReadInt32();
            m_MaxPixelLights = binaryReader.ReadInt32();
            m_ShadowAtlasResolution = (ShadowResolution)binaryReader.ReadInt32();

            m_ShaderVariantLogLevel = (ShaderVariantLogLevel)binaryReader.ReadInt32();
            m_VolumeFrameworkUpdateMode = (VolumeFrameworkUpdateMode)binaryReader.ReadInt32();
        }

        public override bool Equals(object obj)
        {
            if(base.Equals(obj) == false)
            {
                return false;
            }

            var clone = obj as UniversalRenderPipelineAssetKun;
            if(clone == null)
            {
                return false;
            }
            if(k_AssetVersion != clone.k_AssetVersion)
            {
                return false;
            }
            if(k_AssetPreviousVersion != clone.k_AssetPreviousVersion)
            {
                return false;
            }
            
            if(m_RendererType != clone.m_RendererType)
            {
                return false;
            }
            if(m_RendererData.Equals(clone.m_RendererData) == false)
            {
                return false;
            }

            if(m_RendererDataList.Length != clone.m_RendererDataList.Length)
            {
                return false;
            }
                       
            if(m_DefaultRendererIndex != clone.m_DefaultRendererIndex)
            {
                return false;
            }

            if(m_RequireDepthTexture != clone.m_RequireDepthTexture)
            {
                return false;
            }
            
            if(m_RequireOpaqueTexture != clone.m_RequireOpaqueTexture)
            {
                return false;
            }
            if(m_OpaqueDownsampling != clone.m_OpaqueDownsampling)
            {
                return false;
            }
            if(m_SupportsTerrainHoles != clone.m_SupportsTerrainHoles)
            {
                return false;
            }
            if(m_StoreActionsOptimization != clone.m_StoreActionsOptimization)
            {
                return false;
            }            
            if(m_SupportsHDR != clone.m_SupportsHDR)
            {
                return false;
            }
            if(m_MSAA != clone.m_MSAA)
            {
                return false;
            }
            if(m_RenderScale != clone.m_RenderScale)
            {
                return false;
            }            
            if(m_MainLightRenderingMode != clone.m_MainLightRenderingMode)
            {
                return false;
            }
            if(m_MainLightShadowsSupported != clone.m_MainLightShadowsSupported)
            {
                return false;
            }
            if(m_MainLightShadowmapResolution != clone.m_MainLightShadowmapResolution)
            {
                return false;
            }            
            if(m_AdditionalLightsRenderingMode != clone.m_AdditionalLightsRenderingMode)
            {
                return false;
            }
            if(m_AdditionalLightsPerObjectLimit != clone.m_AdditionalLightsPerObjectLimit)
            {
                return false;
            }
            if(m_AdditionalLightShadowsSupported != clone.m_AdditionalLightShadowsSupported)
            {
                return false;
            }
            if(m_AdditionalLightsShadowmapResolution != clone.m_AdditionalLightsShadowmapResolution)
            {
                return false;
            }
            if(m_AdditionalLightsShadowResolutionTierLow != clone.m_AdditionalLightsShadowResolutionTierLow)
            {
                return false;
            }
            if(m_AdditionalLightsShadowResolutionTierMedium != clone.m_AdditionalLightsShadowResolutionTierMedium)
            {
                return false;
            }
            if(m_AdditionalLightsShadowResolutionTierHigh != clone.m_AdditionalLightsShadowResolutionTierHigh)
            {
                return false;
            }
            // Reflection Probes
            if(m_ReflectionProbeBlending != clone.m_ReflectionProbeBlending)
            {
                return false;
            }
            if(m_ReflectionProbeBoxProjection != clone.m_ReflectionProbeBoxProjection)
            {
                return false;
            }
            if(m_ShadowDistance != clone.m_ShadowDistance)
            {
                return false;
            }
            if(m_ShadowCascadeCount != clone.m_ShadowCascadeCount)
            {
                return false;
            }
            if(m_Cascade2Split != clone.m_Cascade2Split)
            {
                return false;
            }
            if(m_Cascade3Split.Equals(clone.m_Cascade3Split) == false)
            {
                return false;
            }
            if(m_Cascade4Split.Equals(clone.m_Cascade4Split) == false)
            {
                return false;
            }
            if(m_CascadeBorder != clone.m_CascadeBorder)
            {
                return false;
            }
            if(m_ShadowDepthBias != clone.m_ShadowDepthBias)
            {
                return false;
            }
            if(m_ShadowNormalBias != clone.m_ShadowNormalBias)
            {
                return false;
            }
            if(m_SoftShadowsSupported != clone.m_SoftShadowsSupported)
            {
                return false;
            }
            if(m_ConservativeEnclosingSphere != clone.m_ConservativeEnclosingSphere)
            {
                return false;
            }
            if(m_NumIterationsEnclosingSphere != clone.m_NumIterationsEnclosingSphere)
            {
                return false;
            }            
            if(m_AdditionalLightsCookieResolution != clone.m_AdditionalLightsCookieResolution)
            {
                return false;
            }
            if(m_AdditionalLightsCookieFormat != clone.m_AdditionalLightsCookieFormat)
            {
                return false;
            }            
            if(m_UseSRPBatcher != clone.m_UseSRPBatcher)
            {
                return false;
            }
            if(m_SupportsDynamicBatching != clone.m_SupportsDynamicBatching)
            {
                return false;
            }
            if(m_MixedLightingSupported != clone.m_MixedLightingSupported)
            {
                return false;
            }
            if(m_SupportsLightLayers != clone.m_SupportsLightLayers)
            {
                return false;
            }            
            if(m_UseAdaptivePerformance != clone.m_UseAdaptivePerformance)
            {
                return false;
            }            
            if(m_ColorGradingMode != clone.m_ColorGradingMode)
            {
                return false;
            }
            if(m_ColorGradingLutSize != clone.m_ColorGradingLutSize)
            {
                return false;
            }
            if(m_UseFastSRGBLinearConversion != clone.m_UseFastSRGBLinearConversion)
            {
                return false;
            }            
            if(m_ShadowType != clone.m_ShadowType)
            {
                return false;
            }
            if(m_LocalShadowsSupported != clone.m_LocalShadowsSupported)
            {
                return false;
            }
            if(m_LocalShadowsAtlasResolution != clone.m_LocalShadowsAtlasResolution)
            {
                return false;
            }
            if(m_MaxPixelLights != clone.m_MaxPixelLights)
            {
                return false;
            }
            if(m_ShadowAtlasResolution != clone.m_ShadowAtlasResolution)
            {
                return false;
            }
            if(m_ShaderVariantLogLevel != clone.m_ShaderVariantLogLevel)
            {
                return false;
            }
            if(m_VolumeFrameworkUpdateMode != clone.m_VolumeFrameworkUpdateMode)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        float ValidateShadowBias(float value)
        {
            return Mathf.Max(0.0f, Mathf.Min(value, 10));
        }

        int ValidatePerObjectLights(int value)
        {
            return System.Math.Max(0, System.Math.Min(value, 10));
        }

        float ValidateRenderScale(float value)
        {
            return Mathf.Max(0.1f, Mathf.Min(value, 2f));
        }
    }
}
