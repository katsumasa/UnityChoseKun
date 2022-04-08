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
                public static readonly GUIContent SupportsComputeShaders = new GUIContent("supportsComputeShaders", "Compute シェーダーがサポートされているかどうか");
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






            public void OnGUI()
            {
                mScrollPos = EditorGUILayout.BeginScrollView(mScrollPos);
                EditorGUILayout.FloatField(Styles.BatteryLevel, SystemInfoKun.batteryLevel);
                EditorGUILayout.EnumPopup(Styles.BatteryStatus, SystemInfoKun.batteryStatus);
                EditorGUILayout.EnumPopup(Styles.CopyTextureSupport, SystemInfoKun.copyTextureSupport);
                EditorGUILayout.TextField(Styles.DeviceModel, SystemInfoKun.deviceModel);
                EditorGUILayout.TextField(Styles.DeviceName, SystemInfoKun.deviceName);
                EditorGUILayout.EnumPopup(Styles.DeviceType, SystemInfoKun.deviceType);
                EditorGUILayout.TextField(Styles.DeviceUniqueIdentifier, SystemInfoKun.deviceUniqueIdentifier);
                EditorGUILayout.IntField(Styles.GraphicsDeviceID, SystemInfoKun.graphicsDeviceID);
                EditorGUILayout.TextField(Styles.DeviceName, SystemInfoKun.deviceName);
                EditorGUILayout.EnumPopup(Styles.GraphicsDeviceType, SystemInfoKun.graphicsDeviceType);
                EditorGUILayout.TextField(Styles.GraphicsDeviceVendor, SystemInfoKun.graphicsDeviceVendor);
                EditorGUILayout.IntField(Styles.GraphicsDeviceVendorID, SystemInfoKun.graphicsDeviceVendorID);
                EditorGUILayout.TextField(Styles.GraphicsDeviceVersion, SystemInfoKun.graphicsDeviceVersion);
                EditorGUILayout.IntField(Styles.GraphicsMemorySize, SystemInfoKun.graphicsMemorySize);
                EditorGUILayout.Toggle(Styles.GraphicsMultiThreaded, SystemInfoKun.graphicsMultiThreaded);
                EditorGUILayout.IntField(Styles.GraphicsShaderLevel, SystemInfoKun.graphicsShaderLevel);
                EditorGUILayout.Toggle(Styles.GraphicsUVStartsAtTop, SystemInfoKun.graphicsUVStartsAtTop);
                EditorGUILayout.Toggle(Styles.HasDynamicUniformArrayIndexingInFragmentShaders, SystemInfoKun.hasDynamicUniformArrayIndexingInFragmentShaders);
                EditorGUILayout.Toggle(Styles.HasHiddenSurfaceRemovalOnGPU, SystemInfoKun.hasHiddenSurfaceRemovalOnGPU);
                EditorGUILayout.Toggle(Styles.HasMipMaxLevel, SystemInfoKun.hasMipMaxLevel);
                EditorGUILayout.IntField(Styles.MaxComputeBufferInputsCompute, SystemInfoKun.maxComputeBufferInputsCompute);
                EditorGUILayout.IntField(Styles.MaxComputeBufferInputsDomain, SystemInfoKun.maxComputeBufferInputsDomain);
                EditorGUILayout.IntField(Styles.MaxComputeBufferInputsFragment, SystemInfoKun.maxComputeBufferInputsFragment);
                EditorGUILayout.IntField(Styles.MaxComputeBufferInputsGeometry, SystemInfoKun.maxComputeBufferInputsGeometry);
                EditorGUILayout.IntField(Styles.MaxComputeBufferInputsHull, SystemInfoKun.maxComputeBufferInputsHull);
                EditorGUILayout.IntField(Styles.MaxComputeBufferInputsVertex, SystemInfoKun.maxComputeBufferInputsVertex);
                EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSize, SystemInfoKun.maxComputeWorkGroupSize);
                EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSizeX, SystemInfoKun.maxComputeWorkGroupSizeX);
                EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSizeY, SystemInfoKun.maxComputeWorkGroupSizeY);
                EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSizeZ, SystemInfoKun.maxComputeWorkGroupSizeZ);
                EditorGUILayout.IntField(Styles.MaxCubemapSize, SystemInfoKun.maxCubemapSize);
                EditorGUILayout.IntField(Styles.MaxTextureSize, SystemInfoKun.maxTextureSize);
#if UNITY_2020_1_OR_NEWER
                EditorGUILayout.IntField(Styles.MaxTextureSize, SystemInfoKun.constantBufferOffsetAlignment);
#else
            EditorGUILayout.Toggle(Styles.MinConstantBufferOffsetAlignment, SystemInfoKun.minConstantBufferOffsetAlignment);
