using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun {
    [System.Serializable]
    public class ResolutionKun : ISerializerKun{
        [SerializeField] public int width;
        [SerializeField] public int height;
        [SerializeField] public int refreshRate;


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolution"></param>
        public ResolutionKun(Resolution resolution) : this(resolution.width, resolution.height, resolution.refreshRate) { }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Resolution GetResolution()
        {
            var resolution = new Resolution();
            resolution.width = width;
            resolution.height = height;
            resolution.refreshRate = refreshRate;
            return resolution;                
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(width);
            binaryWriter.Write(height);
            binaryWriter.Write(refreshRate);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            width = binaryReader.ReadInt32();
            height = binaryReader.ReadInt32();
            refreshRate = binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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