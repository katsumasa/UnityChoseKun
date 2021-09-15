using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SystemInfoKun : ISerializerKun
    {
        [SerializeField] public float batteryLevel;
        [SerializeField] public BatteryStatus batteryStatus;
        [SerializeField] public UnityEngine.Rendering.CopyTextureSupport copyTextureSupport;
        [SerializeField] public string deviceModel;
        [SerializeField] public string deviceName;
        [SerializeField] public DeviceType deviceType;

        [SerializeField] public string deviceUniqueIdentifier;
        [SerializeField] public int graphicsDeviceID;
        [SerializeField] public string graphicsDeviceName;
        [SerializeField] public UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType;

        [SerializeField] public string graphicsDeviceVendor;
        [SerializeField] public int graphicsDeviceVendorID;
        [SerializeField] public string graphicsDeviceVersion;
        [SerializeField] public int graphicsMemorySize;
        [SerializeField] public bool graphicsMultiThreaded;
        [SerializeField] public int graphicsShaderLevel;
        [SerializeField] public bool graphicsUVStartsAtTop;
        [SerializeField] public bool hasDynamicUniformArrayIndexingInFragmentShaders;
        [SerializeField] public bool hasHiddenSurfaceRemovalOnGPU;
        [SerializeField] public bool hasMipMaxLevel;
        [SerializeField] public int maxComputeBufferInputsCompute;
        [SerializeField] public int maxComputeBufferInputsDomain;
        [SerializeField] public int maxComputeBufferInputsFragment;
        [SerializeField] public int maxComputeBufferInputsGeometry;
        [SerializeField] public int maxComputeBufferInputsHull;
        [SerializeField] public int maxComputeBufferInputsVertex;
        [SerializeField] public int maxComputeWorkGroupSize;
        [SerializeField] public int maxComputeWorkGroupSizeX;
        [SerializeField] public int maxComputeWorkGroupSizeY;
        [SerializeField] public int maxComputeWorkGroupSizeZ;
        [SerializeField] public int maxCubemapSize;
        [SerializeField] public int maxTextureSize;
#if UNITY_2020_1_OR_NEWER
        [SerializeField] public int constantBufferOffsetAlignment;
#else
        [SerializeField] public bool minConstantBufferOffsetAlignment;
#endif
        [SerializeField] public NPOTSupport npotSupport;
        [SerializeField] public string operatingSystem;
        [SerializeField] public OperatingSystemFamily operatingSystemFamily;
        [SerializeField] public int processorCount;
        [SerializeField] public int processorFrequency;
        [SerializeField] public string processorType;
        [SerializeField] public UnityEngine.Rendering.RenderingThreadingMode renderingThreadingMode;
        [SerializeField] public int supportedRandomWriteTargetCount;
        [SerializeField] public int supportedRenderTargetCount;
        [SerializeField] public bool supports2DArrayTextures;
        [SerializeField] public bool supports32bitsIndexBuffer;
        [SerializeField] public bool supports3DRenderTextures;
        [SerializeField] public bool supports3DTextures;
        [SerializeField] public bool supportsAccelerometer;
        [SerializeField] public bool supportsAsyncCompute;
        [SerializeField] public bool supportsAsyncGPUReadback;
        [SerializeField] public bool supportsAudio;
        [SerializeField] public bool supportsComputeShaders;
        [SerializeField] public bool supportsCubemapArrayTextures;
        [SerializeField] public bool supportsGeometryShaders;
        [SerializeField] public bool supportsGraphicsFence;
        [SerializeField] public bool supportsGyroscope;
        [SerializeField] public bool supportsHardwareQuadTopology;
        [SerializeField] public bool supportsInstancing;
        [SerializeField] public bool supportsLocationService;
        [SerializeField] public bool supportsMipStreaming;
        [SerializeField] public bool supportsMotionVectors;
        [SerializeField] public bool supportsMultisampleAutoResolve;
        [SerializeField] public int supportsMultisampledTextures;
        [SerializeField] public bool supportsRawShadowDepthSampling;
        [SerializeField] public bool supportsRayTracing;
        [SerializeField] public bool supportsSeparatedRenderTargetsBlend;
        [SerializeField] public bool supportsSetConstantBuffer;
        [SerializeField] public bool supportsShadows;
        [SerializeField] public bool supportsSparseTextures;
        [SerializeField] public bool supportsTessellationShaders;
        [SerializeField] public int supportsTextureWrapMirrorOnce;
        [SerializeField] public bool supportsVibration;
        [SerializeField] public int systemMemorySize;
        [SerializeField] public string unsupportedIdentifier;
        [SerializeField] public bool usesLoadStoreActions;
        [SerializeField] public bool usesReversedZBuffer;


        public SystemInfoKun() : this(false) { }



        public SystemInfoKun(bool isSet)
        {
            if (isSet)
            {
                batteryLevel = SystemInfo.batteryLevel;
                batteryStatus = SystemInfo.batteryStatus;
                copyTextureSupport = SystemInfo.copyTextureSupport;
                deviceModel = SystemInfo.deviceModel;
                deviceName = SystemInfo.deviceName;
                deviceType = SystemInfo.deviceType;
                deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
                graphicsDeviceID = SystemInfo.graphicsDeviceID;
                graphicsDeviceName = SystemInfo.graphicsDeviceName;
                graphicsDeviceType = SystemInfo.graphicsDeviceType;
                graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
                graphicsDeviceVendorID = SystemInfo.graphicsDeviceVendorID;
                graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
                graphicsMemorySize = SystemInfo.graphicsMemorySize;
                graphicsMultiThreaded = SystemInfo.graphicsMultiThreaded;
                graphicsShaderLevel = SystemInfo.graphicsShaderLevel;
                graphicsUVStartsAtTop = SystemInfo.graphicsUVStartsAtTop;
                hasDynamicUniformArrayIndexingInFragmentShaders = SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
                hasHiddenSurfaceRemovalOnGPU = SystemInfo.hasHiddenSurfaceRemovalOnGPU;
                hasMipMaxLevel = SystemInfo.hasMipMaxLevel;
                maxComputeBufferInputsCompute = SystemInfo.maxComputeBufferInputsCompute;
                maxComputeBufferInputsDomain = SystemInfo.maxComputeBufferInputsDomain;
                maxComputeBufferInputsFragment = SystemInfo.maxComputeBufferInputsFragment;
                maxComputeBufferInputsGeometry = SystemInfo.maxComputeBufferInputsGeometry;
                maxComputeBufferInputsHull = SystemInfo.maxComputeBufferInputsHull;
                maxComputeBufferInputsVertex = SystemInfo.maxComputeBufferInputsVertex;
                maxComputeWorkGroupSize = SystemInfo.maxComputeWorkGroupSize;
                maxComputeWorkGroupSizeX = SystemInfo.maxComputeWorkGroupSizeX;
                maxComputeWorkGroupSizeY = SystemInfo.maxComputeWorkGroupSizeY;
                maxComputeWorkGroupSizeZ = SystemInfo.maxComputeWorkGroupSizeX;
                maxCubemapSize = SystemInfo.maxCubemapSize;
                maxTextureSize = SystemInfo.maxTextureSize;
#if UNITY_2020_1_OR_NEWER
                constantBufferOffsetAlignment = SystemInfo.constantBufferOffsetAlignment;
#else
                minConstantBufferOffsetAlignment = SystemInfo.minConstantBufferOffsetAlignment;
#endif
                npotSupport = SystemInfo.npotSupport;
                operatingSystem = SystemInfo.operatingSystem;
                operatingSystemFamily = SystemInfo.operatingSystemFamily;
                processorCount = SystemInfo.processorCount;
                processorFrequency = SystemInfo.processorFrequency;
                processorType = SystemInfo.processorType;
                renderingThreadingMode = SystemInfo.renderingThreadingMode;
                supportedRandomWriteTargetCount = SystemInfo.supportedRenderTargetCount;
                supportedRenderTargetCount = SystemInfo.supportedRenderTargetCount;
                supports2DArrayTextures = SystemInfo.supports2DArrayTextures;
                supports32bitsIndexBuffer = SystemInfo.supports32bitsIndexBuffer;
                supports3DRenderTextures = SystemInfo.supports3DRenderTextures;
                supports3DTextures = SystemInfo.supports3DTextures;
                supportsAccelerometer = SystemInfo.supportsAccelerometer;
                supportsAsyncCompute = SystemInfo.supportsAsyncCompute;
                supportsAsyncGPUReadback = SystemInfo.supportsAsyncGPUReadback;
                supportsAudio = SystemInfo.supportsAudio;
                supportsComputeShaders = SystemInfo.supportsComputeShaders;
                supportsCubemapArrayTextures = SystemInfo.supportsCubemapArrayTextures;
                supportsGeometryShaders = SystemInfo.supportsGeometryShaders;
                supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
                supportsGyroscope = SystemInfo.supportsGyroscope;
                supportsHardwareQuadTopology = SystemInfo.supportsHardwareQuadTopology;
                supportsInstancing = SystemInfo.supportsInstancing;
                supportsLocationService = SystemInfo.supportsLocationService;
                supportsMipStreaming = SystemInfo.supportsMipStreaming;
                supportsMotionVectors = SystemInfo.supportsMotionVectors;
                supportsMultisampleAutoResolve = SystemInfo.supportsMultisampleAutoResolve;
                supportsMultisampledTextures = SystemInfo.supportsMultisampledTextures;
                supportsRawShadowDepthSampling = SystemInfo.supportsRawShadowDepthSampling;
                supportsRayTracing = SystemInfo.supportsRayTracing;
                supportsSeparatedRenderTargetsBlend = SystemInfo.supportsSeparatedRenderTargetsBlend;
                supportsSetConstantBuffer = SystemInfo.supportsSetConstantBuffer;
                supportsShadows = SystemInfo.supportsShadows;
                supportsSparseTextures = SystemInfo.supportsSparseTextures;
                supportsTessellationShaders = SystemInfo.supportsTessellationShaders;
                supportsTextureWrapMirrorOnce = SystemInfo.supportsTextureWrapMirrorOnce;
                supportsVibration = SystemInfo.supportsVibration;
                systemMemorySize = SystemInfo.systemMemorySize;
                unsupportedIdentifier = SystemInfo.unsupportedIdentifier;
                usesLoadStoreActions = SystemInfo.usesLoadStoreActions;
                usesReversedZBuffer = SystemInfo.usesReversedZBuffer;
            }
            else
            {
                deviceModel = "";
                deviceName = "";
                deviceUniqueIdentifier = "";
                graphicsDeviceName = "";
                graphicsDeviceVendor = "";
                graphicsDeviceVersion = "";
                operatingSystem = "";
                processorType = "";
                unsupportedIdentifier = "";
            }
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {            
            binaryWriter.Write(batteryLevel);
            binaryWriter.Write((int)batteryStatus);
            binaryWriter.Write((int)copyTextureSupport);
            binaryWriter.Write(deviceModel);
            binaryWriter.Write(deviceName);
            binaryWriter.Write((int)deviceType);
            binaryWriter.Write(deviceUniqueIdentifier);
            binaryWriter.Write(graphicsDeviceID);
            binaryWriter.Write(graphicsDeviceName);
            binaryWriter.Write((int)graphicsDeviceType) ;
            binaryWriter.Write(graphicsDeviceVendor);
            binaryWriter.Write(graphicsDeviceVendorID);
            binaryWriter.Write(graphicsDeviceVersion);
            binaryWriter.Write(graphicsMemorySize);
            binaryWriter.Write(graphicsMultiThreaded);
            binaryWriter.Write(graphicsShaderLevel);
            binaryWriter.Write(graphicsUVStartsAtTop);
            binaryWriter.Write(hasDynamicUniformArrayIndexingInFragmentShaders);
            binaryWriter.Write(hasHiddenSurfaceRemovalOnGPU);
            binaryWriter.Write(hasMipMaxLevel);
            binaryWriter.Write(maxComputeBufferInputsCompute);
            binaryWriter.Write(maxComputeBufferInputsDomain);
            binaryWriter.Write(maxComputeBufferInputsFragment);
            binaryWriter.Write(maxComputeBufferInputsGeometry);
            binaryWriter.Write(maxComputeBufferInputsHull);
            binaryWriter.Write(maxComputeBufferInputsVertex);
            binaryWriter.Write(maxComputeWorkGroupSize);
            binaryWriter.Write(maxComputeWorkGroupSizeX);
            binaryWriter.Write(maxComputeWorkGroupSizeY);
            binaryWriter.Write(maxComputeWorkGroupSizeZ);
            binaryWriter.Write(maxCubemapSize);
            binaryWriter.Write(maxTextureSize);
#if UNITY_2020_1_OR_NEWER
            binaryWriter.Write(constantBufferOffsetAlignment);
#else
            binaryWriter.Write(minConstantBufferOffsetAlignment);
#endif
            binaryWriter.Write((int)npotSupport);
            binaryWriter.Write(operatingSystem);
            binaryWriter.Write((int)operatingSystemFamily);
            binaryWriter.Write(processorCount);
            binaryWriter.Write(processorFrequency);
            binaryWriter.Write(processorType);
            binaryWriter.Write((int)renderingThreadingMode);
            binaryWriter.Write(supportedRandomWriteTargetCount);
            binaryWriter.Write(supportedRenderTargetCount);
            binaryWriter.Write(supports2DArrayTextures);
            binaryWriter.Write(supports32bitsIndexBuffer);
            binaryWriter.Write(supports3DRenderTextures);
            binaryWriter.Write(supports3DTextures);
            binaryWriter.Write(supportsAccelerometer);
            binaryWriter.Write(supportsAsyncCompute);
            binaryWriter.Write(supportsAsyncGPUReadback);
            binaryWriter.Write(supportsAudio);
            binaryWriter.Write(supportsComputeShaders);
            binaryWriter.Write(supportsCubemapArrayTextures);
            binaryWriter.Write(supportsGeometryShaders);
            binaryWriter.Write(supportsGraphicsFence);
            binaryWriter.Write(supportsGyroscope);
            binaryWriter.Write(supportsHardwareQuadTopology);
            binaryWriter.Write(supportsInstancing);
            binaryWriter.Write(supportsLocationService);
            binaryWriter.Write(supportsMipStreaming);
            binaryWriter.Write(supportsMotionVectors);
            binaryWriter.Write(supportsMultisampleAutoResolve);
            binaryWriter.Write(supportsMultisampledTextures);
            binaryWriter.Write(supportsRawShadowDepthSampling);
            binaryWriter.Write(supportsRayTracing);
            binaryWriter.Write(supportsSeparatedRenderTargetsBlend);
            binaryWriter.Write(supportsSetConstantBuffer);
            binaryWriter.Write(supportsShadows);
            binaryWriter.Write(supportsSparseTextures);
            binaryWriter.Write(supportsTessellationShaders);
            binaryWriter.Write(supportsTextureWrapMirrorOnce);
            binaryWriter.Write(supportsVibration);
            binaryWriter.Write(systemMemorySize);
            binaryWriter.Write(unsupportedIdentifier);
            binaryWriter.Write(usesLoadStoreActions);
            binaryWriter.Write(usesReversedZBuffer);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            batteryLevel = binaryReader.ReadSingle();
            batteryStatus = (BatteryStatus)binaryReader.ReadInt32();
            copyTextureSupport = (UnityEngine.Rendering.CopyTextureSupport)binaryReader.ReadInt32();
            deviceModel = binaryReader.ReadString();
            deviceName = binaryReader.ReadString();
            deviceType = (DeviceType)binaryReader.ReadInt32();
            deviceUniqueIdentifier = binaryReader.ReadString();
            graphicsDeviceID = binaryReader.ReadInt32();
            graphicsDeviceName = binaryReader.ReadString();
            graphicsDeviceType = (UnityEngine.Rendering.GraphicsDeviceType)binaryReader.ReadInt32();
            graphicsDeviceVendor = binaryReader.ReadString();
            graphicsDeviceVendorID = binaryReader.ReadInt32();
            graphicsDeviceVersion = binaryReader.ReadString();
            graphicsMemorySize = binaryReader.ReadInt32();
            graphicsMultiThreaded = binaryReader.ReadBoolean();
            graphicsShaderLevel = binaryReader.ReadInt32();
            graphicsUVStartsAtTop = binaryReader.ReadBoolean();
            hasDynamicUniformArrayIndexingInFragmentShaders = binaryReader.ReadBoolean();
            hasHiddenSurfaceRemovalOnGPU = binaryReader.ReadBoolean();
            hasMipMaxLevel = binaryReader.ReadBoolean();
            maxComputeBufferInputsCompute = binaryReader.ReadInt32();
            maxComputeBufferInputsDomain = binaryReader.ReadInt32();
            maxComputeBufferInputsFragment = binaryReader.ReadInt32();
            maxComputeBufferInputsGeometry = binaryReader.ReadInt32();
            maxComputeBufferInputsHull = binaryReader.ReadInt32();
            maxComputeBufferInputsVertex = binaryReader.ReadInt32();
            maxComputeWorkGroupSize = binaryReader.ReadInt32();
            maxComputeWorkGroupSizeX = binaryReader.ReadInt32();
            maxComputeWorkGroupSizeY = binaryReader.ReadInt32();
            maxComputeWorkGroupSizeZ = binaryReader.ReadInt32();
            maxCubemapSize = binaryReader.ReadInt32();
            maxTextureSize = binaryReader.ReadInt32();
#if UNITY_2020_1_OR_NEWER
            constantBufferOffsetAlignment = binaryReader.ReadInt32();
#else
            minConstantBufferOffsetAlignment = binaryReader.ReadBoolean();
#endif
            npotSupport = (NPOTSupport)binaryReader.ReadInt32();
            operatingSystem = binaryReader.ReadString();
            operatingSystemFamily = (OperatingSystemFamily)binaryReader.ReadInt32();
            processorCount = binaryReader.ReadInt32();
            processorFrequency = binaryReader.ReadInt32();
            processorType = binaryReader.ReadString();
            renderingThreadingMode = (UnityEngine.Rendering.RenderingThreadingMode)binaryReader.ReadInt32();
            supportedRandomWriteTargetCount = binaryReader.ReadInt32();
            supportedRenderTargetCount = binaryReader.ReadInt32();
            supports2DArrayTextures = binaryReader.ReadBoolean();
            supports32bitsIndexBuffer = binaryReader.ReadBoolean();
            supports3DRenderTextures = binaryReader.ReadBoolean();
            supports3DTextures = binaryReader.ReadBoolean();
            supportsAccelerometer = binaryReader.ReadBoolean();
            supportsAsyncCompute = binaryReader.ReadBoolean();
            supportsAsyncGPUReadback = binaryReader.ReadBoolean();
            supportsAudio = binaryReader.ReadBoolean();
            supportsComputeShaders = binaryReader.ReadBoolean();
            supportsCubemapArrayTextures = binaryReader.ReadBoolean();
            supportsGeometryShaders = binaryReader.ReadBoolean();
            supportsGraphicsFence = binaryReader.ReadBoolean();
            supportsGyroscope = binaryReader.ReadBoolean();
            supportsHardwareQuadTopology = binaryReader.ReadBoolean();
            supportsInstancing = binaryReader.ReadBoolean();
            supportsLocationService = binaryReader.ReadBoolean();
            supportsMipStreaming = binaryReader.ReadBoolean();
            supportsMotionVectors = binaryReader.ReadBoolean();
            supportsMultisampleAutoResolve = binaryReader.ReadBoolean();
            supportsMultisampledTextures = binaryReader.ReadInt32();
            supportsRawShadowDepthSampling = binaryReader.ReadBoolean();
            supportsRayTracing = binaryReader.ReadBoolean();
            supportsSeparatedRenderTargetsBlend = binaryReader.ReadBoolean();
            supportsSetConstantBuffer = binaryReader.ReadBoolean();
            supportsShadows = binaryReader.ReadBoolean();
            supportsSparseTextures = binaryReader.ReadBoolean();
            supportsTessellationShaders = binaryReader.ReadBoolean();
            supportsTextureWrapMirrorOnce = binaryReader.ReadInt32();
            supportsVibration = binaryReader.ReadBoolean();
            systemMemorySize = binaryReader.ReadInt32();
            unsupportedIdentifier = binaryReader.ReadString();
            usesLoadStoreActions = binaryReader.ReadBoolean();
            usesReversedZBuffer = binaryReader.ReadBoolean();
        }
    }
}