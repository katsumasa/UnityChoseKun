using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun {
    [System.Serializable]
    public class ResolutionKun : ISerializerKun{
        [SerializeField] public int width;
        [SerializeField] public int height;
#if UNITY_2022_1_OR_NEWER
        [SerializeField] public RefreshRate refreshRateRatio;
#else
        [SerializeField] public int refreshRate;
#endif


        /// <summary>
        /// 
        /// </summary>
#if UNITY_2022_1_OR_NEWER
        public ResolutionKun()
        {
            this.width = 256;
            this.height = 256;
            this.refreshRateRatio = new RefreshRate();
            this.refreshRateRatio.numerator = 60;
            this.refreshRateRatio.denominator = 0;
        }

        public ResolutionKun(int width, int height, RefreshRate refreshRateRatio)
        {
            this.width = width;
            this.height = height;
            this.refreshRateRatio = refreshRateRatio;            
        }

        public ResolutionKun(Resolution resolution) : this(resolution.width, resolution.height, resolution.refreshRateRatio) { }

        public Resolution GetResolution()
        {
            var resolution = new Resolution();
            resolution.width = width;
            resolution.height = height;
            resolution.refreshRateRatio = refreshRateRatio;
            return resolution;
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(width);
            binaryWriter.Write(height);
            binaryWriter.Write(refreshRateRatio.numerator);
            binaryWriter.Write(refreshRateRatio.denominator);
        }


        public virtual void Deserialize(BinaryReader binaryReader)
        {
            width = binaryReader.ReadInt32();
            height = binaryReader.ReadInt32();
            refreshRateRatio = new RefreshRate();
            refreshRateRatio.numerator = binaryReader.ReadUInt32();
            refreshRateRatio.denominator = binaryReader.ReadUInt32();            
        }

        public override bool Equals(object obj)
        {
            var other = obj as ResolutionKun;
            if (other == null)
            {
                return false;
            }
            if (!int.Equals(width, other.width))
            {
                return false;
            }
            if (!int.Equals(height, other.height))
            {
                return false;
            }
            if (!int.Equals(refreshRateRatio, other.refreshRateRatio))
            {
                return false;
            }
            return true;
        }
#else
        public ResolutionKun() : this(256, 256, 60) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="refreshRate"></param>
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


        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(width);
            binaryWriter.Write(height);
            binaryWriter.Write(refreshRate);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            width = binaryReader.ReadInt32();
            height = binaryReader.ReadInt32();
            refreshRate = binaryReader.ReadInt32();
        }


        public override bool Equals(object obj)
        {
            var other = obj as ResolutionKun;
            if(other == null)
            {
                return false;
            }
            if (!int.Equals(width, other.width))
            {
                return false;
            }
            if (!int.Equals(height, other.height))
            {
                return false;
            }
            if (!int.Equals(refreshRate, other.refreshRate))
            {
                return false;
            }
            return true;
        }
#endif       

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}