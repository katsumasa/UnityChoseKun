using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    // <summary>
    // SerializeObject for Vector2
    // </summary>
    [System.Serializable]
    public class Vector2Kun
    {
        [SerializeField]public float x;
        [SerializeField]public float y;
        

        public Vector2Kun(float x,float y)
        {
            this.x = x;
            this.y = y;
        }


        public Vector2Kun(Vector2 v2) : this(v2.x, v2.y) { }
        

        public Vector2 GetVector2()
        {
            return new Vector2(x,y);
        }
    }
}
