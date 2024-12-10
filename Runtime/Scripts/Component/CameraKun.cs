using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


namespace  Utj.UnityChoseKun.Engine
{
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
        CameraClearFlags m_clearFlags;
        ColorKun m_backgroundColor;
        int m_cullingMask;
        
        // Projection
        bool m_orthographic;
        float m_orthographicSize;
        float m_fieldOfView;
        bool m_usePhysicalProperties;
        
        // 物理カメラの設定
        float m_focalLength;        
        Vector2Kun m_sensorSize;
        Vector2Kun m_lensShift;
        Camera.GateFitMode m_gateFit;
        
        // Clipping Plane
        float m_nearClipPlane;
        float m_farClipPlane;
        
        // Viewport Rect
        RectKun m_rect;
        float m_depth;
        int m_renderingPath;
        bool m_useOcclusionCulling;
        bool m_allowHDR;
        bool m_allowMSAA;
        bool m_allowDynamicResolution;
        int m_targetEye;

        public CameraClearFlags clearFlags
        {
            get { return m_clearFlags; }
            set { m_clearFlags = value; dirty = true; }
        }

        public Color backgroundColor
        {
            get { return m_backgroundColor.GetColor(); }
            set { m_backgroundColor = new ColorKun(value); dirty = true; }
        }

        public int cullingMask
        {
            get { return m_cullingMask; }
            set { m_cullingMask = value; dirty = true; }
        }

        public bool orthographic
        {
            get { return m_orthographic; }
            set { m_orthographic = value; dirty = true; }
        }

        public float orthographicSize
        {
            get { return m_orthographicSize; }
            set { m_orthographicSize = value; dirty = true; }
        }

        public float fieldOfView
        {
            get { return m_fieldOfView; }
            set { m_fieldOfView = value; dirty = true; }
        }

        public bool usePhysicalProperties
        {
            get { return m_usePhysicalProperties; }
            set { m_usePhysicalProperties = value; dirty = true; }
        }

        public float focalLength
        {
            get { return m_focalLength; }
            set { m_focalLength = value; dirty = true; }
        }
        public Vector2 sensorSize
        {
            get { return m_sensorSize.GetVector2(); }
            set { m_sensorSize = new Vector2Kun(value); dirty = true; }
        }

        public Vector2 lensShift
        {
            get { return m_lensShift.GetVector2(); }
            set { m_lensShift = new Vector2Kun(value); dirty = true; }
        }

        public Camera.GateFitMode gateFit
        {
            get { return m_gateFit; }
            set { m_gateFit = value; dirty = true; }
        }

        public float nearClipPlane
        {
            get { return m_nearClipPlane; }
            set { m_nearClipPlane = value; dirty = true; }
        }

        public float farClipPlane
        {
            get { return m_farClipPlane; }
            set { m_farClipPlane = value; dirty = true; }
        }


        public Rect rect {
            get { return m_rect.GetRect(); }
            set { m_rect = new RectKun(value); }
        }


        public float depth
        {
            get { return m_depth; }
            set { m_depth = value; dirty = true; }
        }
        
        public int renderingPath
        {
            get { return m_renderingPath; }
            set { m_renderingPath = value; }
        }
        
        public bool useOcclusionCulling
        {
            get { return m_useOcclusionCulling; }
            set { m_useOcclusionCulling = value; dirty = true; }
        }
        
        public bool allowHDR
        {
            get { return m_allowHDR; }
            set { m_allowHDR = value; dirty = true; }
        }
        
        public bool allowMSAA
        {
            get { return m_allowMSAA; }
            set { m_allowMSAA = value; dirty = true; }
        }
        
        public bool allowDynamicResolution
        {
            get
            {
                return m_allowDynamicResolution;
            }
            set
            {
                m_allowDynamicResolution = value; dirty = true;
            }
        }

