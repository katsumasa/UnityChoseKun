using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  Utj.UnityChoseKun 
{    
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
        [SerializeField] public ColorKun m_color ;
        public Color color
        {
            get { return m_color.GetColor(); }
            set { m_color = new ColorKun(value); }
        }

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
        
        public LightKun() : this(null){}        

        public LightKun(Component component) : base(component)
        {                     
            componentKunType = BehaviourKun.ComponentKunType.Light;
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
    }
}