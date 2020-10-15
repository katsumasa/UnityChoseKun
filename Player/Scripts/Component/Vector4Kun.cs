using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class Vector4Kun
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        [SerializeField] public float z;
        [SerializeField] public float w;

        public Vector4Kun(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4Kun(Vector4 v4) : this(v4.x, v4.y, v4.z, v4.w) { }

        
        public Vector4 GetVector4()
        {
            return new Vector4(x, y, x, w);
        }
    }

}