using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class RectKun
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        [SerializeField] public float width;
        [SerializeField] public float height;

        public RectKun(float x,float y,float w,float h)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
        }

        public RectKun(Rect rect)
        {
            x = rect.x;
            y = rect.y;
            width = rect.width;
            height = rect.height;
        }

        public RectKun()
        {
            x = 0;
            y = 0;
            width = 1;
            height = 1;
        }

        public Rect GetRect()
        {
            return new Rect(x, y, width, height);
        }
    }
}
