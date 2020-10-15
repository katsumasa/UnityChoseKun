using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun {
    
    [System.Serializable]
    public class SkinnedMeshRendererKun : RendererKun
    {
        [SerializeField] TransformKun[] m_bones;
        public TransformKun[] bones {
            get{return m_bones;}
            set{m_bones = value;}
        }

        [SerializeField] bool m_forceMatrixRecalculationPerRender ;
        public bool forceMatrixRecalculationPerRender {
            get{return m_forceMatrixRecalculationPerRender;}
            set{m_forceMatrixRecalculationPerRender = value;}
        }

        [SerializeField] BoundsKun m_localBounds ;
        public Bounds localBounds {
            get{return m_localBounds.GetBounds();}
            set{m_localBounds = new BoundsKun(value);}
        }

        [SerializeField] SkinQuality m_quality;
        public SkinQuality quality {
            get{return m_quality;}
            set{m_quality = value;}
        }
    
        [SerializeField] string m_sharedMesh;
        public string sharedMesh {
            get{return m_sharedMesh;}
            set{m_sharedMesh = value;}
        }

        [SerializeField] bool m_skinnedMotionVectors ;
        public bool skinnedMotionVectors {
            get{return m_skinnedMotionVectors;}
            set{m_skinnedMotionVectors = value;}
        }

        [SerializeField] bool m_updateWhenOffscreen ;
        public bool updateWhenOffscreen {
            get{return m_updateWhenOffscreen;}
            set{m_updateWhenOffscreen = value;}
        }
        

        public SkinnedMeshRendererKun():this(null){}
        public SkinnedMeshRendererKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.SkinnedMeshMeshRenderer;
            var skinnedMeshRenderer = component as SkinnedMeshRenderer;
            if(skinnedMeshRenderer){
                if(skinnedMeshRenderer.bones!=null){
                    bones = new TransformKun[skinnedMeshRenderer.bones.Length];
                    for(var i = 0; i < bones.Length; i++){
                        bones[i] = new TransformKun(skinnedMeshRenderer.bones[i]);
                    }
                }
                forceMatrixRecalculationPerRender = skinnedMeshRenderer.forceMatrixRecalculationPerRender;
                localBounds = skinnedMeshRenderer.localBounds;                
                quality = skinnedMeshRenderer.quality;
                sharedMesh = skinnedMeshRenderer.sharedMesh.ToString();                
                skinnedMotionVectors = skinnedMeshRenderer.skinnedMotionVectors;
                updateWhenOffscreen = skinnedMeshRenderer.updateWhenOffscreen;
            }
        }

        public override bool WriteBack(Component component)
        {
            if(base.WriteBack(component)){
                var skinnedMeshRenderer = component as SkinnedMeshRenderer;
                if(skinnedMeshRenderer){
                    skinnedMeshRenderer.quality = quality;
                    skinnedMeshRenderer.skinnedMotionVectors = skinnedMotionVectors;
                    skinnedMeshRenderer.forceMatrixRecalculationPerRender = forceMatrixRecalculationPerRender;
                    skinnedMeshRenderer.localBounds = localBounds;
                    skinnedMeshRenderer.updateWhenOffscreen = updateWhenOffscreen;
                }
                return true;
            }
            return false;
        }
    }
}