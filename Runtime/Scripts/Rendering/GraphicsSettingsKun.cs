using System.IO;
using UnityEngine;
using UnityEngine.Rendering;


namespace Utj.UnityChoseKun
{
    namespace Engine
    {
        namespace Rendering
        {
            using Universal;


            public class GraphicsSettingsKun : ObjectKun
            {



                public static bool isDirty
                {
                    get { return instance.dirty; }
                    set { instance.dirty = value; }
                }


                static GraphicsSettingsKun mInstance;

                public static GraphicsSettingsKun instance
                {
                    get
                    {
                        if (mInstance == null)
                        {
#if UNITY_EDITOR
                            mInstance = new GraphicsSettingsKun(false);
#else
                            mInstance = new GraphicsSettingsKun(true);
#endif
                        }
                        return mInstance;
                    }
                }

#if UNITY_2019_1_OR_NEWER
                RenderPipelineAssetKun[] mAllConfiguredRenderPipelines;
                public static RenderPipelineAssetKun[] allConfiguredRenderPipelines
                {
                    get
                    {
                        return instance.mAllConfiguredRenderPipelines;
                    }
                }

                int mCurrentRenderPipelineIdx;
                public static int currentRenderPipelineIdx
                {
                    get { return instance.mCurrentRenderPipelineIdx; }
                }

                int mDefaultRenderPipelineIdx;
                public static int defaultRenderPipelineIdx
                {
                    get { return instance.mDefaultRenderPipelineIdx; }
                }

                public static RenderPipelineAssetKun currentRenderPipeline
                {
                    get
                    {
                        if(instance.mCurrentRenderPipelineIdx == -1)
                        {
                            return null;
                        }
                        return instance.mAllConfiguredRenderPipelines[instance.mCurrentRenderPipelineIdx];
                    }
                }
                
                public static RenderPipelineAssetKun defaultRenderPipeline
                {
                    get 
                    {
                        if(instance.mDefaultRenderPipelineIdx == -1)
                        {
                            return null;
                        }
                        return instance.mAllConfiguredRenderPipelines[instance.mDefaultRenderPipelineIdx]; 
                    }
                }

                RenderPipelineAssetType mRenderPipelineAssetType;
                public static RenderPipelineAssetType renderPipelineAssetType
                {
                    get{return instance.mRenderPipelineAssetType;}
                }

                public static RenderPipelineAssetKun renderPipelineAssetKun
                {
                    get{return defaultRenderPipeline;}
                }
#if UNITY_2020_2_OR_NEWER
                uint mDefaultRenderingLayerMask;
                public static uint defaultRenderingLayerMask
                {
                    get{return instance.mDefaultRenderingLayerMask;}

                    set
                    {
                        instance.mDefaultRenderingLayerMask = value;
                        instance.dirty = true;
                    }
                }
#endif

#if UNITY_2020_3_OR_NEWER
                bool mDisableBuiltinCustomRenderTextureUpdate;
                public static bool disableBuiltinCustomRenderTextureUpdate
                {
                    get { return instance.mDisableBuiltinCustomRenderTextureUpdate; }
                    set
                    {
                        instance.mDisableBuiltinCustomRenderTextureUpdate = value;
                        instance.dirty = true;
                    }
                }

#endif
                bool mLogWhenShaderIsCompiled;
                public static bool logWhenShaderIsCompiled
                {
                    get { return instance.mLogWhenShaderIsCompiled; }
                    set
                    {
                        instance.mLogWhenShaderIsCompiled = value;
                        instance.dirty = true;
                    }
                }

                bool mRealtimeDirectRectangularAreaLights;
                public static bool realtimeDirectRectangularAreaLights
                {
                    get { return instance.mRealtimeDirectRectangularAreaLights; }
                    set
                    {
                        instance.mRealtimeDirectRectangularAreaLights = value;
                        instance.dirty = value;
                    }
                }
#endif

#if UNITY_2020_1_OR_NEWER
                VideoShadersIncludeMode mVideoShadersIncludeMode;
                public static VideoShadersIncludeMode videoShadersIncludeMode
                {
                    get
                    {
                        return instance.mVideoShadersIncludeMode;
                    }
                }
#endif

                bool mLightsUseColorTemperature;
                public static bool lightsUseColorTemperature
                {
                    get { return instance.mLightsUseColorTemperature; }
                    set
                    {
                        instance.mLightsUseColorTemperature = value;
                        instance.dirty = true;
                    }
                }

                bool mLightsUseLinearIntensity;
                public static bool lightsUseLinearIntensity
                {
                    get { return instance.mLightsUseLinearIntensity; }
                    set
                    {
                        instance.mLightsUseLinearIntensity = value;
                        instance.dirty = true;
                    }
                }

                Vector3Kun mTransparencySortAxis;
                public static Vector3Kun transparencySortAxis
                {
                    get { return instance.mTransparencySortAxis; }
                    set
                    {
                        instance.mTransparencySortAxis = value;
                        instance.dirty = true;
                    }
                }

