using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class BoundsKun
    {
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