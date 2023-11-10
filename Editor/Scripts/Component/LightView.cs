using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;
    using Engine.Rendering.Universal;


    namespace Editor
    {

        using Editor.Rendering.Universal;


        public class LightView : ComponentView
        {

            private static class StylesEx
            {
                public static readonly GUIContent iconRemove = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove command buffer");
                public static readonly GUIContent DisabledLightWarning = EditorGUIUtility.TrTextContent("Lighting has been disabled in at least one Scene view. Any changes applied to lights in the Scene will not be updated in these views until Lighting has been enabled again.");
                public static readonly GUIStyle invisibleButton = "InvisibleButton";
                public static readonly GUIContent noDiscLightInEnlighten = EditorGUIUtility.TrTextContent("Only the Progressive lightmapper supports Disc lights. The Enlighten lightmapper doesn't so please consider using a different light shape instead or switch to Progressive in the Lighting window.");
            }


            public LightKun lightKun
            {
                get { return componentKun as LightKun; }
                private set { componentKun = value; }
            }

            UniversalAdditionalLightDataView mUniversalAdditionalLightDataView;





            public bool isAreaLightType { get { return lightKun.type == LightType.Rectangle || lightKun.type == LightType.Disc; } }


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


            private static class Styles
            {
                public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_AreaLight Icon");
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
                public static readonly int[] LightTypeValues = { (int)LightType.Spot, (int)LightType.Directional, (int)LightType.Point, (int)LightType.Rectangle};
                public static readonly GUIContent[] AreaLightShapeTitles = { EditorGUIUtility.TrTextContent("Rectangle"), EditorGUIUtility.TrTextContent("Disc") };
                public static readonly int[] AreaLightShapeValues = { (int)AreaLightShape.Rectangle, (int)AreaLightShape.Disc };
                public static readonly GUIContent[] RenderModeDisplayedOptions = new[]
                {
                    EditorGUIUtility.TrTextContent("Auto"),
                    EditorGUIUtility.TrTextContent("Important"),
                    EditorGUIUtility.TrTextContent("Not Important"),
                };

                public static readonly int[] RenderModeOptions = { (int)LightRenderingMode.Disabled,(int)LightRenderMode.ForcePixel,(int)LightRenderMode.ForceVertex};


                public static readonly GUIContent LightAppearance = EditorGUIUtility.TrTextContent("Light Appearance ");

                public static readonly GUIContent[] LightAppearanceDisplayedOptions = new[]
                {
                    EditorGUIUtility.TrTextContent("Color"),
                    EditorGUIUtility.TrTextContent("Fillter and Temperature"),
                };

                public static int[] LightAppearanceOptionValues =
                {
                    0,1
                };

            }


            public LightView()
            {
                componentIcon = Styles.ComponentIcon;
                foldout = true;
            }


            public bool DrawEnabled()
            {
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                EditorGUILayout.BeginHorizontal();
                var iconContent = new GUIContent(mComponentIcon);
                foldout = EditorGUILayout.Foldout(foldout, iconContent);                          // Foldout & Icon

                EditorGUI.BeginChangeCheck();
                var content = new GUIContent(lightKun.name);

                var rect = EditorGUILayout.GetControlRect();
                lightKun.enabled = EditorGUI.ToggleLeft(new Rect(rect.x - 24, rect.y, rect.width, rect.height), content, lightKun.enabled);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.dirty = true;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

                return foldout;
            }

            public void DrawType(bool isDrawShape)
            {
                var selectedLightType = (isAreaLightType) ? 3 : (int)lightKun.type;
                var selectedShape = isAreaLightType ? (int)lightKun.type : (int)AreaLightShape.None;
                EditorGUI.BeginChangeCheck();
                int type = EditorGUILayout.IntPopup(
                    Styles.Type, selectedLightType, Styles.LightTypeTitles, Styles.LightTypeValues);

                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.type = (LightType)type;
                }

                
                var mode = lightKun.bakingOutput.lightmapBakeType;
                EditorGUILayout.EnumPopup("Mode", mode);

                if (isDrawShape)
                {
                    if (isAreaLightType && selectedShape != (int)AreaLightShape.None)
                    {
                        EditorGUI.BeginChangeCheck();
                        int shape = EditorGUILayout.IntPopup(Styles.Shape, selectedShape, Styles.AreaLightShapeTitles, Styles.AreaLightShapeValues);
                        if (EditorGUI.EndChangeCheck())
                        {
                            lightKun.type = (LightType)shape;
                        }
                    }
                }
            }

           
            public void DrawRange(bool showAreaOptions = false)
            {
                EditorGUI.indentLevel += 1;
                EditorGUI.BeginChangeCheck();
                var range = EditorGUILayout.FloatField(Styles.Range, lightKun.range);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.range = range;
                }
                EditorGUI.indentLevel -= 1;
            }


            public void DrawInnerAndOuterSpotAngle()
            {
                float min = lightKun.innerSpotAngle;
                float max = lightKun.spotAngle;
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(Styles.InnerOuterSpotAngle);
                        EditorGUILayout.IntField((int)min,GUILayout.Width(64f));
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.MinMaxSlider(ref min, ref max, 0f, 179f);
                        if (EditorGUI.EndChangeCheck())
                        {
                            lightKun.innerSpotAngle = min;
                            lightKun.spotAngle = max;
                        }
                        EditorGUILayout.IntField((int)max,GUILayout.Width(64f));
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }


            public void DrawColor()
            {
#if UNITY_2019_1_OR_NEWER
                if (Engine.Rendering.GraphicsSettingsKun.lightsUseLinearIntensity && Engine.Rendering.GraphicsSettingsKun.lightsUseColorTemperature)
                {
                    EditorGUI.BeginChangeCheck();
                    var useColorTemperature = lightKun.useColorTemperature ? 1 : 0;
                    useColorTemperature = EditorGUILayout.IntPopup(Styles.LightAppearance,useColorTemperature,Styles.LightAppearanceDisplayedOptions,Styles.LightAppearanceOptionValues);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.useColorTemperature = (useColorTemperature == 1) ? true : false;
                    }

                    EditorGUI.indentLevel += 1;
                    if (lightKun.useColorTemperature)
                    {                        
                        

                        EditorGUI.BeginChangeCheck();
                        var color = EditorGUILayout.ColorField(Styles.ColorFilter, lightKun.color);
                        if (EditorGUI.EndChangeCheck())
                        {
                            lightKun.color = color;
                        }
                        EditorGUI.BeginChangeCheck();                        
                        var colorTemperature = EditorGUILayout.Slider(Styles.ColorTemperature, lightKun.colorTemperature, kMinKelvin, kMaxKelvin);                        
                        if (EditorGUI.EndChangeCheck())
                        {
                            lightKun.colorTemperature = colorTemperature;
                        }                        
                    }
                    else
                    {
                        EditorGUI.BeginChangeCheck();
                        var color = EditorGUILayout.ColorField(Styles.Color, lightKun.color);
                        if (EditorGUI.EndChangeCheck())
                        {
                            lightKun.color = color;
                        }
                    }
                    EditorGUI.indentLevel -= 1;
                }
                else
                {
                    EditorGUI.BeginChangeCheck();
                    var color = EditorGUILayout.ColorField(Styles.Color, lightKun.color);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.color = color;
                    }
                }
