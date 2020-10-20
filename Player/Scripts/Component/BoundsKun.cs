using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// Boundsをシリアライズ・デシリアライズする為のClass
    /// </summary>
    [System.Serializable]
    public class BoundsKun
    {
        // 受け渡しはVector3で行うが内部的にはVector3Kun
        [SerializeField] Vector3Kun m_center;
        [SerializeField] Vector3Kun m_size;

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
        
    }

}