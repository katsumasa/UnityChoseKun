using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;
    using Engine.Rendering.Universal;


    namespace Editor.Rendering.Universal
    {
        public class UniversalAdditionalCameraDataView : BehaviourView
        {
            private static class Styles
            {
                public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_Camera Icon");


                public static readonly GUIContent RenderType = new GUIContent("Render Type");
                public static GUIContent projectionSettingsHeaderContent { get; } = EditorGUIUtility.TrTextContent("Projection");
                public static readonly GUIContent projectionContent = EditorGUIUtility.TrTextContent("Projection", "How the Camera renders perspective.\n\nChoose Perspective to render objects with perspective.\n\nChoose Orthographic to render objects uniformly, with no sense of perspective.");
                public static readonly GUIContent FOVAxisModeContent = EditorGUIUtility.TrTextContent("Field of View Axis", "The axis the Camera's view angle is measured along.");
                public static readonly GUIContent cameraBody = EditorGUIUtility.TrTextContent("Camera Body");
                public static readonly GUIContent sensorType = EditorGUIUtility.TrTextContent("Sensor Type", "Common sensor sizes. Choose an item to set Sensor Size, or edit Sensor Size for your custom settings.");
                public static readonly GUIContent sensorSize = EditorGUIUtility.TrTextContent("Sensor Size", "The size of the camera sensor in millimeters.");
                public static readonly GUIContent lens = EditorGUIUtility.TrTextContent("Lens");

                public static readonly GUIContent header = EditorGUIUtility.TrTextContent("Rendering", "These settings control for the specific rendering features for this camera.");
                public static readonly GUIContent rendererType = EditorGUIUtility.TrTextContent("Renderer", "The series of operations that translates code into visuals. These have different capabilities and performance characteristics.");
                public static readonly GUIContent renderPostProcessing = EditorGUIUtility.TrTextContent("Post Processing", "Enable this to make this camera render post-processing effects.");
                public static readonly GUIContent antialiasing = EditorGUIUtility.TrTextContent("Anti-aliasing", "The method the camera uses to smooth jagged edges.");
                public static readonly GUIContent renderingShadows = EditorGUIUtility.TrTextContent("Render Shadows", "Makes this camera render shadows.");
                public static readonly GUIContent requireDepthTexture = EditorGUIUtility.TrTextContent("Depth Texture", "If this is enabled, the camera builds a screen-space depth texture. Note that generating the texture incurs a performance cost.");
                public static readonly GUIContent requireOpaqueTexture = EditorGUIUtility.TrTextContent("Opaque Texture", "If this is enabled, the camera copies the rendered view so it can be accessed at a later stage in the pipeline.");

            }



            public UniversalAdditionalCameraDataKun universalAdditionalCameraDataKun
            {
                get { return behaviourKun as UniversalAdditionalCameraDataKun; }
                set { behaviourKun = value; }
            }


            public CameraKun mBaseCameraKun;

            public CameraKun baseCameraKun
            {
                get
                {
                    if (mBaseCameraKun == null)
                    {
                        for (var i = 0; i < componentKun.gameObjectKun.componentKunTypes.Length; i++)
                        {
                            if (componentKun.gameObjectKun.componentKunTypes[i] == ComponentKun.ComponentKunType.Camera)
                            {
                                mBaseCameraKun = componentKun.gameObjectKun.componentKuns[i] as CameraKun;
                                break;
                            }
                        }
                    }
                    return mBaseCameraKun;
                }
            }


            Vector2 mCustomSensorSize;

            Vector2 customSensorSize
            {
                get
                {
                    if (mCustomSensorSize == null)
                    {
                        mCustomSensorSize = new Vector2();
                    }
                    return mCustomSensorSize;
                }

                set
                {
                    mCustomSensorSize = value;
                }
            }

            public bool projectionFoldOut;
            public bool renderingFoldOut;
            public bool stackFoldOut;

            public UniversalAdditionalCameraDataView() : base()
            {
                componentIcon = Styles.ComponentIcon;
                projectionFoldOut = true;
                renderingFoldOut = true;
                CameraView.isEnableOnGUI = false;
            }


            public void DrawContents()
            {                
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.BeginChangeCheck();
                    var renderType = (UniversalAdditionalCameraDataKun.CameraRenderType)EditorGUILayout.EnumPopup(Styles.RenderType, universalAdditionalCameraDataKun.renderType);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.renderType = renderType;
                    }

                    projectionFoldOut = EditorGUILayout.Foldout(projectionFoldOut, Styles.projectionSettingsHeaderContent);
                                        
                    using (new EditorGUI.IndentLevelScope())
                    {
                        if (projectionFoldOut)
                            DrawProjection();
                    }

                    renderingFoldOut = EditorGUILayout.Foldout(renderingFoldOut, Styles.header);
                    using (new EditorGUI.IndentLevelScope())
                    {
                        if (renderingFoldOut)
                            DrawRendering();
                    }

                    stackFoldOut = EditorGUILayout.Foldout(stackFoldOut, "Stack");
                    using (new EditorGUI.IndentLevelScope())
                    {
                        if (stackFoldOut)
                        {
                            DrawStack();
                        }
                    }
                }
            }


            public override bool OnGUI()
            {
                if (base.OnGUI())
                {
                    
                }
                return true;
            }


            void DrawProjection()
            {
                CameraView.ProjectionType projectionType = universalAdditionalCameraDataKun.orthographic ? CameraView.ProjectionType.Orthographic : CameraView.ProjectionType.Perspective;
                projectionType = (CameraView.ProjectionType)EditorGUILayout.EnumPopup(CameraView.Styles.projection, projectionType);
                universalAdditionalCameraDataKun.orthographic = (projectionType == CameraView.ProjectionType.Orthographic);

                EditorGUI.BeginChangeCheck();
                var fieldOfView = EditorGUILayout.Slider(CameraView.Styles.fieldOfView, universalAdditionalCameraDataKun.fieldOfView, 0.00001f, 179f);
                if(EditorGUI.EndChangeCheck())
                {
                    universalAdditionalCameraDataKun.fieldOfView = fieldOfView;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Clipping Planes");
                EditorGUILayout.BeginVertical();
                
                EditorGUI.BeginChangeCheck();
                var nearClipPlane = EditorGUILayout.FloatField("Near", universalAdditionalCameraDataKun.nearClipPlane);
                if (EditorGUI.EndChangeCheck())
                {
                    universalAdditionalCameraDataKun.nearClipPlane = nearClipPlane;
                }

                EditorGUI.BeginChangeCheck();
                var farClipPlane = EditorGUILayout.FloatField("Fear", universalAdditionalCameraDataKun.farClipPlane);
                if (EditorGUI.EndChangeCheck())
                {
                    universalAdditionalCameraDataKun.farClipPlane = farClipPlane;
                }
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

                if (projectionType == CameraView.ProjectionType.Perspective)
                {
                    EditorGUI.BeginChangeCheck();
                    var usePhysicalProperties = EditorGUILayout.Toggle(CameraView.Styles.physicalCamera, universalAdditionalCameraDataKun.usePhysicalProperties);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.usePhysicalProperties = usePhysicalProperties;
                    }

                    if (universalAdditionalCameraDataKun.usePhysicalProperties)
                    {

                        EditorGUILayout.LabelField(Styles.cameraBody);

                        using (new EditorGUI.IndentLevelScope())
                        {
                            EditorGUI.BeginChangeCheck();
                            var oldFilmGateIndex = Array.IndexOf(CameraView.k_ApertureFormatValues, new Vector2((float)Math.Round(universalAdditionalCameraDataKun.sensorSize.x, 3), (float)Math.Round(universalAdditionalCameraDataKun.sensorSize.y, 3)));
                            oldFilmGateIndex = (oldFilmGateIndex == -1) ? CameraView.k_ApertureFormatNames.Length - 1 : oldFilmGateIndex;


                            var newFilmGateIndex = EditorGUILayout.Popup(Styles.sensorType, oldFilmGateIndex, CameraView.k_ApertureFormatNames);
                            if (EditorGUI.EndChangeCheck())
                            {
                                if (newFilmGateIndex == CameraView.k_ApertureFormatNames.Length - 1)
                                {
                                    universalAdditionalCameraDataKun.sensorSize = new Vector2Kun(customSensorSize);
                                }
                                else
                                {
                                    universalAdditionalCameraDataKun.sensorSize = new Vector2Kun(CameraView.k_ApertureFormatValues[newFilmGateIndex]);
                                }
                            }

                            var sensorSize = universalAdditionalCameraDataKun.sensorSize.GetVector2();

                            EditorGUI.BeginChangeCheck();
                            sensorSize = EditorGUILayout.Vector2Field(Styles.sensorSize, sensorSize);
                            if (EditorGUI.EndChangeCheck())
                            {
                                customSensorSize = sensorSize;
                                universalAdditionalCameraDataKun.sensorSize = new Vector2Kun(sensorSize);
                            }

                            EditorGUI.BeginChangeCheck();
                            var gateFit = (Camera.GateFitMode)EditorGUILayout.EnumPopup(CameraView.Styles.gateFit, universalAdditionalCameraDataKun.gateFit);
                            if (EditorGUI.EndChangeCheck())
                            {
                                universalAdditionalCameraDataKun.gateFit = gateFit;
                            }
                        }

                        EditorGUILayout.LabelField(Styles.lens);
                        using (new EditorGUI.IndentLevelScope())
                        {
                            EditorGUI.BeginChangeCheck();
                            var focalLength = EditorGUILayout.FloatField(CameraView.Styles.focalLength, universalAdditionalCameraDataKun.focalLength);
                            if (EditorGUI.EndChangeCheck())
                            {
                                universalAdditionalCameraDataKun.focalLength = focalLength;
                            }
                            
                            var v2 = universalAdditionalCameraDataKun.lensShift.GetVector2();
                            EditorGUI.BeginChangeCheck();
                            v2 = EditorGUILayout.Vector2Field(CameraView.Styles.lensShift, v2);
                            if (EditorGUI.EndChangeCheck())
                            {
                                universalAdditionalCameraDataKun.lensShift = new Vector2Kun(v2);
                            }
                        }
                    }
                }
            }


            void DrawRendering()
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(Styles.rendererType);
                    EditorGUI.BeginChangeCheck();
                    var rendererIndex = EditorGUILayout.IntPopup(universalAdditionalCameraDataKun.rendererIndex, universalAdditionalCameraDataKun.renderers, universalAdditionalCameraDataKun.rendererIndexList);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.rendererIndex = rendererIndex;
                    }
                }
                EditorGUILayout.EndHorizontal();

                {
                    EditorGUI.BeginChangeCheck();
                    var renderPostProcessing = EditorGUILayout.Toggle(Styles.renderPostProcessing, universalAdditionalCameraDataKun.renderPostProcessing);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.renderPostProcessing = renderPostProcessing;
                    }
                }

                {
                    EditorGUI.BeginChangeCheck();
                    var antialiasingMode = (UniversalAdditionalCameraDataKun.AntialiasingMode)EditorGUILayout.EnumPopup(Styles.antialiasing, universalAdditionalCameraDataKun.antialiasingMode);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.antialiasingMode = antialiasingMode;
                    }
                }

                {
                    EditorGUI.BeginChangeCheck();
                    var stopNaN = EditorGUILayout.Toggle("Stop Nans", universalAdditionalCameraDataKun.stopNaN);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.stopNaN = stopNaN;
                    }
                }

                {
                    EditorGUI.BeginChangeCheck();
                    var dithering = EditorGUILayout.Toggle("Dithering", universalAdditionalCameraDataKun.dithering);
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.dithering = dithering;
                    }
                }

                {
                    EditorGUI.BeginChangeCheck();
                    var renderShadows = EditorGUILayout.Toggle(Styles.renderingShadows, universalAdditionalCameraDataKun.renderShadows);
                    if (EditorGUI.EndChangeCheck())
                        universalAdditionalCameraDataKun.renderShadows = renderShadows;
                }

                {
                    EditorGUI.BeginChangeCheck();
                    var depth = (int)EditorGUILayout.IntSlider("Priority", (int)universalAdditionalCameraDataKun.depth, -100, 100);
                    if (EditorGUI.EndChangeCheck())
                        universalAdditionalCameraDataKun.depth = depth;
                }


                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(Styles.requireOpaqueTexture);
                    EditorGUI.BeginChangeCheck();
                    var requiresColorOption = (UniversalAdditionalCameraDataKun.CameraOverrideOption)EditorGUILayout.EnumPopup(universalAdditionalCameraDataKun.requiresColorOption);
                    if (EditorGUI.EndChangeCheck())
                        universalAdditionalCameraDataKun.requiresColorOption = requiresColorOption;
                }   
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(Styles.requireDepthTexture);
                    EditorGUI.BeginChangeCheck();
                    var requiresDepthTextureOption = (UniversalAdditionalCameraDataKun.CameraOverrideOption)EditorGUILayout.EnumPopup(universalAdditionalCameraDataKun.requiresDepthTextureOption);
                    if (EditorGUI.EndChangeCheck())
                        universalAdditionalCameraDataKun.requiresDepthTextureOption = requiresDepthTextureOption;
                }
                EditorGUILayout.EndHorizontal();

                {
                    EditorGUI.BeginChangeCheck();
                    var cullingMask = LayerMaskKun.LayerMaskField(new GUIContent("Culling Mask"), universalAdditionalCameraDataKun.cullingMask).value;
                    if (EditorGUI.EndChangeCheck())
                    {
                        universalAdditionalCameraDataKun.cullingMask = cullingMask;
                    }
                }

                {
                    EditorGUI.BeginChangeCheck();
                    var useOcclusionCulling = EditorGUILayout.Toggle("Occlusion Culling", universalAdditionalCameraDataKun.useOcclusionCulling);
                    if (EditorGUI.EndChangeCheck())
                        universalAdditionalCameraDataKun.useOcclusionCulling = useOcclusionCulling;
                }
            }


            void DrawStack()
            {
                EditorGUILayout.LabelField("Cameras");
                for (var i = 0; i < universalAdditionalCameraDataKun.cameraStack.Length; i++)
                {
                    EditorGUILayout.LabelField(universalAdditionalCameraDataKun.cameraStack[i].name);
                }
            }


            
        }
    }
}