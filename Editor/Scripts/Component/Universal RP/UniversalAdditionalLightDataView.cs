using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;


namespace Utj.UnityChoseKun
{
    using Engine;
    using Engine.Rendering;
    using Engine.Rendering.Universal;


    namespace Editor.Rendering.Universal
    {
        public class UniversalAdditionalLightDataView : BehaviourView
        {

            private static class Styles
            {
                public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_AreaLight Icon");
                public static readonly GUIContent Type = EditorGUIUtility.TrTextContent("Type", "Specifies the current type of light. Possible types are Directional, Spot, Point, and Area lights.");
                public static readonly GUIContent Mode = EditorGUIUtility.TrTextContent("Mode", "");

                public static readonly int[] LightTypeValues = { (int)LightType.Spot, (int)LightType.Directional, (int)LightType.Point, (int)LightType.Rectangle };
                public static readonly GUIContent[] AreaLightShapeTitles = { EditorGUIUtility.TrTextContent("Rectangle"), EditorGUIUtility.TrTextContent("Disc") };
                public static readonly int[] AreaLightShapeValues = { (int)AreaLightShape.Rectangle, (int)AreaLightShape.Disc };
                
                public static readonly GUIContent AreaLightShapeContent = EditorGUIUtility.TrTextContent("Shape", "Specifies the shape of the Area light. Possible types are Rectangle and Disc.");

                public static readonly GUIContent color = EditorGUIUtility.TrTextContent("Color", "Specifies the color this Light emits.");
                public static readonly GUIContent lightAppearance = EditorGUIUtility.TrTextContent("Light Appearance", "Specifies the mode for this Light's color is calculated.");
                public static readonly GUIContent[] lightAppearanceOptions = new[]
                {
                    EditorGUIUtility.TrTextContent("Color"),
                    EditorGUIUtility.TrTextContent("Filter and Temperature")
                };
                public static readonly GUIContent colorFilter = EditorGUIUtility.TrTextContent("Filter", "Specifies a color which tints the Light source.");
                public static readonly GUIContent colorTemperature = EditorGUIUtility.TrTextContent("Temperature", "Specifies a temperature (in Kelvin) used to correlate a color for the Light. For reference, White is 6500K.");


                public static readonly GUIContent Intensity = EditorGUIUtility.TrTextContent("Intensity", "Controls the brightness of the light. Light color is multiplied by this value.");
                public static readonly GUIContent LightBounceIntensity = EditorGUIUtility.TrTextContent("Indirect Multiplier", "Controls the intensity of indirect light being contributed to the scene. A value of 0 will cause Realtime lights to be removed from realtime global illumination and Baked and Mixed lights to no longer emit indirect lighting. Has no effect when both Realtime and Baked Global Illumination are disabled.");
                public static readonly GUIContent Range = EditorGUIUtility.TrTextContent("Range", "Controls how far the light is emitted from the center of the object.");

                public static readonly GUIContent width = EditorGUIUtility.TrTextContent("Width", "Control the width in units of the area light.");
                public static readonly GUIContent Height = EditorGUIUtility.TrTextContent("Height", "Control the height in units of the area light.");
                public static readonly GUIContent radius = EditorGUIUtility.TrTextContent("Radius", "Control the radius in units of the disc light.");

                public static readonly GUIContent InnerOuterSpotAngle = EditorGUIUtility.TrTextContent("Inner / Outer Spot Angle", "Controls the inner and outer angle in degrees, at the base of a Spot light's cone.");

                public static readonly GUIContent[] renderModeOptions = new[]
                {
                    EditorGUIUtility.TrTextContent("Auto"),
                    EditorGUIUtility.TrTextContent("Important"),
                    EditorGUIUtility.TrTextContent("Not Important"),
                };

