using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class QuaternionKun : ISerializerKun
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        [SerializeField] public float z;
        [SerializeField] public float w;


        /// <summary>
        /// 
        /// </summary>
        public QuaternionKun() : this(0, 0, 0, 1) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public QuaternionKun(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="quaternion"></param>
        public QuaternionKun(Quaternion quaternion) : this(quaternion.x, quaternion.y, quaternion.z, quaternion.z) { }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Quaternion GetQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(x);
            binaryWriter.Write(y);
            binaryWriter.Write(z);
            binaryWriter.Write(w);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void Deserialize(BinaryReader binaryReader)
        {
            x = binaryReader.ReadSingle();
            y = binaryReader.ReadSingle();
            z = binaryReader.ReadSingle();
            w = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as QuaternionKun;
            if(other == null)
            {
                return false;
            }
            if (x.Equals(other.x) == false)
            {
                return false;
            }
            if (y.Equals(other.y) == false)
            {
                return false;
            }
            if (z.Equals(other.z) == false)
            {
                return false;
            }
            if (w.Equals(other.w) == false)
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
