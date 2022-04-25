using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;

    namespace Editor
    {
        [System.Serializable]
        public class SystemInfoView
        {
            static class Styles
            {
                public static readonly GUIContent BatteryLevel = new GUIContent("batteryLevel", "The current battery level ");
                public static readonly GUIContent BatteryStatus = new GUIContent("batteryStatus", "Returns the current status of the device's battery ");
                public static readonly GUIContent CopyTextureSupport = new GUIContent("copyTextureSupport", "さまざまな Graphics.CopyTexture ケースをサポートします");
                public static readonly GUIContent DeviceModel = new GUIContent("deviceModel", "デバイスのモデル");
                public static readonly GUIContent DeviceName = new GUIContent("deviceName", "デバイス名");
                public static readonly GUIContent DeviceType = new GUIContent("deviceType", "デバイスタイプ");
                public static readonly GUIContent DeviceUniqueIdentifier = new GUIContent("deviceUniqueIdentifier", "一意のデバイス識別子。すべてのデバイスで一意であることが保証されています");
                public static readonly GUIContent GraphicsDeviceID = new GUIContent("graphicsDeviceID", "グラフィックデバイスの識別コード");
                public static readonly GUIContent GraphicsDeviceName = new GUIContent("graphicsDeviceName", "グラフィックデバイス名");
                public static readonly GUIContent GraphicsDeviceType = new GUIContent("graphicsDeviceType", "デバイスのタイプを返します");
                public static readonly GUIContent GraphicsDeviceVendor = new GUIContent("graphicsDeviceVendor", "グラフィックデバイスのベンダー");
                public static readonly GUIContent GraphicsDeviceVendorID = new GUIContent("graphicsDeviceVendorID", "グラフィックデバイスのベンダーの識別コード");
                public static readonly GUIContent GraphicsDeviceVersion = new GUIContent("graphicsDeviceVersion", "グラフィックデバイスがサポートしている、グラフィックス API タイプとドライバーのバージョン");
                public static readonly GUIContent GraphicsMemorySize = new GUIContent("graphicsMemorySize", "ビデオメモリの量");
                public static readonly GUIContent GraphicsMultiThreaded = new GUIContent("graphicsMultiThreaded", "グラフィックデバイスがマルチスレッドレンダリングを行うかどうか");
                public static readonly GUIContent GraphicsShaderLevel = new GUIContent("graphicsShaderLevel", "グラフィックデバイスのシェーダーの性能レベル");
                public static readonly GUIContent GraphicsUVStartsAtTop = new GUIContent("graphicsUVStartsAtTop", "Returns true if the texture UV coordinate convention for this platform has Y starting at the top of the image.");
                public static readonly GUIContent HasDynamicUniformArrayIndexingInFragmentShaders = new GUIContent("hasDynamicUniformArrayIndexingInFragmentShaders", "Returns true when the GPU has native support for indexing uniform arrays in fragment shaders without restrictions.");
                public static readonly GUIContent HasHiddenSurfaceRemovalOnGPU = new GUIContent("hasHiddenSurfaceRemovalOnGPU", "True if the GPU supports hidden surface removal.");
                public static readonly GUIContent HasMipMaxLevel = new GUIContent("hasMipMaxLevel", "Returns true if the GPU supports partial mipmap chains");
#if UNITY_2020_1_OR_NEWER                
                public static readonly GUIContent HdrDisplaySupportFlags = new GUIContent("hdrDisplaySupportFlags", "Returns a bitwise combination of HDRDisplaySupportFlags describing the support for HDR displays on the system.");
#endif
                public static readonly GUIContent MaxComputeBufferInputsCompute = new GUIContent("maxComputeBufferInputsCompute", "Determines how many compute buffers Unity supports simultaneously in a compute shader for reading.");
                public static readonly GUIContent MaxComputeBufferInputsDomain = new GUIContent("maxComputeBufferInputsDomain", "Determines how many compute buffers Unity supports simultaneously in a domain shader for reading.");
                public static readonly GUIContent MaxComputeBufferInputsFragment = new GUIContent("maxComputeBufferInputsFragment", "Determines how many compute buffers Unity supports simultaneously in a fragment shader for reading. ");
                public static readonly GUIContent MaxComputeBufferInputsGeometry = new GUIContent("maxComputeBufferInputsGeometry", "Determines how many compute buffers Unity supports simultaneously in a geometry shader for reading. ");
                public static readonly GUIContent MaxComputeBufferInputsHull = new GUIContent("maxComputeBufferInputsHull", "Determines how many compute buffers Unity supports simultaneously in a hull shader for reading. ");
                public static readonly GUIContent MaxComputeBufferInputsVertex = new GUIContent("maxComputeBufferInputsVertex", "Determines how many compute buffers Unity supports simultaneously in a vertex shader for reading. ");
                public static readonly GUIContent MaxComputeWorkGroupSize = new GUIContent("maxComputeWorkGroupSize", "The largest total number of invocations in a single local work group that can be dispatched to a compute shader ");
                public static readonly GUIContent MaxComputeWorkGroupSizeX = new GUIContent("maxComputeWorkGroupSizeX", "The maximum number of work groups that a compute shader can use in X dimension ");
                public static readonly GUIContent MaxComputeWorkGroupSizeY = new GUIContent("maxComputeWorkGroupSizeY", "The maximum number of work groups that a compute shader can use in Y dimension ");
                public static readonly GUIContent MaxComputeWorkGroupSizeZ = new GUIContent("maxComputeWorkGroupSizeZ", "The maximum number of work groups that a compute shader can use in Z dimension ");
                public static readonly GUIContent MaxCubemapSize = new GUIContent("maxCubemapSize", "Maximum Cubemap texture size ");
#if UNITY_2021_2_OR_NEWER
                public static readonly GUIContent MaxGraphicsBufferSize = new GUIContent("maxGraphicsBufferSize", "The maximum size of a graphics buffer (GraphicsBuffer, ComputeBuffer, vertex/index buffer, etc.) in bytes (Read Only).Any GPU buffer (GraphicsBuffer, ComputeBuffer or a vertex/index buffer used by a Mesh) can not be larger than this amount of bytes.");
#endif                

                public static readonly GUIContent MaxTextureSize = new GUIContent("maxTextureSize", "テクスチャの最大サイズ");
#if UNITY_2020_1_OR_NEWER
                public static readonly GUIContent ConstantBufferOffsetAlignment = new GUIContent("constantBufferOffsetAlignment", "Minimum buffer offset (in bytes) when binding a constant buffer using Shader.SetConstantBuffer or Material.SetConstantBuffer.");
#else
            public static readonly GUIContent MinConstantBufferOffsetAlignment = new GUIContent("minConstantBufferOffsetAlignment", "Minimum buffer offset (in bytes) when binding a constant buffer using Shader.SetConstantBuffer or Material.SetConstantBuffer.");
#endif
                public static readonly GUIContent NpotSupport = new GUIContent("npotSupport", "GPU はどのような NPOT (2の2乗でない) テクスチャのサポートを提供するか。");
                public static readonly GUIContent OperatingSystem = new GUIContent("operatingSystem", "OS 名とバージョン");
                public static readonly GUIContent OperatingSystemFamily = new GUIContent("OperatingSystemFamily", "Returns the operating system family the game is running on ");
                public static readonly GUIContent ProcessorCount = new GUIContent("processorCount", "現在のプロセッサーの数");
                public static readonly GUIContent ProcessorFrequency = new GUIContent("processorFrequency", "MHz単位のプロセッサー周波数");
                public static readonly GUIContent ProcessorType = new GUIContent("processorType", "プロセッサー名");
                public static readonly GUIContent RenderingThreadingMode = new GUIContent("renderingThreadingMode", "Application's actual rendering threading mode ");
                public static readonly GUIContent SupportedRandomWriteTargetCount = new GUIContent("supportedRandomWriteTargetCount", "The maximum number of random write targets (UAV) that Unity supports simultaneously. ");
                public static readonly GUIContent SupportedRenderTargetCount = new GUIContent("supportedRenderTargetCount", "サポートしているレンダリングターゲットの数");
                public static readonly GUIContent Supports2DArrayTextures = new GUIContent("supports2DArrayTextures", "2D 配列テクスチャがサポートされているかどうか");
                public static readonly GUIContent Supports32bitsIndexBuffer = new GUIContent("supports32bitsIndexBuffer", "Are 32-bit index buffers supported? ");
                public static readonly GUIContent Supports3DRenderTextures = new GUIContent("supports3DRenderTextures", "Are 3D (volume) RenderTextures supported?");
                public static readonly GUIContent Supports3DTextures = new GUIContent("supports3DTextures", "3D (volume) テクスチャがサポートされているかどうか");
                public static readonly GUIContent SupportsAccelerometer = new GUIContent("supportsAccelerometer", "加速度センサーを利用できるかどうか");
                public static readonly GUIContent SupportsAsyncCompute = new GUIContent("supportsAsyncCompute", "Returns true when the platform supports asynchronous compute queues and false if otherwise.");
                public static readonly GUIContent SupportsAsyncGPUReadback = new GUIContent("supportsAsyncGPUReadback", "Returns true if asynchronous readback of GPU data is available for this device and false otherwise.");
                public static readonly GUIContent SupportsAudio = new GUIContent("supportsAudio", "Is there an Audio device available for playback?");
#if UNITY_2020_1_OR_NEWER
                public static readonly GUIContent SupportsCompressed3DTextures = new GUIContent("supportsCompressed3DTextures", "Are compressed formats for 3D (volume) textures supported? (Read Only).Not all graphics APIs and platforms support compressed Texture3D textures, for example Metal on macOS before 10.15 does not.");
#endif
                public static readonly GUIContent SupportsComputeShaders = new GUIContent("supportsComputeShaders", "Compute シェーダーがサポートされているかどうか");
#if UNITY_2020_1_OR_NEWER
                public static readonly GUIContent SupportsConservativeRaster = new GUIContent("supportsConservativeRaster", "Is conservative rasterization supported? ");
#endif
                public static readonly GUIContent SupportsCubemapArrayTextures = new GUIContent("supportsCubemapArrayTextures", "Are Cubemap Array textures supported?");
                public static readonly GUIContent SupportsGeometryShaders = new GUIContent("supportsGeometryShaders", "Are geometry shaders supported? ");
                public static readonly GUIContent SupportsGraphicsFence = new GUIContent("supportsGraphicsFence", "Returns true when the platform supports GraphicsFences, and false if otherwise.");
                public static readonly GUIContent SupportsGyroscope = new GUIContent("supportsGyroscope", "Returns true when the platform supports GraphicsFences, and false if otherwise.");
                public static readonly GUIContent SupportsHardwareQuadTopology = new GUIContent("supportsHardwareQuadTopology", "Does the hardware support quad topology? (Read Only)");
                public static readonly GUIContent SupportsInstancing = new GUIContent("supportsInstancing", "GPU ドローコールのインスタンス化がサポートされているかどうか");
                public static readonly GUIContent SupportsLocationService = new GUIContent("supportsLocationService", "ロケーションサービス（ GPS ）が利用できるかどうか");
                public static readonly GUIContent SupportsMipStreaming = new GUIContent("supportsMipStreaming", "streaming of texture mip maps supported?");
                public static readonly GUIContent SupportsMotionVectors = new GUIContent("supportsMotionVectors", "Whether motion vectors are supported on this platform.");
                public static readonly GUIContent SupportsMultisampleAutoResolve = new GUIContent("supportsMultisampleAutoResolve", "Returns true if multisampled textures are resolved automatically");
                public static readonly GUIContent SupportsMultisampledTextures = new GUIContent("supportsMultisampledTextures", "Are multisampled textures supported?");
                public static readonly GUIContent SupportsRawShadowDepthSampling = new GUIContent("supportsRawShadowDepthSampling", "サポートされているシャドウマップからのサンプリングは生のデプスか?");
                public static readonly GUIContent SupportsRayTracing = new GUIContent("supportsRayTracing", "Checks if ray tracing is supported by the current configuration.");
                public static readonly GUIContent SupportsSeparatedRenderTargetsBlend = new GUIContent("supportsSeparatedRenderTargetsBlend", "supportsSeparatedRenderTargetsBlend");
                public static readonly GUIContent SupportsSetConstantBuffer = new GUIContent("supportsSetConstantBuffer", "Does the current renderer support binding constant buffers directly?");
                public static readonly GUIContent SupportsShadows = new GUIContent("SupportsShadows", "グラフィックスカードが影をサポートしているかどうか");
                public static readonly GUIContent SupportsSparseTextures = new GUIContent("supportsSparseTextures", "スパーステクスチャはサポートされますか。");
                public static readonly GUIContent SupportsTessellationShaders = new GUIContent("supportsTessellationShaders", "Are tessellation shaders supported? ");
                public static readonly GUIContent SupportsTextureWrapMirrorOnce = new GUIContent("supportsTextureWrapMirrorOnce", "Returns true if the 'Mirror Once' texture wrap mode is supported. ");
                public static readonly GUIContent SupportsVibration = new GUIContent("supportsVibration", "そのデバイスは振動によるユーザーの触覚フィードバックを提供することができるか。");
                public static readonly GUIContent SystemMemorySize = new GUIContent("systemMemorySize", "システムメモリの量");
                public static readonly GUIContent UnsupportedIdentifier = new GUIContent("unsupportedIdentifier", "現在のプラットフォームでサポートされていない SystemInfo 文字列プロパティーからの戻り値");
                public static readonly GUIContent UsesLoadStoreActions = new GUIContent("usesLoadStoreActions", "True if the Graphics API takes RenderBufferLoadAction and RenderBufferStoreAction into account, false if otherwise.");
                public static readonly GUIContent UsesReversedZBuffer = new GUIContent("usesReversedZBuffer", "his property is true if the current platform uses a reversed depth buffer (where values range from 1 at the near plane and 0 at far plane), and false if the depth buffer is normal (0 is near, 1 is far). ");

            }




            [SerializeField] Vector2 mScrollPos;


            static SystemInfoView mInstance;
            public static SystemInfoView instance
            {
                get
                {
                    if(mInstance == null)
                    {
                        mInstance = new SystemInfoView();
                    }
                    return mInstance;
                }
            }



            public void OnGUI()
            {
                mScrollPos = EditorGUILayout.BeginScrollView(mScrollPos);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.BatteryLevel);
                EditorGUILayout.FloatField(SystemInfoKun.batteryLevel);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.BatteryStatus);
                EditorGUILayout.EnumPopup(SystemInfoKun.batteryStatus);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.CopyTextureSupport);
                EditorGUILayout.EnumPopup(SystemInfoKun.copyTextureSupport);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.DeviceModel);
                EditorGUILayout.TextField( SystemInfoKun.deviceModel);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.DeviceName);
                EditorGUILayout.TextField(SystemInfoKun.deviceName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.DeviceType);
                EditorGUILayout.EnumPopup(SystemInfoKun.deviceType);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.DeviceUniqueIdentifier);
                EditorGUILayout.TextField(SystemInfoKun.deviceUniqueIdentifier);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsDeviceID);
                EditorGUILayout.IntField(SystemInfoKun.graphicsDeviceID);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.DeviceName);
                EditorGUILayout.TextField(SystemInfoKun.deviceName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsDeviceType);
                EditorGUILayout.EnumPopup(SystemInfoKun.graphicsDeviceType);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsDeviceVendor);
                EditorGUILayout.TextField(SystemInfoKun.graphicsDeviceVendor);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsDeviceVendorID);
                EditorGUILayout.IntField(SystemInfoKun.graphicsDeviceVendorID);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsDeviceVersion);
                EditorGUILayout.TextField(SystemInfoKun.graphicsDeviceVersion);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsMemorySize);
                EditorGUILayout.IntField(SystemInfoKun.graphicsMemorySize);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsMultiThreaded);
                EditorGUILayout.Toggle(SystemInfoKun.graphicsMultiThreaded);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsShaderLevel);
                EditorGUILayout.IntField(SystemInfoKun.graphicsShaderLevel);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.GraphicsUVStartsAtTop);
                EditorGUILayout.Toggle(SystemInfoKun.graphicsUVStartsAtTop);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.HasDynamicUniformArrayIndexingInFragmentShaders);
                EditorGUILayout.Toggle(SystemInfoKun.hasDynamicUniformArrayIndexingInFragmentShaders);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.HasHiddenSurfaceRemovalOnGPU);
                EditorGUILayout.Toggle(SystemInfoKun.hasHiddenSurfaceRemovalOnGPU);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.HasMipMaxLevel);
                EditorGUILayout.Toggle(SystemInfoKun.hasMipMaxLevel);
                EditorGUILayout.EndHorizontal();