#else
                EditorGUI.BeginChangeCheck();
                var color = EditorGUILayout.ColorField(Styles.Color, lightKun.color);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.color = color;
                }
#endif
            }



            public void DrawIntensity()
            {
                EditorGUI.BeginChangeCheck();
                var intensity = EditorGUILayout.FloatField(Styles.Intensity, lightKun.intensity);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.intensity = intensity;
                }
            }


            public void DrawBounceIntensity()
            {
                EditorGUI.BeginChangeCheck();
                var bounceIntensity = EditorGUILayout.FloatField(Styles.LightBounceIntensity, lightKun.bounceIntensity);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.bounceIntensity = bounceIntensity;
                }
            }


            public void DrawCookie()
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Styles.Cookie);

                if (TexturesView.textureKuns == null)
                {
                    if (lightKun.cookie == null)
                    {
                        EditorGUILayout.LabelField("None(Texture)");
                    }
                    else
                    {
                        EditorGUILayout.LabelField(lightKun.cookie.name);
                    }
                } 
                else
                {
                    var idx = 0;
                    var optionValues = new int[TexturesView.textureKuns.Length];

                                       
                    for (var i = 0; i < TexturesView.textureKuns.Length; i++)
                    {
                        optionValues[i] = i;
                        if (lightKun.cookie != null && TexturesView.textureKuns[i] == lightKun.cookie)
                        {
                            idx = i;
                        }
                    }
                    
                    EditorGUI.BeginChangeCheck();
                    idx = EditorGUILayout.IntPopup(idx, TexturesView.textureNames, optionValues);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (idx == 0)
                        {
                            lightKun.cookie = null;
                        }
                        else
                        {
                            lightKun.cookie = TexturesView.textureKuns[idx];
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }


            public void DrawCookieSize()
            {
                EditorGUI.BeginChangeCheck();
                var cookieSize = EditorGUILayout.FloatField(Styles.CookieSize, lightKun.cookieSize);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.cookieSize = cookieSize;
                }
            }

            public void DrawHalo()
            {
                EditorGUI.BeginChangeCheck();
                var halo = EditorGUILayout.Toggle(Styles.DrawHalo, lightKun.halo);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.halo = halo;
                }
            }
            
            public void DrawRenderMode()
            {
                var renderMode = (int)lightKun.renderMode;
                EditorGUI.BeginChangeCheck();
                renderMode  = EditorGUILayout.IntPopup(Styles.RenderMode, renderMode, Styles.RenderModeDisplayedOptions, Styles.RenderModeOptions);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.renderMode = (LightRenderMode)renderMode;                    
                }
            }

            public void DrawCullingMask()
            {
                
                EditorGUI.BeginChangeCheck();
                var cullingMask = LayerMaskKun.LayerMaskField(new GUIContent("Culling Mask"), lightKun.cullingMask).value;
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.cullingMask = cullingMask;
                }
            }

            public void DrawRenderingLayerMask()
            {
#if UNITY_2019_1_OR_NEWER
                var srpAsset = Engine.Rendering.GraphicsSettingsKun.currentRenderPipeline;
                bool usingSRP = srpAsset != null;
                if (!usingSRP)
                    return;
                var layerNames = srpAsset.renderingLayerMaskNames;
                if (layerNames == null)
                {
                    return;
                }
                EditorGUI.BeginChangeCheck();
                var renderingLayerMask = EditorGUILayout.MaskField(Styles.RenderingLayerMask, lightKun.renderingLayerMask, layerNames);
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.renderingLayerMask = renderingLayerMask;
                }
