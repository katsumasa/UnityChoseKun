using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    namespace Engine
    {
        using Rendering;
        using Rendering.Universal;


        /// <summary>
        /// 
        /// </summary>
        [System.Serializable]
        public class QualitySettingsKun : ISerializerKun
        {

            static QualitySettingsKun m_instance;

#if UNITY_2019_1_OR_NEWER
            [SerializeField] RenderPipelineAssetType m_renderPipelineType;
            [SerializeField] RenderPipelineAssetKun m_renderPipeline;
#endif
            [SerializeField] ColorSpace m_activeColorSpace;
            [SerializeField] AnisotropicFiltering m_anisotropicFiltering;
            [SerializeField] int m_antiAliasing;
            [SerializeField] int m_asyncUploadBufferSize;
            [SerializeField] bool m_asyncUploadPersistentBuffer;
            [SerializeField] int m_asyncUploadTimeSlice;
            [SerializeField] bool m_billboardsFaceCameraPosition;
            [SerializeField] ColorSpace m_desiredColorSpace;
            [SerializeField] float m_lodBias;
            [SerializeField] int m_masterTextureLimit;
            [SerializeField] int m_maximumLODLevel;
            [SerializeField] int m_maxQueuedFrames;
            [SerializeField] string[] m_names;
            [SerializeField] int m_particleRaycastBudget;
            [SerializeField] int m_pixelLightCount;
            [SerializeField] bool m_realtimeReflectionProbes;
            [SerializeField] float m_resolutionScalingFixedDPIFactor;
            [SerializeField] float m_shadowCascade2Split;
            [SerializeField] Vector3Kun m_shadowCascade4Split;
            [SerializeField] int m_shadowCascades;
            [SerializeField] float m_shadowDistance;
            [SerializeField] ShadowmaskMode m_shadowmaskMode;
            [SerializeField] float m_shadowNearPlaneOffset;
            [SerializeField] ShadowProjection m_shadowProjection;
            [SerializeField] UnityEngine.ShadowResolution m_shadowResolution;
            [SerializeField] UnityEngine.ShadowQuality m_shadows;
#if UNITY_2019_1_OR_NEWER
            [SerializeField] SkinWeights m_skinWeights;
#endif
            [SerializeField] bool m_softParticles;
            [SerializeField] bool m_softVegetation;
            [SerializeField] bool m_streamingMipmapsActive;
            [SerializeField] bool m_streamingMipmapsAddAllCameras;
            [SerializeField] int m_streamingMipmapsMaxFileIORequests;
            [SerializeField] int m_streamingMipmapsMaxLevelReduction;
            [SerializeField] float m_streamingMipmapsMemoryBudget;
            [SerializeField] int m_streamingMipmapsRenderersPerFrame;
            [SerializeField] int m_vSyncCount;
            [SerializeField] bool m_dirty;


            public static QualitySettingsKun instance
            {
                get
                {
                    if(m_instance == null)
                    {
                        m_instance = new QualitySettingsKun();
                    }
                    return m_instance;
                }
            }


#if UNITY_2019_1_OR_NEWER
            public static RenderPipelineAssetType renderPipelineType
            {
                get { return instance.m_renderPipelineType; }
                set {
                    instance.m_renderPipelineType = value;
                    instance.m_dirty = true;
                }
            }
            
            public static RenderPipelineAssetKun renderPipeline
            {
                get { return instance.m_renderPipeline; }
                set {
                    instance.m_renderPipeline = value;
                    instance.m_dirty = true;
                }
            }
#endif

            public static ColorSpace activeColorSpace
            {
                get
                {
                    return instance.m_activeColorSpace;
                }

                set
                {
                    instance.m_activeColorSpace = value;
                    instance.m_dirty = true;
                }
            }

            public static AnisotropicFiltering anisotropicFiltering
            {
                get
                {
                    return instance.m_anisotropicFiltering;
                }

                set
                {
                    instance.m_anisotropicFiltering = value;
                    instance.m_dirty = true;
                }
            }

            public static int antiAliasing
            {
                get
                {
                    return instance.m_antiAliasing;
                }

                set
                {
                    instance.m_antiAliasing = value;
                    instance.m_dirty = true;
                }
            }

            public static int asyncUploadBufferSize
            {
                get
                {
                    return instance.m_asyncUploadBufferSize;
                }

                set
                {
                    instance.m_asyncUploadBufferSize = value;
                    instance.m_dirty = true;
                }
            }

            public static bool asyncUploadPersistentBuffer
            {
                get
                {
                    return instance.m_asyncUploadPersistentBuffer;
                }

                set
                {
                    instance.m_asyncUploadPersistentBuffer = value;
                    instance.m_dirty = true;
                }
            }

            public static int asyncUploadTimeSlice
            {
                get
                {
                    return instance.m_asyncUploadTimeSlice;
                }

                set
                {
                    instance.m_asyncUploadTimeSlice = value;
                    instance.m_dirty = true;
                }
            }

            public static bool billboardsFaceCameraPosition
            {
                get
                {
                    return instance.m_billboardsFaceCameraPosition;
                }

                set
                {
                    instance.m_billboardsFaceCameraPosition = value;
                    instance.m_dirty = true;
                }
            }

            public static ColorSpace desiredColorSpace
            {
                get
                {
                    return instance.m_desiredColorSpace;
                }

                set
                {
                    instance.m_desiredColorSpace = value;
                    instance.m_dirty = true;
                }
            }

            public static float lodBias
            {
                get
                {
                    return instance.m_lodBias;
                }

                set
                {
                    instance.m_lodBias = value;
                    instance.m_dirty = true;
                }
            }

            public static int masterTextureLimit
            {
                get
                {
                    return instance.m_masterTextureLimit;
                }

                set
                {
                    instance.m_masterTextureLimit = value;
                    instance.m_dirty = true;
                }
            }

            public static int maximumLODLevel
            {
                get
                {
                    return instance.m_maximumLODLevel;
                }

                set
                {
                    instance.m_maximumLODLevel = value;
                    instance.m_dirty = true;
                }
            }

            public static int maxQueuedFrames
            {
                get
                {
                    return instance.m_maxQueuedFrames;

                }

                set
                {
                    instance.m_maxQueuedFrames = value;
                    instance.m_dirty = true;
                }
            }

            public static string[] names
            {
                get
                {
                    return instance.m_names;
                }

                set
                {
                    instance.m_names = value;
                    instance.m_dirty = true;
                }
            }

            public static int particleRaycastBudget
            {
                get
                {
                    return instance.m_particleRaycastBudget;
                }

                set
                {
                    instance.m_particleRaycastBudget = value;
                    instance.m_dirty = true;
                }
            }

            public static int pixelLightCount
            {
                get
                {
                    return instance.m_pixelLightCount;
                }

                set
                {
                    instance.m_pixelLightCount = value;
                    instance.m_dirty = true;
                }
            }

            public static bool realtimeReflectionProbes
            {
                get
                {
                    return instance.m_realtimeReflectionProbes;

                }

                set
                {
                    instance.m_realtimeReflectionProbes = value;
                    instance.m_dirty = true;
                }
            }

            public static float resolutionScalingFixedDPIFactor
            {
                get
                {
                    return instance.m_resolutionScalingFixedDPIFactor;
                }

                set
                {
                    instance.m_resolutionScalingFixedDPIFactor = value;
                    instance.m_dirty = true;
                }
            }

            public static float shadowCascade2Split
            {
                get
                {
                    return instance.m_shadowCascade2Split;
                }

                set
                {
                    instance.m_shadowCascade2Split = value;
                    instance.m_dirty = true;
                }
            }

            public static Vector3Kun shadowCascade4Split
            {
                get
                {
                    return instance.m_shadowCascade4Split;
                }

                set
                {
                    instance.m_shadowCascade4Split = value;
                    instance.m_dirty = true;
                }
            }


            public static int shadowCascades
            {
                get
                {
                    return instance.m_shadowCascades;
                }

                set
                {
                    instance.m_shadowCascades = value;
                    instance.m_dirty = true;
                }
            }

            public static float shadowDistance
            {
                get
                {
                    return instance.m_shadowDistance;
                }

                set
                {
                    instance.m_shadowDistance = value;
                    instance.m_dirty = true;
                }
            }

            public static ShadowmaskMode shadowmaskMode
            {
                get
                {
                    return instance.m_shadowmaskMode;
                }

                set
                {
                    instance.m_shadowmaskMode = value;
                    instance.m_dirty = true;
                }
            }

            public static float shadowNearPlaneOffset
            {
                get
                {
                    return instance.m_shadowNearPlaneOffset;
                }

                set
                {
                    instance.m_shadowNearPlaneOffset = value;
                    instance.m_dirty = true;
                }
            }

            public static ShadowProjection shadowProjection
            {
                get
                {
                    return instance.m_shadowProjection;
                }

                set
                {
                    instance.m_shadowProjection = value;
                    instance.m_dirty = true;
                }
            }

            public static UnityEngine.ShadowResolution shadowResolution
            {
                get
                {
                    return instance.m_shadowResolution;
                }

                set
                {
                    instance.m_shadowResolution = value;
                    instance.m_dirty = true;
                }
            }

            public static UnityEngine.ShadowQuality shadows
            {
                get
                {
                    return instance.m_shadows;
                }

                set
                {
                    instance.m_shadows = value;
                    instance.m_dirty = true;
                }
            }

#if UNITY_2019_1_OR_NEWER
            public static SkinWeights skinWeights
            {
                get
                {
                    return instance.m_skinWeights;
                }

                set
                {
                    instance.m_skinWeights = value;
                    instance.m_dirty = true;
                }
            }
#endif
            public static bool softParticles
            {
                get
                {
                    return instance.m_softParticles;
                }

                set
                {
                    instance.m_softParticles = value;
                    instance.m_dirty = true;
                }
            }

            public static bool softVegetation
            {
                get
                {
                    return instance.m_softVegetation;
                }

                set
                {
                    instance.m_softVegetation = value;
                    instance.m_dirty = true;
                }
            }

            public static bool streamingMipmapsActive
            {
                get
                {
                    return instance.m_streamingMipmapsActive;
                }

                set
                {
                    instance.m_streamingMipmapsActive = value;
                    instance.m_dirty = true;
                }
            }

            public static bool streamingMipmapsAddAllCameras
            {
                get
                {
                    return instance.m_streamingMipmapsAddAllCameras;
                }

                set
                {
                    instance.m_streamingMipmapsAddAllCameras = value;
                    instance.m_dirty = true;
                }
            }

            public static int streamingMipmapsMaxFileIORequests
            {
                get
                {
                    return instance.m_streamingMipmapsMaxFileIORequests;
                }

                set
                {
                    instance.m_streamingMipmapsMaxFileIORequests = value;
                    instance.m_dirty = true;
                }
            }
            

            public static int streamingMipmapsMaxLevelReduction
            {
                get
                {
                    return instance.m_streamingMipmapsMaxLevelReduction;
                }

                set
                {
                    instance.m_streamingMipmapsMaxLevelReduction = value;
                    instance.m_dirty = true;
                }
            }
            

            public static float streamingMipmapsMemoryBudget
            {
                get
                {
                    return instance.m_streamingMipmapsMemoryBudget;
                }

                set
                {
                    instance.m_streamingMipmapsMemoryBudget = value ;
                    instance.m_dirty = true;
                }
            }


            public static int streamingMipmapsRenderersPerFrame
            {
                get
                {
                    return instance.m_streamingMipmapsRenderersPerFrame;
                }

                set
                {
                    instance.m_streamingMipmapsRenderersPerFrame = value;
                    instance.m_dirty = true;
                }
            }


            public static int vSyncCount
            {
                get
                {
                    return instance.m_streamingMipmapsRenderersPerFrame;
                }

                set
                {
                    instance.m_vSyncCount = value;
                    instance.m_dirty = true;
                }
            }


            public static bool dirty
            {
                get
                {
                    return instance.m_dirty;
                }

                set
                {
                    instance.m_dirty = value;
                }
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
                m_dirty = false;
#if UNITY_2019_1_OR_NEWER
                m_renderPipelineType = RenderPipelineAssetType.NONE;
                m_renderPipeline = null;
#endif
                if (isSet)
                {
#if UNITY_2019_1_OR_NEWER
                    if (QualitySettings.renderPipeline)
                    {
                        var type = QualitySettings.renderPipeline.GetType();
                        if (type != null && type.Name == "UniversalRenderPipelineAsset")
                        {
                            m_renderPipelineType = RenderPipelineAssetType.URP;
                            m_renderPipeline = new UniversalRenderPipelineAssetKun(QualitySettings.renderPipeline);
                        }
                        else
                        {
                            m_renderPipelineType = RenderPipelineAssetType.RP;
                            m_renderPipeline = new RenderPipelineAssetKun(QualitySettings.renderPipeline);
                        }
                    }
#endif
                    m_activeColorSpace = QualitySettings.activeColorSpace;
                    m_anisotropicFiltering = QualitySettings.anisotropicFiltering;
                    m_antiAliasing = QualitySettings.antiAliasing;
                    m_asyncUploadBufferSize = QualitySettings.asyncUploadBufferSize;
                    m_asyncUploadPersistentBuffer = QualitySettings.asyncUploadPersistentBuffer;
                    m_asyncUploadTimeSlice = QualitySettings.asyncUploadTimeSlice;
                    m_billboardsFaceCameraPosition = QualitySettings.billboardsFaceCameraPosition;
                    m_desiredColorSpace = QualitySettings.desiredColorSpace;
                    m_lodBias = QualitySettings.lodBias;
                    m_masterTextureLimit = QualitySettings.masterTextureLimit;
                    m_maximumLODLevel = QualitySettings.maximumLODLevel;
                    m_maxQueuedFrames = QualitySettings.maxQueuedFrames;
                    m_names = QualitySettings.names;
                    m_particleRaycastBudget = QualitySettings.particleRaycastBudget;
                    m_pixelLightCount = QualitySettings.pixelLightCount;
                    m_realtimeReflectionProbes = QualitySettings.realtimeReflectionProbes;
                    m_resolutionScalingFixedDPIFactor = QualitySettings.resolutionScalingFixedDPIFactor;
                    m_shadowCascade2Split = QualitySettings.shadowCascade2Split;
                    m_shadowCascade4Split = new Vector3Kun(QualitySettings.shadowCascade4Split);
                    m_shadowCascades = QualitySettings.shadowCascades;
                    m_shadowDistance = QualitySettings.shadowDistance;
                    m_shadowmaskMode = QualitySettings.shadowmaskMode;
                    m_shadowNearPlaneOffset = QualitySettings.shadowNearPlaneOffset;
                    m_shadowProjection = QualitySettings.shadowProjection;
                    m_shadowResolution = QualitySettings.shadowResolution;
                    m_shadows = QualitySettings.shadows;
#if UNITY_2019_1_OR_NEWER
                    m_skinWeights = QualitySettings.skinWeights;
#endif
                    m_softParticles = QualitySettings.softParticles;
                    m_softVegetation = QualitySettings.softVegetation;
                    m_streamingMipmapsActive = QualitySettings.streamingMipmapsActive;
                    m_streamingMipmapsAddAllCameras = QualitySettings.streamingMipmapsAddAllCameras;
                    m_streamingMipmapsMaxFileIORequests = QualitySettings.streamingMipmapsMaxFileIORequests;
                    m_streamingMipmapsMaxLevelReduction = QualitySettings.streamingMipmapsMaxLevelReduction;
                    m_streamingMipmapsMemoryBudget = QualitySettings.streamingMipmapsMemoryBudget;
                    m_streamingMipmapsRenderersPerFrame = QualitySettings.streamingMipmapsRenderersPerFrame;
                    m_vSyncCount = QualitySettings.vSyncCount;
                }
            }


            /// <summary>
            /// 
            /// </summary>
            public void WriteBack()
            {
                if (m_dirty)
                {
#if UNITY_2019_1_OR_NEWER
                    m_renderPipeline.WriteBack(QualitySettings.renderPipeline);
#endif
                    QualitySettings.anisotropicFiltering = m_anisotropicFiltering;
                    QualitySettings.antiAliasing = m_antiAliasing;
                    QualitySettings.asyncUploadBufferSize = m_asyncUploadBufferSize;
                    QualitySettings.asyncUploadPersistentBuffer = m_asyncUploadPersistentBuffer;
                    QualitySettings.asyncUploadTimeSlice = m_asyncUploadTimeSlice;
                    QualitySettings.billboardsFaceCameraPosition = m_billboardsFaceCameraPosition;
                    QualitySettings.lodBias = m_lodBias;
                    QualitySettings.masterTextureLimit = m_masterTextureLimit;
                    QualitySettings.maximumLODLevel = m_maximumLODLevel;
                    QualitySettings.maxQueuedFrames = m_maxQueuedFrames;
                    QualitySettings.particleRaycastBudget = m_particleRaycastBudget;
                    QualitySettings.pixelLightCount = m_pixelLightCount;
                    QualitySettings.realtimeReflectionProbes = m_realtimeReflectionProbes;
                    QualitySettings.resolutionScalingFixedDPIFactor = m_resolutionScalingFixedDPIFactor;
                    QualitySettings.shadowCascade2Split = m_shadowCascade2Split;
                    QualitySettings.shadowCascade4Split = m_shadowCascade4Split.GetVector3();
                    QualitySettings.shadowCascades = m_shadowCascades;
                    QualitySettings.shadowDistance = m_shadowDistance;
                    QualitySettings.shadowmaskMode = m_shadowmaskMode;
                    QualitySettings.shadowNearPlaneOffset = m_shadowNearPlaneOffset;
                    QualitySettings.shadowProjection = m_shadowProjection;
                    QualitySettings.shadowResolution = m_shadowResolution;
                    QualitySettings.shadows = m_shadows;
#if UNITY_2019_1_OR_NEWER
                    QualitySettings.skinWeights = m_skinWeights; ;
#endif
                    QualitySettings.softParticles = m_softParticles;
                    QualitySettings.softVegetation = m_softVegetation;
                    QualitySettings.streamingMipmapsActive = m_streamingMipmapsActive;
                    QualitySettings.streamingMipmapsAddAllCameras = m_streamingMipmapsAddAllCameras;
                    QualitySettings.streamingMipmapsMaxFileIORequests = m_streamingMipmapsMaxFileIORequests;
                    QualitySettings.streamingMipmapsMaxLevelReduction = m_streamingMipmapsMaxLevelReduction;
                    QualitySettings.streamingMipmapsMemoryBudget = m_streamingMipmapsMemoryBudget;
                    //QualitySettings.streamingMipmapsRenderersPerFrame = streamingMipmapsRenderersPerFrame;
                    QualitySettings.vSyncCount = m_vSyncCount;
                }
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryWriter"></param>
            public virtual void Serialize(BinaryWriter binaryWriter)
            {
#if UNITY_2019_1_OR_NEWER
                binaryWriter.Write((int)m_renderPipelineType);
                switch (m_renderPipelineType)
                {
                    case RenderPipelineAssetType.RP:
                        SerializerKun.Serialize<RenderPipelineAssetKun>(binaryWriter, m_renderPipeline);
                        break;
                    case RenderPipelineAssetType.URP:
                        SerializerKun.Serialize<UniversalRenderPipelineAssetKun>(binaryWriter, (UniversalRenderPipelineAssetKun)m_renderPipeline);
                        break;
                }
#endif
                binaryWriter.Write((int)m_activeColorSpace);
                binaryWriter.Write((int)m_anisotropicFiltering);
                binaryWriter.Write(m_antiAliasing);
                binaryWriter.Write(m_asyncUploadBufferSize);
                binaryWriter.Write(m_asyncUploadPersistentBuffer);
                binaryWriter.Write(m_asyncUploadTimeSlice);
                binaryWriter.Write(m_billboardsFaceCameraPosition);
                binaryWriter.Write((int)m_desiredColorSpace);
                binaryWriter.Write(m_lodBias);
                binaryWriter.Write(m_masterTextureLimit);
                binaryWriter.Write(m_maximumLODLevel);
                binaryWriter.Write(m_maxQueuedFrames);
                SerializerKun.Serialize(binaryWriter, m_names);
                binaryWriter.Write(m_particleRaycastBudget);
                binaryWriter.Write(m_pixelLightCount);
                binaryWriter.Write(m_realtimeReflectionProbes);
                binaryWriter.Write(m_resolutionScalingFixedDPIFactor);
                binaryWriter.Write(m_shadowCascade2Split);
                SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_shadowCascade4Split);
                binaryWriter.Write(m_shadowCascades);
                binaryWriter.Write(m_shadowDistance);
                binaryWriter.Write((int)m_shadowmaskMode);
                binaryWriter.Write(m_shadowNearPlaneOffset);
                binaryWriter.Write((int)m_shadowProjection);
                binaryWriter.Write((int)m_shadowResolution);
                binaryWriter.Write((int)m_shadows);
#if UNITY_2019_1_OR_NEWER
                binaryWriter.Write((int)m_skinWeights);
#endif
                binaryWriter.Write(m_softParticles);
                binaryWriter.Write(m_softVegetation);
                binaryWriter.Write(m_streamingMipmapsActive);
                binaryWriter.Write(m_streamingMipmapsAddAllCameras);
                binaryWriter.Write(m_streamingMipmapsMaxFileIORequests);
                binaryWriter.Write(m_streamingMipmapsMaxLevelReduction);
                binaryWriter.Write(m_streamingMipmapsMemoryBudget);
                binaryWriter.Write(m_streamingMipmapsRenderersPerFrame);
                binaryWriter.Write(m_vSyncCount);
                binaryWriter.Write(m_dirty);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryReader"></param>
            public virtual void Deserialize(BinaryReader binaryReader)
            {
#if UNITY_2019_1_OR_NEWER
                m_renderPipelineType = (RenderPipelineAssetType)binaryReader.ReadInt32();
                switch (m_renderPipelineType)
                {
                    case RenderPipelineAssetType.RP:
                        m_renderPipeline = SerializerKun.DesirializeObject<RenderPipelineAssetKun>(binaryReader);
                        break;
                    case RenderPipelineAssetType.URP:
                        m_renderPipeline = SerializerKun.DesirializeObject<UniversalRenderPipelineAssetKun>(binaryReader);
                        break;
                }
#endif
                m_activeColorSpace = (ColorSpace)binaryReader.ReadInt32();
                m_anisotropicFiltering = (AnisotropicFiltering)binaryReader.ReadInt32();
                m_antiAliasing = binaryReader.ReadInt32();
                m_asyncUploadBufferSize = binaryReader.ReadInt32();
                m_asyncUploadPersistentBuffer = binaryReader.ReadBoolean();
                m_asyncUploadTimeSlice = binaryReader.ReadInt32();
                m_billboardsFaceCameraPosition = binaryReader.ReadBoolean();
                m_desiredColorSpace = (ColorSpace)binaryReader.ReadInt32();
                m_lodBias = binaryReader.ReadSingle();
                m_masterTextureLimit = binaryReader.ReadInt32();
                m_maximumLODLevel = binaryReader.ReadInt32();
                m_maxQueuedFrames = binaryReader.ReadInt32();
                m_names = SerializerKun.DesirializeStrings(binaryReader);
                m_particleRaycastBudget = binaryReader.ReadInt32();
                m_pixelLightCount = binaryReader.ReadInt32();
                m_realtimeReflectionProbes = binaryReader.ReadBoolean();
                m_resolutionScalingFixedDPIFactor = binaryReader.ReadSingle();
                m_shadowCascade2Split = binaryReader.ReadSingle();
                m_shadowCascade4Split = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
                m_shadowCascades = binaryReader.ReadInt32();
                m_shadowDistance = binaryReader.ReadSingle();
                m_shadowmaskMode = (ShadowmaskMode)binaryReader.ReadInt32();
                m_shadowNearPlaneOffset = binaryReader.ReadSingle();
                m_shadowProjection = (UnityEngine.ShadowProjection)binaryReader.ReadInt32();
                m_shadowResolution = (UnityEngine.ShadowResolution)binaryReader.ReadInt32();
                m_shadows = (UnityEngine.ShadowQuality)binaryReader.ReadInt32();
#if UNITY_2019_1_OR_NEWER
                m_skinWeights = (SkinWeights)binaryReader.ReadInt32();
#endif
                m_softParticles = binaryReader.ReadBoolean();
                m_softVegetation = binaryReader.ReadBoolean();
                m_streamingMipmapsActive = binaryReader.ReadBoolean();
                m_streamingMipmapsAddAllCameras = binaryReader.ReadBoolean();
                m_streamingMipmapsMaxFileIORequests = binaryReader.ReadInt32();
                m_streamingMipmapsMaxLevelReduction = binaryReader.ReadInt32();
                m_streamingMipmapsMemoryBudget = binaryReader.ReadSingle();
                m_streamingMipmapsRenderersPerFrame = binaryReader.ReadInt32();
                m_vSyncCount = binaryReader.ReadInt32();
                m_dirty = binaryReader.ReadBoolean();
            }


            public override bool Equals(object obj)
            {
                var clone = obj as QualitySettingsKun;
                if (clone == null)
                {
                    return false;
                }

#if UNITY_2019_1_OR_NEWER
                if (m_renderPipelineType.Equals(clone.m_renderPipelineType) == false)
                {
                    return false;
                }
                if (m_renderPipeline.Equals(clone.m_renderPipeline) == false)
                {
                    return false;
                }

#endif
                if (m_activeColorSpace.Equals(clone.m_activeColorSpace) == false)
                {
                    return false;
                }
                if (m_anisotropicFiltering.Equals(clone.m_anisotropicFiltering) == false)
                {
                    return false;
                }
                if (m_antiAliasing.Equals(clone.m_antiAliasing) == false)
                {
                    return false;
                }
                if (m_asyncUploadBufferSize.Equals(clone.m_asyncUploadBufferSize) == false)
                {
                    return false;
                }
                if (m_asyncUploadPersistentBuffer.Equals(clone.m_asyncUploadPersistentBuffer) == false)
                {
                    return false;
                }
                if (m_asyncUploadTimeSlice.Equals(clone.m_asyncUploadTimeSlice) == false)
                {
                    return false;
                }
                if (m_billboardsFaceCameraPosition.Equals(clone.m_billboardsFaceCameraPosition) == false)
                {
                    return false;
                }
                if (m_desiredColorSpace.Equals(clone.m_desiredColorSpace) == false)
                {
                    return false;
                }
                if (m_lodBias.Equals(clone.m_lodBias) == false)
                {
                    return false;
                }
                if (m_masterTextureLimit.Equals(clone.m_masterTextureLimit) == false)
                {
                    return false;
                }
                if (m_maximumLODLevel.Equals(clone.m_maximumLODLevel) == false)
                {
                    return false;
                }
                if (m_maxQueuedFrames.Equals(clone.m_maxQueuedFrames) == false)
                {
                    return false;
                }
                if (m_names.Length != clone.m_names.Length)
                {
                    return false;
                }
                for (var i = 0; i < m_names.Length; i++)
                {
                    if (m_names[i] != clone.m_names[i])
                    {
                        return false;
                    }
                }

                if (m_particleRaycastBudget.Equals(clone.m_particleRaycastBudget) == false)
                {
                    return false;
                }
                if (m_pixelLightCount.Equals(clone.m_pixelLightCount) == false)
                {
                    return false;
                }
                if (m_realtimeReflectionProbes.Equals(clone.m_realtimeReflectionProbes) == false)
                {
                    return false;
                }
                if (m_resolutionScalingFixedDPIFactor.Equals(clone.m_resolutionScalingFixedDPIFactor) == false)
                {
                    return false;
                }
                if (m_shadowCascade2Split.Equals(clone.m_shadowCascade2Split) == false)
                {
                    return false;
                }
                if (m_shadowCascade4Split.Equals(clone.m_shadowCascade4Split) == false)
                {
                    return false;
                }
                if (m_shadowCascades.Equals(clone.m_shadowCascades) == false)
                {
                    return false;
                }
                if (m_shadowDistance.Equals(clone.m_shadowDistance) == false)
                {
                    return false;
                }
                if (m_shadowmaskMode.Equals(clone.m_shadowmaskMode) == false)
                {
                    return false;
                }
                if (m_shadowNearPlaneOffset.Equals(clone.m_shadowNearPlaneOffset) == false)
                {
                    return false;
                }
                if (m_shadowProjection.Equals(clone.m_shadowProjection) == false)
                {
                    return false;
                }
                if (m_shadowResolution.Equals(clone.m_shadowResolution) == false)
                {
                    return false;
                }
                if (m_shadows.Equals(clone.m_shadows) == false)
                {
                    return false;
                }
#if UNITY_2019_1_OR_NEWER
                if (m_skinWeights.Equals(clone.m_skinWeights) == false)
                {
                    return false;
                }
#endif
                if (m_softParticles.Equals(clone.m_softParticles) == false)
                {
                    return false;
                }
                if (m_softVegetation.Equals(clone.m_softVegetation) == false)
                {
                    return false;
                }
                if (m_streamingMipmapsActive.Equals(clone.m_streamingMipmapsActive) == false)
                {
                    return false;
                }
                if (m_streamingMipmapsAddAllCameras.Equals(clone.m_streamingMipmapsAddAllCameras) == false)
                {
                    return false;
                }
                if (m_streamingMipmapsMaxFileIORequests.Equals(clone.m_streamingMipmapsMaxFileIORequests) == false)
                {
                    return false;
                }
                if (m_streamingMipmapsMaxLevelReduction.Equals(clone.m_streamingMipmapsMaxLevelReduction) == false)
                {
                    return false;
                }
                if (m_streamingMipmapsMemoryBudget.Equals(clone.m_streamingMipmapsMemoryBudget) == false)
                {
                    return false;
                }
                if (m_streamingMipmapsRenderersPerFrame.Equals(clone.m_streamingMipmapsRenderersPerFrame) == false)
                {
                    return false;
                }
                if (m_vSyncCount.Equals(clone.m_vSyncCount) == false)
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
}