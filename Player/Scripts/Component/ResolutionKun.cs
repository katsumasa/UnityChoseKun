using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun {
    [System.Serializable]
    public class ResolutionKun {
        [SerializeField] public int width;
        [SerializeField] public int height;
        [SerializeField] public int refreshRate;

        public ResolutionKun(int width, int height, int refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }

        public ResolutionKun(Resolution resolution) : this(resolution.width, resolution.height, resolution.refreshRate) { }
        
        public Resolution GetResolution()
        {
            var resolution = new Resolution();
            resolution.width = width;
            resolution.height = height;
            resolution.refreshRate = refreshRate;
            return resolution;                
        }
    }
}