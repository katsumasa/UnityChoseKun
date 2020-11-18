using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun {
    // <summary> Rendererをシリアライズする為のClass </summary>
    [System.Serializable]
    public class RendererKun : ComponentKun
    {
        //
        // メンバーの変数の定義
        //
#if UNITY_2019_1_OR_NEWER
        [SerializeField] bool m_forceRenderingOff ;
        [SerializeField] UnityEngine.Experimental.Rendering.RayTracingMode m_rayTracingMode;                
#endif                                
        [SerializeField] UnityEngine.Rendering.LightProbeUsage m_lightProbeUsage;                
        [SerializeField] MotionVectorGenerationMode m_motionVectorGenerationMode;                                
        [SerializeField] UnityEngine.Rendering.ReflectionProbeUsage m_reflectionProbeUsage;                
        [SerializeField] UnityEngine.Rendering.ShadowCastingMode m_shadowCastingMode;
        [SerializeField] int m_lightmapIndex;
        [SerializeField] public int m_rendererPriority;
        [SerializeField] int m_realtimeLightmapIndex;
        [SerializeField] int m_sortingLayerID;        
        [SerializeField] int m_sortingOrder;
        [SerializeField] uint m_renderingLayerMask;
        [SerializeField] string m_lightProbeProxyVolumeOverride;
        [SerializeField] string m_sortingLayerName;
        [SerializeField] bool m_receiveShadows;
        [SerializeField] bool m_allowOcclusionWhenDynamic;
        [SerializeField] bool m_enabled;
        [SerializeField] bool m_isPartOfStaticBatch;
        [SerializeField] bool m_isVisible;
        [SerializeField] BoundsKun m_bounds;
        [SerializeField] TransformKun m_probeAnchor;
        [SerializeField] Vector4Kun m_lightmapScaleOffset;
        [SerializeField] Vector4Kun m_realtimeLightmapScaleOffset;
        [SerializeField] Matrix4x4Kun m_localToWorldMatrix;
        [SerializeField] Matrix4x4Kun m_worldToLocalMatrix;
        [SerializeField] MaterialKun m_material;
        [SerializeField] MaterialKun m_sharedMaterial;
        [SerializeField] MaterialKun[] m_materials;
        [SerializeField] MaterialKun[] m_sharedMaterials;


#if UNITY_2019_1_OR_NEWER
        public bool forceRenderingOff
        {
            get { return m_forceRenderingOff; }
            set { m_forceRenderingOff = value; }
        }

        public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode
        {
            get { return m_rayTracingMode; }
            set { m_rayTracingMode = value; }
        }
#endif
        public bool allowOcclusionWhenDynamic{
            get{return m_allowOcclusionWhenDynamic;}
            set{m_allowOcclusionWhenDynamic = value;}
        }

        public Bounds bounds {
            get{return m_bounds.GetBounds();}
            private set{m_bounds = new BoundsKun(value);}
        }
        
        public bool enabled{
            get{return m_enabled;}
            set{m_enabled = value;}
        }

        public bool isPartOfStaticBatch {
            get{return m_isPartOfStaticBatch;}
            private set{m_isPartOfStaticBatch = value;}
        }

        public bool isVisible {
            get {return m_isVisible;}
            private set {m_isVisible = value;}
        }

        public int lightmapIndex {
            get{return m_lightmapIndex;}
            set{m_lightmapIndex = value;}
        }
        
        public Vector4 lightmapScaleOffset {
            get{return m_lightmapScaleOffset.GetVector4();}
            set{m_lightmapScaleOffset = new Vector4Kun(value);}
        }
                
        public string lightProbeProxyVolumeOverride {
            get{return m_lightProbeProxyVolumeOverride;}
            private set{m_lightProbeProxyVolumeOverride = value;}
        }
        
        public UnityEngine.Rendering.LightProbeUsage lightProbeUsage {
            get{return m_lightProbeUsage;}

            set{m_lightProbeUsage = value;}
        }

        public Matrix4x4 localToWorldMatrix {
            get{return m_localToWorldMatrix.GetMatrix4X4();}
            private set{m_localToWorldMatrix = new Matrix4x4Kun(value);}
        }
        
        public MaterialKun material {
            get{return m_material;}
            private set{m_material = value;}
        }
        
        public MaterialKun[] materials {
            get{return m_materials;}
            private set{m_materials = value;}
        }
        
        public MotionVectorGenerationMode motionVectorGenerationMode {
            get{return m_motionVectorGenerationMode;}
            set{m_motionVectorGenerationMode = value;}
        }
        
        public TransformKun probeAnchor {
            get{return m_probeAnchor;}
            private set{m_probeAnchor = value;}
        }
        
        public int realtimeLightmapIndex {
            get{return m_realtimeLightmapIndex;}
            set{m_realtimeLightmapIndex = value;}
        }

        public Vector4 realtimeLightmapScaleOffset {
            get{return m_realtimeLightmapScaleOffset.GetVector4() ;}
            set{m_realtimeLightmapScaleOffset = new Vector4Kun(value);}
        }

        public bool receiveShadows {
            get{return m_receiveShadows;}
            set{m_receiveShadows = value;}
        }

        public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage {
            get{return m_reflectionProbeUsage;}
            set{m_reflectionProbeUsage = value;}
        }
        
        public int rendererPriority {
            get{return m_rendererPriority;}
            set{m_rendererPriority = value;}
        }
        
        public uint renderingLayerMask {
            get{return m_renderingLayerMask;}
            set{m_renderingLayerMask = value;}
        }
        
        public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode {
            get{return m_shadowCastingMode;}
            set{m_shadowCastingMode = value;}
        }
        
        public MaterialKun sharedMaterial {
            get{return m_sharedMaterial;}
            private set {m_sharedMaterial = value;}
        }
        
        public MaterialKun[] sharedMaterials {
            get{return m_sharedMaterials;}
            private set{m_sharedMaterials = value;}
        }

        public int sortingLayerID {
            get{return m_sortingLayerID;}
            set{m_sortingLayerID = value;}
        }
        
        public string sortingLayerName {
            get {return m_sortingLayerName;}
            set {m_sortingLayerName = value;}
        }
        
        public int sortingOrder {
            get {return m_sortingOrder;}
            set {m_sortingOrder = value;}
        }
        
        public Matrix4x4 worldToLocalMatrix {
            get {return m_worldToLocalMatrix.GetMatrix4X4();}
            private set {m_worldToLocalMatrix = new Matrix4x4Kun(value);}
        }

        //
        // メンバー関数の定義
        //


        /// <summary>
        /// 
        /// </summary>
        public RendererKun():this(null){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public RendererKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Renderer;
            m_bounds = new BoundsKun();
            m_lightmapScaleOffset = new Vector4Kun();
            m_lightProbeProxyVolumeOverride = "";
            m_localToWorldMatrix = new Matrix4x4Kun();            
            m_probeAnchor = new TransformKun();
            m_realtimeLightmapScaleOffset = new Vector4Kun();
            m_worldToLocalMatrix = new Matrix4x4Kun();

            m_sortingLayerName = "";
            

            var renderer = component as Renderer;
            if(renderer){
                #if UNITY_2019_1_OR_NEWER
                forceRenderingOff = renderer.forceRenderingOff;
                rayTracingMode = renderer.rayTracingMode;
                #endif

                allowOcclusionWhenDynamic = renderer.allowOcclusionWhenDynamic;
                bounds = renderer.bounds;
                enabled = renderer.enabled;
                
                isPartOfStaticBatch = renderer.isPartOfStaticBatch;
                isVisible =renderer.isVisible;
                lightmapIndex = renderer.lightmapIndex;
                lightmapScaleOffset = renderer.lightmapScaleOffset;
                if(renderer.lightProbeProxyVolumeOverride){
                    lightProbeProxyVolumeOverride = renderer.lightProbeProxyVolumeOverride.ToString();
                }
                lightProbeUsage = renderer.lightProbeUsage;
                localToWorldMatrix = renderer.localToWorldMatrix;
#if !UNITY_EDITOR
                if (renderer.material != null)
                {
                    material = new MaterialKun(renderer.material);
                }

                if(renderer.materials != null){
                    materials = new MaterialKun[renderer.materials.Length];
                    for(var i = 0; i < renderer.materials.Length; i++){
                        materials[i] = new MaterialKun(renderer.materials[i]);
                    }
                }
#endif
                motionVectorGenerationMode = renderer.motionVectorGenerationMode;
                if(renderer.probeAnchor!=null){
                    probeAnchor = new TransformKun(renderer.probeAnchor);
                }
                
                realtimeLightmapIndex = renderer.realtimeLightmapIndex;
                realtimeLightmapScaleOffset = renderer.realtimeLightmapScaleOffset;
                rendererPriority = renderer.rendererPriority;
                renderingLayerMask = renderer.renderingLayerMask;
                shadowCastingMode = renderer.shadowCastingMode;
                sharedMaterial = new MaterialKun(renderer.sharedMaterial);
                if(renderer.sharedMaterials!=null){
                    sharedMaterials = new MaterialKun[renderer.sharedMaterials.Length];
                    for(var i = 0; i < renderer.sharedMaterials.Length;i++){
                        sharedMaterials[i] = new MaterialKun(renderer.sharedMaterials[i]);
                    }
                }
                sortingLayerID = renderer.sortingLayerID;
                sortingLayerName = renderer.sortingLayerName;
                sortingOrder = renderer.sortingOrder;
                worldToLocalMatrix = renderer.worldToLocalMatrix;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool WriteBack(Component component)
        {               
            var result = base.WriteBack(component);            
            var renderer = component as Renderer;
            if(renderer){
                if(result){
#if UNITY_2019_1_OR_NEWER
                    renderer.forceRenderingOff = forceRenderingOff;                                
                    renderer.rayTracingMode = rayTracingMode;
#endif

                    renderer.allowOcclusionWhenDynamic =allowOcclusionWhenDynamic;                
                    renderer.enabled = enabled;
                    
                    //renderer.lightmapIndex = lightmapIndex;
                    //renderer.lightmapScaleOffset = lightmapScaleOffset ;                
                    renderer.lightProbeUsage = lightProbeUsage;
                    renderer.motionVectorGenerationMode = motionVectorGenerationMode;                                   
                    //renderer.realtimeLightmapIndex = realtimeLightmapIndex;
                    //renderer.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
                    renderer.rendererPriority = rendererPriority;
                    renderer.renderingLayerMask = renderingLayerMask;
                    renderer.shadowCastingMode = shadowCastingMode;
                    renderer.sortingLayerID = sortingLayerID;
                    renderer.sortingLayerName = sortingLayerName;
                    renderer.sortingOrder = sortingOrder;                    
                }
                //
                // RendererはDirtyでは無いがMaterialが書き換わっているケースもある
                //
                for(var i = 0; i < materials.Length; i++){
                    var materialKun = materials[i];
                    materialKun.WriteBack(renderer.materials[i]);
                }
            }                        
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);

#if UNITY_2019_1_OR_NEWER
            binaryWriter.Write(m_forceRenderingOff);
            binaryWriter.Write((int)m_rayTracingMode);
#endif
            binaryWriter.Write((int)m_lightProbeUsage);
            binaryWriter.Write((int)m_motionVectorGenerationMode);
            binaryWriter.Write((int)m_reflectionProbeUsage);
            binaryWriter.Write((int)m_shadowCastingMode);
            binaryWriter.Write(m_lightmapIndex);
            binaryWriter.Write(m_rendererPriority);
            binaryWriter.Write(m_realtimeLightmapIndex);
            binaryWriter.Write(m_sortingLayerID);
            binaryWriter.Write(m_sortingOrder);
            binaryWriter.Write(m_renderingLayerMask);
            binaryWriter.Write(m_lightProbeProxyVolumeOverride);
            binaryWriter.Write(m_sortingLayerName);
            binaryWriter.Write(m_receiveShadows);
            binaryWriter.Write(m_allowOcclusionWhenDynamic);
            binaryWriter.Write(m_enabled);
            binaryWriter.Write(m_isPartOfStaticBatch);
            binaryWriter.Write(m_isVisible);

            SerializerKun.Serialize<BoundsKun>(binaryWriter, m_bounds);
            SerializerKun.Serialize<TransformKun>(binaryWriter, m_probeAnchor);
            SerializerKun.Serialize<Vector4Kun>(binaryWriter, m_lightmapScaleOffset);
            SerializerKun.Serialize<Vector4Kun>(binaryWriter, m_realtimeLightmapScaleOffset);
            SerializerKun.Serialize<Matrix4x4Kun>(binaryWriter, m_localToWorldMatrix);
            SerializerKun.Serialize<Matrix4x4Kun>(binaryWriter, m_worldToLocalMatrix);
            SerializerKun.Serialize<MaterialKun>(binaryWriter, m_material);
            SerializerKun.Serialize<MaterialKun>(binaryWriter, m_sharedMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter, m_materials);
            SerializerKun.Serialize<MaterialKun>(binaryWriter, m_sharedMaterials);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {            
            base.Deserialize(binaryReader);

#if UNITY_2019_1_OR_NEWER
            m_forceRenderingOff = binaryReader.ReadBoolean();
            m_rayTracingMode = (UnityEngine.Experimental.Rendering.RayTracingMode)binaryReader.ReadInt32();
#endif
            m_lightProbeUsage = (UnityEngine.Rendering.LightProbeUsage)binaryReader.ReadInt32();
            m_motionVectorGenerationMode = (MotionVectorGenerationMode)binaryReader.ReadInt32();
            m_reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)binaryReader.ReadInt32();
            m_shadowCastingMode = (UnityEngine.Rendering.ShadowCastingMode)binaryReader.ReadInt32();
            m_lightmapIndex = binaryReader.ReadInt32();
            m_rendererPriority = binaryReader.ReadInt32();
            m_realtimeLightmapIndex = binaryReader.ReadInt32();
            m_sortingLayerID = binaryReader.ReadInt32();
            m_sortingOrder = binaryReader.ReadInt32();
            m_renderingLayerMask = binaryReader.ReadUInt32();
            m_lightProbeProxyVolumeOverride = binaryReader.ReadString();
            m_sortingLayerName = binaryReader.ReadString();
            m_receiveShadows = binaryReader.ReadBoolean();
            m_allowOcclusionWhenDynamic = binaryReader.ReadBoolean();
            m_enabled = binaryReader.ReadBoolean();
            m_isPartOfStaticBatch = binaryReader.ReadBoolean();
            m_isVisible = binaryReader.ReadBoolean();
            m_bounds = SerializerKun.DesirializeObject<BoundsKun>(binaryReader);
            m_probeAnchor = SerializerKun.DesirializeObject<TransformKun>(binaryReader);
            m_lightmapScaleOffset = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader);
            m_realtimeLightmapScaleOffset = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader);
            m_localToWorldMatrix = SerializerKun.DesirializeObject<Matrix4x4Kun>(binaryReader);
            m_worldToLocalMatrix = SerializerKun.DesirializeObject<Matrix4x4Kun>(binaryReader);
            m_material = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
            m_sharedMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
            m_materials = SerializerKun.DesirializeObjects<MaterialKun>(binaryReader);
            m_sharedMaterials = SerializerKun.DesirializeObjects<MaterialKun>(binaryReader);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {

            var other = obj as RendererKun;
            if(other == null)
            {
                return false;
            }

#if UNITY_2019_1_OR_NEWER
            if (!m_forceRenderingOff.Equals(other.m_forceRenderingOff))
            {
                return false;
            }
            if (!m_rayTracingMode.Equals(other.m_rayTracingMode))
            {
                return false;
            }
#endif
            if (!m_allowOcclusionWhenDynamic.Equals(other.m_allowOcclusionWhenDynamic))
            {
                return false;
            }
            if(m_bounds != null && !m_bounds.Equals(other.m_bounds))
            {
                return false;
            }
            if (!m_enabled.Equals(other.m_enabled))
            {
                return false;
            }
            if (!m_isPartOfStaticBatch.Equals(other.m_isPartOfStaticBatch))
            {
                return false;
            }
            if (!m_isVisible.Equals(other.m_isVisible))
            {
                return false;
            }
            if (!m_lightmapIndex.Equals(other.m_lightmapIndex))
            {
                return false;
            }
            if(m_lightmapScaleOffset != null && !m_lightmapIndex.Equals(other.m_lightmapIndex))
            {
                return false;
            }
            if(m_lightProbeProxyVolumeOverride != null && !m_lightProbeProxyVolumeOverride.Equals(other.m_lightProbeProxyVolumeOverride))
            {
                return false;
            }
            if (!m_lightProbeUsage.Equals(other.m_lightProbeUsage))
            {
                return false;
            }
            
            if(m_localToWorldMatrix != null && !m_localToWorldMatrix.Equals(other.m_localToWorldMatrix))
            {
                return false;
            }

            if(m_material != null && !m_material.Equals(other.m_material))
            {
                return false;
            }

            if(m_materials != null)
            {
                if(m_materials.Length != other.m_materials.Length)
                {
                    return false;
                }
                for(var i =0; i < m_materials.Length; i++)
                {
                    if (!m_materials[i].Equals(other.m_materials[i]))
                    {
                        return false;
                    }
                }
            }

            if (!m_motionVectorGenerationMode.Equals(other.m_motionVectorGenerationMode))
            {
                return false;
            }

            if (m_probeAnchor != null && !m_probeAnchor.Equals(other.m_probeAnchor))
            {
                return false;
            }

            if (!m_realtimeLightmapIndex.Equals(other.m_realtimeLightmapIndex))
            {
                return false;
            }

            if(m_realtimeLightmapScaleOffset != null && !m_realtimeLightmapScaleOffset.Equals(other.m_realtimeLightmapScaleOffset))
            {
                return false;
            }

            if (!m_receiveShadows.Equals(other.m_receiveShadows))
            {
                return false;
            }

            if (!m_reflectionProbeUsage.Equals(other.m_reflectionProbeUsage))
            {
                return false;
            }

            if (!m_rendererPriority.Equals(other.m_rendererPriority))
            {
                return false;
            }

            if (!m_renderingLayerMask.Equals(other.m_renderingLayerMask))
            {
                return false;
            }

            if (!m_shadowCastingMode.Equals(other.m_shadowCastingMode))
            {
                return false;
            }

            if(!MaterialKun.Equals(m_sharedMaterial, other.m_sharedMaterial)) {
                return false;            
            }

            if(m_sharedMaterials != null)
            {
                if (!m_sharedMaterials.Length.Equals(other.m_sharedMaterials.Length))
                {
                    return false;
                }
                for(var i = 0; i < m_sharedMaterials.Length; i++)
                {
                    if (!m_sharedMaterials[i].Equals(other.m_sharedMaterials[i])){
                        return false;
                    }
                }
            }

            if (!m_sortingLayerID.Equals(other.m_sortingLayerID))
            {
                return false;
            }

            if (!string.Equals(m_sortingLayerName, other.m_sortingLayerName))
            {
                return false;
            }
            if (!m_sortingOrder.Equals(other.m_sortingOrder))
            {
                return false;
            }
            if (!Matrix4x4Kun.Equals(m_worldToLocalMatrix, other.m_worldToLocalMatrix))
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