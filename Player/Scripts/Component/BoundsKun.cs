using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class BoundsKun
    {
        [SerializeField] public Vector3Kun center;
        [SerializeField] public Vector3Kun size;

        public BoundsKun(Vector3Kun center,Vector3Kun size)
        {
            this.center = center;
            this.size = size;
        }

        public BoundsKun(Bounds bounds)
        {
            this.center = new Vector3Kun(bounds.center);
            this.size = new Vector3Kun(bounds.size);
        }

        public Bounds GetBounds()
        {
            return new Bounds(center.GetVector3(), size.GetVector3());
        }
        
    }

}