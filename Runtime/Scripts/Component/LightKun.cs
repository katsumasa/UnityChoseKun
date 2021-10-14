using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace  Utj.UnityChoseKun 
{    
    /// <summary>
    /// LightをSerialize/Desrializeする為のClass
    /// Programed by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    public class LightKun : BehaviourKun
    {
#if UNITY_2019_1_OR_NEWER
        [SerializeField] public LightShape lightShape;
        [SerializeField] public float colorTemperature ;
        [SerializeField] public bool useColorTemperature ;
        [SerializeField] public int renderingLayerMask ;
#endif
        [SerializeField] public LightType lightType;
        [SerializeField] public float range;
        [SerializeField] public float spotAngle;
        [SerializeField] public float innerSpotAngle;
        [SerializeField] public float cookieSize;
        [SerializeField] public ColorKun m_colorKun ;
        [SerializeField] public float intensity;
        [SerializeField] public float bounceIntensity ;
        [SerializeField] public string cookie;
        [SerializeField] public LightShadows shadowsType;
        [SerializeField] public float shadowsStrength;
        [SerializeField] public UnityEngine.Rendering.LightShadowResolution  shadowsResolution;
        [SerializeField] public float shadowsBias;
        [SerializeField] public float shadowsNormalBias ;
        [SerializeField] public float shadowsNearPlane ;
        [SerializeField] public bool halo;
        [SerializeField] public string flare;
        [SerializeField] public LightRenderMode renderMode ;
        [SerializeField] public int cullingMask ;


        public Color color
        {
            get { return m_colorKun.GetColor(); }
            set { m_colorKun = new ColorKun(value); }
        }


        public LightKun() : this(null){}        


        public LightKun(Component component) : base(component)
        {                     
            componentKunType = BehaviourKun.ComponentKunType.Light;
            cookie = "";
            flare = "";
            m_colorKun = new ColorKun();
            var light = component as Light;
            if(light){       

#if UNITY_2019_1_OR_NEWER
                lightShape = light.shape;
                useColorTemperature = light.useColorTemperature;
                colorTemperature = light.colorTemperature;
                renderingLayerMask = light.renderingLayerMask;
#endif
                enabled = light.enabled;
                lightType = light.type;                            
                range = light.range;
                spotAngle = light.spotAngle;
                innerSpotAngle = light.spotAngle;
                cookieSize = light.cookieSize;
                if(light.cookie != null){
                    cookie = light.cookie.name;
                }else{
                    cookie = "";
                }
                if(light.flare != null){
                    flare = light.flare.name;
                }else{
                    flare = "";
                }
                color = light.color;
                intensity = light.intensity;
                bounceIntensity = light.bounceIntensity;
                                
                shadowsType = light.shadows;
                shadowsStrength = light.shadowStrength;
                shadowsBias = light.shadowBias;
                shadowsNormalBias = light.shadowNormalBias;
                shadowsNormalBias = light.shadowNormalBias;
                // halo
                renderMode = light.renderMode;
                cullingMask =light.cullingMask;                                        
            }
        }


        public override bool WriteBack(Component component)
        {
            //Debug.Log("WriteBack");
            if(base.WriteBack(component)){                
                var light = component as Light;
                if(light != null){

                    #if UNITY_2019_1_OR_NEWER
                    light.shape = lightShape;
                    light.renderingLayerMask = renderingLayerMask;
                    light.colorTemperature = colorTemperature;
                    light.useColorTemperature = useColorTemperature;
                    #endif

                    light.enabled = enabled;
                    light.type = lightType;                    
                    light.range = range;
                    light.spotAngle = spotAngle;
                    light.spotAngle = innerSpotAngle;
                    cookieSize = light.cookieSize;

                    // ToDo::cookie と flare            
                    light.color = color;
                    light.intensity = intensity;
                    light.bounceIntensity = bounceIntensity;
                    
                    light.shadows = shadowsType;
                    light.shadowStrength = shadowsStrength;
                    light.shadowBias = shadowsBias;
                    light.shadowNormalBias = shadowsNormalBias;
                    
                    //ToDo::halo
                    light.renderMode = renderMode;
                    light.cullingMask = cullingMask;
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
#if UNITY_2019_1_OR_NEWER
            binaryWriter.Write((int)lightShape);
            binaryWriter.Write(colorTemperature);
            binaryWriter.Write(useColorTemperature);
            binaryWriter.Write(renderingLayerMask);
#endif
            binaryWriter.Write((int)lightType);
            binaryWriter.Write(range);
            binaryWriter.Write(spotAngle);
            binaryWriter.Write(innerSpotAngle);
            binaryWriter.Write(cookieSize);

            SerializerKun.Serialize<ColorKun>(binaryWriter, m_colorKun);
            
            binaryWriter.Write(intensity);
            binaryWriter.Write(bounceIntensity);
            binaryWriter.Write(cookie);
            binaryWriter.Write((int)shadowsType);
            binaryWriter.Write(shadowsStrength);
            binaryWriter.Write((int)shadowsResolution);
            binaryWriter.Write(shadowsBias);
            binaryWriter.Write(shadowsNormalBias);
            binaryWriter.Write(shadowsNearPlane);
            binaryWriter.Write(halo);
            binaryWriter.Write(flare);
            binaryWriter.Write((int)renderMode);
            binaryWriter.Write(cullingMask);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
#if UNITY_2019_1_OR_NEWER
            lightShape = (LightShape)binaryReader.ReadInt32();
            colorTemperature = binaryReader.ReadSingle();
            useColorTemperature = binaryReader.ReadBoolean();
            renderingLayerMask =  binaryReader.ReadInt32();
#endif
            lightType = (LightType)binaryReader.ReadInt32();
            range = binaryReader.ReadSingle();
            spotAngle = binaryReader.ReadSingle();
            innerSpotAngle = binaryReader.ReadSingle();
            cookieSize = binaryReader.ReadSingle();
            m_colorKun = SerializerKun.DesirializeObject<ColorKun>(binaryReader);            
            intensity = binaryReader.ReadSingle();
            bounceIntensity = binaryReader.ReadSingle();
            cookie = binaryReader.ReadString();
            shadowsType = (LightShadows)binaryReader.ReadInt32();
            shadowsStrength = binaryReader.ReadSingle();
            shadowsResolution = (UnityEngine.Rendering.LightShadowResolution)binaryReader.ReadInt32();
            shadowsBias = binaryReader.ReadSingle();
            shadowsNormalBias = binaryReader.ReadSingle();
            shadowsNearPlane = binaryReader.ReadSingle();
            halo = binaryReader.ReadBoolean();
            flare = binaryReader.ReadString();
            renderMode = (LightRenderMode)binaryReader.ReadInt32();
            cullingMask = binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var otherKun = other as LightKun;
            if(otherKun == null)
            {
                return false;
            }
#if UNITY_2019_1_OR_NEWER
            if (lightShape != otherKun.lightShape)
            {
                return false;
            }
            if(colorTemperature.Equals(otherKun.colorTemperature) == false)
            {
                return false;
            }
            if(useColorTemperature.Equals(otherKun.useColorTemperature) == false)
            {
                return false;
            }
            if (renderingLayerMask.Equals(otherKun.renderingLayerMask) == false)
            {
                return false;
            }
#endif
            if (lightType.Equals(otherKun.lightType) == false)
            {
                return false;
            }
            if (range.Equals(otherKun.range) == false)
            {
                return false;
            }
            if (spotAngle.Equals(otherKun.spotAngle) == false)
            {
                return false;
            }
            if (innerSpotAngle.Equals(otherKun.innerSpotAngle) == false)
            {
                return false;
            }
            if (cookieSize.Equals(otherKun.cookieSize) == false)
            {
                return false;
            }


            if (!ColorKun.Equals(m_colorKun, otherKun.m_colorKun))
            {                        
                return false;
            }

            if (intensity.Equals(otherKun.intensity) == false)
            {
                return false;
            }
            if (bounceIntensity.Equals(otherKun.bounceIntensity) == false)
            {
                return false;
            }
            if (cookie.Equals(otherKun.cookie) == false)
            {
                return false;
            }
            if (shadowsType.Equals(otherKun.shadowsType) == false)
            {
                return false;
            }
            if (shadowsStrength.Equals(otherKun.shadowsStrength) == false)
            {
                return false;
            }
            if (shadowsResolution.Equals(otherKun.shadowsResolution) == false)
            {
                return false;
            }
            if (shadowsBias.Equals(otherKun.shadowsBias) == false)
            {
                return false;
            }
            if (shadowsNormalBias.Equals(otherKun.shadowsNormalBias) == false)
            {
                return false;
            }
            if (shadowsNearPlane.Equals(otherKun.shadowsNearPlane) == false)
            {
                return false;
            }
            if (halo.Equals(otherKun.halo) == false)
            {
                return false;
            }
            if (flare.Equals(otherKun.flare) == false)
            {
                return false;
            }
            if (renderMode.Equals(otherKun.renderMode) == false)
            {
                return false;
            }
            if (cullingMask.Equals(otherKun.cullingMask) == false)
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