        public int targetEye
        {
            get
            {
                return m_targetEye;
            }

            set
            {
                m_targetEye = value;
                dirty = true;
            }
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
                m_clearFlags = camera.clearFlags;
                m_backgroundColor = new ColorKun(camera.backgroundColor);
                m_cullingMask = camera.cullingMask;
                m_orthographic = camera.orthographic;
                m_orthographicSize = camera.orthographicSize;
                m_fieldOfView = camera.fieldOfView;                
                m_usePhysicalProperties = camera.usePhysicalProperties;
                
                m_focalLength = camera.focalLength;                
                m_sensorSize = new Vector2Kun(camera.sensorSize);
                m_lensShift = new Vector2Kun(camera.lensShift);
                m_gateFit = camera.gateFit;

                m_nearClipPlane = camera.nearClipPlane;
                m_farClipPlane = camera.farClipPlane;
                m_rect = new RectKun(camera.rect);
                m_depth = camera.depth;
                m_renderingPath = (int)camera.renderingPath;
                m_useOcclusionCulling = camera.useOcclusionCulling;
                m_allowHDR = camera.allowHDR;
                m_allowMSAA = camera.allowMSAA;
                m_allowDynamicResolution = camera.allowDynamicResolution;
                m_targetEye = (int)camera.stereoTargetEye;                
            }         
        }
                        

        public override bool WriteBack(Component component) 
        {
            //Debug.Log("Camera WriteBack");
            if(base.WriteBack(component)){                
                var camera = component as Camera;
                if(camera){
                    camera.enabled = enabled;
                    camera.clearFlags = m_clearFlags;
                    camera.backgroundColor = m_backgroundColor.GetColor();
                    camera.cullingMask = m_cullingMask;
                    camera.orthographic = m_orthographic;
                    camera.orthographicSize = m_orthographicSize;
                    camera.fieldOfView = m_fieldOfView;                                    
                    camera.usePhysicalProperties = m_usePhysicalProperties;
                    if (m_usePhysicalProperties)
                    {
                        camera.focalLength = m_focalLength;
                        camera.sensorSize = m_sensorSize.GetVector2();
                        camera.lensShift = m_lensShift.GetVector2();
                        camera.gateFit = m_gateFit;
                    }
                    camera.nearClipPlane = m_nearClipPlane;
                    camera.farClipPlane = m_farClipPlane;
                    camera.rect = m_rect.GetRect();
                    camera.depth = m_depth;
                    camera.renderingPath = (RenderingPath)m_renderingPath;
                    camera.useOcclusionCulling = m_useOcclusionCulling;
                    camera.allowHDR = m_allowHDR;
                    camera.allowMSAA = m_allowMSAA;
                    camera.allowDynamicResolution = m_allowDynamicResolution;
                    camera.stereoTargetEye = (StereoTargetEyeMask)m_targetEye;                
                }
                return true;
            }
            return false;
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((Int32)m_clearFlags);                        
            binaryWriter.Write(m_cullingMask);
            binaryWriter.Write(m_orthographic);
            binaryWriter.Write(m_orthographicSize);
            binaryWriter.Write(m_fieldOfView);
            binaryWriter.Write(m_usePhysicalProperties);
            binaryWriter.Write(m_focalLength);            
            binaryWriter.Write((Int32)m_gateFit);
            binaryWriter.Write(m_nearClipPlane);
            binaryWriter.Write(m_farClipPlane);                                    
            binaryWriter.Write(m_depth);
            binaryWriter.Write(m_renderingPath);
            binaryWriter.Write(m_useOcclusionCulling);
            binaryWriter.Write(m_allowHDR);
            binaryWriter.Write(m_allowMSAA);
            binaryWriter.Write(m_allowDynamicResolution);
            binaryWriter.Write(m_targetEye);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_sensorSize);
            SerializerKun.Serialize<ColorKun>(binaryWriter, m_backgroundColor);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_lensShift);
            SerializerKun.Serialize<RectKun>(binaryWriter, m_rect);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {            
            base.Deserialize(binaryReader);
            m_clearFlags = (CameraClearFlags)binaryReader.ReadInt32();                        
            m_cullingMask = binaryReader.ReadInt32();
            m_orthographic = binaryReader.ReadBoolean();
            m_orthographicSize = binaryReader.ReadSingle();
            m_fieldOfView = binaryReader.ReadSingle();
            m_usePhysicalProperties = binaryReader.ReadBoolean();
            m_focalLength = binaryReader.ReadSingle();            
            m_gateFit = (Camera.GateFitMode)binaryReader.ReadInt32();
            m_nearClipPlane = binaryReader.ReadSingle();
            m_farClipPlane = binaryReader.ReadSingle();                                    
            m_depth = binaryReader.ReadSingle();
            m_renderingPath = binaryReader.ReadInt32();
            m_useOcclusionCulling = binaryReader.ReadBoolean();
            m_allowHDR = binaryReader.ReadBoolean();
            m_allowMSAA = binaryReader.ReadBoolean();
            m_allowDynamicResolution = binaryReader.ReadBoolean();
            m_targetEye = binaryReader.ReadInt32();
            m_sensorSize = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
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
            if(m_clearFlags != other.m_clearFlags)
            {
                return false;
            }
            if (m_backgroundColor.Equals(other.m_backgroundColor) == false)
            {
                return false;
            }
            if (m_cullingMask.Equals(other.m_cullingMask) == false)
            {
                return false;
            }
            if (m_orthographic.Equals(other.m_orthographic) == false)
            {
                return false;
            }
            if (m_orthographicSize.Equals(other.m_orthographicSize) == false)
            {
                return false;
            }
            if (m_fieldOfView.Equals(other.m_fieldOfView) == false)
            {
                return false;
            }
            if (m_usePhysicalProperties.Equals(other.m_usePhysicalProperties) == false)
            {
                return false;
            }
            if (m_focalLength.Equals(other.m_focalLength) == false)
            {
                return false;
            }
            if (m_sensorSize != null)
            {
                if (m_sensorSize.Equals(other.m_sensorSize) == false)
                {
                    return false;
                }
            }
            if (m_lensShift.Equals(other.m_lensShift) == false)
            {
                return false;
            }
            if (m_gateFit.Equals(other.m_gateFit) == false)
            {
                return false;
            }
            if (m_nearClipPlane.Equals(other.m_nearClipPlane) == false)
            {
                return false;
            }
            if (m_farClipPlane.Equals(other.m_farClipPlane) == false)
            {
                return false;
            }
            if (m_rect.Equals(other.m_rect) == false)
            {
                return false;
            }
            if (m_depth.Equals(other.m_depth) == false)
            {
                return false;
            }
            if (m_renderingPath.Equals(other.m_renderingPath) == false)
            {
                return false;
            }
            if (m_useOcclusionCulling.Equals(other.m_useOcclusionCulling) == false)
            {
                return false;
            }
            if (m_allowHDR.Equals(other.m_allowHDR) == false)
            {
                return false;
            }
            if (m_allowMSAA.Equals(other.m_allowMSAA) == false)
            {
                return false;
            }
            if (m_allowDynamicResolution.Equals(other.m_allowDynamicResolution) == false)
            {
                return false;
            }
            if (m_targetEye.Equals(other.m_targetEye) == false)
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
