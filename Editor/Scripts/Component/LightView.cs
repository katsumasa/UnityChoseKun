
namespace  Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.AnimatedValues;

    public class LightView : ComponentView
    {
        public class Settings
        {
            [SerializeField] private LightKun m_lightKun;
            
            #if UNITY_2019_1_OR_NEWER
            public LightShape lightShape { get{return lightKun.lightShape;}  private set{lightKun.lightShape = value;}}
            public float colorTemperature { get{return lightKun.colorTemperature;} private set{lightKun.colorTemperature = value;} }
            public bool useColorTemperature { get{return lightKun.useColorTemperature;} private set{lightKun.useColorTemperature = value;} }
            public int renderingLayerMask { get{return lightKun.renderingLayerMask;} private set{lightKun.renderingLayerMask = value;} }
            #endif

            public LightKun lightKun {get {if(m_lightKun == null){m_lightKun = new LightKun();}return m_lightKun;} set {m_lightKun = value;}}
            public bool enabled {get{return lightKun.enabled;} private set {lightKun.enabled = value;}}
            public LightType lightType {get {return lightKun.lightType;} private set {lightKun.lightType = value;}}
            

            public float range { get{return lightKun.range;}  private set {lightKun.range = value;}}
            public float spotAngle {get{return lightKun.spotAngle;}  private set{lightKun.spotAngle = value;}}
            public float innerSpotAngle { get{return lightKun.innerSpotAngle;} private set{lightKun.innerSpotAngle = value;} }
            public float cookieSize { get{return lightKun.cookieSize;} private set{lightKun.cookieSize = value;} }             
            public Color color { get{return lightKun.color;} private set{lightKun.color = value;} }
            public float intensity { get{return lightKun.intensity;} private set{lightKun.intensity = value;} }
            public float bounceIntensity { get {return lightKun.bounceIntensity;} private set{lightKun.bounceIntensity = value;} }
            
            public string cookiName {get{return lightKun.cookie;}private set {lightKun.cookie = value;}}
            public LightShadows shadowsType { get{return lightKun.shadowsType;} private set{lightKun.shadowsType = value;} }
            public float shadowsStrength { get{return lightKun.shadowsStrength;} private set{lightKun.shadowsStrength = value;} }
            public UnityEngine.Rendering.LightShadowResolution shadowsResolution { get{return lightKun.shadowsResolution;} private set{lightKun.shadowsResolution = value;} }
            public float shadowsBias { get{return lightKun.shadowsBias;} private set{lightKun.shadowsBias = value;} }
            public float shadowsNormalBias { get{return lightKun.shadowsNormalBias;} private set{lightKun.shadowsNormalBias = value;} }
            public float shadowsNearPlane { get{return lightKun.shadowsNearPlane;} private set{lightKun.shadowsNearPlane = value;} }
            public bool halo { get{return lightKun.halo;} private set{lightKun.halo = value;} }
            public string flare { get{return lightKun.flare;} private set{lightKun.flare = value;} }
            public LightRenderMode renderMode { get{return lightKun.renderMode;} private set{lightKun.renderMode = value;} }
            public int cullingMask { get{return lightKun.cullingMask;} private set{lightKun.cullingMask = value;} }
            

            
            
            
            public bool isAreaLightType { get { return lightType == LightType.Rectangle || lightType  == LightType.Disc; } }

            
            
            const float kMinKelvin = 1000f;
            const float kMaxKelvin = 20000f;
            const float kSliderPower = 2f;

            // should have the same int values as corresponding shape in LightType
            private enum AreaLightShape
            {
                None = 0,
                Rectangle = 3,
                Disc = 4
            }

            public Settings(){}
            public Settings(string json)
            {
                lightKun = JsonUtility.FromJson<LightKun>(json);
            }

        public static readonly GUIContent LightContent = new GUIContent("", (Texture2D)EditorGUIUtility.Load("d_AreaLight Icon"));
            private static class Styles
            {
                public static readonly GUIContent Type = EditorGUIUtility.TrTextContent("Type", "Specifies the current type of light. Possible types are Directional, Spot, Point, and Area lights.");
                public static readonly GUIContent Shape = EditorGUIUtility.TrTextContent("Shape", "Specifies the shape of the Area light. Possible types are Rectangle and Disc.");
                public static readonly GUIContent Range = EditorGUIUtility.TrTextContent("Range", "Controls how far the light is emitted from the center of the object.");
                public static readonly GUIContent SpotAngle = EditorGUIUtility.TrTextContent("Spot Angle", "Controls the angle in degrees at the base of a Spot light's cone.");
                public static readonly GUIContent InnerOuterSpotAngle = EditorGUIUtility.TrTextContent("Inner / Outer Spot Angle", "Controls the inner and outer angle in degrees, at the base of a Spot light's cone.");
                public static readonly GUIContent Color = EditorGUIUtility.TrTextContent("Color", "Controls the color being emitted by the light.");
                public static readonly GUIContent UseColorTemperature = EditorGUIUtility.TrTextContent("Use color temperature mode", "Choose between RGB and temperature mode for light's color.");
                public static readonly GUIContent ColorFilter = EditorGUIUtility.TrTextContent("Filter", "A colored gel can be put in front of the light source to tint the light.");
                public static readonly GUIContent ColorTemperature = EditorGUIUtility.TrTextContent("Temperature", "Also known as CCT (Correlated color temperature). The color temperature of the electromagnetic radiation emitted from an ideal black body is defined as its surface temperature in Kelvin. White is 6500K");
                public static readonly GUIContent Intensity = EditorGUIUtility.TrTextContent("Intensity", "Controls the brightness of the light. Light color is multiplied by this value.");
                public static readonly GUIContent LightmappingMode = EditorGUIUtility.TrTextContent("Mode", "Specifies the light mode used to determine if and how a light will be baked. Possible modes are Baked, Mixed, and Realtime.");
                public static readonly GUIContent LightBounceIntensity = EditorGUIUtility.TrTextContent("Indirect Multiplier", "Controls the intensity of indirect light being contributed to the scene. A value of 0 will cause Realtime lights to be removed from realtime global illumination and Baked and Mixed lights to no longer emit indirect lighting. Has no effect when both Realtime and Baked Global Illumination are disabled.");
                public static readonly GUIContent ShadowType = EditorGUIUtility.TrTextContent("Shadow Type", "Specifies whether Hard Shadows, Soft Shadows, or No Shadows will be cast by the light.");
                public static readonly GUIContent CastShadows = EditorGUIUtility.TrTextContent("Cast Shadows", "Specifies whether Soft Shadows or No Shadows will be cast by the light.");
                //realtime
                public static readonly GUIContent ShadowRealtimeSettings = EditorGUIUtility.TrTextContent("Realtime Shadows", "Settings for realtime direct shadows.");
                public static readonly GUIContent ShadowStrength = EditorGUIUtility.TrTextContent("Strength", "Controls how dark the shadows cast by the light will be.");
                public static readonly GUIContent ShadowResolution = EditorGUIUtility.TrTextContent("Resolution", "Controls the rendered resolution of the shadow maps. A higher resolution will increase the fidelity of shadows at the cost of GPU performance and memory usage.");
                public static readonly GUIContent ShadowBias = EditorGUIUtility.TrTextContent("Bias", "Controls the distance at which the shadows will be pushed away from the light. Useful for avoiding false self-shadowing artifacts.");
                public static readonly GUIContent ShadowNormalBias = EditorGUIUtility.TrTextContent("Normal Bias", "Controls distance at which the shadow casting surfaces will be shrunk along the surface normal. Useful for avoiding false self-shadowing artifacts.");
                public static readonly GUIContent ShadowNearPlane = EditorGUIUtility.TrTextContent("Near Plane", "Controls the value for the near clip plane when rendering shadows. Currently clamped to 0.1 units or 1% of the lights range property, whichever is lower.");
                //baked
                public static readonly GUIContent BakedShadowRadius = EditorGUIUtility.TrTextContent("Baked Shadow Radius", "Controls the amount of artificial softening applied to the edges of shadows cast by the Point or Spot light.");
                public static readonly GUIContent BakedShadowAngle = EditorGUIUtility.TrTextContent("Baked Shadow Angle", "Controls the amount of artificial softening applied to the edges of shadows cast by directional lights.");
                public static readonly GUIContent Cookie = EditorGUIUtility.TrTextContent("Cookie", "Specifies the Texture mask to cast shadows, create silhouettes, or patterned illumination for the light.");
                public static readonly GUIContent CookieSize = EditorGUIUtility.TrTextContent("Cookie Size", "Controls the size of the cookie mask currently assigned to the light.");
                public static readonly GUIContent DrawHalo = EditorGUIUtility.TrTextContent("Draw Halo", "When enabled, draws a spherical halo of light with a radius equal to the lights range value.");
                public static readonly GUIContent Flare = EditorGUIUtility.TrTextContent("Flare", "Specifies the flare object to be used by the light to render lens flares in the scene.");
                public static readonly GUIContent RenderMode = EditorGUIUtility.TrTextContent("Render Mode", "Specifies the importance of the light which impacts lighting fidelity and performance. Options are Auto, Important, and Not Important. This only affects Forward Rendering.");
                public static readonly GUIContent CullingMask = EditorGUIUtility.TrTextContent("Culling Mask", "Specifies which layers will be affected or excluded from the light's effect on objects in the scene.");
                public static readonly GUIContent RenderingLayerMask = EditorGUIUtility.TrTextContent("Rendering Layer Mask", "Mask that can be used with SRP when drawing shadows to filter renderers outside of the normal layering system.");
                public static readonly GUIContent AreaWidth = EditorGUIUtility.TrTextContent("Width", "Controls the width in units of the area light.");
                public static readonly GUIContent AreaHeight = EditorGUIUtility.TrTextContent("Height", "Controls the height in units of the area light.");
                public static readonly GUIContent AreaRadius = EditorGUIUtility.TrTextContent("Radius", "Controls the radius in units of the disc area light.");
                public static readonly GUIContent BakingWarning = EditorGUIUtility.TrTextContent("Light mode is currently overridden to Realtime mode. Enable Baked Global Illumination to use Mixed or Baked light modes.");
                public static readonly GUIContent IndirectBounceShadowWarning = EditorGUIUtility.TrTextContent("Realtime indirect bounce shadowing is not supported for Spot and Point lights.");
                public static readonly GUIContent CookieWarning = EditorGUIUtility.TrTextContent("Cookie textures for spot lights should be set to clamp, not repeat, to avoid artifacts.");
                public static readonly GUIContent MixedUnsupportedWarning = EditorGUIUtility.TrTextContent("Light mode is currently overridden to Realtime mode. The current render pipeline doesn't support Mixed mode and/or any of the lighting modes.");
                public static readonly GUIContent BakedUnsupportedWarning = EditorGUIUtility.TrTextContent("Light mode is currently overridden to Realtime mode. The current render pipeline doesn't support Baked mode.");
                public static readonly GUIContent[] LightmapBakeTypeTitles = { EditorGUIUtility.TrTextContent("Realtime"), EditorGUIUtility.TrTextContent("Mixed"), EditorGUIUtility.TrTextContent("Baked") };
                public static readonly int[] LightmapBakeTypeValues = { (int)LightmapBakeType.Realtime, (int)LightmapBakeType.Mixed, (int)LightmapBakeType.Baked };
                public static readonly GUIContent[] LightTypeTitles = { EditorGUIUtility.TrTextContent("Spot"), EditorGUIUtility.TrTextContent("Directional"), EditorGUIUtility.TrTextContent("Point"), EditorGUIUtility.TrTextContent("Area (baked only)") };
                public static readonly int[] LightTypeValues = { (int)LightType.Spot, (int)LightType.Directional, (int)LightType.Point, (int)LightType.Rectangle };
                public static readonly GUIContent[] AreaLightShapeTitles = { EditorGUIUtility.TrTextContent("Rectangle"), EditorGUIUtility.TrTextContent("Disc") };
                public static readonly int[] AreaLightShapeValues = { (int)AreaLightShape.Rectangle, (int)AreaLightShape.Disc };
            }
        
            public bool DrawEnabled(bool foldout)
            {
                EditorGUILayout.Space();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));            
                EditorGUILayout.BeginHorizontal();
                foldout = EditorGUILayout.Foldout(foldout,LightContent);                
                
                EditorGUI.BeginChangeCheck();
                enabled = EditorGUILayout.ToggleLeft("Light",enabled);                
                if(EditorGUI.EndChangeCheck()){
                    lightKun.dirty = true;
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                EditorGUILayout.Space();
                return foldout;
            }

            public void DrawType()
            {
                var selectedLightType = (int)lightType;
                var selectedShape = isAreaLightType ? (int)lightType : (int)AreaLightShape.None;                                                
                EditorGUI.BeginChangeCheck();
                int type = EditorGUILayout.IntPopup(                    
                    Styles.Type, selectedLightType, Styles.LightTypeTitles, Styles.LightTypeValues);

                if (EditorGUI.EndChangeCheck())
                {
                    lightType = (LightType)type;
                }                
                if (isAreaLightType && selectedShape != (int)AreaLightShape.None)
                {                                        
                    EditorGUI.BeginChangeCheck();
                    int shape = EditorGUILayout.IntPopup(Styles.Shape, selectedShape, Styles.AreaLightShapeTitles, Styles.AreaLightShapeValues);
                    if (EditorGUI.EndChangeCheck())
                    {                        
                        lightType = (LightType)shape;
                    }                    
                }
            }

            public void DrawRange(bool showAreaOptions = false)
            {
                EditorGUI.indentLevel += 1;
                range = EditorGUILayout.FloatField(Styles.Range,range);
                EditorGUI.indentLevel -= 1;
            }

            public void DrawSpotAngle()
            {
                EditorGUI.indentLevel += 1;
                spotAngle = EditorGUILayout.Slider(Styles.SpotAngle,spotAngle, 1f, 179f);
                EditorGUI.indentLevel -= 1;
            }

        

            public void DrawColor()
            {
                #if UNITY_2019_1_OR_NEWER
                if (UnityEngine.Rendering.GraphicsSettings.lightsUseLinearIntensity && UnityEngine.Rendering.GraphicsSettings.lightsUseColorTemperature)
                {
                    useColorTemperature =  EditorGUILayout.Toggle(Styles.UseColorTemperature,useColorTemperature);
                    if (useColorTemperature)
                    {
                        EditorGUILayout.LabelField(Styles.Color);
                        EditorGUI.indentLevel += 1;
                        color = EditorGUILayout.ColorField(Styles.ColorFilter,color);
                        // TODO:Textureの表示
                        colorTemperature =  EditorGUILayout.Slider(Styles.ColorTemperature, colorTemperature, kMinKelvin, kMaxKelvin);
                        EditorGUI.indentLevel -= 1;
                    }
                    else
                        color = EditorGUILayout.ColorField(Styles.Color,color);
                }
                else
                    color = EditorGUILayout.ColorField(Styles.Color,color);
                #else
                    color = EditorGUILayout.ColorField(Styles.Color,color);
                #endif
            }
            
            

            public void DrawIntensity()
            {
                intensity = EditorGUILayout.FloatField(Styles.Intensity,intensity);
            }


            public void DrawBounceIntensity()
            {
                bounceIntensity =  EditorGUILayout.FloatField( Styles.LightBounceIntensity,bounceIntensity);                
            }

            public void DrawCookie()
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.Cookie);
                if(string.IsNullOrEmpty(cookiName)){
                    EditorGUILayout.LabelField("None(Texture)");
                }else{
                    EditorGUILayout.LabelField(cookiName);
                }
                EditorGUILayout.EndHorizontal();
            }

            public void DrawCookieSize()
            {
                cookieSize =  EditorGUILayout.FloatField(Styles.CookieSize,cookieSize);
            }

            public void DrawHalo()
            {
                halo = EditorGUILayout.Toggle(Styles.DrawHalo,halo);
            }

            public void DrawRenderMode()
            {
                renderMode = (LightRenderMode)EditorGUILayout.EnumPopup( Styles.RenderMode,renderMode);
            }

            public void DrawCullingMask()
            {
                LayerMask layerMask = cullingMask;
                cullingMask = LayerMaskField("Culling Mask",layerMask).value;
            }
            
            public void DrawRenderingLayerMask()
            {
                #if UNITY_2019_1_OR_NEWER
                var srpAsset = UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline;
                bool usingSRP = srpAsset != null;
                if (!usingSRP)
                    return;                
                var layerNames = srpAsset.renderingLayerMaskNames;
                if(layerNames == null){
                    return;
                }
                renderingLayerMask = EditorGUILayout.MaskField(Styles.RenderingLayerMask,renderingLayerMask,layerNames); 
                #endif
            }


            public void DrawShadowsType()
            {
                if(isAreaLightType){
                    EditorGUI.BeginChangeCheck();
                    var shadows = EditorGUILayout.Toggle(Styles.CastShadows,shadowsType != LightShadows.None);
                    if (EditorGUI.EndChangeCheck())
                    {
                        shadowsType = shadows ? LightShadows.Soft : LightShadows.None;
                    }
                } else {                    
                    shadowsType = (LightShadows) EditorGUILayout.EnumPopup(Styles.ShadowType,shadowsType);
                }
            }
             
            public void DrawRuntimeShadow()
            {
                EditorGUILayout.LabelField(Styles.ShadowRealtimeSettings);
                using(new EditorGUI.IndentLevelScope()){
                    shadowsStrength = EditorGUILayout.Slider(Styles.ShadowStrength,shadowsStrength, 0f, 1f);
                    shadowsResolution = (UnityEngine.Rendering.LightShadowResolution)EditorGUILayout.EnumPopup(Styles.ShadowResolution,shadowsResolution);
                    shadowsBias = EditorGUILayout.Slider(Styles.ShadowBias,shadowsBias, 0.0f, 2.0f);
                    shadowsNormalBias = EditorGUILayout.Slider(Styles.ShadowNormalBias,shadowsNormalBias, 0.0f, 3.0f);
                }
            }

            public LayerMask LayerMaskField(string label,LayerMask layerMask)
            {
                List<string> layers = new List<string>();
                List<int> layerNumbers = new List<int>();

                for (var i = 0; i < 32; ++i)
                {
                    string layerName = LayerMask.LayerToName(i);
                    if (!string.IsNullOrEmpty(layerName))
                    {
                        layers.Add(layerName);
                        layerNumbers.Add(i);
                    }
                }
                int maskWithoutEmpty = 0;
                for (var i = 0; i < layerNumbers.Count; ++i)
                {
                    if (0 < ((1 << layerNumbers[i]) & layerMask.value))
                        maskWithoutEmpty |= 1 << i;
                }
                maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers.ToArray());
                int mask = 0;
                for (var i = 0; i < layerNumbers.Count; ++i)
                {
                    if (0 < (maskWithoutEmpty & (1 << i)))
                        mask |= 1 << layerNumbers[i];
                }
                layerMask.value = mask;
                return layerMask;
            }               
        }
 
        [SerializeField] Settings m_settings;
        Settings settings {
            get {if(m_settings == null){m_settings = new Settings();}return m_settings;}
            set {m_settings = value;}
        }
        
        private static class StylesEx
        {
            public static readonly GUIContent iconRemove = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove command buffer");
            public static readonly GUIContent DisabledLightWarning = EditorGUIUtility.TrTextContent("Lighting has been disabled in at least one Scene view. Any changes applied to lights in the Scene will not be updated in these views until Lighting has been enabled again.");
            public static readonly GUIStyle invisibleButton = "InvisibleButton";
            public static readonly GUIContent noDiscLightInEnlighten = EditorGUIUtility.TrTextContent("Only the Progressive lightmapper supports Disc lights. The Enlighten lightmapper doesn't so please consider using a different light shape instead or switch to Progressive in the Lighting window.");
        }

        [SerializeField] bool foldout = true;




        public override ComponentKun GetComponentKun()
        {
            return settings.lightKun;
        }


        public override void SetComponentKun(ComponentKun componentKun)
        {
            settings.lightKun = (LightKun)componentKun;
        }


        // <summary> OnGUIから呼び出す処理 </summary>
        public override bool OnGUI()
        {
            
            foldout =  settings.DrawEnabled(foldout);
            if(foldout){
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.BeginChangeCheck();                    
                    settings.DrawType();
                    if (LightmapEditorSettings.lightmapper == LightmapEditorSettings.Lightmapper.Enlighten && settings.lightKun.lightType == LightType.Disc)
                        EditorGUILayout.HelpBox(StylesEx.noDiscLightInEnlighten.text, MessageType.Warning);
                    
                    EditorGUILayout.Space();
                    
                    if(settings.lightKun.lightType == LightType.Spot){
                        settings.DrawRange();
                        settings.DrawSpotAngle();
                    }                                                                
                                                        
                    settings.DrawColor();            

                    EditorGUILayout.Space();

                    ShadowsGUI();

                    settings.DrawIntensity();
                    
                    settings.DrawBounceIntensity();
                                
                    settings.DrawCookie();
                    
                    settings.DrawHalo();
                    //settings.DrawFlare();
                    settings.DrawRenderMode();
                    settings.DrawCullingMask();
                    settings.DrawRenderingLayerMask();
                    if(EditorGUI.EndChangeCheck()){
                        settings.lightKun.dirty = true;
                    }
                }
            }
            return true;
        }

        void ShadowsGUI()
        {
            //NOTE: FadeGroup's dont support nesting. Thus we just multiply the fade values here.
            settings.DrawShadowsType();

            EditorGUI.indentLevel += 1;
                                    
            // Runtime shadows - shadow strength, resolution, bias
            
            settings.DrawRuntimeShadow();
            

            EditorGUI.indentLevel -= 1;

            EditorGUILayout.Space();
        }


    }
}