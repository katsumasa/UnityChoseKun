using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine.Rendering.Universal
{        
    [System.Serializable]
    public class UniversalAdditionalCameraDataKun : BehaviourKun
    {
        public enum CameraOverrideOption
        {
            Off,
            On,
            UsePipelineSettings,
        }


        public enum CameraRenderType
        {
            Base,
            Overlay,
        }

        public enum AntialiasingMode
        {
            [InspectorName("No Anti-aliasing")]
            None,
            [InspectorName("Fast Approximate Anti-aliasing (FXAA)")]
            FastApproximateAntialiasing,
            [InspectorName("Subpixel Morphological Anti-aliasing (SMAA)")]
            SubpixelMorphologicalAntiAliasing,
            //TemporalAntialiasing
        }

        public enum AntialiasingQuality
        {
            Low,
            Medium,
            High
        }

        // UniversalRenderPipelineAssetより抜粋
        public enum VolumeFrameworkUpdateMode
        {
            [InspectorName("Every Frame")]
            EveryFrame = 0,
            [InspectorName("Via Scripting")]
            ViaScripting = 1,
            [InspectorName("Use Pipeline Settings")]
            UsePipelineSettings = 2,
        }


        [SerializeField]
        bool m_RenderShadows;

        [SerializeField]
        CameraOverrideOption m_RequiresDepthTextureOption;

        [SerializeField]
        CameraOverrideOption m_RequiresOpaqueTextureOption;

        [SerializeField] CameraRenderType m_CameraType;

        [SerializeField] CameraKun[] m_Cameras;

        [SerializeField] int m_RendererIndex;

        [SerializeField] LayerMask m_VolumeLayerMask; // "Default"
        [SerializeField] TransformKun m_VolumeTrigger;
        [SerializeField] VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateModeOption;

        [SerializeField] bool m_RenderPostProcessing;
        [SerializeField] AntialiasingMode m_Antialiasing;
        [SerializeField] AntialiasingQuality m_AntialiasingQuality;
        [SerializeField] bool m_StopNaN;
        [SerializeField] bool m_Dithering;
        [SerializeField] bool m_ClearDepth;
        [SerializeField] bool m_AllowXRRendering;

        // Camera
        [SerializeField] bool m_Orthographic;
        [SerializeField] float m_FieldOfView;
        [SerializeField] float m_NearClipPlane;
        [SerializeField] float m_FarClipPlane;
        [SerializeField] bool m_UsePhysicalProperties;
        [SerializeField] Vector2Kun m_SensorSize;
        [SerializeField] float m_FocalLength;
        [SerializeField] Vector2Kun m_LensShift;
        [SerializeField] Camera.GateFitMode m_GateFit;
        [SerializeField] float m_Depth;
        [SerializeField] int m_CullingMask;
        [SerializeField] bool m_UseOcclusionCulling;


        public bool orthographic
        {
            get => m_Orthographic;
            set { m_Orthographic = value; dirty = true; }
        }

        public float fieldOfView
        {
            get => m_FieldOfView;
            set { m_FieldOfView = value; dirty = true; }
        }

        public float nearClipPlane
        {
            get => m_NearClipPlane;
            set { m_NearClipPlane = value; dirty = true; }
        }

        public float farClipPlane
        {
            get => m_FarClipPlane;
            set { m_FarClipPlane = value; dirty = true; }
        }
        public bool usePhysicalProperties
        {
            get => m_UsePhysicalProperties;
            set { m_UsePhysicalProperties = value; dirty = true; }
        }
        
        public Vector2Kun sensorSize
        {
            get => m_SensorSize;
            set { m_SensorSize = value; dirty = true; }
        }
        public float focalLength
        {
            get => m_FocalLength;
            set { m_FocalLength = value; dirty = true; }
        }
        
        public Vector2Kun lensShift
        {
            get => m_LensShift;
            set { m_LensShift = value; dirty = true; }
        }
        
        public Camera.GateFitMode gateFit
        {
            get => m_GateFit;
            set { m_GateFit = value; dirty = true; }
        }

        public float depth
        {
            get => m_Depth;
            set { m_Depth = value; dirty = true; }
        }

        public int cullingMask
        {
            get => m_CullingMask;
            set { m_CullingMask = value; dirty = true; }
        }

        public bool useOcclusionCulling
        {
            get => m_UseOcclusionCulling;
            set { m_UseOcclusionCulling = value; dirty = true; }
        }

        // UniversalRenderPipelineAssetのデータ
        [SerializeField] string[] m_Renderers;
        [SerializeField] int m_DefaultRendererIndex;
        [SerializeField] int[] m_RendererIndexList;

        public bool renderShadows
        {
            get => m_RenderShadows;
            set { m_RenderShadows = value; dirty = true; }
        }

        public CameraOverrideOption requiresDepthTextureOption
        {
            get => m_RequiresDepthTextureOption;
            set { m_RequiresDepthTextureOption = value; dirty = true; }
        }

        public CameraOverrideOption requiresColorOption
        {
            get => m_RequiresOpaqueTextureOption;
            set { m_RequiresOpaqueTextureOption = value; dirty = true; }
        }

        public CameraRenderType renderType
        {
            get => m_CameraType;
            set { m_CameraType = value; dirty = true; }
        }

        public CameraKun[] cameraStack
        {
            get => m_Cameras;
        }

        public bool crearDepth
        {
            get => m_ClearDepth;
        }

        //public bool requiresDepthTexture
        //public bool requiresColorTexture
        //public ScriptableRenderer scriptableRenderer


        public void SetRenderer(int index)
        {
            m_RendererIndex = index;
        }

        public LayerMask volumeLayerMask
        {
            get => m_VolumeLayerMask;
            set { m_VolumeLayerMask = value; dirty = true; }
        }

        public TransformKun volumeTrigger
        {
            get => m_VolumeTrigger;
            set { m_VolumeTrigger = value; dirty = true; }
        }

        internal VolumeFrameworkUpdateMode volumeFrameworkUpdateMode
        {
            get => m_VolumeFrameworkUpdateModeOption;
            set { m_VolumeFrameworkUpdateModeOption = value; dirty = true; }
        }

        //public bool requiresVolumeFrameworkUpdate
        //{
        //    get
        //    {
        //        if (m_VolumeFrameworkUpdateModeOption == VolumeFrameworkUpdateMode.UsePipelineSettings)
        //        {
        //            return UniversalRenderPipeline.asset.volumeFrameworkUpdateMode != VolumeFrameworkUpdateMode.ViaScripting;
        //        }
        //
        //        return m_VolumeFrameworkUpdateModeOption == VolumeFrameworkUpdateMode.EveryFrame;
        //    }
        //}





        public bool renderPostProcessing 
        { 
            get => m_RenderPostProcessing;
            set { m_RenderPostProcessing = value; dirty = true; }
        }

        public AntialiasingMode antialiasingMode
        {
            get => m_Antialiasing;
            set { m_Antialiasing = value; dirty = true; }
        }

        public AntialiasingQuality antialiasingQuality
        {
            get => m_AntialiasingQuality;
            set { m_AntialiasingQuality = value; dirty = true; }
        }

        
        public bool stopNaN
        {
            get => m_StopNaN;
            set { m_StopNaN = value; dirty = true; }
        }

        public bool dithering
        {
            get => m_Dithering;
            set { m_Dithering = value; dirty = true; }
        }

        public bool allowXRRendering
        {
            get => m_AllowXRRendering;
            set { m_AllowXRRendering = value; dirty = true; }
        }

        public int rendererIndex
        {
            get => m_RendererIndex;
            set { m_RendererIndex = value; dirty = true; }
        }

        public string[] renderers
        {
            get => m_Renderers;
        }

        public int defaultRendererIndex
        {
            get => m_DefaultRendererIndex;
        }

        public int[] rendererIndexList
        {
            get => m_RendererIndexList;
        }


        public UniversalAdditionalCameraDataKun() : this(null) { }


        public UniversalAdditionalCameraDataKun(Component component) : base(component)
        {           
            if (component != null)
            {

                var t = component.GetType();
                FieldInfo fieldInfo;

                fieldInfo = t.GetField("m_RenderShadows", BindingFlags.Instance | BindingFlags.NonPublic);
                m_RenderShadows = (bool)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_RequiresDepthTextureOption", BindingFlags.Instance | BindingFlags.NonPublic);
                m_RequiresDepthTextureOption = (CameraOverrideOption)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_RequiresOpaqueTextureOption", BindingFlags.Instance | BindingFlags.NonPublic);
                m_RequiresOpaqueTextureOption = (CameraOverrideOption)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_CameraType", BindingFlags.Instance | BindingFlags.NonPublic);
                m_CameraType = (CameraRenderType)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_Cameras", BindingFlags.Instance | BindingFlags.NonPublic);
                var cameras = (List<Camera>)fieldInfo.GetValue(component);
                m_Cameras = new CameraKun[cameras.Count];
                for (var i = 0; i < cameras.Count; i++)
                {
                    m_Cameras[i] = new CameraKun(cameras[i]);
                }

                fieldInfo = t.GetField("m_RendererIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                m_RendererIndex = (int)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_VolumeLayerMask", BindingFlags.Instance | BindingFlags.NonPublic);
                m_VolumeLayerMask = (LayerMask)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_VolumeTrigger", BindingFlags.Instance | BindingFlags.NonPublic);
                m_VolumeTrigger = new TransformKun((Transform)fieldInfo.GetValue(component));

                fieldInfo = t.GetField("m_VolumeFrameworkUpdateModeOption", BindingFlags.Instance | BindingFlags.NonPublic);
                m_VolumeFrameworkUpdateModeOption = (VolumeFrameworkUpdateMode)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_RenderPostProcessing", BindingFlags.Instance | BindingFlags.NonPublic);
                m_RenderPostProcessing = (bool)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_Antialiasing", BindingFlags.Instance | BindingFlags.NonPublic);
                m_Antialiasing = (AntialiasingMode)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_AntialiasingQuality", BindingFlags.Instance | BindingFlags.NonPublic);
                m_AntialiasingQuality = (AntialiasingQuality)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_StopNaN", BindingFlags.Instance | BindingFlags.NonPublic);
                m_StopNaN = (bool)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_Dithering", BindingFlags.Instance | BindingFlags.NonPublic);
                m_Dithering = (bool)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_ClearDepth", BindingFlags.Instance | BindingFlags.NonPublic);
                m_ClearDepth = (bool)fieldInfo.GetValue(component);

                fieldInfo = t.GetField("m_AllowXRRendering", BindingFlags.Instance | BindingFlags.NonPublic);
                m_AllowXRRendering = (bool)fieldInfo.GetValue(component);


                // Camera
                var camera = component.gameObject.GetComponent<Camera>();
                if (camera)
                {
                    m_Orthographic = camera.orthographic;
                    m_FieldOfView = camera.fieldOfView;
                    m_NearClipPlane = camera.nearClipPlane;
                    m_FarClipPlane = camera.farClipPlane;
                    m_UsePhysicalProperties = camera.usePhysicalProperties;
                    m_SensorSize = new Vector2Kun(camera.sensorSize);
                    m_FocalLength = camera.focalLength;
                    m_LensShift = new Vector2Kun(camera.lensShift);
                    m_GateFit = camera.gateFit;
                    m_Depth = camera.depth;
                    m_CullingMask = camera.cullingMask;
                    m_UseOcclusionCulling = camera.useOcclusionCulling;
                }



                //
                var an = new AssemblyName("Unity.RenderPipelines.Universal.Runtime");
                var assembly = Assembly.Load(an);
                var urpType = assembly.GetType("UnityEngine.Rendering.Universal.UniversalRenderPipeline");
                var assetInfo = urpType.GetProperty("asset");
                var asset = assetInfo.GetValue(null);

                var assetType = asset.GetType();


                var fi = assetType.GetField("m_DefaultRendererIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                m_DefaultRendererIndex = (int)fi.GetValue(asset);


                fi = assetType.GetField("m_Renderers", BindingFlags.Instance | BindingFlags.NonPublic);
                var renderers = (object[])fi.GetValue(asset);
                m_Renderers = new string[renderers.Length + 1];
                m_Renderers[0] = "Default Renderer (" + renderers[m_DefaultRendererIndex].ToString() + ")";
                for (var i = 1; i < m_Renderers.Length; i++)
                {
                    m_Renderers[i] =  (i - 1).ToString() +":"+ renderers[i - 1].ToString();
                }


                var pi = assetType.GetProperty("rendererIndexList", BindingFlags.Instance | BindingFlags.NonPublic);
                m_RendererIndexList = (int[]) pi.GetValue(asset);

            }
        }

        
        public override bool WriteBack(Component component)
        {
            base.WriteBack(component);
            if (dirty)
            {                
                var t = component.GetType();
                
                FieldInfo fieldInfo;

                fieldInfo = t.GetField("m_RenderShadows", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_RenderShadows);

                fieldInfo = t.GetField("m_RequiresDepthTextureOption", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, (int)m_RequiresDepthTextureOption);

                fieldInfo = t.GetField("m_RequiresOpaqueTextureOption", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, (int)m_RequiresOpaqueTextureOption);

                fieldInfo = t.GetField("m_CameraType", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, (int)m_CameraType);

                // m_Camerasはライトバックしない

                fieldInfo = t.GetField("m_RendererIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_RendererIndex);

                fieldInfo = t.GetField("m_VolumeLayerMask", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component,m_VolumeLayerMask);

                fieldInfo = t.GetField("m_VolumeTrigger", BindingFlags.Instance | BindingFlags.NonPublic);
                m_VolumeTrigger.WriteBack((Transform)fieldInfo.GetValue(component));


                fieldInfo = t.GetField("m_VolumeFrameworkUpdateModeOption", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, (int)m_VolumeFrameworkUpdateModeOption);

                fieldInfo = t.GetField("m_RenderPostProcessing", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_RenderPostProcessing);

                fieldInfo = t.GetField("m_Antialiasing", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, (int)m_Antialiasing);

                fieldInfo = t.GetField("m_AntialiasingQuality", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, (int)m_AntialiasingQuality);

                fieldInfo = t.GetField("m_StopNaN", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_StopNaN);

                fieldInfo = t.GetField("m_Dithering", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_Dithering);

                fieldInfo = t.GetField("m_ClearDepth", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_ClearDepth);

                fieldInfo = t.GetField("m_AllowXRRendering", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(component, m_AllowXRRendering);


                var camera = component.gameObject.GetComponent<Camera>();
                if (camera)
                {
                    camera.orthographic =  m_Orthographic;
                    camera.fieldOfView =  m_FieldOfView;
                    camera.nearClipPlane = m_NearClipPlane;
                    camera.farClipPlane = m_FarClipPlane;
                    camera.usePhysicalProperties = m_UsePhysicalProperties;
                    camera.sensorSize = m_SensorSize.GetVector2();
                    camera.focalLength =  m_FocalLength;
                    camera.lensShift =  m_LensShift.GetVector2();
                    camera.gateFit =  m_GateFit;
                    camera.depth = m_Depth;
                    camera.cullingMask = m_CullingMask;
                    camera.useOcclusionCulling = m_UseOcclusionCulling;
                }

                return true;
            }


            return false;
        }





        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((bool)m_RenderShadows);
            binaryWriter.Write((int)m_RequiresDepthTextureOption);
            binaryWriter.Write((int)m_RequiresOpaqueTextureOption);
            binaryWriter.Write((int)m_CameraType);
            SerializerKun.Serialize<CameraKun>(binaryWriter,m_Cameras);
            binaryWriter.Write((int)m_RendererIndex);
            binaryWriter.Write((int)m_VolumeLayerMask);
            SerializerKun.Serialize<TransformKun>(binaryWriter, m_VolumeTrigger);
            binaryWriter.Write((int)m_VolumeFrameworkUpdateModeOption);
            binaryWriter.Write((bool)m_RenderPostProcessing);
            binaryWriter.Write((int)m_Antialiasing);
            binaryWriter.Write((int)m_AntialiasingQuality);
            binaryWriter.Write((bool)m_StopNaN);
            binaryWriter.Write((bool)m_Dithering);
            binaryWriter.Write((bool)m_ClearDepth);
            binaryWriter.Write((bool)m_AllowXRRendering);


            binaryWriter.Write((bool)m_Orthographic);
            binaryWriter.Write((float)m_FieldOfView);
            binaryWriter.Write((float)m_NearClipPlane);
            binaryWriter.Write((float)m_FarClipPlane);
            binaryWriter.Write((bool)m_UsePhysicalProperties);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter,m_SensorSize);
            binaryWriter.Write((float)m_FocalLength);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_LensShift);
            binaryWriter.Write((int)m_GateFit);
            binaryWriter.Write((float)m_Depth);
            binaryWriter.Write((int)m_CullingMask);
            binaryWriter.Write((bool)m_UseOcclusionCulling);

            binaryWriter.Write((int)m_DefaultRendererIndex);
            SerializerKun.Serialize(binaryWriter, m_Renderers);            
            SerializerKun.Serialize(binaryWriter, m_RendererIndexList);
        }





      
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_RenderShadows = binaryReader.ReadBoolean();
            m_RequiresDepthTextureOption = (CameraOverrideOption)binaryReader.ReadInt32();
            m_RequiresOpaqueTextureOption = (CameraOverrideOption)binaryReader.ReadInt32();
            m_CameraType = (CameraRenderType)binaryReader.ReadInt32();
            m_Cameras = SerializerKun.DesirializeObjects<CameraKun>(binaryReader);
            m_RendererIndex = binaryReader.ReadInt32();
            m_VolumeLayerMask = (LayerMask)binaryReader.ReadInt32();
            m_VolumeTrigger = SerializerKun.DesirializeObject<TransformKun>(binaryReader);
            m_VolumeFrameworkUpdateModeOption = (VolumeFrameworkUpdateMode)binaryReader.ReadInt32();
            m_RenderPostProcessing = binaryReader.ReadBoolean();
            m_Antialiasing = (AntialiasingMode)binaryReader.ReadInt32();
            m_AntialiasingQuality = (AntialiasingQuality)binaryReader.ReadInt32();
            m_StopNaN = binaryReader.ReadBoolean();
            m_Dithering = binaryReader.ReadBoolean();
            m_ClearDepth = binaryReader.ReadBoolean();
            m_AllowXRRendering = binaryReader.ReadBoolean();


            m_Orthographic = binaryReader.ReadBoolean();
            m_FieldOfView = binaryReader.ReadSingle();
            m_NearClipPlane = binaryReader.ReadSingle();
            m_FarClipPlane = binaryReader.ReadSingle();
            m_UsePhysicalProperties = binaryReader.ReadBoolean();
            m_SensorSize = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
            m_FocalLength = binaryReader.ReadSingle();
            m_LensShift = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
            m_GateFit = (Camera.GateFitMode)binaryReader.ReadInt32();
            m_Depth = binaryReader.ReadSingle();
            m_CullingMask = binaryReader.ReadInt32();
            m_UseOcclusionCulling = binaryReader.ReadBoolean();

            m_DefaultRendererIndex = binaryReader.ReadInt32();
            m_Renderers = SerializerKun.DesirializeStrings(binaryReader);
            m_RendererIndexList = SerializerKun.DesirializeInt32s(binaryReader);
        }
    }
}