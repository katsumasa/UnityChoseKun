using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class QuaternionKun
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        [SerializeField] public float z;
        [SerializeField] public float w;

        public QuaternionKun(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public QuaternionKun(Quaternion quaternion) : this(quaternion.x, quaternion.y, quaternion.z, quaternion.z) { }


        public Quaternion GetQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
    }
}