                TransparencySortMode mTransparencySortMode;
                public static TransparencySortMode transparencySortMode
                {
                    get { return instance.mTransparencySortMode; }
                    set
                    {
                        instance.mTransparencySortMode = value;
                        instance.dirty = true;
                    }
                }


                bool mUseScriptableRenderPipelineBatching;
                public static bool useScriptableRenderPipelineBatching
                {
                    get { return instance.mUseScriptableRenderPipelineBatching; }
                    set
                    {
                        instance.mUseScriptableRenderPipelineBatching = value;
                        instance.dirty = true;
                    }
                }


                /// <summary>
                /// 
                /// </summary>
                public GraphicsSettingsKun() : this(false) { }


                /// <summary>
                /// 
                /// </summary>
                /// <param name="isSet"></param>
                public GraphicsSettingsKun(bool isSet)
                {
                    dirty = false;
                    if (isSet)
                    {
#if UNITY_2019_1_OR_NEWER
                        mCurrentRenderPipelineIdx = -1;
                        mDefaultRenderPipelineIdx = -1;

                        if (GraphicsSettings.renderPipelineAsset == null)
                        {
                            mRenderPipelineAssetType = RenderPipelineAssetType.NONE;
                        } 
                        else
                        {
                            var type = GraphicsSettings.renderPipelineAsset.GetType();

                            if (type.Name == "UniversalRenderPipelineAsset")
                            {
                                mRenderPipelineAssetType = RenderPipelineAssetType.URP;
                            }
                            else
                            {
                                mRenderPipelineAssetType = RenderPipelineAssetType.HDRP;
                            }

                            var len = GraphicsSettings.allConfiguredRenderPipelines.Length;                            
                            mAllConfiguredRenderPipelines = new UniversalRenderPipelineAssetKun[len];
                            for (var i = 0; i < len; i++)
                            {
                                if (type.Name == "UniversalRenderPipelineAsset") {
                                    mAllConfiguredRenderPipelines[i] = new UniversalRenderPipelineAssetKun(GraphicsSettings.allConfiguredRenderPipelines[i]);
                                }
                                else
                                {
                                    mAllConfiguredRenderPipelines[i] = new RenderPipelineAssetKun(GraphicsSettings.allConfiguredRenderPipelines[i]);
                                }
                                if(GraphicsSettings.allConfiguredRenderPipelines[i] == GraphicsSettings.currentRenderPipeline)
                                {
                                    mCurrentRenderPipelineIdx = i;
                                }
                                if(GraphicsSettings.allConfiguredRenderPipelines[i] == GraphicsSettings.defaultRenderPipeline)
                                {
                                    mDefaultRenderPipelineIdx = i;
                                }
                            }
                            
                            
                        }
                        
                        
                        mLogWhenShaderIsCompiled = GraphicsSettings.logWhenShaderIsCompiled;
                        mRealtimeDirectRectangularAreaLights = GraphicsSettings.realtimeDirectRectangularAreaLights;

#endif

#if UNITY_2020_1_OR_NEWER
                        mVideoShadersIncludeMode = GraphicsSettings.videoShadersIncludeMode;
#endif

#if UNITY_2020_2_OR_NEWER
                        mDefaultRenderingLayerMask = GraphicsSettings.defaultRenderingLayerMask;
#endif

#if UNITY_2020_3_OR_NEWER
                        mDisableBuiltinCustomRenderTextureUpdate = GraphicsSettings.disableBuiltinCustomRenderTextureUpdate;
#endif
                        mLightsUseColorTemperature = GraphicsSettings.lightsUseColorTemperature;
                        mLightsUseLinearIntensity = GraphicsSettings.lightsUseLinearIntensity;                                                
                        mTransparencySortAxis = new Vector3Kun(GraphicsSettings.transparencySortAxis);
                        mTransparencySortMode = GraphicsSettings.transparencySortMode;
                        mUseScriptableRenderPipelineBatching = GraphicsSettings.useScriptableRenderPipelineBatching;
                        
                    }
                }



