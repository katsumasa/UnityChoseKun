using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Utj.UnityChoseKun {
    /// <summary>
    /// SkinnedMeshRendererをSerialize/Deserializeする為のClass
    /// </summary>
    [System.Serializable]
    public class SkinnedMeshRendererKun : RendererKun
    {
        [SerializeField] string m_sharedMesh;
        [SerializeField] SkinQuality m_quality;        
        [SerializeField] bool m_forceMatrixRecalculationPerRender;
        [SerializeField] bool m_skinnedMotionVectors;
        [SerializeField] bool m_updateWhenOffscreen;
        [SerializeField] BoundsKun m_localBounds;
        [SerializeField] TransformKun[] m_bones;


        public TransformKun[] bones {
            get{return m_bones;}
            set{m_bones = value;}
        }

        
        public bool forceMatrixRecalculationPerRender {
            get{return m_forceMatrixRecalculationPerRender;}
            set{m_forceMatrixRecalculationPerRender = value;}
        }


        public Bounds localBounds {
            get{                
                return m_localBounds.GetBounds();
            }
            set{m_localBounds = new BoundsKun(value);}
        }


        public SkinQuality quality {
            get{return m_quality;}
            set{m_quality = value;}
        }
    

        public string sharedMesh {
            get{return m_sharedMesh;}
            set{m_sharedMesh = value;}
        }

        
        public bool skinnedMotionVectors {
            get{return m_skinnedMotionVectors;}
            set{m_skinnedMotionVectors = value;}
        }

        
        public bool updateWhenOffscreen {
            get{return m_updateWhenOffscreen;}
            set{m_updateWhenOffscreen = value;}
        }
        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SkinnedMeshRendererKun():this(null){}


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="component"></param>
        public SkinnedMeshRendererKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.SkinnedMeshMeshRenderer;
            m_sharedMesh = "";
            
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


        /// <summary>
        /// Objectに書き戻す
        /// </summary>
        /// <param name="component">SkinMeshRendererオブジェクト</param>
        /// <returns></returns>
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


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(sharedMesh);
            binaryWriter.Write((int)m_quality);
            binaryWriter.Write(m_forceMatrixRecalculationPerRender);
            binaryWriter.Write(m_skinnedMotionVectors);
            binaryWriter.Write(m_updateWhenOffscreen);
            SerializerKun.Serialize<BoundsKun>(binaryWriter, m_localBounds);
            SerializerKun.Serialize<TransformKun>(binaryWriter, m_bones);            
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            m_sharedMesh = binaryReader.ReadString();
            m_quality = (SkinQuality)binaryReader.ReadInt32();
            m_forceMatrixRecalculationPerRender = binaryReader.ReadBoolean();
            m_skinnedMotionVectors = binaryReader.ReadBoolean();
            m_updateWhenOffscreen = binaryReader.ReadBoolean();
            m_localBounds = SerializerKun.DesirializeObject<BoundsKun>(binaryReader);
            m_bones = SerializerKun.DesirializeObjects<TransformKun>(binaryReader);            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as SkinnedMeshRendererKun;
            if(other == null)
            {
                return false;
            }
            if(m_bones == null && other.m_bones != null)
            {
                return false;
            }
            if(m_bones != null && other.m_bones == null)
            {
                return false;
            }
            if(m_bones != null)
            {
                if(m_bones.Length.Equals(other.m_bones.Length)== false)
                {
                    return false;
                }
                for(var i = 0; i < m_bones.Length; i++)
                {
                    if (m_bones[i].Equals(other.m_bones[i]) == false)
                    {
                        return false;
                    }
                }
            }

            if (forceMatrixRecalculationPerRender.Equals(other.forceMatrixRecalculationPerRender) == false)
            {
                return false;
            }
            if(m_localBounds == null && other.m_localBounds != null)
            {
                return false;
            }
            if (m_localBounds != null && other.m_localBounds == null)
            {
                return false;
            }
            if (m_localBounds != null)
            {
                if (m_localBounds.Equals(other.m_localBounds) == false)
                {
                    return false;
                }
            }
            if (quality.Equals(other.quality) == false)
            {
                return false;
            }
            if (sharedMesh.Equals(other.sharedMesh) == false)
            {
                return false;
            }
            if (skinnedMotionVectors.Equals(other.skinnedMotionVectors)==false) {
                return false;
            }
            if (updateWhenOffscreen.Equals(other.updateWhenOffscreen) == false)
            {
                return false;
            }
            return base.Equals(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}