#if UNITY_2020_1_OR_NEWER
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.HdrDisplaySupportFlags);
                EditorGUILayout.EnumFlagsField(SystemInfoKun.hdrDisplaySupportFlags);
                EditorGUILayout.EndHorizontal();
#endif

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeBufferInputsCompute);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeBufferInputsCompute);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeBufferInputsDomain);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeBufferInputsDomain);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeBufferInputsFragment);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeBufferInputsFragment);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeBufferInputsGeometry);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeBufferInputsGeometry);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeBufferInputsHull);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeBufferInputsHull);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeBufferInputsVertex);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeBufferInputsVertex);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeWorkGroupSize);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeWorkGroupSize);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeWorkGroupSizeX);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeWorkGroupSizeX);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeWorkGroupSizeY);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeWorkGroupSizeY);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxComputeWorkGroupSizeZ);
                EditorGUILayout.IntField(SystemInfoKun.maxComputeWorkGroupSizeZ);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxCubemapSize);
                EditorGUILayout.IntField(SystemInfoKun.maxCubemapSize);
                EditorGUILayout.EndHorizontal();

#if UNITY_2021_2_OR_NEWER
                {
                    var maxGraphicsBufferSizeMB = SystemInfoKun.maxGraphicsBufferSize / 1024 / 1024;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Styles.MaxGraphicsBufferSize);
                    EditorGUILayout.LabelField($"{maxGraphicsBufferSizeMB}[MB]");
                    EditorGUILayout.EndHorizontal();
                }