#endif
            }


            public void DrawShadowsType()
            {
                if (isAreaLightType)
                {
                    EditorGUI.BeginChangeCheck();
                    var shadows = EditorGUILayout.Toggle(Styles.CastShadows, lightKun.shadows != LightShadows.None);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.shadows = shadows ? LightShadows.Soft : LightShadows.None;                        
                    }
                }
                else
                {
                    EditorGUI.BeginChangeCheck();
                    var shadows = (LightShadows)EditorGUILayout.EnumPopup(Styles.ShadowType, lightKun.shadows);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.shadows = shadows;
                    }
                }
            }

            public void DrawRuntimeShadow()
            {
                EditorGUILayout.LabelField(Styles.ShadowRealtimeSettings);
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.BeginChangeCheck();
                    var shadowsStrength = EditorGUILayout.Slider(Styles.ShadowStrength, lightKun.shadowsStrength, 0f, 1f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.shadowsStrength = shadowsStrength;
                    }
                    EditorGUI.BeginChangeCheck();
                    var shadowsResolution = (UnityEngine.Rendering.LightShadowResolution)EditorGUILayout.EnumPopup(Styles.ShadowResolution, lightKun.shadowsResolution);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.shadowsResolution = shadowsResolution;
                    }
                    EditorGUI.BeginChangeCheck();
                    var shadowsBias = EditorGUILayout.Slider(Styles.ShadowBias, lightKun.shadowsBias, 0.0f, 2.0f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        lightKun.shadowsBias = shadowsBias;
                    }
                }
            }
            
            
            // <summary> OnGUIから呼び出す処理 </summary>
            public override bool OnGUI()
            {
                if (DrawEnabled())
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
#if UNITY_2019_1_OR_NEWER

                        var additionalLightKun = lightKun.transformKun.gameObjectKun.GetComponentKun<UniversalAdditionalLightDataKun>();
                        if (additionalLightKun == null)
                        {
                            DrawLightContens();
                        } 
                        else
                        {
                            if(mUniversalAdditionalLightDataView == null)
                            {
                                mUniversalAdditionalLightDataView = new UniversalAdditionalLightDataView();
                                mUniversalAdditionalLightDataView.universalAdditionalLightDataKun = additionalLightKun;
                            }
                            mUniversalAdditionalLightDataView.DrawContent(this);
                        }
#else
                        DrawLightContens();
#endif

                    }
                }
                return true;
            }


            void DrawLightContens()
            {
                EditorGUI.BeginChangeCheck();
                DrawType(true);

                EditorGUILayout.Space();

                if (lightKun.type == LightType.Spot)
                {
                    DrawRange();
                    DrawInnerAndOuterSpotAngle();
                }

                DrawColor();

                EditorGUILayout.Space();

                ShadowsGUI();

                DrawIntensity();

                DrawBounceIntensity();

                DrawCookie();

                DrawHalo();
                //settings.DrawFlare();

                DrawCullingMask();
                DrawRenderingLayerMask();
                if (EditorGUI.EndChangeCheck())
                {
                    lightKun.dirty = true;
                }
            }

            void ShadowsGUI()
            {
                //NOTE: FadeGroup's dont support nesting. Thus we just multiply the fade values here.
                DrawShadowsType();

                EditorGUI.indentLevel += 1;

                // Runtime shadows - shadow strength, resolution, bias

                DrawRuntimeShadow();


                EditorGUI.indentLevel -= 1;

                EditorGUILayout.Space();
            }


        }
    }
}