                public static readonly GUIContent ShadowRealtimeSettings = EditorGUIUtility.TrTextContent("Realtime Shadows", "Settings for realtime direct shadows.");
                public static readonly GUIContent ShadowResolution = EditorGUIUtility.TrTextContent("Resolution", $"Sets the rendered resolution of the shadow maps. A higher resolution increases the fidelity of shadows at the cost of GPU performance and memory usage. Rounded to the next power of two, and clamped to be at least {UniversalAdditionalLightDataKun.AdditionalLightsShadowMinimumResolution}.");

                public static readonly int[] ShadowResolutionDefaultValues =
                {
                    UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierCustom,
                    UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierLow,
                    UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierMedium,
                    UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierHigh
                };

                public static GUIContent[] additionalLightsShadowResolutionTierNames =
                {
                    new GUIContent("Low"),
                    new GUIContent("Medium"),
                    new GUIContent("High")
                };

                public static readonly GUIContent[] ShadowResolutionDefaultOptions =
                {
                    EditorGUIUtility.TrTextContent("Custom"),
                    additionalLightsShadowResolutionTierNames[0],
                    additionalLightsShadowResolutionTierNames[1],
                    additionalLightsShadowResolutionTierNames[2],
                };

                public static readonly GUIContent ShadowStrength = EditorGUIUtility.TrTextContent("Strength", "Controls how dark the shadows cast by the light will be.");
                public static GUIContent shadowBias = EditorGUIUtility.TrTextContent("Bias", "Select if the Bias should use the settings from the Pipeline Asset or Custom settings.");
                public static int[] optionDefaultValues = { 0, 1 };
                public static GUIContent[] displayedDefaultOptions =
                {
                    EditorGUIUtility.TrTextContent("Custom"),
                    EditorGUIUtility.TrTextContent("Use settings from Render Pipeline Asset")
                };
                public static readonly GUIContent ShadowNormalBias = EditorGUIUtility.TrTextContent("Normal", "Controls the distance shadow caster vertices are offset along their normals when rendering shadow maps. Currently ignored for Point Lights.");
                public static readonly GUIContent ShadowDepthBias = EditorGUIUtility.TrTextContent("Depth", "Determines the distance at which Unity pushes shadows away from the shadow-casting GameObject along the line from the Light.");
                public static readonly GUIContent ShadowNearPlane = EditorGUIUtility.TrTextContent("Near Plane", "Controls the value for the near clip plane when rendering shadows. Currently clamped to 0.1 units or 1% of the lights range property, whichever is lower.");

                public static readonly GUIContent customShadowLayers = EditorGUIUtility.TrTextContent("Custom Shadow Layers", "When enabled, you can use the Layer property below to specify the layers for shadows seperately to lighting. When disabled, the Light Layer property in the General section specifies the layers for both lighting and for shadows.");


                public static readonly GUIContent BakedShadowRadius = EditorGUIUtility.TrTextContent("Baked Shadow Radius", "Controls the amount of artificial softening applied to the edges of shadows cast by the Point or Spot light.");
                public static readonly GUIContent BakedShadowAngle = EditorGUIUtility.TrTextContent("Baked Shadow Angle", "Controls the amount of artificial softening applied to the edges of shadows cast by directional lights.");

                public static readonly GUIContent lightCookieHeader = new GUIContent("Light Cookie");
                public static readonly GUIContent LightCookieSize = EditorGUIUtility.TrTextContent("Cookie Size", "Controls the size of the cookie mask currently assigned to the light.");
                public static readonly GUIContent LightCookieOffset = EditorGUIUtility.TrTextContent("Cookie Offset", "Controls the offset of the cookie mask currently assigned to the light.");



            }



            private enum AreaLightShape
            {
                None = 0,
                Rectangle = 3,
                Disc = 4
            }


            public UniversalAdditionalLightDataKun universalAdditionalLightDataKun
            {
                get { return behaviourKun as UniversalAdditionalLightDataKun; }
                set { behaviourKun = value; }
            }




