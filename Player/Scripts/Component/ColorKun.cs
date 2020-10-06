using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public struct ColorKun
    {
        [SerializeField] public float a;
        [SerializeField] public float b;
        [SerializeField] public float g;
        [SerializeField] public float r;

        

        public ColorKun(Color color)
        {
            this.a = color.a;
            this.b = color.b;
            this.g = color.g;
            this.r = color.r;
        }

        public Color GetColor()
        {
            return new Color(this.r,this.g,this.b,this.a);
        }
    }
}