#endif

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MaxTextureSize);
                EditorGUILayout.IntField(SystemInfoKun.maxTextureSize);
                EditorGUILayout.EndHorizontal();

#if UNITY_2020_1_OR_NEWER
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.ConstantBufferOffsetAlignment);
                EditorGUILayout.IntField(SystemInfoKun.constantBufferOffsetAlignment);
                EditorGUILayout.EndHorizontal();
#else
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.MinConstantBufferOffsetAlignment);
                EditorGUILayout.Toggle(SystemInfoKun.minConstantBufferOffsetAlignment,GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();
#endif
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.NpotSupport);
                EditorGUILayout.EnumPopup( SystemInfoKun.npotSupport);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.OperatingSystem);
                EditorGUILayout.TextField( SystemInfoKun.operatingSystem);
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.OperatingSystemFamily);
                EditorGUILayout.EnumPopup( SystemInfoKun.operatingSystemFamily);
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.ProcessorCount);
                EditorGUILayout.IntField(SystemInfoKun.processorCount);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.ProcessorFrequency);
                EditorGUILayout.IntField(SystemInfoKun.processorFrequency);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.ProcessorType);
                EditorGUILayout.TextField(SystemInfoKun.processorType);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.RenderingThreadingMode);
                EditorGUILayout.EnumPopup(SystemInfoKun.renderingThreadingMode);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportedRandomWriteTargetCount);
                EditorGUILayout.IntField(SystemInfoKun.supportedRandomWriteTargetCount);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportedRenderTargetCount);
                EditorGUILayout.IntField(SystemInfoKun.supportedRenderTargetCount);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.Supports2DArrayTextures);
                EditorGUILayout.Toggle(SystemInfoKun.supports2DArrayTextures);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.Supports32bitsIndexBuffer);
                EditorGUILayout.Toggle(SystemInfoKun.supports32bitsIndexBuffer);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.Supports3DRenderTextures);
                EditorGUILayout.Toggle(SystemInfoKun.supports3DRenderTextures);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.Supports3DTextures);
                EditorGUILayout.Toggle(SystemInfoKun.supports3DTextures);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsAccelerometer);
                EditorGUILayout.Toggle(SystemInfoKun.supportsAccelerometer);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsAsyncCompute);
                EditorGUILayout.Toggle(SystemInfoKun.supportsAsyncCompute);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsAsyncGPUReadback);
                EditorGUILayout.Toggle(SystemInfoKun.supportsAsyncGPUReadback);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsAudio);
                EditorGUILayout.Toggle(SystemInfoKun.supportsAudio);
                EditorGUILayout.EndHorizontal();

