namespace  Utj.UnityChoseKun {
    using System;
    using System.IO;
    using System.Collections.Generic;
    using UnityEngine;


    // <summary>
    // CameraをPlayer/Editorの両方でシリアライズ/デシリアライズする為のClass
    // </summary>
    [System.Serializable]
    public class CameraKun : BehaviourKun {
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

        // メンバー変数の定義        
        [SerializeField] public CameraClearFlags clearFlags;
        [SerializeField] private ColorKun m_backgroundColor;
        [SerializeField] public int cullingMask;
        // Projection
        [SerializeField] public bool orthographic;
        [SerializeField] public float orthographicSize;
        [SerializeField] public float fieldOfView;
        [SerializeField] public bool usePhysicalProperties;
        // 物理カメラの設定
        [SerializeField] public float focalLength;
        [SerializeField] public int sensorType;
        [SerializeField] private Vector2Kun m_lensShift;
        [SerializeField] public Camera.GateFitMode gateFit;
        // Clipping Plane
        [SerializeField] public float nearClipPlane;
        [SerializeField] public float farClipPlane;
        // Viewport Rect
        [SerializeField] RectKun m_rect;
        [SerializeField] public float depth;
        [SerializeField] public int renderingPath;
        [SerializeField] public bool useOcclusionCulling;
        [SerializeField] public bool allowHDR;
        [SerializeField] public bool allowMSAA;
        [SerializeField] public bool allowDynamicResolution;
        [SerializeField] public int targetEye;



        public Color backgroundColor
        {
            get { return m_backgroundColor.GetColor(); }
            set { m_backgroundColor = new ColorKun(value); }
        }

        public Vector2 lensShift
        {
            get { return m_lensShift.GetVector2(); }
            set { m_lensShift = new Vector2Kun(value); }
        }

        public Rect rect {
            get { return m_rect.GetRect(); }
            set { m_rect = new RectKun(value); }
        }


        static List<CameraKun> m_AllCameras;
        

        public static List<CameraKun> allCameraList
        {
            set
            {
                m_AllCameras = value;
            }
        }


        public static CameraKun[] allCameras
        {
            get
            {
                if(m_AllCameras == null)
                {
                    return null;
                } 
                else
                {
                    return m_AllCameras.ToArray();
                }
            }            
        }

    



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraKun(): this(null){}
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="component">Cameraオブジェクト</param>
        public CameraKun(Component component):base(component)
        {                                    
            componentKunType = ComponentKunType.Camera;
            m_backgroundColor = new ColorKun();
            m_lensShift = new Vector2Kun();
            m_rect = new RectKun();

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
                        

        public override bool WriteBack(Component component) 
        {
            //Debug.Log("Camera WriteBack");
            if(base.WriteBack(component)){                
                var camera = component as Camera;
                if(camera){
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
                return true;
            }
            return false;
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((Int32)clearFlags);                        
            binaryWriter.Write(cullingMask);
            binaryWriter.Write(orthographic);
            binaryWriter.Write(orthographicSize);
            binaryWriter.Write(fieldOfView);
            binaryWriter.Write(usePhysicalProperties);
            binaryWriter.Write(focalLength);
            binaryWriter.Write(sensorType);                        
            binaryWriter.Write((Int32)gateFit);
            binaryWriter.Write(nearClipPlane);
            binaryWriter.Write(farClipPlane);                                    
            binaryWriter.Write(depth);
            binaryWriter.Write(renderingPath);
            binaryWriter.Write(useOcclusionCulling);
            binaryWriter.Write(allowHDR);
            binaryWriter.Write(allowMSAA);
            binaryWriter.Write(allowDynamicResolution);
            binaryWriter.Write(targetEye);
            SerializerKun.Serialize<ColorKun>(binaryWriter, m_backgroundColor);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_lensShift);
            SerializerKun.Serialize<RectKun>(binaryWriter, m_rect);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {            
            base.Deserialize(binaryReader);
            clearFlags = (CameraClearFlags)binaryReader.ReadInt32();                        
            cullingMask = binaryReader.ReadInt32();
            orthographic = binaryReader.ReadBoolean();
            orthographicSize = binaryReader.ReadSingle();
            fieldOfView = binaryReader.ReadSingle();
            usePhysicalProperties = binaryReader.ReadBoolean();
            focalLength = binaryReader.ReadSingle();
            sensorType = binaryReader.ReadInt32();                                    
            gateFit = (Camera.GateFitMode)binaryReader.ReadInt32();
            nearClipPlane = binaryReader.ReadSingle();
            farClipPlane = binaryReader.ReadSingle();                                    
            depth = binaryReader.ReadSingle();
            renderingPath = binaryReader.ReadInt32();
            useOcclusionCulling = binaryReader.ReadBoolean();
            allowHDR = binaryReader.ReadBoolean();
            allowMSAA = binaryReader.ReadBoolean();
            allowDynamicResolution = binaryReader.ReadBoolean();
            targetEye = binaryReader.ReadInt32();
            m_backgroundColor = SerializerKun.DesirializeObject<ColorKun>(binaryReader);
            m_lensShift = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
            m_rect = SerializerKun.DesirializeObject<RectKun>(binaryReader);
        }


        public override bool Equals(object obj)
        {
            var other = obj as CameraKun;
            if(other == null)
            {
                return false;
            }
            if(clearFlags != other.clearFlags)
            {
                return false;
            }
            if (m_backgroundColor.Equals(other.m_backgroundColor) == false)
            {
                return false;
            }
            if (cullingMask.Equals(other.cullingMask) == false)
            {
                return false;
            }
            if (orthographic.Equals(other.orthographic) == false)
            {
                return false;
            }
            if (orthographicSize.Equals(other.orthographicSize) == false)
            {
                return false;
            }
            if (fieldOfView.Equals(other.fieldOfView) == false)
            {
                return false;
            }
            if (usePhysicalProperties.Equals(other.usePhysicalProperties) == false)
            {
                return false;
            }
            if (focalLength.Equals(other.focalLength) == false)
            {
                return false;
            }
            if (sensorType.Equals(other.sensorType) == false)
            {
                return false;
            }
            if (m_lensShift.Equals(other.m_lensShift) == false)
            {
                return false;
            }
            if (gateFit.Equals(other.gateFit) == false)
            {
                return false;
            }
            if (nearClipPlane.Equals(other.nearClipPlane) == false)
            {
                return false;
            }
            if (farClipPlane.Equals(other.farClipPlane) == false)
            {
                return false;
            }
            if (m_rect.Equals(other.m_rect) == false)
            {
                return false;
            }
            if (depth.Equals(other.depth) == false)
            {
                return false;
            }
            if (renderingPath.Equals(other.renderingPath) == false)
            {
                return false;
            }
            if (useOcclusionCulling.Equals(other.useOcclusionCulling) == false)
            {
                return false;
            }
            if (allowHDR.Equals(other.allowHDR) == false)
            {
                return false;
            }
            if (allowMSAA.Equals(other.allowMSAA) == false)
            {
                return false;
            }
            if (allowDynamicResolution.Equals(other.allowDynamicResolution) == false)
            {
                return false;
            }
            if (targetEye.Equals(other.targetEye) == false)
            {
                return false;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }



}
