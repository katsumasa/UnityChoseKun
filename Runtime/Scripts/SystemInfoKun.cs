using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{
    [System.Serializable]
    public class SystemInfoKun : ISerializerKun
    {
        static SystemInfoKun mInstance;

        public static SystemInfoKun instance
        {
            get { 
                if(mInstance == null)
                {
                    mInstance = new SystemInfoKun();
                }
                return mInstance; 
            }
        }



        float m_batteryLevel;
        BatteryStatus m_batteryStatus;
#if UNITY_2020_1_OR_NEWER
        int m_constantBufferOffsetAlignment;
#else
        bool m_minConstantBufferOffsetAlignment;
#endif
        UnityEngine.Rendering.CopyTextureSupport m_copyTextureSupport;
        string m_deviceModel;
        string m_deviceName;
        DeviceType m_deviceType;

        string m_deviceUniqueIdentifier;
        int m_graphicsDeviceID;
        string m_graphicsDeviceName;
        UnityEngine.Rendering.GraphicsDeviceType m_graphicsDeviceType;

        string m_graphicsDeviceVendor;
        int m_graphicsDeviceVendorID;
        string m_graphicsDeviceVersion;
        int m_graphicsMemorySize;
        bool m_graphicsMultiThreaded;
        int m_graphicsShaderLevel;
        bool m_graphicsUVStartsAtTop;
        bool m_hasDynamicUniformArrayIndexingInFragmentShaders;
        bool m_hasHiddenSurfaceRemovalOnGPU;
        bool m_hasMipMaxLevel;
#if UNITY_2020_1_OR_NEWER
        HDRDisplaySupportFlags m_hdrDisplaySupportFlags;
#endif
        int m_maxComputeBufferInputsCompute;
        int m_maxComputeBufferInputsDomain;
        int m_maxComputeBufferInputsFragment;
        int m_maxComputeBufferInputsGeometry;
        int m_maxComputeBufferInputsHull;
        int m_maxComputeBufferInputsVertex;
        int m_maxComputeWorkGroupSize;
        int m_maxComputeWorkGroupSizeX;
        int m_maxComputeWorkGroupSizeY;
        int m_maxComputeWorkGroupSizeZ;
        int m_maxCubemapSize;
#if UNITY_2021_2_OR_NEWER
        long m_maxGraphicsBufferSize;
#endif
        int m_maxTextureSize;

        NPOTSupport m_npotSupport;
        string m_operatingSystem;
        OperatingSystemFamily m_operatingSystemFamily;
        int m_processorCount;
        int m_processorFrequency;
        string m_processorType;
        UnityEngine.Rendering.RenderingThreadingMode m_renderingThreadingMode;
        int m_supportedRandomWriteTargetCount;
        int m_supportedRenderTargetCount;
        bool m_supports2DArrayTextures;
        bool m_supports32bitsIndexBuffer;
        bool m_supports3DRenderTextures;
        bool m_supports3DTextures;
        bool m_supportsAccelerometer;
        bool m_supportsAsyncCompute;
        bool m_supportsAsyncGPUReadback;
        bool m_supportsAudio;
#if UNITY_2020_1_OR_NEWER
        bool m_supportsCompressed3DTextures;
#endif
        bool m_supportsComputeShaders;
#if UNITY_2020_1_OR_NEWER
        bool m_supportsConservativeRaster;
#endif
        bool m_supportsCubemapArrayTextures;
        bool m_supportsGeometryShaders;
#if UNITY_2020_1_OR_NEWER
        bool m_supportsGpuRecorder;
#endif
        bool m_supportsGraphicsFence;
        bool m_supportsGyroscope;
        bool m_supportsHardwareQuadTopology;
        bool m_supportsInstancing;
        bool m_supportsLocationService;
        bool m_supportsMipStreaming;
        bool m_supportsMotionVectors;
        bool m_supportsMultisampleAutoResolve;
#if UNITY_2020_1_OR_NEWER        
        bool m_supportsMultisampled2DArrayTextures;
#endif
        int m_supportsMultisampledTextures;
#if UNITY_2020_1_OR_NEWER
        bool m_supportsMultiview;
#endif
        bool m_supportsRawShadowDepthSampling;
        bool m_supportsRayTracing;
        bool m_supportsSeparatedRenderTargetsBlend;
        bool m_supportsSetConstantBuffer;
        bool m_supportsShadows;
        bool m_supportsSparseTextures;
        bool m_supportsTessellationShaders;
        int m_supportsTextureWrapMirrorOnce;
        bool m_supportsVibration;
        int m_systemMemorySize;
        string m_unsupportedIdentifier;
        bool m_usesLoadStoreActions;
        bool m_usesReversedZBuffer;



        public static float batteryLevel
        {
            get { return instance.m_batteryLevel; }
        }
        
        public static BatteryStatus batteryStatus
        {
            get { return instance.m_batteryStatus; }
        }

        public static UnityEngine.Rendering.CopyTextureSupport copyTextureSupport
        {
            get { return instance.m_copyTextureSupport; }
        }

        public static string deviceModel
        {
            get { return instance.m_deviceModel; }
        }

        public static string deviceName
        {
            get { return instance.m_deviceName; }
        }

        public static DeviceType deviceType
        {
            get { return instance.m_deviceType; }
        }

        public static string deviceUniqueIdentifier
        {
            get { return instance.m_deviceUniqueIdentifier; }
        }

        public static int graphicsDeviceID
        {
            get { return instance.m_graphicsDeviceID; }
        }

        public static string graphicsDeviceName
        {
            get { return instance.m_graphicsDeviceName; }
        }

        public static UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType
        {
            get { return instance.m_graphicsDeviceType; }
        }

        public static string graphicsDeviceVendor
        {
            get { return instance.m_graphicsDeviceVendor; }
        }

        public static int graphicsDeviceVendorID
        {
            get { return instance.m_graphicsDeviceVendorID; }
        }

        public static string graphicsDeviceVersion
        {
            get { return instance.m_graphicsDeviceVersion; }
        }

        public static int graphicsMemorySize
        {
            get { return instance.m_graphicsMemorySize; }
        }

        public static bool graphicsMultiThreaded
        {
            get { return instance.m_graphicsMultiThreaded; }
        }
        
        public static int graphicsShaderLevel
        {
            get { return instance.m_graphicsShaderLevel; }
        }

        public static bool graphicsUVStartsAtTop
        {
            get { return instance.m_graphicsUVStartsAtTop; }
        }

        public static bool hasDynamicUniformArrayIndexingInFragmentShaders
        {
            get { return instance.m_hasDynamicUniformArrayIndexingInFragmentShaders; }
        }

        public static bool hasHiddenSurfaceRemovalOnGPU
        {
            get { return instance.m_hasHiddenSurfaceRemovalOnGPU; }
        }

        public static bool hasMipMaxLevel
        {
            get { return instance.m_hasMipMaxLevel; }
        }

#if UNITY_2020_1_OR_NEWER
        public static HDRDisplaySupportFlags hdrDisplaySupportFlags
        {
            get { return instance.m_hdrDisplaySupportFlags; }
        }
#endif

        public static int maxComputeBufferInputsCompute
        {
            get { return instance.m_maxComputeBufferInputsCompute; }
        }

        public static int maxComputeBufferInputsDomain
        {
            get { return instance.m_maxComputeBufferInputsDomain; }
        }

        public static int maxComputeBufferInputsFragment
        {
            get { return instance.m_maxComputeBufferInputsFragment; }
        }

        public static int maxComputeBufferInputsGeometry
        {
            get { return instance.m_maxComputeBufferInputsGeometry; }
        }

        public static int maxComputeBufferInputsHull
        {
            get { return instance.m_maxComputeBufferInputsHull; }
        }

        public static int maxComputeBufferInputsVertex
        {
            get { return instance.m_maxComputeBufferInputsVertex; }
        }

        public static int maxComputeWorkGroupSize
        {
            get { return instance.m_maxComputeWorkGroupSize; }
        }
        
        public static int maxComputeWorkGroupSizeX
        {
            get { return instance.m_maxComputeWorkGroupSizeX; }
        }

        public static int maxComputeWorkGroupSizeY
        {
            get { return instance.m_maxComputeWorkGroupSizeY; }
        }

        public static int maxComputeWorkGroupSizeZ
        {
            get { return instance.m_maxComputeWorkGroupSizeZ; }
        }

        public static int maxCubemapSize
        {
            get { return instance.m_maxCubemapSize; }
        }

#if UNITY_2021_2_OR_NEWER
        public static long maxGraphicsBufferSize
        {
            get { return instance.m_maxGraphicsBufferSize; }
        }
#endif

        public static int maxTextureSize
        {
            get { return instance.m_maxTextureSize; }
        }

#if UNITY_2020_1_OR_NEWER
        public static int constantBufferOffsetAlignment
        {
            get { return instance.m_constantBufferOffsetAlignment; }
        }
#else
        public static bool minConstantBufferOffsetAlignment
        {
            get {return instance.m_minConstantBufferOffsetAlignment;}
        }
#endif
        public static NPOTSupport npotSupport
        {
            get { return instance.m_npotSupport; }
        }

        public static string operatingSystem
        {
            get { return instance.m_operatingSystem; }
        }

        public static OperatingSystemFamily operatingSystemFamily
        {
            get { return instance.m_operatingSystemFamily; }
        }

        public static int processorCount
        {
            get { return instance.m_processorCount; }
        }

        public static int processorFrequency
        {
            get { return instance.m_processorFrequency; }
        }

        public static string processorType
        {
            get { return instance.m_processorType; }
        }

        public static UnityEngine.Rendering.RenderingThreadingMode renderingThreadingMode
        {
            get { return instance.m_renderingThreadingMode; }
        }

        public static int supportedRandomWriteTargetCount
        {
            get { return instance.m_supportedRandomWriteTargetCount; }
        }

        public static int supportedRenderTargetCount
        {
            get { return instance.m_supportedRenderTargetCount; }
        }
        
        public static bool supports2DArrayTextures
        {
            get { return instance.m_supports2DArrayTextures; }
        }

        public static bool supports32bitsIndexBuffer
        {
            get { return instance.m_supports32bitsIndexBuffer; }
        }

        public static bool supports3DRenderTextures
        {
            get { return instance.m_supports3DRenderTextures; }
        }

        public static bool supports3DTextures
        {
            get { return instance.m_supports3DTextures; }
        }

        public static bool supportsAccelerometer
        { 
            get { return instance.m_supportsAccelerometer; }
        }

        public static bool supportsAsyncCompute
        {
            get { return instance.m_supportsAsyncCompute; }
        }

        public static bool supportsAsyncGPUReadback
        {
            get { return instance.m_supportsAsyncGPUReadback; }
        }

        public static bool supportsAudio
        {
            get { return instance.m_supportsAudio; }
        }

#if UNITY_2020_1_OR_NEWER
        public static bool supportsCompressed3DTextures
        {
            get { return instance.m_supportsCompressed3DTextures; }
        }
#endif

        public static bool supportsComputeShaders
        {
            get { return instance.m_supportsComputeShaders; }
        }

#if UNITY_2020_1_OR_NEWER        
        public static bool supportsConservativeRaster
        {
            get { return instance.m_supportsConservativeRaster; }
        }
#endif

        public static bool supportsCubemapArrayTextures
        {
            get { return instance.m_supportsCubemapArrayTextures; }
        }

        public static bool supportsGeometryShaders
        {
            get { return instance.m_supportsGeometryShaders; }
        }
#if UNITY_2020_1_OR_NEWER
        public static bool supportsGpuRecorder
        {
            get { return instance.m_supportsGpuRecorder; }
        }
#endif
        public static bool supportsGraphicsFence
        {
            get { return instance.m_supportsGraphicsFence; }
        }

        public static bool supportsGyroscope
        {
            get { return instance.m_supportsGyroscope; }
        }

        public static bool supportsHardwareQuadTopology
        {
            get { return instance.m_supportsHardwareQuadTopology; }
        }

        public static bool supportsInstancing
        {
            get { return instance.m_supportsInstancing; }
        }

        public static bool supportsLocationService
        {
            get { return instance.m_supportsLocationService; }
        }

        public static bool supportsMipStreaming
        {
            get { return instance.m_supportsMipStreaming; }
        }

        public static bool supportsMotionVectors
        {
            get { return instance.m_supportsMotionVectors; }
        }
        
        public static bool supportsMultisampleAutoResolve
        {
            get { return instance.m_supportsMultisampleAutoResolve; }
        }

#if UNITY_2020_1_OR_NEWER        
        public static bool supportsMultisampled2DArrayTextures
        {
            get { return instance.m_supportsMultisampled2DArrayTextures; }
        }
#endif        
        public static int supportsMultisampledTextures
        {
            get { return instance.m_supportsMultisampledTextures; }
        }

#if UNITY_2020_1_OR_NEWER
        public static bool supportsMultiview
        {
            get { return instance.m_supportsMultiview; }
        }
#endif

        public static bool supportsRawShadowDepthSampling
        {
            get { return instance.m_supportsRawShadowDepthSampling; }
        }

        public static bool supportsRayTracing
        {
            get { return instance.m_supportsRayTracing; }
        }

        public static bool supportsSeparatedRenderTargetsBlend
        {
            get { return instance.m_supportsSeparatedRenderTargetsBlend; }
        }

        public static bool supportsSetConstantBuffer
        {
            get { return instance.m_supportsSetConstantBuffer; }
        }

        public static bool supportsShadows
        {
            get { return instance.m_supportsShadows; }
        }

        public static bool supportsSparseTextures
        {
            get { return instance.m_supportsSparseTextures; }
        }
        
        public static bool supportsTessellationShaders
        {
            get { return instance.m_supportsTessellationShaders; }
        }

        public static int supportsTextureWrapMirrorOnce
        {
            get { return instance.m_supportsTextureWrapMirrorOnce; }
        }

        public static bool supportsVibration
        {
            get { return instance.m_supportsVibration; }
        }

        public static int systemMemorySize
        {
            get { return instance.m_systemMemorySize; }
        }

        public static string unsupportedIdentifier
        {
            get { return instance.m_unsupportedIdentifier; }
        }

        public static bool usesLoadStoreActions
        {
            get { return instance.m_usesLoadStoreActions; }
        }

        public static bool usesReversedZBuffer
        {
            get { return instance.m_usesReversedZBuffer; }
        }


        public SystemInfoKun() : this(false) { }



        public SystemInfoKun(bool isSet)
        {
            if (isSet)
            {
                m_batteryLevel = SystemInfo.batteryLevel;
                m_batteryStatus = SystemInfo.batteryStatus;
#if UNITY_2020_1_OR_NEWER
                m_constantBufferOffsetAlignment = SystemInfo.constantBufferOffsetAlignment;
#else
                m_minConstantBufferOffsetAlignment = SystemInfo.minConstantBufferOffsetAlignment;
#endif
                m_copyTextureSupport = SystemInfo.copyTextureSupport;
                m_deviceModel = SystemInfo.deviceModel;
                m_deviceName = SystemInfo.deviceName;
                m_deviceType = SystemInfo.deviceType;
                m_deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
                m_graphicsDeviceID = SystemInfo.graphicsDeviceID;
                m_graphicsDeviceName = SystemInfo.graphicsDeviceName;
                m_graphicsDeviceType = SystemInfo.graphicsDeviceType;
                m_graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
                m_graphicsDeviceVendorID = SystemInfo.graphicsDeviceVendorID;
                m_graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
                m_graphicsMemorySize = SystemInfo.graphicsMemorySize;
                m_graphicsMultiThreaded = SystemInfo.graphicsMultiThreaded;
                m_graphicsShaderLevel = SystemInfo.graphicsShaderLevel;
                m_graphicsUVStartsAtTop = SystemInfo.graphicsUVStartsAtTop;
                m_hasDynamicUniformArrayIndexingInFragmentShaders = SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
                m_hasHiddenSurfaceRemovalOnGPU = SystemInfo.hasHiddenSurfaceRemovalOnGPU;
                m_hasMipMaxLevel = SystemInfo.hasMipMaxLevel;
#if UNITY_2020_1_OR_NEWER
                m_hdrDisplaySupportFlags = SystemInfo.hdrDisplaySupportFlags;
#endif
                m_maxComputeBufferInputsCompute = SystemInfo.maxComputeBufferInputsCompute;
                m_maxComputeBufferInputsDomain = SystemInfo.maxComputeBufferInputsDomain;
                m_maxComputeBufferInputsFragment = SystemInfo.maxComputeBufferInputsFragment;
                m_maxComputeBufferInputsGeometry = SystemInfo.maxComputeBufferInputsGeometry;
                m_maxComputeBufferInputsHull = SystemInfo.maxComputeBufferInputsHull;
                m_maxComputeBufferInputsVertex = SystemInfo.maxComputeBufferInputsVertex;
                m_maxComputeWorkGroupSize = SystemInfo.maxComputeWorkGroupSize;
                m_maxComputeWorkGroupSizeX = SystemInfo.maxComputeWorkGroupSizeX;
                m_maxComputeWorkGroupSizeY = SystemInfo.maxComputeWorkGroupSizeY;
                m_maxComputeWorkGroupSizeZ = SystemInfo.maxComputeWorkGroupSizeX;
                m_maxCubemapSize = SystemInfo.maxCubemapSize;
#if UNITY_2021_2_OR_NEWER
                m_maxGraphicsBufferSize = SystemInfo.maxGraphicsBufferSize;
#endif
                m_maxTextureSize = SystemInfo.maxTextureSize;
#if UNITY_2020_1_OR_NEWER
                m_constantBufferOffsetAlignment = SystemInfo.constantBufferOffsetAlignment;
#else
                m_minConstantBufferOffsetAlignment = SystemInfo.minConstantBufferOffsetAlignment;
#endif
                m_npotSupport = SystemInfo.npotSupport;
                m_operatingSystem = SystemInfo.operatingSystem;
                m_operatingSystemFamily = SystemInfo.operatingSystemFamily;
                m_processorCount = SystemInfo.processorCount;
                m_processorFrequency = SystemInfo.processorFrequency;
                m_processorType = SystemInfo.processorType;
                m_renderingThreadingMode = SystemInfo.renderingThreadingMode;
                m_supportedRandomWriteTargetCount = SystemInfo.supportedRenderTargetCount;
                m_supportedRenderTargetCount = SystemInfo.supportedRenderTargetCount;
                m_supports2DArrayTextures = SystemInfo.supports2DArrayTextures;
                m_supports32bitsIndexBuffer = SystemInfo.supports32bitsIndexBuffer;
                m_supports3DRenderTextures = SystemInfo.supports3DRenderTextures;
                m_supports3DTextures = SystemInfo.supports3DTextures;
                m_supportsAccelerometer = SystemInfo.supportsAccelerometer;
                m_supportsAsyncCompute = SystemInfo.supportsAsyncCompute;
                m_supportsAsyncGPUReadback = SystemInfo.supportsAsyncGPUReadback;
                m_supportsAudio = SystemInfo.supportsAudio;
#if UNITY_2020_1_OR_NEWER                
                m_supportsCompressed3DTextures = SystemInfo.supportsCompressed3DTextures;
#endif
                m_supportsComputeShaders = SystemInfo.supportsComputeShaders;
#if UNITY_2020_1_OR_NEWER
                m_supportsConservativeRaster = SystemInfo.supportsConservativeRaster;
#endif                
                m_supportsCubemapArrayTextures = SystemInfo.supportsCubemapArrayTextures;
                m_supportsGeometryShaders = SystemInfo.supportsGeometryShaders;
#if UNITY_2020_1_OR_NEWER
                m_supportsGpuRecorder = SystemInfo.supportsGpuRecorder;
#endif
                m_supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
                m_supportsGyroscope = SystemInfo.supportsGyroscope;
                m_supportsHardwareQuadTopology = SystemInfo.supportsHardwareQuadTopology;
                m_supportsInstancing = SystemInfo.supportsInstancing;
                m_supportsLocationService = SystemInfo.supportsLocationService;
                m_supportsMipStreaming = SystemInfo.supportsMipStreaming;
                m_supportsMotionVectors = SystemInfo.supportsMotionVectors;
                m_supportsMultisampleAutoResolve = SystemInfo.supportsMultisampleAutoResolve;
#if UNITY_2020_1_OR_NEWER
                m_supportsMultisampled2DArrayTextures = SystemInfo.supportsMultisampled2DArrayTextures;
#endif
                m_supportsMultisampledTextures = SystemInfo.supportsMultisampledTextures;
#if UNITY_2020_1_OR_NEWER
                m_supportsMultiview = SystemInfo.supportsMultiview;
#endif
                m_supportsRawShadowDepthSampling = SystemInfo.supportsRawShadowDepthSampling;
                m_supportsRayTracing = SystemInfo.supportsRayTracing;
                m_supportsSeparatedRenderTargetsBlend = SystemInfo.supportsSeparatedRenderTargetsBlend;
                m_supportsSetConstantBuffer = SystemInfo.supportsSetConstantBuffer;
                m_supportsShadows = SystemInfo.supportsShadows;
                m_supportsSparseTextures = SystemInfo.supportsSparseTextures;
                m_supportsTessellationShaders = SystemInfo.supportsTessellationShaders;
                m_supportsTextureWrapMirrorOnce = SystemInfo.supportsTextureWrapMirrorOnce;
                m_supportsVibration = SystemInfo.supportsVibration;
                m_systemMemorySize = SystemInfo.systemMemorySize;
                m_unsupportedIdentifier = SystemInfo.unsupportedIdentifier;
                m_usesLoadStoreActions = SystemInfo.usesLoadStoreActions;
                m_usesReversedZBuffer = SystemInfo.usesReversedZBuffer;
            }
            else
            {
                m_deviceModel = "";
                m_deviceName = "";
                m_deviceUniqueIdentifier = "";
                m_graphicsDeviceName = "";
                m_graphicsDeviceVendor = "";
                m_graphicsDeviceVersion = "";
                m_operatingSystem = "";
                m_processorType = "";
                m_unsupportedIdentifier = "";
            }
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {            
            binaryWriter.Write(m_batteryLevel);
            binaryWriter.Write((int)m_batteryStatus);
            binaryWriter.Write((int)m_copyTextureSupport);
            binaryWriter.Write(m_deviceModel);
            binaryWriter.Write(m_deviceName);
            binaryWriter.Write((int)m_deviceType);
            binaryWriter.Write(m_deviceUniqueIdentifier);
            binaryWriter.Write(m_graphicsDeviceID);
            binaryWriter.Write(m_graphicsDeviceName);
            binaryWriter.Write((int)m_graphicsDeviceType) ;
            binaryWriter.Write(m_graphicsDeviceVendor);
            binaryWriter.Write(m_graphicsDeviceVendorID);
            binaryWriter.Write(m_graphicsDeviceVersion);
            binaryWriter.Write(m_graphicsMemorySize);
            binaryWriter.Write(m_graphicsMultiThreaded);
            binaryWriter.Write(m_graphicsShaderLevel);
            binaryWriter.Write(m_graphicsUVStartsAtTop);
            binaryWriter.Write(m_hasDynamicUniformArrayIndexingInFragmentShaders);
            binaryWriter.Write(m_hasHiddenSurfaceRemovalOnGPU);
            binaryWriter.Write(m_hasMipMaxLevel);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write((int)m_hdrDisplaySupportFlags);
#endif
            binaryWriter.Write(m_maxComputeBufferInputsCompute);
            binaryWriter.Write(m_maxComputeBufferInputsDomain);
            binaryWriter.Write(m_maxComputeBufferInputsFragment);
            binaryWriter.Write(m_maxComputeBufferInputsGeometry);
            binaryWriter.Write(m_maxComputeBufferInputsHull);
            binaryWriter.Write(m_maxComputeBufferInputsVertex);
            binaryWriter.Write(m_maxComputeWorkGroupSize);
            binaryWriter.Write(m_maxComputeWorkGroupSizeX);
            binaryWriter.Write(m_maxComputeWorkGroupSizeY);
            binaryWriter.Write(m_maxComputeWorkGroupSizeZ);
            binaryWriter.Write(m_maxCubemapSize);

#if UNITY_2021_2_OR_NEWER
            binaryWriter.Write(m_maxGraphicsBufferSize);
#endif

            binaryWriter.Write(m_maxTextureSize);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(m_constantBufferOffsetAlignment);
#else
            binaryWriter.Write(m_minConstantBufferOffsetAlignment);
#endif
            binaryWriter.Write((int)m_npotSupport);
            binaryWriter.Write(m_operatingSystem);
            binaryWriter.Write((int)m_operatingSystemFamily);
            binaryWriter.Write(m_processorCount);
            binaryWriter.Write(m_processorFrequency);
            binaryWriter.Write(m_processorType);
            binaryWriter.Write((int)m_renderingThreadingMode);
            binaryWriter.Write(m_supportedRandomWriteTargetCount);
            binaryWriter.Write(m_supportedRenderTargetCount);
            binaryWriter.Write(m_supports2DArrayTextures);
            binaryWriter.Write(m_supports32bitsIndexBuffer);
            binaryWriter.Write(m_supports3DRenderTextures);
            binaryWriter.Write(m_supports3DTextures);
            binaryWriter.Write(m_supportsAccelerometer);
            binaryWriter.Write(m_supportsAsyncCompute);
            binaryWriter.Write(m_supportsAsyncGPUReadback);
            binaryWriter.Write(m_supportsAudio);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(m_supportsCompressed3DTextures);
#endif
            binaryWriter.Write(m_supportsComputeShaders);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(m_supportsConservativeRaster);
#endif
            binaryWriter.Write(m_supportsCubemapArrayTextures);
            binaryWriter.Write(m_supportsGeometryShaders);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(m_supportsGpuRecorder);
#endif
            binaryWriter.Write(m_supportsGraphicsFence);
            binaryWriter.Write(m_supportsGyroscope);
            binaryWriter.Write(m_supportsHardwareQuadTopology);
            binaryWriter.Write(m_supportsInstancing);
            binaryWriter.Write(m_supportsLocationService);
            binaryWriter.Write(m_supportsMipStreaming);
            binaryWriter.Write(m_supportsMotionVectors);
            binaryWriter.Write(m_supportsMultisampleAutoResolve);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(m_supportsMultisampled2DArrayTextures);
#endif
            binaryWriter.Write(m_supportsMultisampledTextures);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(m_supportsMultiview);
#endif
            binaryWriter.Write(m_supportsRawShadowDepthSampling);
            binaryWriter.Write(m_supportsRayTracing);
            binaryWriter.Write(m_supportsSeparatedRenderTargetsBlend);
            binaryWriter.Write(m_supportsSetConstantBuffer);
            binaryWriter.Write(m_supportsShadows);
            binaryWriter.Write(m_supportsSparseTextures);
            binaryWriter.Write(m_supportsTessellationShaders);
            binaryWriter.Write(m_supportsTextureWrapMirrorOnce);
            binaryWriter.Write(m_supportsVibration);
            binaryWriter.Write(m_systemMemorySize);
            binaryWriter.Write(m_unsupportedIdentifier);
            binaryWriter.Write(m_usesLoadStoreActions);
            binaryWriter.Write(m_usesReversedZBuffer);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_batteryLevel = binaryReader.ReadSingle();
            m_batteryStatus = (BatteryStatus)binaryReader.ReadInt32();
            m_copyTextureSupport = (UnityEngine.Rendering.CopyTextureSupport)binaryReader.ReadInt32();
            m_deviceModel = binaryReader.ReadString();
            m_deviceName = binaryReader.ReadString();
            m_deviceType = (DeviceType)binaryReader.ReadInt32();
            m_deviceUniqueIdentifier = binaryReader.ReadString();
            m_graphicsDeviceID = binaryReader.ReadInt32();
            m_graphicsDeviceName = binaryReader.ReadString();
            m_graphicsDeviceType = (UnityEngine.Rendering.GraphicsDeviceType)binaryReader.ReadInt32();
            m_graphicsDeviceVendor = binaryReader.ReadString();
            m_graphicsDeviceVendorID = binaryReader.ReadInt32();
            m_graphicsDeviceVersion = binaryReader.ReadString();
            m_graphicsMemorySize = binaryReader.ReadInt32();
            m_graphicsMultiThreaded = binaryReader.ReadBoolean();
            m_graphicsShaderLevel = binaryReader.ReadInt32();
            m_graphicsUVStartsAtTop = binaryReader.ReadBoolean();
            m_hasDynamicUniformArrayIndexingInFragmentShaders = binaryReader.ReadBoolean();
            m_hasHiddenSurfaceRemovalOnGPU = binaryReader.ReadBoolean();
            m_hasMipMaxLevel = binaryReader.ReadBoolean();
#if UNITY_2020_1_OR_NEWER
            m_hdrDisplaySupportFlags = (HDRDisplaySupportFlags)binaryReader.ReadInt32();
#endif
            m_maxComputeBufferInputsCompute = binaryReader.ReadInt32();
            m_maxComputeBufferInputsDomain = binaryReader.ReadInt32();
            m_maxComputeBufferInputsFragment = binaryReader.ReadInt32();
            m_maxComputeBufferInputsGeometry = binaryReader.ReadInt32();
            m_maxComputeBufferInputsHull = binaryReader.ReadInt32();
            m_maxComputeBufferInputsVertex = binaryReader.ReadInt32();
            m_maxComputeWorkGroupSize = binaryReader.ReadInt32();
            m_maxComputeWorkGroupSizeX = binaryReader.ReadInt32();
            m_maxComputeWorkGroupSizeY = binaryReader.ReadInt32();
            m_maxComputeWorkGroupSizeZ = binaryReader.ReadInt32();
            m_maxCubemapSize = binaryReader.ReadInt32();

#if UNITY_2021_2_OR_NEWER
            m_maxGraphicsBufferSize = binaryReader.ReadInt64();
#endif

            m_maxTextureSize = binaryReader.ReadInt32();
#if UNITY_2020_1_OR_NEWER
            m_constantBufferOffsetAlignment = binaryReader.ReadInt32();
#else
            m_minConstantBufferOffsetAlignment = binaryReader.ReadBoolean();
#endif
            m_npotSupport = (NPOTSupport)binaryReader.ReadInt32();
            m_operatingSystem = binaryReader.ReadString();
            m_operatingSystemFamily = (OperatingSystemFamily)binaryReader.ReadInt32();
            m_processorCount = binaryReader.ReadInt32();
            m_processorFrequency = binaryReader.ReadInt32();
            m_processorType = binaryReader.ReadString();
            m_renderingThreadingMode = (UnityEngine.Rendering.RenderingThreadingMode)binaryReader.ReadInt32();
            m_supportedRandomWriteTargetCount = binaryReader.ReadInt32();
            m_supportedRenderTargetCount = binaryReader.ReadInt32();
            m_supports2DArrayTextures = binaryReader.ReadBoolean();
            m_supports32bitsIndexBuffer = binaryReader.ReadBoolean();
            m_supports3DRenderTextures = binaryReader.ReadBoolean();
            m_supports3DTextures = binaryReader.ReadBoolean();
            m_supportsAccelerometer = binaryReader.ReadBoolean();
            m_supportsAsyncCompute = binaryReader.ReadBoolean();
            m_supportsAsyncGPUReadback = binaryReader.ReadBoolean();
            m_supportsAudio = binaryReader.ReadBoolean();
#if UNITY_2020_1_OR_NEWER
            m_supportsCompressed3DTextures = binaryReader.ReadBoolean();
#endif
            m_supportsComputeShaders = binaryReader.ReadBoolean();
#if UNITY_2020_1_OR_NEWER
            m_supportsConservativeRaster = binaryReader.ReadBoolean();
#endif
            m_supportsCubemapArrayTextures = binaryReader.ReadBoolean();
            m_supportsGeometryShaders = binaryReader.ReadBoolean();
#if UNITY_2020_1_OR_NEWER
            m_supportsGpuRecorder = binaryReader.ReadBoolean();
#endif
            m_supportsGraphicsFence = binaryReader.ReadBoolean();
            m_supportsGyroscope = binaryReader.ReadBoolean();
            m_supportsHardwareQuadTopology = binaryReader.ReadBoolean();
            m_supportsInstancing = binaryReader.ReadBoolean();
            m_supportsLocationService = binaryReader.ReadBoolean();
            m_supportsMipStreaming = binaryReader.ReadBoolean();
            m_supportsMotionVectors = binaryReader.ReadBoolean();
            m_supportsMultisampleAutoResolve = binaryReader.ReadBoolean();
#if UNITY_2020_1_OR_NEWER
            m_supportsMultisampled2DArrayTextures = binaryReader.ReadBoolean();
#endif
            m_supportsMultisampledTextures = binaryReader.ReadInt32();
#if UNITY_2020_1_OR_NEWER
            m_supportsMultiview = binaryReader.ReadBoolean();
#endif
            m_supportsRawShadowDepthSampling = binaryReader.ReadBoolean();
            m_supportsRayTracing = binaryReader.ReadBoolean();
            m_supportsSeparatedRenderTargetsBlend = binaryReader.ReadBoolean();
            m_supportsSetConstantBuffer = binaryReader.ReadBoolean();
            m_supportsShadows = binaryReader.ReadBoolean();
            m_supportsSparseTextures = binaryReader.ReadBoolean();
            m_supportsTessellationShaders = binaryReader.ReadBoolean();
            m_supportsTextureWrapMirrorOnce = binaryReader.ReadInt32();
            m_supportsVibration = binaryReader.ReadBoolean();
            m_systemMemorySize = binaryReader.ReadInt32();
            m_unsupportedIdentifier = binaryReader.ReadString();
            m_usesLoadStoreActions = binaryReader.ReadBoolean();
            m_usesReversedZBuffer = binaryReader.ReadBoolean();

            mInstance = this;
        }


        public override bool Equals(object obj)
        {
            var other = obj as SystemInfoKun;
            if(other == null)
            {
                return false;
            }


            if(m_batteryLevel != other.m_batteryLevel)
            {
                return false;
            }
            if(m_batteryStatus != other.m_batteryStatus)
            {
                return false;
            }

            if(m_copyTextureSupport != other.m_copyTextureSupport)
            {
                return false;
            }
            if(m_deviceModel != other.m_deviceModel)
            {
                return false;
            }

            if(m_deviceName != other.m_deviceName)
            {
                return false;
            }
            
            if(m_deviceType != other.m_deviceType)
            { return false; }

            if(m_deviceUniqueIdentifier != other.m_deviceUniqueIdentifier)
            {
                return false;
            }
            if(m_graphicsDeviceID != other.m_graphicsDeviceID)
            {
                return false;
            }
            if(m_graphicsDeviceName != other.m_graphicsDeviceName)
            {
                return false;
            }
            if(m_graphicsDeviceType != other.m_graphicsDeviceType)
            {
                return false;
            }

            if(m_graphicsDeviceVendor != other.m_graphicsDeviceVendor)
            {
                return false;
            }
            if(m_graphicsDeviceVendorID != other.m_graphicsDeviceVendorID)
            { 
                return false;
            }
            if(m_graphicsDeviceVersion != other.m_graphicsDeviceVersion)
            {
                return false;
            }
            if(m_graphicsMemorySize != other.m_graphicsMemorySize)
            {
                return false;
            }
            if(m_graphicsMultiThreaded != other.m_graphicsMultiThreaded)
            {
                return false;
            }
            if(m_graphicsShaderLevel != other.m_graphicsShaderLevel)
            {
                return false;
            }
            if(m_graphicsUVStartsAtTop != other.m_graphicsUVStartsAtTop)
            {
                return false;
            }
            if(m_hasDynamicUniformArrayIndexingInFragmentShaders != other.m_hasDynamicUniformArrayIndexingInFragmentShaders)
            {
                return false;
            }
            if(m_hasHiddenSurfaceRemovalOnGPU != other.m_hasHiddenSurfaceRemovalOnGPU)
            {
                return false;
            }
            if(m_hasMipMaxLevel != other.m_hasMipMaxLevel)
            { 
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_hdrDisplaySupportFlags != other.m_hdrDisplaySupportFlags)
            {
                return false;
            }
#endif

            if(m_maxComputeBufferInputsCompute != other.m_maxComputeBufferInputsCompute)
            {
                return false;
            }
            if(m_maxComputeBufferInputsDomain != other.m_maxComputeBufferInputsDomain)
            {
                return false;
            }
            if(m_maxComputeBufferInputsFragment != other.m_maxComputeBufferInputsFragment)
            {
                return false;
            }
            if(m_maxComputeBufferInputsGeometry != other.m_maxComputeBufferInputsGeometry)
            {
                return false;
            }
            if(m_maxComputeBufferInputsHull != other.m_maxComputeBufferInputsHull)
            {
                return false;
            }
            if(m_maxComputeBufferInputsVertex != other.m_maxComputeBufferInputsVertex)
            {
                return false;
            }
            if(m_maxComputeWorkGroupSize != other.m_maxComputeWorkGroupSize)
            {
                return false;
            }
            if(m_maxComputeWorkGroupSizeX != other.m_maxComputeWorkGroupSizeX)
            {
                return false;
            }
            if(m_maxComputeWorkGroupSizeY != other.m_maxComputeWorkGroupSizeY)
            {
                return false;
            }
            if(m_maxComputeWorkGroupSizeZ != other.m_maxComputeWorkGroupSizeZ)
            {
                return false;
            }
            if(m_maxCubemapSize != other.m_maxCubemapSize)
            {
                return false;
            }
#if UNITY_2021_2_OR_NEWER
            if(m_maxGraphicsBufferSize != other.m_maxGraphicsBufferSize)
            {
                return false;
            }
#endif

            if (m_maxTextureSize != other.m_maxTextureSize)
            {
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_constantBufferOffsetAlignment != other.m_constantBufferOffsetAlignment)
            {
                return false;
            }
#else
            if(m_minConstantBufferOffsetAlignment != other.m_minConstantBufferOffsetAlignment)
            {
                return false;
            }
#endif
            if(m_npotSupport != other.m_npotSupport)
            {
                return false;
            }
            if(m_operatingSystem != other.m_operatingSystem)
            {
                return false;
            }
            if(m_operatingSystemFamily != other.m_operatingSystemFamily)
            {
                return false;
            }
            if(m_processorCount != other.m_processorCount)
            {
                return false;
            }
            if(m_processorFrequency != other.m_processorFrequency)
            {
                return false;
            }
            if(m_processorType != other.m_processorType)
            {
                return false;
            }
            if(m_renderingThreadingMode != other.m_renderingThreadingMode)
            {
                return false;
            }
            if(m_supportedRandomWriteTargetCount != other.m_supportedRenderTargetCount)
            {
                return false;
            }
            if(m_supportedRenderTargetCount != other.m_supportedRenderTargetCount)
            {
                return false;
            }
            if(m_supports2DArrayTextures != other.m_supports2DArrayTextures)
            {
                return false;
            }
            if(m_supports32bitsIndexBuffer != other.m_supports32bitsIndexBuffer)
            {
                return false;
            }
            if(m_supports3DRenderTextures != other.m_supports3DRenderTextures)
            {
                return false;
            }
            if(m_supports3DTextures != other.m_supports3DTextures)
            {
                return false;
            }
            if(m_supportsAccelerometer != other.m_supportsAccelerometer)
            {
                return false;
            }
            if(m_supportsAsyncCompute != other.m_supportsAsyncCompute)
            {
                return false;
            }
            if(m_supportsAsyncGPUReadback != other.m_supportsAsyncGPUReadback)
            {
                return false;
            }
            if(m_supportsAudio != other.m_supportsAudio)
            {
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_supportsCompressed3DTextures != other.m_supportsCompressed3DTextures)
            {
                return false;
            }
#endif
            if (m_supportsComputeShaders != other.m_supportsComputeShaders)
            {
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_supportsConservativeRaster != other.m_supportsConservativeRaster)
            {
                return false;
            }
#endif
            if (m_supportsCubemapArrayTextures != other.m_supportsCubemapArrayTextures)
            {
                return false;
            }
            if(m_supportsGeometryShaders != other.m_supportsGeometryShaders)
            {
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_supportsGpuRecorder != other.m_supportsGpuRecorder)
            {
                return false;
            }
#endif
            if (m_supportsGraphicsFence != other.m_supportsGraphicsFence)
            {
                return false;
            }
            if(m_supportsGyroscope != other.m_supportsGyroscope)
            {
                return false;
            }
            if(m_supportsHardwareQuadTopology != other.m_supportsHardwareQuadTopology)
            {
                return false;
            }
            if(m_supportsInstancing != other.m_supportsInstancing)
            {
                return false;
            }
            if(m_supportsLocationService != other.m_supportsLocationService)
            {
                return false;
            }
            if(m_supportsMipStreaming != other.m_supportsMipStreaming)
            {
                return false;
            }
            if(m_supportsMotionVectors != other.m_supportsMotionVectors)
            {
                return false;
            }
            if(m_supportsMultisampleAutoResolve != other.m_supportsMultisampleAutoResolve)
            {
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_supportsMultisampled2DArrayTextures != other.m_supportsMultisampled2DArrayTextures)
            {
                return false;
            }
#endif
            if(m_supportsMultisampledTextures != other.m_supportsMultisampledTextures)
            {
                return false;
            }
#if UNITY_2020_1_OR_NEWER
            if(m_supportsMultiview != other.m_supportsMultiview)
            {
                return false;
            }
#endif
            if (m_supportsRawShadowDepthSampling != other.m_supportsRawShadowDepthSampling)
            {
                return false;
            }
            if(m_supportsRayTracing != other.m_supportsRayTracing)
            {
                return false;
            }
            if(m_supportsSeparatedRenderTargetsBlend != other.m_supportsSeparatedRenderTargetsBlend)
            {
                return false;
            }
            if(m_supportsSetConstantBuffer != other.m_supportsSetConstantBuffer)
            {
                return false;
            }
            if(m_supportsShadows != other.m_supportsShadows)
            {
                return false;
            }
            if(m_supportsSparseTextures != other.m_supportsSparseTextures)
            {
                return false;
            }
            if(m_supportsTessellationShaders != other.m_supportsTessellationShaders)
            {
                return false;
            }
            if(m_supportsTextureWrapMirrorOnce != other.m_supportsTextureWrapMirrorOnce)
            {
                return false;
            }
            if(m_supportsVibration != other.m_supportsVibration)
            {
                return false;
            }
            if(m_systemMemorySize != other.m_systemMemorySize)
            {
                return false;
            }
            if(m_unsupportedIdentifier != other.m_unsupportedIdentifier)
            {
                return false;
            }
            if(m_usesLoadStoreActions != other.m_usesLoadStoreActions)
            {
                return false;
            }
            if(m_usesReversedZBuffer != other.m_usesReversedZBuffer)
            {
                return false;
            }

            return true;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}