using System.Collections;
using System.Collections.Generic;
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
        public bool forceRenderingOff {
            get{return m_forceRenderingOff;}
            set{m_forceRenderingOff = value;}
        }
        
        [SerializeField] UnityEngine.Experimental.Rendering.RayTracingMode m_rayTracingMode ;
        public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode {
            get{return m_rayTracingMode;}
            set{m_rayTracingMode = value;}
        }
        #endif


        [SerializeField]bool m_allowOcclusionWhenDynamic ;
        public bool allowOcclusionWhenDynamic{
            get{return m_allowOcclusionWhenDynamic;}
            set{m_allowOcclusionWhenDynamic = value;}
        }

        [SerializeField] Bounds m_bounds ;
        public Bounds bounds {
            get{return m_bounds;}
            private set{m_bounds = value;}
        }

        [SerializeField]bool m_enabled;
        public bool enabled{
            get{return m_enabled;}
            set{m_enabled = value;}
        }
        

        [SerializeField] bool m_isPartOfStaticBatch ;
        public bool isPartOfStaticBatch {
            get{return m_isPartOfStaticBatch;}
            private set{m_isPartOfStaticBatch = value;}
        }

        [SerializeField] bool m_isVisible ;
        public bool isVisible {
            get {return m_isVisible;}
            private set {m_isVisible = value;}
        }

        [SerializeField] int m_lightmapIndex ;
        public int lightmapIndex {
            get{return m_lightmapIndex;}
            set{m_lightmapIndex = value;}
        }

        [SerializeField] Vector4 m_lightmapScaleOffset ;
        public Vector4 lightmapScaleOffset {
            get{return m_lightmapScaleOffset;}
            set{m_lightmapScaleOffset = value;}
        }
        
        [SerializeField] string m_lightProbeProxyVolumeOverride ;
        public string lightProbeProxyVolumeOverride {
            get{return m_lightProbeProxyVolumeOverride;}
            private set{m_lightProbeProxyVolumeOverride = value;}
        }
        
        [SerializeField] UnityEngine.Rendering.LightProbeUsage m_lightProbeUsage ;
        public UnityEngine.Rendering.LightProbeUsage lightProbeUsage {
            get{return m_lightProbeUsage;}
            set{m_lightProbeUsage = value;}
        }

        [SerializeField] Matrix4x4 m_localToWorldMatrix ;    
        public Matrix4x4 localToWorldMatrix {
            get{return m_localToWorldMatrix;}
            private set{m_localToWorldMatrix = value;}
        }
        
        [SerializeField]  MaterialKun m_material ;
        public MaterialKun material {
            get{return m_material;}
            private set{m_material = value;}
        }

        [SerializeField] MaterialKun[] m_materials;
        public MaterialKun[] materials {
            get{return m_materials;}
            private set{m_materials = value;}
        }

        [SerializeField] MotionVectorGenerationMode m_motionVectorGenerationMode ;
        public MotionVectorGenerationMode motionVectorGenerationMode {
            get{return m_motionVectorGenerationMode;}
            set{m_motionVectorGenerationMode = value;}
        }

        [SerializeField] TransformKun m_probeAnchor ;
        public TransformKun probeAnchor {
            get{return m_probeAnchor;}
            private set{m_probeAnchor = value;}
        }



        [SerializeField] int m_realtimeLightmapIndex ;
        public int realtimeLightmapIndex {
            get{return realtimeLightmapIndex;}
            set{realtimeLightmapIndex = value;}
        }

        [SerializeField] Vector4 m_realtimeLightmapScaleOffset ;
        public Vector4 realtimeLightmapScaleOffset {
            get{return m_realtimeLightmapScaleOffset;}
            set{m_realtimeLightmapScaleOffset = value;}
        }

        [SerializeField]  bool m_receiveShadows ;
        public bool receiveShadows {
            get{return m_receiveShadows;}
            set{m_receiveShadows = value;}
        }

        [SerializeField] UnityEngine.Rendering.ReflectionProbeUsage m_reflectionProbeUsage ;
        public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage {
            get{return m_reflectionProbeUsage;}
            set{m_reflectionProbeUsage = value;}
        }

        [SerializeField] public int m_rendererPriority ;
        public int rendererPriority {
            get{return m_rendererPriority;}
            set{m_rendererPriority = value;}
        }

        [SerializeField] uint m_renderingLayerMask ;
        public uint renderingLayerMask {
            get{return m_renderingLayerMask;}
            set{m_renderingLayerMask = value;}
        }

        [SerializeField] UnityEngine.Rendering.ShadowCastingMode m_shadowCastingMode ;
        public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode {
            get{return m_shadowCastingMode;}
            set{m_shadowCastingMode = value;}
        }

        [SerializeField] MaterialKun m_sharedMaterial ;
        public MaterialKun sharedMaterial {
            get{return m_sharedMaterial;}
            private set {m_sharedMaterial = value;}
        }

        [SerializeField] MaterialKun[] m_sharedMaterials ;
        public MaterialKun[] sharedMaterials {
            get{return m_sharedMaterials;}
            private set{m_sharedMaterials = value;}
        }

        [SerializeField] int m_sortingLayerID ;
        public int sortingLayerID {
            get{return m_sortingLayerID;}
            set{m_sortingLayerID = value;}
        }

        [SerializeField] string m_sortingLayerName;
        public string sortingLayerName {
            get {return m_sortingLayerName;}
            set {m_sortingLayerName = value;}
        }

        [SerializeField] int m_sortingOrder ;
        public int sortingOrder {
            get {return m_sortingOrder;}
            set {m_sortingOrder = value;}
        }

        [SerializeField] Matrix4x4 m_worldToLocalMatrix ;
        public Matrix4x4 worldToLocalMatrix {
            get {return m_worldToLocalMatrix;}
            private set {m_worldToLocalMatrix = value;}
        }

        //
        // メンバー関数の定義
        //

        public RendererKun():this(null){}

        public RendererKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Renderer;
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
                material = new MaterialKun(renderer.material);
                if(renderer.materials != null){
                    materials = new MaterialKun[renderer.materials.Length];
                    for(var i = 0; i < renderer.materials.Length; i++){
                        materials[i] = new MaterialKun(renderer.materials[i]);
                    }
                }
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

        public override bool WriteBack(Component component)
        {   
            Debug.Log("Renderer.WriteBack("+dirty+")");
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
                for(var i = 0; i < materials.Length; i++){
                    var materialKun = materials[i];
                    materialKun.WriteBack(renderer.materials[i]);
                }
            }
            
            
            return result;
        }
    }
}