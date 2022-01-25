using System.IO;
using System.Reflection;
using UnityEngine;
using Utj.UnityChoseKun.URP;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class QualitySettingsKun : ISerializerKun
    {
#if UNITY_2019_1_OR_NEWER
        public enum RenderPipelineType
        {
            NONE,
            RP,
            URP,
            HDRP,
        }
#endif


#if UNITY_2019_1_OR_NEWER
        [SerializeField] public RenderPipelineType renderPipelineType;
        [SerializeField] public RenderPipelineAssetKun renderPipeline;
#endif

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
        [SerializeField] public UnityEngine.ShadowResolution shadowResolution;
        [SerializeField] public UnityEngine.ShadowQuality shadows;
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
#if UNITY_2019_1_OR_NEWER            
            renderPipelineType = RenderPipelineType.NONE;
            renderPipeline = null;
#endif
            if (isSet)
            {
#if UNITY_2019_1_OR_NEWER
                if (QualitySettings.renderPipeline)
                {
                    var type = QualitySettings.renderPipeline.GetType();
                    if (type != null && type.Name == "UniversalRenderPipelineAsset")
                    {
                        renderPipelineType = RenderPipelineType.URP;
                        renderPipeline = new UniversalRenderPipelineAssetKun(QualitySettings.renderPipeline);
                    }
                    else
                    {
                        renderPipelineType = RenderPipelineType.RP;
                        renderPipeline = new RenderPipelineAssetKun(QualitySettings.renderPipeline);
                    }
                }                
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
#if UNITY_2019_1_OR_NEWER
            binaryWriter.Write((int)renderPipelineType);
            switch(renderPipelineType)
            {
                case RenderPipelineType.RP:
                    SerializerKun.Serialize<RenderPipelineAssetKun>(binaryWriter, renderPipeline);
                    break;
                case RenderPipelineType.URP:
                    SerializerKun.Serialize<UniversalRenderPipelineAssetKun>(binaryWriter,(UniversalRenderPipelineAssetKun)renderPipeline);
                    break;
            }            
#endif
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
#if UNITY_2019_1_OR_NEWER
            renderPipelineType = (RenderPipelineType)binaryReader.ReadInt32();            
            switch (renderPipelineType)
            {
                case RenderPipelineType.RP:
                    renderPipeline = SerializerKun.DesirializeObject<RenderPipelineAssetKun>(binaryReader);
                    break;
                case RenderPipelineType.URP:
                    renderPipeline = SerializerKun.DesirializeObject<URP.UniversalRenderPipelineAssetKun>(binaryReader);                    
                    break;
            }
#endif            
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
            shadowProjection = (UnityEngine.ShadowProjection)binaryReader.ReadInt32();
            shadowResolution = (UnityEngine.ShadowResolution)binaryReader.ReadInt32();
            shadows = (UnityEngine.ShadowQuality)binaryReader.ReadInt32();
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

        public override bool Equals(object obj)
        {        
            
            var clone = obj as QualitySettingsKun;
            if(clone == null)
            {
                return false;
            }

#if UNITY_2019_1_OR_NEWER
           if(renderPipelineType.Equals(clone.renderPipelineType) == false)
            {
                return false;
            }
            if(renderPipeline.Equals(clone.renderPipeline) == false)
            {
                return false;
            }

#endif
            if(activeColorSpace.Equals(clone.activeColorSpace)  == false)
            {
                return false;
            }
            if(anisotropicFiltering.Equals(clone.anisotropicFiltering) == false)
            {
                return false;
            }
            if(antiAliasing.Equals(clone.antiAliasing) == false)
            {
                return false;
            }
            if(asyncUploadBufferSize.Equals(clone.asyncUploadBufferSize) == false)
            {
                return false;
            }
            if(asyncUploadPersistentBuffer.Equals(clone.asyncUploadPersistentBuffer) == false)
            {
                return false;
            }
            if(asyncUploadTimeSlice.Equals(clone.asyncUploadTimeSlice) == false)
            {
                return false;
            }
            if(billboardsFaceCameraPosition.Equals(clone.billboardsFaceCameraPosition) == false)
            {
                return false;
            }
            if(desiredColorSpace.Equals(clone.desiredColorSpace) == false)
            {
                return false;
            }
            if(lodBias.Equals(clone.lodBias) == false)
            {
                return false;
            }
            if(masterTextureLimit.Equals(clone.masterTextureLimit) == false)
            {
                return false;
            }
            if(maximumLODLevel.Equals(clone.maximumLODLevel) == false)
            {
                return false;
            }
            if(maxQueuedFrames.Equals(clone.maxQueuedFrames) == false)
            {
                return false;
            }
            if(names.Length != clone.names.Length)
            {
                return false;
            }
            for(var i = 0; i < names.Length; i++)
            {
                if(names[i] != clone.names[i])
                {
                    return false;
                }
            }
            
            if(particleRaycastBudget.Equals(clone.particleRaycastBudget) == false)
            {
                return false;
            }
            if (pixelLightCount.Equals(clone.pixelLightCount) == false)
            {
                return false;
            }
            if(realtimeReflectionProbes.Equals(clone.realtimeReflectionProbes) == false)
            {
                return false;
            }            
            if(resolutionScalingFixedDPIFactor.Equals(clone.resolutionScalingFixedDPIFactor) == false)
            {
                return false;
            }
            if(shadowCascade2Split.Equals(clone.shadowCascade2Split) == false)
            {
                return false;
            }
            if(mShadowCascade4Split.Equals(clone.mShadowCascade4Split) == false)
            {
                return false;
            }
            if(shadowCascades.Equals(clone.shadowCascades) == false)
            {
                return false;
            }
            if(shadowDistance.Equals(clone.shadowDistance) == false)
            {
                return false;
            }
            if(shadowmaskMode.Equals(clone.shadowmaskMode) == false)
            {
                return false;
            }
            if(shadowNearPlaneOffset.Equals(clone.shadowNearPlaneOffset) == false)
            {
                return false;
            }
            if(shadowProjection.Equals(clone.shadowProjection) == false)
            {
                return false;
            }
            if(shadowResolution.Equals(clone.shadowResolution) == false)
            {
                return false;
            }
            if(shadows.Equals(clone.shadows) == false)
            {
                return false;
            }
#if UNITY_2019_1_OR_NEWER
            if(skinWeights.Equals(clone.skinWeights) == false)
            {
                return false;
            }
#endif
            if(softParticles.Equals(clone.softParticles) == false)
            {
                return false;
            }
            if(softVegetation.Equals(clone.softVegetation) == false)
            {
                return false;
            }
            if(streamingMipmapsActive.Equals(clone.streamingMipmapsActive) == false)
            {
                return false;
            }
            if(streamingMipmapsAddAllCameras.Equals(clone.streamingMipmapsAddAllCameras) == false)
            {
                return false;
            }
            if(streamingMipmapsMaxFileIORequests.Equals(clone.streamingMipmapsMaxFileIORequests) == false)
            {
                return false;
            }
            if(streamingMipmapsMaxLevelReduction.Equals(clone.streamingMipmapsMaxLevelReduction) == false)
            {
                return false;
            }
            if(streamingMipmapsMemoryBudget.Equals(clone.streamingMipmapsMemoryBudget) == false)
            {
                return false;
            }
            if(streamingMipmapsRenderersPerFrame.Equals(clone.streamingMipmapsRenderersPerFrame) == false)
            {
                return false;
            }
            if(vSyncCount.Equals(clone.vSyncCount) == false)
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
