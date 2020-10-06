using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public struct Vector3Kun
    {
        [SerializeField] public float x;        
        [SerializeField] public float y;        
        [SerializeField] public float z;
        

        public Vector3Kun(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3Kun(Vector3 v3) : this(v3.x, v3.y, v3.x) { }                

        public Vector3 GetVector3()
        {
            return new Vector3(x, y, z);
        }

    }
}