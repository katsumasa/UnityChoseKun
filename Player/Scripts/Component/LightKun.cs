using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  Utj.UnityChoseKun 
{
    

    [System.Serializable]
    public class LightKun : BehaviourKun
    {
        public LightType lightType;
        public LightShape lightShape;
        public float range;
        public float spotAngle;
        public float innerSpotAngle;
        public float cookieSize;
        public Color color ;
        public float intensity;
        public float bounceIntensity ;
        public float colorTemperature ;
        public bool useColorTemperature ;
        public string cookie;
        public LightShadows shadowsType;
        public float shadowsStrength;
        public UnityEngine.Rendering.LightShadowResolution  shadowsResolution;
        public float shadowsBias;
        public float shadowsNormalBias ;
        public float shadowsNearPlane ;
        public bool halo;
        public string flare;
        public LightRenderMode renderMode ;
        public int cullingMask ;                        
        public int renderingLayerMask ;
        
        public LightKun() : this(null){}        

        public LightKun(Component component) : base(component)
        {                     
            componentKunType = BehaviourKun.ComponentKunType.Light;
            var light = component as Light;
            if(light){                
                enabled = light.enabled;
                lightType = light.type;
                lightShape = light.shape;
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
                colorTemperature = light.colorTemperature;
                useColorTemperature = light.useColorTemperature;
                shadowsType = light.shadows;
                shadowsStrength = light.shadowStrength;
                shadowsBias = light.shadowBias;
                shadowsNormalBias = light.shadowNormalBias;
                shadowsNormalBias = light.shadowNormalBias;
                // halo
                renderMode = light.renderMode;
                cullingMask =light.cullingMask;
                        
                renderingLayerMask = light.renderingLayerMask;            
            }
        }

        public override void WriteBack(Component component)
        {
            base.WriteBack(component);
            Debug.Log("WriteBack");            

            var light = component as Light;
            if(light != null){                
                light.enabled = enabled;
                light.type = lightType;
                light.shape = lightShape;
                light.range = range;
                light.spotAngle = spotAngle;
                light.spotAngle = innerSpotAngle;
                cookieSize = light.cookieSize;

                // ToDo::cookie と flare            

                light.color = color;
                light.intensity = intensity;
                light.bounceIntensity = bounceIntensity;
                light.colorTemperature = colorTemperature;
                light.useColorTemperature = useColorTemperature;
                light.shadows = shadowsType;
                light.shadowStrength = shadowsStrength;
                light.shadowBias = shadowsBias;
                light.shadowNormalBias = shadowsNormalBias;
                
                //ToDo::halo
                light.renderMode = renderMode;
                light.cullingMask = cullingMask;
                            
                light.renderingLayerMask = renderingLayerMask;
            }
        }
    }
}