                public override void Deserialize(BinaryReader binaryReader)
                {
                    base.Deserialize(binaryReader);
#if UNITY_2019_1_OR_NEWER
                    mRenderPipelineAssetType = (RenderPipelineAssetType)binaryReader.ReadInt32();

                    switch (mRenderPipelineAssetType)
                    {
                        case RenderPipelineAssetType.NONE:
                            {

                            }
                            break;
                        case RenderPipelineAssetType.RP:
                            {
                                mAllConfiguredRenderPipelines = SerializerKun.DesirializeObjects<RenderPipelineAssetKun>(binaryReader);                
                            }
                            break;
                        case RenderPipelineAssetType.URP:
                            {
                                mAllConfiguredRenderPipelines = SerializerKun.DesirializeObjects<UniversalRenderPipelineAssetKun>(binaryReader);
                            }
                            break;
                    }
                    mCurrentRenderPipelineIdx = binaryReader.ReadInt32();
                    mDefaultRenderPipelineIdx = binaryReader.ReadInt32();

                    
                    mLogWhenShaderIsCompiled = binaryReader.ReadBoolean();
                    mRealtimeDirectRectangularAreaLights = binaryReader.ReadBoolean();
#endif

#if UNITY_2020_1_OR_NEWER
                    mVideoShadersIncludeMode = (VideoShadersIncludeMode)binaryReader.ReadInt32();
#endif

#if UNITY_2020_1_OR_NEWER
                    mDefaultRenderingLayerMask = binaryReader.ReadUInt32();
#endif

#if UNITY_2020_3_OR_NEWER
                    mDisableBuiltinCustomRenderTextureUpdate = binaryReader.ReadBoolean();
#endif
                    mLightsUseColorTemperature = binaryReader.ReadBoolean();
                    mLightsUseLinearIntensity = binaryReader.ReadBoolean();
                    mTransparencySortAxis = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
                    mTransparencySortMode = (TransparencySortMode)binaryReader.ReadInt32();
                    mUseScriptableRenderPipelineBatching = binaryReader.ReadBoolean();


                    mInstance = this;
                }


                public override void Serialize(BinaryWriter binaryWriter)
                {
                    base.Serialize(binaryWriter);
#if UNITY_2019_1_OR_NEWER
                    binaryWriter.Write((int)mRenderPipelineAssetType);
                    switch (mRenderPipelineAssetType)
                    {
                        case RenderPipelineAssetType.NONE:
                            {
                            }
                            break;

                        case RenderPipelineAssetType.RP:
                            {
                                SerializerKun.Serialize<RenderPipelineAssetKun>(binaryWriter, mAllConfiguredRenderPipelines);                                
                            }
                            break;

                        case RenderPipelineAssetType.URP:
                            {
                                SerializerKun.Serialize<UniversalRenderPipelineAssetKun>(binaryWriter, mAllConfiguredRenderPipelines as UniversalRenderPipelineAssetKun[]);                                
                            }
                            break;
                    }
                    binaryWriter.Write(mCurrentRenderPipelineIdx);
                    binaryWriter.Write(mDefaultRenderPipelineIdx);
                    
                    
                    binaryWriter.Write(mLogWhenShaderIsCompiled);
                    binaryWriter.Write(mRealtimeDirectRectangularAreaLights);

#endif

#if UNITY_2020_1_OR_NEWER
                    binaryWriter.Write((int)mVideoShadersIncludeMode);
#endif

#if UNITY_2020_2_OR_NEWER
                    binaryWriter.Write(mDefaultRenderingLayerMask);
#endif

#if UNITY_2020_3_OR_NEWER
                    binaryWriter.Write(mDisableBuiltinCustomRenderTextureUpdate);
#endif

                    binaryWriter.Write(mLightsUseColorTemperature);
                    binaryWriter.Write(mLightsUseLinearIntensity);                                        
                    SerializerKun.Serialize<Vector3Kun>(binaryWriter, mTransparencySortAxis);
                    binaryWriter.Write((int)mTransparencySortMode);
                    binaryWriter.Write(mUseScriptableRenderPipelineBatching);
                    
                }


                public void WriteBack()
                {                    
                    if (base.WriteBack(null) == false)
                    {
                        return;
                    }
#if UNITY_2019_1_OR_NEWER                                        
                    GraphicsSettings.logWhenShaderIsCompiled = mLogWhenShaderIsCompiled;
                    GraphicsSettings.realtimeDirectRectangularAreaLights = mRealtimeDirectRectangularAreaLights;
#endif

#if UNITY_2020_2_OR_NEWER
                    GraphicsSettings.defaultRenderingLayerMask = mDefaultRenderingLayerMask;
#endif

#if UNITY_2020_3_OR_NEWER
                    GraphicsSettings.disableBuiltinCustomRenderTextureUpdate = mDisableBuiltinCustomRenderTextureUpdate;
#endif
                    GraphicsSettings.lightsUseColorTemperature = mLightsUseColorTemperature;
                    GraphicsSettings.lightsUseLinearIntensity = mLightsUseLinearIntensity;                                        
                    GraphicsSettings.transparencySortAxis = mTransparencySortAxis.GetVector3();
                    GraphicsSettings.transparencySortMode = mTransparencySortMode;
                    GraphicsSettings.useScriptableRenderPipelineBatching = mUseScriptableRenderPipelineBatching;                    
                }


                public override bool Equals(object obj)
                {
                    return true;
                }


                public override int GetHashCode()
                {
                    return base.GetHashCode();
                }


                public override string ToString()
                {
                    return base.ToString();
                }

                public static void OnMessageEvent(BinaryReader binaryReader)
                {
                    instance.Deserialize(binaryReader);
                }
            }
        }
    }
}
