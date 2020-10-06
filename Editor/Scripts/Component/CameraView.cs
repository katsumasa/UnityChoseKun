namespace Utj.UnityChoseKun{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using UnityEditor;

    public class CameraView : ComponentView
    {
        private static class Styles
        {
            public static GUIContent cameraFoldout = new GUIContent("", (Texture2D)EditorGUIUtility.Load("d_Camera Icon"));
            
            public static GUIContent iconRemove = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove command buffer");
            public static GUIContent clearFlags = EditorGUIUtility.TrTextContent("Clear Flags", "What to display in empty areas of this Camera's view.\n\nChoose Skybox to display a skybox in empty areas, defaulting to a background color if no skybox is found.\n\nChoose Solid Color to display a background color in empty areas.\n\nChoose Depth Only to display nothing in empty areas.\n\nChoose Don't Clear to display whatever was displayed in the previous frame in empty areas.");
            public static GUIContent background = EditorGUIUtility.TrTextContent("Background", "The Camera clears the screen to this color before rendering.");
            public static GUIContent projection = EditorGUIUtility.TrTextContent("Projection", "How the Camera renders perspective.\n\nChoose Perspective to render objects with perspective.\n\nChoose Orthographic to render objects uniformly, with no sense of perspective.");
            public static GUIContent size = EditorGUIUtility.TrTextContent("Size", "The vertical size of the camera view.");
            public static GUIContent fieldOfView = EditorGUIUtility.TrTextContent("Field of View", "The camera's view angle measured in degrees along the selected axis.");
            public static GUIContent viewportRect = EditorGUIUtility.TrTextContent("Viewport Rect", "Four values that indicate where on the screen this camera view will be drawn. Measured in Viewport Coordinates (values 0-1).");
            public static GUIContent sensorSize = EditorGUIUtility.TrTextContent("Sensor Size", "The size of the camera sensor in millimeters.");
            public static GUIContent lensShift = EditorGUIUtility.TrTextContent("Lens Shift", "Offset from the camera sensor. Use these properties to simulate a shift lens. Measured as a multiple of the sensor size.");
            public static GUIContent physicalCamera = EditorGUIUtility.TrTextContent("Physical Camera", "Enables Physical camera mode. When checked, the field of view is calculated from properties for simulating physical attributes (focal length, sensor size, and lens shift)");
            public static GUIContent cameraType = EditorGUIUtility.TrTextContent("Sensor Type", "Common sensor sizes. Choose an item to set Sensor Size, or edit Sensor Size for your custom settings.");
            public static GUIContent renderingPath = EditorGUIUtility.TrTextContent("Rendering Path", "Choose a rendering method for this camera.\n\nUse Graphics Settings to use the rendering path specified in Player settings.\n\nUse Forward to render all objects with one pass per material.\n\nUse Deferred to draw all objects once without lighting and then draw the lighting of all objects at the end of the render queue.\n\nUse Legacy Vertex Lit to render all lights in a single pass, calculated in vertices.\n\nLegacy Deferred has been deprecated.");
            public static GUIContent focalLength = EditorGUIUtility.TrTextContent("Focal Length", "The simulated distance between the lens and the sensor of the physical camera. Larger values give a narrower field of view.");
            public static GUIContent allowOcclusionCulling = EditorGUIUtility.TrTextContent("Occlusion Culling", "Occlusion Culling means that objects that are hidden behind other objects are not rendered, for example if they are behind walls.");
            public static GUIContent allowHDR = EditorGUIUtility.TrTextContent("HDR", "High Dynamic Range gives you a wider range of light intensities, so your lighting looks more realistic. With it, you can still see details and experience less saturation even with bright light.");
            public static GUIContent allowMSAA = EditorGUIUtility.TrTextContent("MSAA", "Use Multi Sample Anti-aliasing to reduce aliasing.");
            public static GUIContent gateFit = EditorGUIUtility.TrTextContent("Gate Fit", "Determines how the rendered area (resolution gate) fits into the sensor area (film gate).");
            public static GUIContent allowDynamicResolution = EditorGUIUtility.TrTextContent("Allow Dynamic Resolution", "Scales render textures to support dynamic resolution if the target platform/graphics API supports it.");
            public static GUIContent FOVAxisMode = EditorGUIUtility.TrTextContent("FOV Axis", "Field of view axis.");           
            public static GUIStyle invisibleButton = "InvisibleButton";
            public static GUIContent[] displayedOptions = new[] { new GUIContent("Off"), new GUIContent("Use Graphics Settings") };
            public static int[] optionValues = new[] { 0, 1 };
        }


        public sealed class Settings
        {
            enum ProjectionType { Orthographic, Perspective }

            
            CameraKun m_cameraKun;            
            public CameraKun cameraKun {
                get {
                    if(m_cameraKun == null){
                        m_cameraKun = new CameraKun(null);                     
                    }
                    return m_cameraKun;
                }
                set {
                    m_cameraKun = value;
                }
            }                        

            public static IEnumerable<string> ApertureFormatNames => k_ApertureFormatNames;

            private static readonly string[] k_ApertureFormatNames =
            {
                "8mm",
                "Super 8mm",
                "16mm",
                "Super 16mm",
                "35mm 2-perf",
                "35mm Academy",
                "Super-35",
                "65mm ALEXA",
                "70mm",
                "70mm IMAX",
                "Custom"
            };

            public static IEnumerable<Vector2> ApertureFormatValues => k_ApertureFormatValues;

            private static readonly Vector2[] k_ApertureFormatValues =
            {
                new Vector2(4.8f, 3.5f) , // 8mm
                new Vector2(5.79f, 4.01f) , // Super 8mm
                new Vector2(10.26f, 7.49f) , // 16mm
                new Vector2(12.52f, 7.41f) , // Super 16mm
                new Vector2(21.95f, 9.35f) , // 35mm 2-perf
                new Vector2(21.0f, 15.2f) , // 35mm academy
                new Vector2(24.89f, 18.66f) , // Super-35
                new Vector2(54.12f, 25.59f) , // 65mm ALEXA
                new Vector2(70.0f, 51.0f) , // 70mm
                new Vector2(70.41f, 52.63f), // 70mm IMAX
            };

            // Manually entered rendering path names/values, since we want to show them
            // in different order than they appear in the enum.
            private static readonly GUIContent[] kCameraRenderPaths =
            {
                EditorGUIUtility.TrTextContent("Use Graphics Settings"),
                EditorGUIUtility.TrTextContent("Forward"),
                EditorGUIUtility.TrTextContent("Deferred"),
                EditorGUIUtility.TrTextContent("Legacy Vertex Lit"),
                EditorGUIUtility.TrTextContent("Legacy Deferred (light prepass)")
            };
            private static readonly int[] kCameraRenderPathValues =
            {
                (int)RenderingPath.UsePlayerSettings,
                (int)RenderingPath.Forward,
                (int)RenderingPath.DeferredShading,
                (int)RenderingPath.VertexLit,
                (int)RenderingPath.DeferredLighting
            };


            private static readonly GUIContent[] kCameraHDRNames =
            {
                   EditorGUIUtility.TrTextContent("Off"),
                   EditorGUIUtility.TrTextContent("Use Graphics Settings"),
            };

            private static readonly int[] kCameraHDRValues =
            {
                0,
                1
            };


            private static readonly GUIContent[] kTargetEyes =
            {
                EditorGUIUtility.TrTextContent("Both"),
                EditorGUIUtility.TrTextContent("Left"),
                EditorGUIUtility.TrTextContent("Right"),
                EditorGUIUtility.TrTextContent("None (Main Display)"),
            };
            private static readonly int[] kTargetEyeValues = { (int)StereoTargetEyeMask.Both, (int)StereoTargetEyeMask.Left, (int)StereoTargetEyeMask.Right, (int)StereoTargetEyeMask.None };
            


            public bool DrawCamera(bool foldout)
            {                
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                EditorGUILayout.BeginHorizontal();
                foldout =  EditorGUILayout.Foldout(foldout,Styles.cameraFoldout);
                
                EditorGUI.BeginChangeCheck();
                cameraKun.enabled = EditorGUILayout.ToggleLeft("Camera",cameraKun.enabled);
                if(EditorGUI.EndChangeCheck()){
                    cameraKun.dirty = true;
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));                
                return foldout;
            }
        

            public void DrawClearFlags()
            {
                cameraKun.clearFlags = (CameraClearFlags)EditorGUILayout.EnumPopup(Styles.clearFlags,cameraKun.clearFlags);
            }

            public void DrawBackgroundColor()
            {
                cameraKun.backgroundColor = EditorGUILayout.ColorField(Styles.background, cameraKun.backgroundColor);
            }

            public void DrawCullingMask()
            {
                LayerMask layerMask = cameraKun.cullingMask;
                cameraKun.cullingMask = LayerMaskField("Culling Mask", layerMask).value;                
            }

            public void DrawProjection()
            {
                ProjectionType projectionType = cameraKun.orthographic ? ProjectionType.Orthographic : ProjectionType.Perspective;
                projectionType = (ProjectionType)EditorGUILayout.EnumPopup(Styles.projection, projectionType);
                cameraKun.orthographic =(projectionType == ProjectionType.Orthographic);
                cameraKun.fieldOfView = EditorGUILayout.Slider(Styles.fieldOfView, cameraKun.fieldOfView, 0.00001f, 179f);               
                cameraKun.usePhysicalProperties = EditorGUILayout.Toggle(Styles.physicalCamera,cameraKun.usePhysicalProperties);
                if (cameraKun.usePhysicalProperties)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        cameraKun.focalLength = EditorGUILayout.FloatField(Styles.focalLength, cameraKun.focalLength);
                        cameraKun.sensorType = EditorGUILayout.Popup(Styles.cameraType, cameraKun.sensorType, k_ApertureFormatNames);
                        
                        cameraKun.lensShift = EditorGUILayout.Vector2Field(Styles.lensShift, cameraKun.lensShift);
                        cameraKun.gateFit = (Camera.GateFitMode)EditorGUILayout.EnumPopup(Styles.gateFit, cameraKun.gateFit);
;                    }
                }                               
            }


            public void DrawClippingPlanes()
            {
                EditorGUILayout.LabelField("Clipping Planes");
                cameraKun.nearClipPlane = EditorGUILayout.FloatField("Near", cameraKun.nearClipPlane);
                cameraKun.farClipPlane = EditorGUILayout.FloatField("Fear", cameraKun.farClipPlane);                
            }


            public void DrawNormalizedViewPort()
            {
                cameraKun.rect = EditorGUILayout.RectField("Viewport Rect", cameraKun.rect);
            }

            public void DrawDepth()
            {
                cameraKun.depth = EditorGUILayout.FloatField("Depth",cameraKun.depth);
            }

            public void DrawRenderingPath()
            {
                cameraKun.renderingPath = EditorGUILayout.IntPopup(Styles.renderingPath, cameraKun.renderingPath,kCameraRenderPaths, kCameraRenderPathValues);
            }


            public void DrawOcclusionCulling()
            {
                cameraKun.useOcclusionCulling = EditorGUILayout.Toggle(Styles.allowOcclusionCulling,cameraKun.useOcclusionCulling);                
            }

            public void DrawHDR()
            {                
                int value = cameraKun.allowHDR ? 1 : 0;
                value = EditorGUILayout.IntPopup(Styles.allowHDR, value, kCameraHDRNames, kCameraHDRValues);
                cameraKun.allowHDR = value == 1 ? true : false;
            }


            public void DrawMSAA()
            {
                int value = cameraKun.allowMSAA ? 1 : 0;
                value = EditorGUILayout.IntPopup(Styles.allowMSAA, value, kCameraHDRNames, kCameraHDRValues);
                cameraKun.allowMSAA = value == 1 ? true : false;
            }

            public void DrawDynamicResolution()
            {
                cameraKun.allowDynamicResolution = EditorGUILayout.Toggle(Styles.allowDynamicResolution, cameraKun.allowDynamicResolution);
            }


            public void DrawTargetEye()
            {
                cameraKun.targetEye = EditorGUILayout.IntPopup(new GUIContent("Target Eye"), cameraKun.targetEye,kTargetEyes, kTargetEyeValues);                
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

        private Settings m_Settings;
        protected Settings settings
        {
            get
            {
                if (m_Settings == null)
                    m_Settings = new Settings();                    
                return m_Settings;
            }
        }

        private bool cameraFoldout {
            get; set;
        }
#if false
        public override void SetJson(string json)
        {
            settings.cameraKun = JsonUtility.FromJson<CameraKun>(json);
        }

        public override string GetJson()
        {
            return JsonUtility.ToJson(settings.cameraKun);
        }
#else
        public override void SetBytes(byte[] bytes)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(bytes);

            settings.cameraKun= (CameraKun)bf.Deserialize(ms);
            ms.Close();
        }

        public override byte[] GetBytes()
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();

            bf.Serialize(ms, settings.cameraKun);
            var bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }

#endif
        public override void OnGUI()
        {                    
            cameraFoldout = settings.DrawCamera(cameraFoldout);
            if(cameraFoldout){
                EditorGUI.BeginChangeCheck();
                using (new EditorGUI.IndentLevelScope())
                {
                    settings.DrawClearFlags();
                    settings.DrawBackgroundColor();
                    settings.DrawCullingMask();
                    settings.DrawProjection();
                    settings.DrawClippingPlanes();
                    settings.DrawNormalizedViewPort();
                    settings.DrawDepth();
                    settings.DrawRenderingPath();
                    settings.DrawOcclusionCulling();
                    settings.DrawHDR();
                    settings.DrawMSAA();
                    settings.DrawDynamicResolution();
                    settings.DrawTargetEye();
                }
                if(EditorGUI.EndChangeCheck()){
                    settings.cameraKun.dirty = true;
                }
            }
        }

    }


    
}