#endif

                EditorGUILayout.EnumPopup(Styles.NpotSupport, SystemInfoKun.npotSupport);
                EditorGUILayout.TextField(Styles.OperatingSystem, SystemInfoKun.operatingSystem);
                EditorGUILayout.EnumPopup(Styles.OperatingSystemFamily, SystemInfoKun.operatingSystemFamily);
                EditorGUILayout.IntField(Styles.ProcessorCount, SystemInfoKun.processorCount);
                EditorGUILayout.IntField(Styles.ProcessorFrequency, SystemInfoKun.processorFrequency);
                EditorGUILayout.TextField(Styles.ProcessorType, SystemInfoKun.processorType);
                EditorGUILayout.EnumPopup(Styles.RenderingThreadingMode, SystemInfoKun.renderingThreadingMode);
                EditorGUILayout.IntField(Styles.SupportedRandomWriteTargetCount, SystemInfoKun.supportedRandomWriteTargetCount);
                EditorGUILayout.IntField(Styles.SupportedRenderTargetCount, SystemInfoKun.supportedRenderTargetCount);
                EditorGUILayout.Toggle(Styles.Supports2DArrayTextures, SystemInfoKun.supports2DArrayTextures);
                EditorGUILayout.Toggle(Styles.Supports32bitsIndexBuffer, SystemInfoKun.supports32bitsIndexBuffer);
                EditorGUILayout.Toggle(Styles.Supports3DRenderTextures, SystemInfoKun.supports3DRenderTextures);
                EditorGUILayout.Toggle(Styles.Supports3DTextures, SystemInfoKun.supports3DTextures);
                EditorGUILayout.Toggle(Styles.SupportsAccelerometer, SystemInfoKun.supportsAccelerometer);
                EditorGUILayout.Toggle(Styles.SupportsAsyncCompute, SystemInfoKun.supportsAsyncCompute);
                EditorGUILayout.Toggle(Styles.SupportsAsyncGPUReadback, SystemInfoKun.supportsAsyncGPUReadback);
                EditorGUILayout.Toggle(Styles.SupportsAudio, SystemInfoKun.supportsAudio);
                EditorGUILayout.Toggle(Styles.SupportsComputeShaders, SystemInfoKun.supportsComputeShaders);
                EditorGUILayout.Toggle(Styles.SupportsCubemapArrayTextures, SystemInfoKun.supportsCubemapArrayTextures);
                EditorGUILayout.Toggle(Styles.SupportsGeometryShaders, SystemInfoKun.supportsGeometryShaders);
                EditorGUILayout.Toggle(Styles.SupportsGraphicsFence, SystemInfoKun.supportsGraphicsFence);
                EditorGUILayout.Toggle(Styles.SupportsGyroscope, SystemInfoKun.supportsGyroscope);
                EditorGUILayout.Toggle(Styles.SupportsHardwareQuadTopology, SystemInfoKun.supportsHardwareQuadTopology);
                EditorGUILayout.Toggle(Styles.SupportsInstancing, SystemInfoKun.supportsInstancing);
                EditorGUILayout.Toggle(Styles.SupportsLocationService, SystemInfoKun.supportsLocationService);
                EditorGUILayout.Toggle(Styles.SupportsMipStreaming, SystemInfoKun.supportsMipStreaming);
                EditorGUILayout.Toggle(Styles.SupportsMotionVectors, SystemInfoKun.supportsMotionVectors);
                EditorGUILayout.Toggle(Styles.SupportsMultisampleAutoResolve, SystemInfoKun.supportsMultisampleAutoResolve);
                EditorGUILayout.IntField(Styles.SupportsMultisampledTextures, SystemInfoKun.supportsMultisampledTextures);
                EditorGUILayout.Toggle(Styles.SupportsRawShadowDepthSampling, SystemInfoKun.supportsRawShadowDepthSampling);
                EditorGUILayout.Toggle(Styles.SupportsRayTracing, SystemInfoKun.supportsRayTracing);
                EditorGUILayout.Toggle(Styles.SupportsSeparatedRenderTargetsBlend, SystemInfoKun.supportsSeparatedRenderTargetsBlend);
                EditorGUILayout.Toggle(Styles.SupportsSetConstantBuffer, SystemInfoKun.supportsSetConstantBuffer);
                EditorGUILayout.Toggle(Styles.SupportsSparseTextures, SystemInfoKun.supportsSparseTextures);
                EditorGUILayout.Toggle(Styles.SupportsTessellationShaders, SystemInfoKun.supportsTessellationShaders);
                EditorGUILayout.IntField(Styles.SupportsTextureWrapMirrorOnce, SystemInfoKun.supportsTextureWrapMirrorOnce);
                EditorGUILayout.IntField(Styles.SystemMemorySize, SystemInfoKun.systemMemorySize);
                EditorGUILayout.TextField(Styles.UnsupportedIdentifier, SystemInfoKun.unsupportedIdentifier);
                EditorGUILayout.Toggle(Styles.UsesLoadStoreActions, SystemInfoKun.usesLoadStoreActions);
                EditorGUILayout.Toggle(Styles.UsesReversedZBuffer, SystemInfoKun.usesReversedZBuffer);
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