            bool m_foldOutGeneral = false;
            bool m_foldOutShape = false;
            bool m_foldOutEmission = false;
            bool m_foldOutRendering = false;
            bool m_foldOutShadow = false;
            bool m_foldOutCookie = false;


            public UniversalAdditionalLightDataView():base()
            {
                componentIcon = Styles.ComponentIcon;
            }

            public override bool OnGUI()
            {
                if (base.OnGUI())
                {

                }
                return true;
            }



            public void DrawContent(LightView lightView)
            {                
                DrawGeneralContentInternal(lightView);                    
                DrawShapeContentInternal(lightView);                    
                DrawEmissionContentInternal(lightView);
                DrawRenderingContentInternal(lightView);
                DrawShadowContent(lightView);
                DrawLightCookieContent(lightView);

                if (universalAdditionalLightDataKun.dirty)
                {
                    //UnityChoseKunEditor.SendMessage<UniversalAdditionalLightDataKun>(UnityChoseKun.MessageID.)
                    universalAdditionalLightDataKun.dirty = false;
                }
            }


            void DrawGeneralContentInternal(LightView lightView)
            {
                m_foldOutGeneral = EditorGUILayout.Foldout(m_foldOutGeneral, "General");
                if (m_foldOutGeneral)
                {
                    EditorGUI.indentLevel += 1;
                    lightView.DrawType(false);
                    EditorGUI.indentLevel -= 1;
                }
            }


            void DrawShapeContentInternal(LightView lightView)
            {
                m_foldOutShape = EditorGUILayout.Foldout(m_foldOutShape, "Shape");

                var isAreaLightType = lightView.lightKun.type == LightType.Rectangle || lightView.lightKun.type == LightType.Disc;
                var selectedShape = isAreaLightType ? (int)lightView.lightKun.type : (int)AreaLightShape.None;                                
                if(isAreaLightType && selectedShape == (int)AreaLightShape.None){
                    return;
                }
                
                if (m_foldOutShape)
                {
                    if (isAreaLightType && selectedShape != (int)AreaLightShape.None)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        {
                            // AreaTypeÇÃShapeÇÕBaked OnlyÇÃà◊ïœçXïââ◊
                            EditorGUILayout.IntPopup(Styles.AreaLightShapeContent, selectedShape, Styles.AreaLightShapeTitles, Styles.AreaLightShapeValues);
                            using (new EditorGUI.IndentLevelScope())
                            {
                                if (selectedShape == (int)AreaLightShape.Rectangle)
                                {
                                    EditorGUILayout.TextField(new GUIContent("Width"),"Unknown");
                                    EditorGUILayout.TextField(new GUIContent("Height"), "Unknown");
                                }
                                else if(selectedShape == (int)AreaLightShape.Disc)
                                {
                                    EditorGUILayout.TextField(new GUIContent("Radius"), "Unknown");
                                }
                            }
                        }
                    }


                    if (lightView.lightKun.type == LightType.Spot)
                    {
                        lightView.DrawInnerAndOuterSpotAngle();
                    }
                    
                }
            }


