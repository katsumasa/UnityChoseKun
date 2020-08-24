namespace  Utj.UnityChoseKun {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    [System.Serializable]
    public class CameraKun : BehaviourKun{
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

        

        public CameraClearFlags clearFlags;
        public Color backgroundColor;
        public int cullingMask;
        // Projection
        public bool  orthographic;
        public float orthographicSize;
        public float fieldOfView;
        public bool usePhysicalProperties;
        // 物理カメラの設定
        public float focalLength;
        public int sensorType;
        //public Vector2 sensorSize;
        public Vector2 lensShift;        
        public Camera.GateFitMode gateFit;
        // Clipping Plane
        public float nearClipPlane;
        public float farClipPlane;
        // Viewport Rect
        public Rect rect;
        public float depth;             
        public int  renderingPath;
        public bool useOcclusionCulling;        
        public bool allowHDR;
        public bool allowMSAA;
        public bool allowDynamicResolution;
        public int targetEye;


        public CameraKun(): this(null){}
        
        public CameraKun(Component component):base(component)
        {                                    
            var camera = component as Camera;
            if(camera != null){
                enabled = camera.enabled;
                clearFlags = camera.clearFlags;
                backgroundColor = camera.backgroundColor;
                cullingMask = camera.cullingMask;
                orthographic = camera.orthographic;
                orthographicSize = camera.orthographicSize;
                fieldOfView = camera.fieldOfView;
                usePhysicalProperties = camera.usePhysicalProperties;
                focalLength = camera.focalLength;

                sensorType = System.Array.IndexOf(k_ApertureFormatValues, new Vector2((float)System.Math.Round(camera.sensorSize.x, 3), (float)System.Math.Round(camera.sensorSize.y, 3)));
                lensShift = camera.lensShift;
                gateFit = camera.gateFit;

                nearClipPlane = camera.nearClipPlane;
                farClipPlane = camera.farClipPlane;
                rect = camera.rect;
                depth = camera.depth;
                renderingPath = (int)camera.renderingPath;
                useOcclusionCulling = camera.useOcclusionCulling;
                allowHDR = camera.allowHDR;
                allowMSAA = camera.allowMSAA;
                allowDynamicResolution = camera.allowDynamicResolution;
                targetEye = (int)camera.stereoTargetEye;                
            }
        }

        public override void WriteBack(Component component) 
        {
            base.WriteBack(component);            
            var camera = component as Camera;
            if(camera == null){
                camera.enabled = enabled;
                camera.clearFlags = clearFlags;
                camera.backgroundColor = backgroundColor;
                camera.cullingMask = cullingMask;
                camera.orthographic = orthographic;
                camera.orthographicSize = orthographicSize;
                camera.fieldOfView = fieldOfView;
                camera.usePhysicalProperties = usePhysicalProperties;
                if (usePhysicalProperties)
                {
                    camera.focalLength = focalLength;
                    camera.sensorSize = k_ApertureFormatValues[sensorType];
                    camera.lensShift = lensShift;
                    camera.gateFit = gateFit;
                }
                camera.nearClipPlane = nearClipPlane;
                camera.farClipPlane = farClipPlane;
                camera.rect = rect;
                camera.depth = depth;
                camera.renderingPath = (RenderingPath)renderingPath;
                camera.useOcclusionCulling = useOcclusionCulling;
                camera.allowHDR = allowHDR;
                camera.allowMSAA = allowMSAA;
                camera.allowDynamicResolution = allowDynamicResolution;
                camera.stereoTargetEye = (StereoTargetEyeMask)targetEye;            
            }            
        }

    }


}
