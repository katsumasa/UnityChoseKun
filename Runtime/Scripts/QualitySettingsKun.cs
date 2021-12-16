using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class QualitySettingsKun : ISerializerKun
    {
        [SerializeField] public RenderPipelineAssetKun renderPipeline;
        [SerializeField] public ColorSpace activeColorSpace;
        [SerializeField] public AnisotropicFiltering anisotropicFiltering;
        [SerializeField] public int antiAliasing;
        [SerializeField] public int asyncUploadBufferSize;
        [SerializeField] public bool asyncUploadPersistentBuffer;
        [SerializeField] public int asyncUploadTimeSlice;
        [SerializeField] public bool billboardsFaceCameraPosition;
        [SerializeField] public ColorSpace desiredColorSpace;
        [SerializeField] public float lodBias;
        [SerializeField] public int masterTextureLimit;
        [SerializeField] public int maximumLODLevel;
        [SerializeField] public int maxQueuedFrames;
        [SerializeField] public string[] names;
        [SerializeField] public int particleRaycastBudget;
        [SerializeField] public int pixelLightCount;
        [SerializeField] public bool realtimeReflectionProbes;
        [SerializeField] public float resolutionScalingFixedDPIFactor;
        [SerializeField] public float shadowCascade2Split;
        [SerializeField] public Vector3Kun mShadowCascade4Split;
        [SerializeField] public int shadowCascades;
        [SerializeField] public float shadowDistance;
        [SerializeField] public ShadowmaskMode shadowmaskMode;
        [SerializeField] public float shadowNearPlaneOffset;
        [SerializeField] public ShadowProjection shadowProjection;
        [SerializeField] public ShadowResolution shadowResolution;
        [SerializeField] public ShadowQuality shadows;
#if UNITY_2019_1_OR_NEWER
        [SerializeField] public SkinWeights skinWeights;
#endif
        [SerializeField] public bool softParticles;
        [SerializeField] public bool softVegetation;
        [SerializeField] public bool streamingMipmapsActive;
        [SerializeField] public bool streamingMipmapsAddAllCameras;
        [SerializeField] public int streamingMipmapsMaxFileIORequests;
        [SerializeField] public int streamingMipmapsMaxLevelReduction;
        [SerializeField] public float streamingMipmapsMemoryBudget;
        [SerializeField] public int streamingMipmapsRenderersPerFrame;
        [SerializeField] public int vSyncCount;

        [SerializeField] public bool isDirty;


        public Vector3 shadowCascade4Split
        {
            get { return mShadowCascade4Split.GetVector3(); }
            set { mShadowCascade4Split = new Vector3Kun(value); }
        }


        /// <summary>
        /// 
        /// </summary>
        public QualitySettingsKun() : this(false) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSet"></param>
        public QualitySettingsKun(bool isSet) : base()
        {
            isDirty = false;
            if (isSet)
            {
#if UNITY_2019_1_OR_NEWER
                if (QualitySettings.renderPipeline)
                {
                    renderPipeline = new RenderPipelineAssetKun(QualitySettings.renderPipeline);
                }
                else
                {
                    renderPipeline = null;
                }
#else
                renderPipeline = null;
#endif
                activeColorSpace = QualitySettings.activeColorSpace;
                anisotropicFiltering = QualitySettings.anisotropicFiltering;
                antiAliasing = QualitySettings.antiAliasing;
                asyncUploadBufferSize = QualitySettings.asyncUploadBufferSize;
                asyncUploadPersistentBuffer = QualitySettings.asyncUploadPersistentBuffer;
                asyncUploadTimeSlice = QualitySettings.asyncUploadTimeSlice;
                billboardsFaceCameraPosition = QualitySettings.billboardsFaceCameraPosition;
                desiredColorSpace = QualitySettings.desiredColorSpace;
                lodBias = QualitySettings.lodBias;
                masterTextureLimit = QualitySettings.masterTextureLimit;
                maximumLODLevel = QualitySettings.maximumLODLevel;
                maxQueuedFrames = QualitySettings.maxQueuedFrames;
                names = QualitySettings.names ;
                particleRaycastBudget = QualitySettings.particleRaycastBudget;
                pixelLightCount = QualitySettings.pixelLightCount;
                realtimeReflectionProbes = QualitySettings.realtimeReflectionProbes;
                resolutionScalingFixedDPIFactor = QualitySettings.resolutionScalingFixedDPIFactor;
                shadowCascade2Split = QualitySettings.shadowCascade2Split;
                mShadowCascade4Split = new Vector3Kun(QualitySettings.shadowCascade4Split);
                shadowCascades = QualitySettings.shadowCascades;
                shadowDistance = QualitySettings.shadowDistance;
                shadowmaskMode = QualitySettings.shadowmaskMode;
                shadowNearPlaneOffset = QualitySettings.shadowNearPlaneOffset;
                shadowProjection = QualitySettings.shadowProjection;
                shadowResolution = QualitySettings.shadowResolution;
                shadows = QualitySettings.shadows;
#if UNITY_2019_1_OR_NEWER
                skinWeights = QualitySettings.skinWeights;
#endif
                softParticles = QualitySettings.softParticles;
                softVegetation = QualitySettings.softVegetation;
                streamingMipmapsActive = QualitySettings.streamingMipmapsActive;
                streamingMipmapsAddAllCameras = QualitySettings.streamingMipmapsAddAllCameras;
                streamingMipmapsMaxFileIORequests = QualitySettings.streamingMipmapsMaxFileIORequests;
                streamingMipmapsMaxLevelReduction = QualitySettings.streamingMipmapsMaxLevelReduction ;
                streamingMipmapsMemoryBudget = QualitySettings.streamingMipmapsMemoryBudget;
                streamingMipmapsRenderersPerFrame = QualitySettings.streamingMipmapsRenderersPerFrame;
                vSyncCount = QualitySettings.vSyncCount;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void WriteBack()
        {
            if (isDirty)
            {
#if UNITY_2019_1_OR_NEWER
                renderPipeline.WriteBack(QualitySettings.renderPipeline);
#endif
                QualitySettings.anisotropicFiltering = anisotropicFiltering;
                QualitySettings.antiAliasing = antiAliasing;
                QualitySettings.asyncUploadBufferSize= asyncUploadBufferSize;
                QualitySettings.asyncUploadPersistentBuffer = asyncUploadPersistentBuffer;
                QualitySettings.asyncUploadTimeSlice = asyncUploadTimeSlice;
                QualitySettings.billboardsFaceCameraPosition = billboardsFaceCameraPosition;
                QualitySettings.lodBias = lodBias;
                QualitySettings.masterTextureLimit = masterTextureLimit;
                QualitySettings.maximumLODLevel = maximumLODLevel;
                QualitySettings.maxQueuedFrames = maxQueuedFrames;
                QualitySettings.particleRaycastBudget = particleRaycastBudget;
                QualitySettings.pixelLightCount = pixelLightCount;
                QualitySettings.realtimeReflectionProbes = realtimeReflectionProbes;
                QualitySettings.resolutionScalingFixedDPIFactor = resolutionScalingFixedDPIFactor;
                QualitySettings.shadowCascade2Split = shadowCascade2Split;
                QualitySettings.shadowCascade4Split = shadowCascade4Split;
                QualitySettings.shadowCascades = shadowCascades;
                QualitySettings.shadowDistance = shadowDistance;
                QualitySettings.shadowmaskMode = shadowmaskMode;
                QualitySettings.shadowNearPlaneOffset = shadowNearPlaneOffset;
                QualitySettings.shadowProjection = shadowProjection;
                QualitySettings.shadowResolution = shadowResolution;
                QualitySettings.shadows = shadows;
#if UNITY_2019_1_OR_NEWER
                QualitySettings.skinWeights = skinWeights; ;
#endif
                QualitySettings.softParticles = softParticles;
                QualitySettings.softVegetation = softVegetation;
                QualitySettings.streamingMipmapsActive = streamingMipmapsActive;
                QualitySettings.streamingMipmapsAddAllCameras = streamingMipmapsAddAllCameras;
                QualitySettings.streamingMipmapsMaxFileIORequests = streamingMipmapsMaxFileIORequests;
                QualitySettings.streamingMipmapsMaxLevelReduction = streamingMipmapsMaxLevelReduction;
                QualitySettings.streamingMipmapsMemoryBudget = streamingMipmapsMemoryBudget;
                //QualitySettings.streamingMipmapsRenderersPerFrame = streamingMipmapsRenderersPerFrame;
                QualitySettings.vSyncCount = vSyncCount;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {

            SerializerKun.Serialize<RenderPipelineAssetKun>(binaryWriter, renderPipeline);
            binaryWriter.Write((int)activeColorSpace);
            binaryWriter.Write((int)anisotropicFiltering);
            binaryWriter.Write(antiAliasing);
            binaryWriter.Write(asyncUploadBufferSize);
            binaryWriter.Write(asyncUploadPersistentBuffer);
            binaryWriter.Write(asyncUploadTimeSlice);
            binaryWriter.Write(billboardsFaceCameraPosition);
            binaryWriter.Write((int)desiredColorSpace);
            binaryWriter.Write(lodBias);
            binaryWriter.Write(masterTextureLimit);
            binaryWriter.Write(maximumLODLevel);
            binaryWriter.Write(maxQueuedFrames);
            SerializerKun.Serialize(binaryWriter, names);
            binaryWriter.Write(particleRaycastBudget);
            binaryWriter.Write(pixelLightCount);
            binaryWriter.Write(realtimeReflectionProbes);
            binaryWriter.Write(resolutionScalingFixedDPIFactor);
            binaryWriter.Write(shadowCascade2Split);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, mShadowCascade4Split);
            binaryWriter.Write(shadowCascades);
            binaryWriter.Write(shadowDistance);
            binaryWriter.Write((int)shadowmaskMode);
            binaryWriter.Write(shadowNearPlaneOffset);
            binaryWriter.Write((int)shadowProjection);
            binaryWriter.Write((int)shadowResolution);
            binaryWriter.Write((int)shadows);
#if UNITY_2019_1_OR_NEWER
            binaryWriter.Write((int)skinWeights);
#endif
            binaryWriter.Write(softParticles);
            binaryWriter.Write(softVegetation);
            binaryWriter.Write(streamingMipmapsActive);
            binaryWriter.Write(streamingMipmapsAddAllCameras);
            binaryWriter.Write(streamingMipmapsMaxFileIORequests);
            binaryWriter.Write(streamingMipmapsMaxLevelReduction);
            binaryWriter.Write(streamingMipmapsMemoryBudget);
            binaryWriter.Write(streamingMipmapsRenderersPerFrame);
            binaryWriter.Write(vSyncCount);
            binaryWriter.Write(isDirty);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            renderPipeline =  SerializerKun.DesirializeObject<RenderPipelineAssetKun>(binaryReader);
            activeColorSpace =  (ColorSpace)binaryReader.ReadInt32();
            anisotropicFiltering = (AnisotropicFiltering)binaryReader.ReadInt32();
            antiAliasing = binaryReader.ReadInt32();
            asyncUploadBufferSize = binaryReader.ReadInt32();            
            asyncUploadPersistentBuffer = binaryReader.ReadBoolean();
            asyncUploadTimeSlice = binaryReader.ReadInt32();
            billboardsFaceCameraPosition = binaryReader.ReadBoolean();
            desiredColorSpace = (ColorSpace)binaryReader.ReadInt32();
            lodBias = binaryReader.ReadSingle();
            masterTextureLimit = binaryReader.ReadInt32();
            maximumLODLevel = binaryReader.ReadInt32();
            maxQueuedFrames = binaryReader.ReadInt32();
            names = SerializerKun.DesirializeStrings(binaryReader);
            particleRaycastBudget = binaryReader.ReadInt32();
            pixelLightCount = binaryReader.ReadInt32();
            realtimeReflectionProbes = binaryReader.ReadBoolean();
            resolutionScalingFixedDPIFactor = binaryReader.ReadSingle();
            shadowCascade2Split = binaryReader.ReadSingle();
            mShadowCascade4Split = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
            shadowCascades = binaryReader.ReadInt32();
            shadowDistance = binaryReader.ReadSingle();
            shadowmaskMode = (ShadowmaskMode)binaryReader.ReadInt32();
            shadowNearPlaneOffset = binaryReader.ReadSingle();
            shadowProjection = (ShadowProjection)binaryReader.ReadInt32();
            shadowResolution = (ShadowResolution)binaryReader.ReadInt32();
            shadows = (ShadowQuality)binaryReader.ReadInt32();
#if UNITY_2019_1_OR_NEWER
            skinWeights = (SkinWeights)binaryReader.ReadInt32();
#endif
            softParticles = binaryReader.ReadBoolean();
            softVegetation = binaryReader.ReadBoolean();
            streamingMipmapsActive = binaryReader.ReadBoolean();
            streamingMipmapsAddAllCameras = binaryReader.ReadBoolean();
            streamingMipmapsMaxFileIORequests = binaryReader.ReadInt32();
            streamingMipmapsMaxLevelReduction = binaryReader.ReadInt32();
            streamingMipmapsMemoryBudget = binaryReader.ReadSingle();
            streamingMipmapsRenderersPerFrame = binaryReader.ReadInt32();
            vSyncCount = binaryReader.ReadInt32();
            isDirty = binaryReader.ReadBoolean();            
        }

    }
}