            void DrawEmissionContentInternal(LightView lightView)
            {
                m_foldOutEmission = EditorGUILayout.Foldout(m_foldOutEmission, "Emission");
                if (m_foldOutEmission)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        lightView.DrawColor();
                        lightView.DrawIntensity();
                        lightView.DrawBounceIntensity();

                        if (lightView.lightKun.type != LightType.Directional)
                        {
                            lightView.DrawRange();
                        }
                    }
                }
            }


            void DrawRenderingContentInternal(LightView lightView)
            {
                m_foldOutRendering = EditorGUILayout.Foldout(m_foldOutRendering, "Rendering");
                if (m_foldOutRendering)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {

                        lightView.DrawRenderMode();
                        lightView.DrawCullingMask();
                    }
                }
            }


            void DrawShadowContent(LightView lightView)
            {
                m_foldOutShadow = EditorGUILayout.Foldout(m_foldOutShadow,"Shadow");
                if (m_foldOutShadow == false)
                {
                    return;
                }



                using (new EditorGUI.IndentLevelScope())
                {
                    lightView.DrawShadowsType();
                    if (lightView.lightKun.shadows == LightShadows.None)
                    {
                        return;
                    }
                    var lightType = lightView.lightKun.type;

                    if (lightView.lightKun.bakingOutput.lightmapBakeType != LightmapBakeType.Realtime)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        {
                            switch (lightType)
                            {
                                case LightType.Point:
                                case LightType.Spot:
                                    EditorGUILayout.TextField(Styles.BakedShadowRadius, "Unknown");
                                    break;
                                case LightType.Directional:

                                    EditorGUILayout.TextField(Styles.BakedShadowAngle, "Unknown");
                                    break;
                            }
                        }
                    }

                    if (lightType != LightType.Rectangle && lightView.lightKun.bakingOutput.lightmapBakeType != LightmapBakeType.Baked)
                    {
                        EditorGUILayout.LabelField(Styles.ShadowRealtimeSettings, EditorStyles.boldLabel);
                        using (new EditorGUI.IndentLevelScope())
                        {
                            if (lightType == LightType.Point || lightType == LightType.Spot)
                            {
                                DrawShadowsResolutionGUI(lightView);
                            }
                            EditorGUI.BeginChangeCheck();
                            var shadowsStrength = EditorGUILayout.Slider(Styles.ShadowRealtimeSettings, lightView.lightKun.shadowsStrength, 0f, 1f);
                            if (EditorGUI.EndChangeCheck())
                            {
                                lightView.lightKun.shadowsStrength = shadowsStrength;
                            }

                            // Bias
                            DrawAdditionalShadowData(lightView);

                            // this min bound should match the calculation in SharedLightData::GetNearPlaneMinBound()
                            float nearPlaneMinBound = Mathf.Min(0.01f * lightView.lightKun.range, 0.1f);
                            EditorGUI.BeginChangeCheck();
                            var shadowsNearPlane = EditorGUILayout.Slider(Styles.ShadowNearPlane, lightView.lightKun.shadowsNearPlane, nearPlaneMinBound, 10.0f);
                            if (EditorGUI.EndChangeCheck())
                            {
                                lightView.lightKun.shadowsNearPlane = shadowsNearPlane;
                            }
                        }

                        if(UniversalRenderPipelineKun.asset.supportsLightLayers)
                        {                            
                            var customShadowLayers = universalAdditionalLightDataKun.customShadowLayers;
                            EditorGUI.BeginChangeCheck();
                            customShadowLayers = EditorGUILayout.Toggle(Styles.customShadowLayers, universalAdditionalLightDataKun.customShadowLayers);
                            if (EditorGUI.EndChangeCheck())
                            {
                                // Pending
                            }

                            if (customShadowLayers)
                            {
                                EditorGUI.BeginChangeCheck();


                                if (EditorGUI.EndChangeCheck())
                                {
                                    // Pending
                                }
                            }
                        }
                    }
                }

            }


            void DrawAdditionalShadowData(LightView lightView)
            {
                int selectedUseAdditionalData = universalAdditionalLightDataKun.usePipelineSettings ? 1 : 0;
                EditorGUI.BeginChangeCheck();
                selectedUseAdditionalData = EditorGUILayout.IntPopup(Styles.shadowBias, selectedUseAdditionalData, Styles.displayedDefaultOptions, Styles.optionDefaultValues);
                if (EditorGUI.EndChangeCheck())
                {
                    universalAdditionalLightDataKun.usePipelineSettings = selectedUseAdditionalData == 1 ? true : false;
                }

                if (selectedUseAdditionalData != 1)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        using (var checkScope = new EditorGUI.ChangeCheckScope())
                        {
                            var shadowDepthBias = EditorGUILayout.Slider(Styles.ShadowDepthBias, lightView.lightKun.shadowsBias, 0f, 10f);
                            if (checkScope.changed)
                            {
                                lightView.lightKun.shadowsBias = shadowDepthBias;                                
                            }
                        }
                    }
                }
            }


            void DrawShadowsResolutionGUI(LightView lightView)
            {
                var shadowResolutionTier = universalAdditionalLightDataKun.additionalLightsShadowResolutionTier;

                using (new EditorGUILayout.HorizontalScope())
                {
                    using (var checkScope = new EditorGUI.ChangeCheckScope())
                    {
                        Rect r = EditorGUILayout.GetControlRect(true);
                        r.width += 30;
                        shadowResolutionTier = EditorGUI.IntPopup(r, Styles.ShadowResolution, shadowResolutionTier, Styles.ShadowResolutionDefaultOptions, Styles.ShadowResolutionDefaultValues);
                        if (shadowResolutionTier == UniversalAdditionalLightDataKun.AdditionalLightsShadowResolutionTierCustom)
                        {
                            var newResolution = EditorGUILayout.IntField((int)lightView.lightKun.shadowsResolution, GUILayout.ExpandWidth(false));
                            lightView.lightKun.shadowsResolution = (LightShadowResolution)Mathf.Max(UniversalAdditionalLightDataKun.AdditionalLightsShadowMinimumResolution, Mathf.NextPowerOfTwo(newResolution));
                        }
                        else
                        {
                            if(GraphicsSettingsKun.renderPipelineAssetType == RenderPipelineAssetType.URP)
                            {
                                var urpAsset = GraphicsSettingsKun.renderPipelineAssetKun as UniversalRenderPipelineAssetKun;
                                EditorGUILayout.LabelField($"{urpAsset.GetAdditionalLightsShadowResolution(shadowResolutionTier)} ({urpAsset.name})", GUILayout.ExpandWidth(false));
                            }
                        }

                        if (checkScope.changed)
                        {
                            // universalAdditionalLightDataKun.additionalLightsShadowResolutionTier = shadowResolutionTier;
                        }
                    }

                }
            }

            void DrawLightCookieContent(LightView lightView)
            {

                m_foldOutCookie = EditorGUILayout.Foldout(m_foldOutCookie,Styles.lightCookieHeader);
                if(m_foldOutCookie == false)
                {
                    return;
                }

                using (new EditorGUI.IndentLevelScope())
                {

                    lightView.DrawCookie();
                    var isDirectionalLight = lightView.lightKun.type == LightType.Directional;
                    if (isDirectionalLight)
                    {
                        if (lightView.lightKun.cookie != null)
                        {
                            using (new EditorGUI.IndentLevelScope())
                            {
                                var lightCookieSize = new Vector2Int((int)universalAdditionalLightDataKun.lightCookieSize.x, (int)universalAdditionalLightDataKun.lightCookieSize.y);
                                EditorGUI.BeginChangeCheck();
                                lightCookieSize = EditorGUILayout.Vector2IntField(Styles.LightCookieSize, lightCookieSize);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    universalAdditionalLightDataKun.lightCookieSize = new Vector2Kun((float)lightCookieSize.x, (float)lightCookieSize.y);
                                }
                                var lightCookieOffset = new Vector2Int((int)universalAdditionalLightDataKun.lightCookieOffset.x, (int)universalAdditionalLightDataKun.lightCookieOffset.y);
                                EditorGUI.BeginChangeCheck();
                                lightCookieOffset = EditorGUILayout.Vector2IntField(Styles.LightCookieOffset, lightCookieOffset);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    universalAdditionalLightDataKun.lightCookieOffset = new Vector2Kun((float)lightCookieOffset.x, (float)lightCookieOffset.y);
                                }
                            }
                        }
                    }
                }
            }


        }
    }
}