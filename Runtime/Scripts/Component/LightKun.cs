using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    namespace Engine
    {
        [System.Serializable]
        public class LightBakingOutputKun : ISerializerKun
        {
            bool m_dirty;
            bool m_isBaked;


            public bool isBaked
            {
                get { return m_isBaked; }
                set { m_isBaked = value; m_dirty = true; }
            }

            LightmapBakeType m_lightmapBakeType;

            public LightmapBakeType lightmapBakeType
            {
                get { return m_lightmapBakeType; }
                set { m_lightmapBakeType = value; m_dirty = true; }
            }

            MixedLightingMode m_mixedLightingMode;

            public MixedLightingMode mixedLightingMode
            {
                get { return m_mixedLightingMode; }
                set { m_mixedLightingMode = value; m_dirty = true; }
            }

            int m_occlusionMaskChannel;

            public int occlusionMaskChannel
            {
                get { return m_occlusionMaskChannel; }
                set { m_occlusionMaskChannel = value; m_dirty = true; }
            }

            int m_probeOcclusionLightIndex;

            public int probeOcclusionLightIndex
            {
                get { return m_probeOcclusionLightIndex; }
                set { m_probeOcclusionLightIndex = value; }
            }

            public void Deserialize(BinaryReader binaryReader)
            {
                m_dirty = binaryReader.ReadBoolean();
                m_isBaked = binaryReader.ReadBoolean();
                m_lightmapBakeType = (LightmapBakeType)binaryReader.ReadInt32();
                m_mixedLightingMode = (MixedLightingMode)binaryReader.ReadInt32();
                m_occlusionMaskChannel = binaryReader.ReadInt32();
                m_probeOcclusionLightIndex = binaryReader.ReadInt32();

            }

            public void Serialize(BinaryWriter binaryWriter)
            {
                binaryWriter.Write(m_dirty);
                binaryWriter.Write(m_isBaked);
                binaryWriter.Write((int)m_lightmapBakeType);
                binaryWriter.Write((int)m_mixedLightingMode);
                binaryWriter.Write(m_occlusionMaskChannel);
                binaryWriter.Write(m_probeOcclusionLightIndex);
            }

            public LightBakingOutputKun()
            {
                m_dirty = false;
            }


            public LightBakingOutputKun(LightBakingOutput lightBakingOutput)
            {
                m_dirty = false;
                m_isBaked = lightBakingOutput.isBaked;
                m_lightmapBakeType = lightBakingOutput.lightmapBakeType;
                m_mixedLightingMode = lightBakingOutput.mixedLightingMode;
                m_occlusionMaskChannel = lightBakingOutput.occlusionMaskChannel;
                m_probeOcclusionLightIndex = lightBakingOutput.probeOcclusionLightIndex;
            }

            public void WriteBack(LightBakingOutput lightBakingOutput)
            {
                lightBakingOutput.isBaked = m_isBaked;
                lightBakingOutput.lightmapBakeType = m_lightmapBakeType;
                lightBakingOutput.mixedLightingMode = m_mixedLightingMode;
                lightBakingOutput.occlusionMaskChannel = m_occlusionMaskChannel;
                lightBakingOutput.probeOcclusionLightIndex = m_probeOcclusionLightIndex;
                m_dirty = false;
            }

            public override bool Equals(object obj)
            {
                var other = obj as LightBakingOutputKun;
                if (other == null)
                {
                    return false;
                }
                if (m_isBaked != other.m_isBaked)
                {
                    return false;
                }
                if (m_lightmapBakeType != other.m_lightmapBakeType)
                {
                    return false;
                }
                if (m_mixedLightingMode != other.m_mixedLightingMode)
                {
                    return false;
                }
                if (m_occlusionMaskChannel != other.m_occlusionMaskChannel)
                {
                    return false;
                }
                if (m_probeOcclusionLightIndex != other.m_probeOcclusionLightIndex)
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


        /// <summary>
        /// LightをSerialize/Desrializeする為のClass
        /// Programed by Katsumasa.Kimura
        /// </summary>
        [System.Serializable]
        public class LightKun : BehaviourKun
        {
            
            int mCommandBufferCount;
            float[] mLayerShadowCullDistances;
            LightShadowCasterMode mLightShadowCasterMode;
            int mShadowCustomResolution;
#if UNITY_2019_1_OR_NEWER
            LightShape mLightShape;
            float mColorTemperature;
            bool mUseColorTemperature;
            int mRenderingLayerMask;
#endif
            LightType mType;
            float mRange;
            float mSpotAngle;
            float mInnerSpotAngle;
            float mCookieSize;
            ColorKun mColor;
            float mIntensity;
            float mBounceIntensity;
            TextureKun mCookie;
            LightShadows mShadows;
            float mShadowsStrength;
            UnityEngine.Rendering.LightShadowResolution mShadowsResolution;
            float mShadowsBias;
            float mShadowsNearPlane;
            bool mHalo;
            string mFlare;
            int mCullingMask;
            LightBakingOutputKun mBakingOutput;
            LightRenderMode mRenderMode;




            public int commandBufferCount
            {
                get { return mCommandBufferCount; }
            }

            public float[] layerShadowCullDistances
            {
                get { return mLayerShadowCullDistances; }
                set { mLayerShadowCullDistances = value; dirty = true; }
            }

            public LightShadowCasterMode lightShadowCasterMode
            {
                get { return mLightShadowCasterMode; }
                set { mLightShadowCasterMode = value;  dirty = true; }
            }

            public int shadowCustomResolution
            {
                get { return mShadowCustomResolution; }
                set { mShadowCustomResolution = value; dirty = true; }
            }


#if UNITY_2019_1_OR_NEWER
            public LightShape lightShape
            {
                get { return mLightShape; }
                set { mLightShape = value; dirty = true; }
            }
            public float colorTemperature
            {
                get { return mColorTemperature; }
                set { mColorTemperature = value; dirty = true; }
            }
            public bool useColorTemperature
            {
                get { return mUseColorTemperature; }
                set { mUseColorTemperature = value; dirty = true; }
            }
            public int renderingLayerMask
            {
                get { return renderingLayerMask; }
                set { renderingLayerMask = value; dirty = true; }
            }
#endif
            public LightType type
            {
                get { return mType; }
                set { mType = value; dirty = true; }
            }
            public float range
            {
                get { return mRange; }
                set { mRange = value; dirty = true; }
            }
            public float spotAngle
            {
                get { return mSpotAngle; }
                set { mSpotAngle = value; dirty = true; }
            }
            public float innerSpotAngle
            {
                get { return mInnerSpotAngle; }
                set { mInnerSpotAngle = value; dirty = true; }
            }
            public float cookieSize
            {
                get { return mCookieSize; }
                set { mCookieSize = value;  dirty = true; }
            }
            
            public float intensity
            {
                get { return mIntensity; }
                set { mIntensity = value; dirty = true; }
            }
            public float bounceIntensity
            {
                get { return mBounceIntensity; }
                set { mBounceIntensity = value; dirty = true; }
            }
            public TextureKun cookie
            {
                get { return mCookie; }
                set { mCookie = value; dirty = true; }
            }
            public LightShadows shadows
            {
                get { return mShadows; }
                set { mShadows = value; dirty = true; }
            }

            public float shadowsStrength
            {
                get { return mShadowsStrength; }
                set { mShadowsStrength = value; dirty = true; }
            }
            public UnityEngine.Rendering.LightShadowResolution shadowsResolution
            {
                get { return mShadowsResolution; }
                set { mShadowsResolution = value; dirty = true; }
            }
            public float shadowsBias
            {
                get { return mShadowsBias; }
                set { mShadowsBias = value; dirty = true; }
            }
            public float shadowsNearPlane
            {
                get { return mShadowsNearPlane; }
                set { mShadowsNearPlane = value; dirty = true; }
            }
            public bool halo
            {
                get { return mHalo; }
                set { mHalo = value; dirty = true; }
            }
            public string flare
            {
                get { return mFlare; }
                set { mFlare = value; dirty = true; }
            }
            public int cullingMask
            {
                get { return mCullingMask; }
                set { mCullingMask = value; }
            }
            public LightBakingOutputKun bakingOutput
            {
                get { return mBakingOutput; }
                set { mBakingOutput = value; dirty = true; }
            }
            public LightRenderMode renderMode
            {
                get { return mRenderMode; }
                set { mRenderMode = value; }
            }


            public Color color
            {
                get { return mColor.GetColor(); }
                set { mColor = new ColorKun(value); }
            }


            public LightKun() : this(null) { }


            public LightKun(Component component) : base(component)
            {
                componentKunType = BehaviourKun.ComponentKunType.Light;                
                mFlare = "";
                mColor = new ColorKun();                
                mBakingOutput = new LightBakingOutputKun();
                
                var light = component as Light;
                if (light)
                {
                    enabled = light.enabled;

                    
                    mCommandBufferCount = light.commandBufferCount;
                    mLayerShadowCullDistances = light.layerShadowCullDistances;
                    mLightShadowCasterMode = light.lightShadowCasterMode;
                    mShadowCustomResolution = light.shadowCustomResolution;

#if UNITY_2019_1_OR_NEWER
                    mLightShape = light.shape;
                    mUseColorTemperature = light.useColorTemperature;
                    mColorTemperature = light.colorTemperature;
                    mRenderingLayerMask = light.renderingLayerMask;
#endif

                    mType = light.type;
                    mRange = light.range;
                    mSpotAngle = light.spotAngle;
                    mInnerSpotAngle = light.innerSpotAngle;
                    mCookieSize = light.cookieSize;
                    if (light.cookie != null)
                    {
                        mCookie = new TextureKun(light.cookie);
                    }                    
                    mColor = new ColorKun(light.color);
                    mIntensity = light.intensity;
                    mBounceIntensity = light.bounceIntensity;                    
                    mShadows = light.shadows;
                    mShadowsStrength = light.shadowStrength;
                    mShadowsResolution = light.shadowResolution;
                    mShadowsBias = light.shadowBias;
                    mShadowsNearPlane = light.shadowNearPlane;
                    
                    
                    if (light.flare != null)
                    {
                        mFlare = light.flare.name;
                    }
                    else
                    {
                        mFlare = "";
                    }
                    mCullingMask = light.cullingMask;                    
                    mBakingOutput = new LightBakingOutputKun(light.bakingOutput);
                    mRenderMode = light.renderMode;
                }
            }


            public override bool WriteBack(Component component)
            {
                //Debug.Log("WriteBack");
                if (base.WriteBack(component))
                {
                    var light = component as Light;
                    if (light != null)
                    {                        
                        light.layerShadowCullDistances = mLayerShadowCullDistances;
                        light.lightShadowCasterMode = mLightShadowCasterMode;
                        light.shadowCustomResolution = mShadowCustomResolution;
#if UNITY_2019_1_OR_NEWER
                        light.shape = mLightShape;
                        light.renderingLayerMask = mRenderingLayerMask;
                        light.colorTemperature = mColorTemperature;
                        light.useColorTemperature = mUseColorTemperature;
#endif

                        light.enabled = enabled;
                        light.type = mType;
                        light.range = mRange;
                        light.spotAngle = mSpotAngle;
                        light.innerSpotAngle = mInnerSpotAngle;


                        var newTexture = TextureKun.GetCache(instanceID) as Texture;
                        if(newTexture != null && light.cookie != newTexture)
                        {
                            light.cookie = newTexture;
                        }
                        light.cookieSize = mCookieSize;

                        // ToDo::cookie と flare            
                        light.color = mColor.GetColor();
                        light.intensity = mIntensity;
                        light.bounceIntensity = mBounceIntensity;

                        light.shadows = mShadows;
                        light.shadowStrength = mShadowsStrength;
                        light.shadowBias = mShadowsBias;
                        light.shadowNearPlane = mShadowsNearPlane;
                        
                        //ToDo::halo
                        light.cullingMask = mCullingMask;
                        light.renderMode = mRenderMode;
                        bakingOutput.WriteBack(light.bakingOutput);
                    }
                    return true;
                }
                return false;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryWriter"></param>
            public override void Serialize(BinaryWriter binaryWriter)
            {
                base.Serialize(binaryWriter);

                binaryWriter.Write(mCommandBufferCount);
                SerializerKun.Serialize(binaryWriter, mLayerShadowCullDistances);
                binaryWriter.Write((int)mLightShadowCasterMode);
                binaryWriter.Write(mShadowCustomResolution);
#if UNITY_2019_1_OR_NEWER
                binaryWriter.Write((int)mLightShape);
                binaryWriter.Write(mColorTemperature);
                binaryWriter.Write(mUseColorTemperature);
                binaryWriter.Write(mRenderingLayerMask);
#endif
                binaryWriter.Write((int)mType);
                binaryWriter.Write(mRange);
                binaryWriter.Write(mSpotAngle);
                binaryWriter.Write(mInnerSpotAngle);
                binaryWriter.Write(mCookieSize);

                SerializerKun.Serialize<ColorKun>(binaryWriter, mColor);

                binaryWriter.Write(mIntensity);
                binaryWriter.Write(mBounceIntensity);
                SerializerKun.Serialize<TextureKun>(binaryWriter, mCookie);                
                
                binaryWriter.Write((int)mShadows);
                binaryWriter.Write(mShadowsStrength);
                binaryWriter.Write((int)mShadowsResolution);
                binaryWriter.Write(mShadowsBias);                
                binaryWriter.Write(mShadowsNearPlane);
                binaryWriter.Write(mHalo);
                binaryWriter.Write(mFlare);
                binaryWriter.Write(mCullingMask);
                binaryWriter.Write((int)mRenderMode);
                SerializerKun.Serialize<LightBakingOutputKun>(binaryWriter, mBakingOutput);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryReader"></param>
            public override void Deserialize(BinaryReader binaryReader)
            {
                base.Deserialize(binaryReader);

                mCommandBufferCount = binaryReader.ReadInt32();
                mLayerShadowCullDistances = SerializerKun.DesirializeSingles(binaryReader);
                mLightShadowCasterMode = (LightShadowCasterMode)binaryReader.ReadInt32();
                mShadowCustomResolution = binaryReader.ReadInt32();
#if UNITY_2019_1_OR_NEWER
                mLightShape = (LightShape)binaryReader.ReadInt32();
                mColorTemperature = binaryReader.ReadSingle();
                mUseColorTemperature = binaryReader.ReadBoolean();
                mRenderingLayerMask = binaryReader.ReadInt32();
#endif
                mType = (LightType)binaryReader.ReadInt32();
                mRange = binaryReader.ReadSingle();
                mSpotAngle = binaryReader.ReadSingle();
                mInnerSpotAngle = binaryReader.ReadSingle();
                mCookieSize = binaryReader.ReadSingle();
                mColor = SerializerKun.DesirializeObject<ColorKun>(binaryReader);
                mIntensity = binaryReader.ReadSingle();
                mBounceIntensity = binaryReader.ReadSingle();
                mCookie = SerializerKun.DesirializeObject<TextureKun>(binaryReader);

                mShadows = (LightShadows)binaryReader.ReadInt32();
                mShadowsStrength = binaryReader.ReadSingle();
                mShadowsResolution = (UnityEngine.Rendering.LightShadowResolution)binaryReader.ReadInt32();
                mShadowsBias = binaryReader.ReadSingle();
                mShadowsNearPlane = binaryReader.ReadSingle();
                mHalo = binaryReader.ReadBoolean();
                mFlare = binaryReader.ReadString();                
                mCullingMask = binaryReader.ReadInt32();
                mRenderMode = (LightRenderMode)binaryReader.ReadInt32();
                mBakingOutput = SerializerKun.DesirializeObject<LightBakingOutputKun>(binaryReader);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public override bool Equals(object other)
            {
                var otherKun = other as LightKun;
                if (otherKun == null)
                {
                    return false;
                }

                if (mLightShadowCasterMode.Equals(otherKun.mLightShadowCasterMode) == false)
                {
                    return false;
                }
                if (mShadowCustomResolution.Equals(otherKun.mShadowCustomResolution) == false)
                {
                    return false;
                }
#if UNITY_2019_1_OR_NEWER
                if (mLightShape != otherKun.mLightShape)
                {
                    return false;
                }
                if (mColorTemperature.Equals(otherKun.mColorTemperature) == false)
                {
                    return false;
                }
                if (mUseColorTemperature.Equals(otherKun.mUseColorTemperature) == false)
                {
                    return false;
                }
                if (mRenderingLayerMask.Equals(otherKun.mRenderingLayerMask) == false)
                {
                    return false;
                }
#endif
                if (mType.Equals(otherKun.mType) == false)
                {
                    return false;
                }
                if (mRange.Equals(otherKun.mRange) == false)
                {
                    return false;
                }
                if (mSpotAngle.Equals(otherKun.mSpotAngle) == false)
                {
                    return false;
                }
                if (mInnerSpotAngle.Equals(otherKun.mInnerSpotAngle) == false)
                {
                    return false;
                }
                if (mCookieSize.Equals(otherKun.mCookieSize) == false)
                {
                    return false;
                }
                if (!ColorKun.Equals(mColor, otherKun.mColor))
                {
                    return false;
                }
                if (mIntensity.Equals(otherKun.mIntensity) == false)
                {
                    return false;
                }
                if (mBounceIntensity.Equals(otherKun.mBounceIntensity) == false)
                {
                    return false;
                }
                if (mCookie.Equals(otherKun.mCookie) == false)
                {
                    return false;
                }
                if (mShadows.Equals(otherKun.mShadows) == false)
                {
                    return false;
                }
                if (mShadowsStrength.Equals(otherKun.mShadowsStrength) == false)
                {
                    return false;
                }
                if (mShadowsResolution.Equals(otherKun.mShadowsResolution) == false)
                {
                    return false;
                }
                if (mShadowsBias.Equals(otherKun.mShadowsBias) == false)
                {
                    return false;
                }                
                if (mShadowsNearPlane.Equals(otherKun.mShadowsNearPlane) == false)
                {
                    return false;
                }
                if (mHalo.Equals(otherKun.mHalo) == false)
                {
                    return false;
                }
                if (mFlare.Equals(otherKun.mFlare) == false)
                {
                    return false;
                }
                if (mCullingMask.Equals(otherKun.mCullingMask) == false)
                {
                    return false;
                }
                
                if (mBakingOutput.Equals(otherKun.mBakingOutput) == false)
                {
                    return false;
                }
                if(mRenderMode.Equals(otherKun.mRenderMode) == false)
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