#if UNITY_2020_1_OR_NEWER
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsCompressed3DTextures);
                EditorGUILayout.Toggle(SystemInfoKun.supportsCompressed3DTextures);
                EditorGUILayout.EndHorizontal();
#endif

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsComputeShaders);
                EditorGUILayout.Toggle(SystemInfoKun.supportsComputeShaders);
                EditorGUILayout.EndHorizontal();

#if UNITY_2020_1_OR_NEWER
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsConservativeRaster);
                EditorGUILayout.Toggle(SystemInfoKun.supportsConservativeRaster);
                EditorGUILayout.EndHorizontal();
#endif
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsCubemapArrayTextures);
                EditorGUILayout.Toggle(SystemInfoKun.supportsCubemapArrayTextures);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsGeometryShaders);
                EditorGUILayout.Toggle(SystemInfoKun.supportsGeometryShaders);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsGraphicsFence);
                EditorGUILayout.Toggle(SystemInfoKun.supportsGraphicsFence);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsGyroscope);
                EditorGUILayout.Toggle(SystemInfoKun.supportsGyroscope);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsHardwareQuadTopology);
                EditorGUILayout.Toggle(SystemInfoKun.supportsHardwareQuadTopology);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsInstancing);
                EditorGUILayout.Toggle(SystemInfoKun.supportsInstancing);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsLocationService);
                EditorGUILayout.Toggle( SystemInfoKun.supportsLocationService);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsMipStreaming);
                EditorGUILayout.Toggle(SystemInfoKun.supportsMipStreaming);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsMotionVectors);
                EditorGUILayout.Toggle(SystemInfoKun.supportsMotionVectors);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsMultisampleAutoResolve);
                EditorGUILayout.Toggle(SystemInfoKun.supportsMultisampleAutoResolve);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsMultisampledTextures);
                EditorGUILayout.IntField(SystemInfoKun.supportsMultisampledTextures);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsRawShadowDepthSampling);
                EditorGUILayout.Toggle( SystemInfoKun.supportsRawShadowDepthSampling);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsRayTracing);
                EditorGUILayout.Toggle(SystemInfoKun.supportsRayTracing);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsSeparatedRenderTargetsBlend);
                EditorGUILayout.Toggle(SystemInfoKun.supportsSeparatedRenderTargetsBlend);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsSetConstantBuffer);
                EditorGUILayout.Toggle( SystemInfoKun.supportsSetConstantBuffer);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsSparseTextures);
                EditorGUILayout.Toggle(SystemInfoKun.supportsSparseTextures);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsTessellationShaders);
                EditorGUILayout.Toggle(SystemInfoKun.supportsTessellationShaders);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SupportsTextureWrapMirrorOnce);
                EditorGUILayout.IntField(SystemInfoKun.supportsTextureWrapMirrorOnce);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.SystemMemorySize);
                EditorGUILayout.IntField(SystemInfoKun.systemMemorySize);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.UnsupportedIdentifier);
                EditorGUILayout.TextField(SystemInfoKun.unsupportedIdentifier);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.UsesLoadStoreActions);
                EditorGUILayout.Toggle( SystemInfoKun.usesLoadStoreActions);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.UsesReversedZBuffer);
                EditorGUILayout.Toggle(SystemInfoKun.usesReversedZBuffer);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndScrollView();


                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Pull"))
                {
                    UnityChoseKunEditor.SendMessage<SystemInfoKun>(UnityChoseKun.MessageID.SystemInfoPull, null);
                }
                EditorGUILayout.EndHorizontal();
            }



            public void OnMessageEvent(BinaryReader binaryReader)
            {
                SystemInfoKun.instance.Deserialize(binaryReader);
            }


        }
    }
}