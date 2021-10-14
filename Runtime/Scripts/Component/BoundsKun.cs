using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// Boundsをシリアライズ・デシリアライズする為のClass
    /// </summary>
    [System.Serializable]
    public class BoundsKun : ISerializerKun
    {
        [SerializeField] Vector3Kun m_center;        
        [SerializeField] Vector3Kun m_size;


        // 受け渡しはVector3で行うが内部的にはVector3Kun
        public Vector3 center
        {
            get { return m_center.GetVector3();}
            set { m_center = new Vector3Kun(value); }
        }

        public Vector3 size
        {
            get { return m_size.GetVector3();}
            set { m_size = new Vector3Kun(value); }
        }


        /// <summary>
        /// 
        /// </summary>
        public BoundsKun():this(new Vector3Kun(0,0,0),new Vector3Kun(1,1,1))
        {
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="center">中心座標</param>
        /// <param name="size">サイズ</param>
        public BoundsKun(Vector3Kun center,Vector3Kun size)
        {
            this.m_center = center;
            this.m_size = size;
        }


        public BoundsKun(Bounds bounds)
        {
            this.m_center = new Vector3Kun(bounds.center);
            this.m_size = new Vector3Kun(bounds.size);
        }


        public Bounds GetBounds()
        {
            return new Bounds(m_center.GetVector3(), m_size.GetVector3());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_center);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_size);            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_center = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
            m_size = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as BoundsKun;
            if(other == null)
            {
                return false;
            }

            if (!Vector3Kun.Equals(m_center, other.m_center))
            {
                return false;
            }
            if (!Vector3Kun.Equals(m_size, other.m_size))
            {
                return false;
            }
            return true;
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